using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

[QueryProperty("Combat", "Combat")]
public partial class InitiativeViewModel : BaseViewModel
{
    public CreatureService<Creature> CreatureService;
    public CreatureService<InitiativeCreatureData> InitiativeService;
    public CreatureService<Combat> CombatService;
    public CreatureService<Roll> RollService;
    [ObservableProperty] public ObservableCollection<InitiativeCreature> initiative;
    [ObservableProperty] public Combat? combat;
    [ObservableProperty] public ObservableCollection<Roll> rolls = new();
    [ObservableProperty] public InitiativeCreature? currentCreature;
    [ObservableProperty] private static ObservableCollection<string> conditionNames = ["Blinded", "Charmed", "Deafened", "Frightened", "Grappled", "Incapacitated", "Invisible", "Paralyzed", "Petrified", "Poisoned", "Prone", "Restrained", "Stunned", "Unconscious", "Positive A", "Positive B", "Negative A", "Negative B"];
    [ObservableProperty] public String pickedCondition = "";

    public InitiativeViewModel(CreatureService<Creature> creatureService, CreatureService<InitiativeCreatureData> initiativeService, CreatureService<Combat> combatService, CreatureService<Roll> rollService)
    {
        this.CreatureService = creatureService;
        this.InitiativeService = initiativeService;
        this.CombatService = combatService;
        this.RollService = rollService;
        Initiative = new ObservableCollection<InitiativeCreature>();
    }

    [RelayCommand]
    public async Task RefreshInitiativeAsync()
    {
        if (Combat is null)
            return;

        IsBusy = true;
        bool wasStarted = Combat.IsStarted;
        Combat.IsStarted = false;
        await Task.Delay(10);

        List<InitiativeCreatureData> initiativeCreatureDataList = await InitiativeService.GetAllByCombatAsync(Combat.Id);

        Initiative.Clear();

        foreach (InitiativeCreatureData initiativeCreatureData in initiativeCreatureDataList)
        {
            InitiativeCreature initiativeCreature = new(await CreatureService.GetByIdAsync(initiativeCreatureData.CreatureID), initiativeCreatureData);
            Initiative.Add(initiativeCreature);
        }

        Rolls = new ObservableCollection<Roll>(await RollService.GetAllByCombatAsync(Combat.Id));

        await SortInitiativeAsync();
        await SortRollsAsync();

        CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);

        IsBusy = false;
        Combat.IsStarted = wasStarted;
    }

    [RelayCommand]
    public async Task GoToCreatureListAsync()
    {
        if (Combat is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CreatureListPage)}", true,
            new Dictionary<string, object>
            {
                {"Combat", Combat}
            });
    }

    [RelayCommand]
    public async Task GoToMainMenuAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(MainMenuPage)}", true);
    }

    [RelayCommand]
    public async Task RollInitiativeAsync()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        IsBusy = true;
        Combat.IsStarted = false;
        await Task.Delay(10);

        await Task.Run(() =>
        {
            Random rng = new();

            foreach (InitiativeCreature initiativeCreature in Initiative)
            {
                if (!initiativeCreature.Creature.IsPlayer && !initiativeCreature.InitiativeCreatureData.Initiative.HasValue)
                {
                    int initiative = rng.Next(1, 21) + initiativeCreature.Creature.InitiativeBonus;
                    initiativeCreature.InitiativeCreatureData.Initiative = initiative;
                }
            }
        });

        await SortInitiativeAsync();

        await Task.Run(() =>
        {
            CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
        });

        if (CurrentCreature is null)
        {
            Initiative[0].InitiativeCreatureData.IsTurn = true;
            CurrentCreature = Initiative[0];
        }

        Combat.IsStarted = true;
        await CombatService.SaveAsync(Combat);

        IsBusy = false;
    }

    [RelayCommand]
    public async Task SortInitiativeAsync()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        List<InitiativeCreature> sortedCreatures = [];
        await Task.Run(() =>
        {
            sortedCreatures = Initiative.OrderByDescending(x => x.InitiativeCreatureData.Initiative).ThenByDescending(x => x.Creature.InitiativeBonus).ToList();
        });
        Initiative.Clear();
        foreach (InitiativeCreature creature in sortedCreatures)
            Initiative.Add(creature);
    }

    [RelayCommand]
    public async Task SortRollsAsync()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        List<Roll> sortedRolls = [];
        await Task.Run(() =>
        {
            sortedRolls = Rolls.OrderByDescending(x => x.Round).ThenByDescending(x => x.Id).ToList();
        });
        Rolls.Clear();
        foreach (Roll roll in sortedRolls)
            Rolls.Add(roll);
    }

    [RelayCommand]
    public async Task SaveCombatAsync()
    {
        if (Combat is null)
            return;

        await CombatService.SaveAsync(Combat);

        if (Initiative is null || Initiative.Count == 0)
            return;

        foreach (InitiativeCreature creature in Initiative.ToList())
        {
            await InitiativeService.SaveAsync(creature.InitiativeCreatureData);
        }
    }

    [RelayCommand]
    public async Task NextCreature()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
        InitiativeCreature? nextCreature = null;

        if (CurrentCreature is not null)
        {
            if (Initiative.IndexOf(CurrentCreature) + 1 < Initiative.Count)
                nextCreature = Initiative[Initiative.IndexOf(CurrentCreature) + 1];
            else
            {
                nextCreature = Initiative[0];
                Combat.RoundCount++;
                await CombatService.SaveAsync(Combat);
            }

            CurrentCreature.InitiativeCreatureData.IsTurn = false;
            await Task.Run(() => InitiativeService.SaveAsync(CurrentCreature.InitiativeCreatureData));
        }
        else
            nextCreature = Initiative[0];

        if (nextCreature is not null)
        {
            nextCreature.InitiativeCreatureData.IsTurn = true;
            await Task.Run(() => InitiativeService.SaveAsync(nextCreature.InitiativeCreatureData));
        }

        CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);

    }

    [RelayCommand]
    public async Task PreviousCreature()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
        InitiativeCreature? previousCreature = null;

        if (CurrentCreature is not null)
        {
            if (Initiative.IndexOf(CurrentCreature) - 1 >= 0)
                previousCreature = Initiative[Initiative.IndexOf(CurrentCreature) - 1];
            else
            {
                previousCreature = Initiative[^1];
                if (Combat.RoundCount > 0)
                    Combat.RoundCount--;
                await CombatService.SaveAsync(Combat);
            }

            CurrentCreature.InitiativeCreatureData.IsTurn = false;
            await Task.Run(() => InitiativeService.SaveAsync(CurrentCreature.InitiativeCreatureData));
        }
        else
            previousCreature = Initiative[0];

        if (previousCreature is not null)
        {
            previousCreature.InitiativeCreatureData.IsTurn = true;
            await Task.Run(() => InitiativeService.SaveAsync(previousCreature.InitiativeCreatureData));
        }

        CurrentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(InitiativeCreature initiativeCreature)
    {
        if (initiativeCreature is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CreatureDetailsPage)}", true,
            new Dictionary<string, object>
            {
                {"Creature", initiativeCreature.Creature}
            });
    }

    [RelayCommand]
    public async Task PauseInitiativeAsync()
    {
        if (Combat is null)
            return;

        Combat.IsStarted = false;
        await CombatService.SaveAsync(Combat);
    }
    
    [RelayCommand]
    public async Task CurrentHitPointsPlusTenAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints += 10;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task CurrentHitPointsMinusTenAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null || initiativeCreature.InitiativeCreatureData.CurrentHitPoints <= 0)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints -= 10;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task CurrentHitPointsPlusOneAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints++;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task CurrentHitPointsMinusOneAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null || initiativeCreature.InitiativeCreatureData.CurrentHitPoints <= 0)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints--;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task SetActiveTabFirst(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.ActiveTab = 0;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task SetActiveTabSecond(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.ActiveTab = 1;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task SetActiveTabThird(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.ActiveTab = 2;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task SetActiveTabFourth(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.ActiveTab = 3;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task SetCondition(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null || String.IsNullOrEmpty(PickedCondition) || CurrentCreature is null)
            return;

        String timeString = "Round " + Combat.RoundCount.ToString() + "\nInit. " + CurrentCreature.InitiativeCreatureData.Initiative.ToString();

        if (String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString1)) { initiativeCreature.InitiativeCreatureData.ConditionString1 = PickedCondition; initiativeCreature.InitiativeCreatureData.ConditionTimeString1 = timeString; }
        else if (String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString2)) { initiativeCreature.InitiativeCreatureData.ConditionString2 = PickedCondition; initiativeCreature.InitiativeCreatureData.ConditionTimeString2 = timeString; }
        else if (String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString3)) { initiativeCreature.InitiativeCreatureData.ConditionString3 = PickedCondition; initiativeCreature.InitiativeCreatureData.ConditionTimeString3 = timeString; }
        else if (String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString4)) { initiativeCreature.InitiativeCreatureData.ConditionString4 = PickedCondition; initiativeCreature.InitiativeCreatureData.ConditionTimeString4 = timeString; }
        else if (String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString5)) { initiativeCreature.InitiativeCreatureData.ConditionString5 = PickedCondition; initiativeCreature.InitiativeCreatureData.ConditionTimeString5 = timeString; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RemoveCondition1(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        if (!String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString1)) { initiativeCreature.InitiativeCreatureData.ConditionString1 = ""; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RemoveCondition2(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        if (!String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString2)) { initiativeCreature.InitiativeCreatureData.ConditionString2 = ""; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RemoveCondition3(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        if (!String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString3)) { initiativeCreature.InitiativeCreatureData.ConditionString3 = ""; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RemoveCondition4(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        if (!String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString4)) { initiativeCreature.InitiativeCreatureData.ConditionString4 = ""; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RemoveCondition5(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        if (!String.IsNullOrEmpty(initiativeCreature.InitiativeCreatureData.ConditionString5)) { initiativeCreature.InitiativeCreatureData.ConditionString5 = ""; }

        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]

    public async Task ShowConditionInfo(String ConditionName)
    {
        String displayString = "";

        switch(ConditionName)
        {
            case "Blinded":
                displayString = "While you have the Blinded condition, you experience the following effects.\r\n\r\nCan’t See. You can’t see and automatically fail any ability check that requires sight.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage, and your attack rolls have Disadvantage.";
                break;
            case "Charmed":
                displayString = "While you have the Charmed condition, you experience the following effects.\r\n\r\nCan’t Harm the Charmer. You can’t attack the charmer or target the charmer with damaging abilities or magical effects.\r\n\r\nSocial Advantage. The charmer has Advantage on any ability check to interact with you socially.";
                break;
            case "Deafened":
                displayString = "While you have the Deafened condition, you experience the following effect.\r\n\r\nCan’t Hear. You can’t hear and automatically fail any ability check that requires hearing.";
                break;
            case "Frightened":
                displayString = "While you have the Frightened condition, you experience the following effects.\r\n\r\nAbility Checks and Attacks Affected. You have Disadvantage on ability checks and attack rolls while the source of fear is within line of sight.\r\n\r\nCan’t Approach. You can’t willingly move closer to the source of fear.";
                break;
            case "Grappled":
                displayString = "While you have the Grappled condition, you experience the following effects.\r\n\r\nSpeed 0. Your Speed is 0 and can’t increase.\r\n\r\nAttacks Affected. You have Disadvantage on attack rolls against any target other than the grappler.\r\n\r\nMovable. The grappler can drag or carry you when it moves, but every foot of movement costs it 1 extra foot unless you are Tiny or two or more sizes smaller than it.";
                break;
            case "Incapacitated":
                displayString = "While you have the Incapacitated condition, you experience the following effects.\r\n\r\nInactive. You can’t take any action, Bonus Action, or Reaction.\r\n\r\nNo Concentration. Your Concentration is broken.\r\n\r\nSpeechless. You can’t speak.\r\n\r\nSurprised. If you’re Incapacitated when you roll Initiative, you have Disadvantage on the roll.";
                break;
            case "Invisible":
                displayString = "While you have the Invisible condition, you experience the following effects.\r\n\r\nSurprise. If you’re Invisible when you roll Initiative, you have Advantage on the roll.\r\n\r\nConcealed. You aren’t affected by any effect that requires its target to be seen unless the effect’s creator can somehow see you. Any equipment you are wearing or carrying is also concealed.\r\n\r\nAttacks Affected. Attack rolls against you have Disadvantage, and your attack rolls have Advantage. If a creature can somehow see you, you don’t gain this benefit against that creature.";
                break;
            case "Paralyzed":
                displayString = "While you have the Paralyzed condition, you experience the following effects.\r\n\r\nIncapacitated. You have the Incapacitated condition.\r\n\r\nSpeed 0. Your Speed is 0 and can’t increase.\r\n\r\nSaving Throws Affected. You automatically fail Strength and Dexterity saving throws.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage.\r\n\r\nAutomatic Critical Hits. Any attack roll that hits you is a Critical Hit if the attacker is within 5 feet of you.";
                break;
            case "Petrified":
                displayString = "While you have the Petrified condition, you experience the following effects.\r\n\r\nTurned to Inanimate Substance. You are transformed, along with any nonmagical objects you are wearing and carrying, into a solid inanimate substance (usually stone). Your weight increases by a factor of ten, and you cease aging.\r\n\r\nIncapacitated. You have the Incapacitated condition.\r\n\r\nSpeed 0. Your Speed is 0 and can’t increase.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage.\r\n\r\nSaving Throws Affected. You automatically fail Strength and Dexterity saving throws.\r\n\r\nResist Damage. You have Resistance to all damage.\r\n\r\nPoison Immunity. You have Immunity to the Poisoned condition.";
                break;
            case "Poisoned":
                displayString = "While you have the Poisoned condition, you experience the following effect.\r\n\r\nAbility Checks and Attacks Affected. You have Disadvantage on attack rolls and ability checks.";
                break;
            case "Prone":
                displayString = "While you have the Prone condition, you experience the following effects.\r\n\r\nRestricted Movement. Your only movement options are to crawl or to spend an amount of movement equal to half your Speed (round down) to right yourself and thereby end the condition. If your Speed is 0, you can’t right yourself.\r\n\r\nAttacks Affected. You have Disadvantage on attack rolls. An attack roll against you has Advantage if the attacker is within 5 feet of you. Otherwise, that attack roll has Disadvantage.";
                break;
            case "Restrained":
                displayString = "While you have the Restrained condition, you experience the following effects.\r\n\r\nSpeed 0. Your Speed is 0 and can’t increase.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage, and your attack rolls have Disadvantage.\r\n\r\nSaving Throws Affected. You have Disadvantage on Dexterity saving throws.";
                break;
            case "Stunned":
                displayString = "While you have the Stunned condition, you experience the following effects.\r\n\r\nIncapacitated. You have the Incapacitated condition.\r\n\r\nSaving Throws Affected. You automatically fail Strength and Dexterity saving throws.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage.";
                break;
            case "Unconscious":
                displayString = "While you have the Unconscious condition, you experience the following effects.\r\n\r\nInert. You have the Incapacitated and Prone conditions, and you drop whatever you’re holding. When this condition ends, you remain Prone.\r\n\r\nSpeed 0. Your Speed is 0 and can’t increase.\r\n\r\nAttacks Affected. Attack rolls against you have Advantage.\r\n\r\nSaving Throws Affected. You automatically fail Strength and Dexterity saving throws.\r\n\r\nAutomatic Critical Hits. Any attack roll that hits you is a Critical Hit if the attacker is within 5 feet of you.\r\n\r\nUnaware. You’re unaware of your surroundings.";
                break;
            case "Positive A":
                displayString = "This is an undefined positive condition.";
                break;
            case "Positive B":
                displayString = "This is an undefined positive condition.";
                break;
            case "Negative A":
                displayString = "This is an undefined negative condition.";
                break;
            case "Negative B":
                displayString = "This is an undefined negative condition.";
                break;
        }


        await Shell.Current.CurrentPage.DisplayAlert(ConditionName, displayString, "OK"
            );
    }

    [RelayCommand]
    public async Task RollSaveAsync(Tuple<InitiativeCreature?, int> parameters)
    {
        if (Combat is null || parameters.Item1 is null || parameters.Item2 < 0 || parameters.Item2 > 7)
            return;

        InitiativeCreature initiativeCreature = parameters.Item1;
        int roll1 = 0;
        int roll2 = 0;
        string rollName = "";
        int modifier = 0;
        int? damage1 = null;
        string? damageType1 = null;
        int? damage2 = null;
        string? damageType2 = null;

        switch (parameters.Item2)
        {
            case 0:
                rollName = "Str Save";
                modifier = initiativeCreature.Creature.StrSaveBonus;
                break;
            case 1:
                rollName = "Dex Save";
                modifier = initiativeCreature.Creature.DexSaveBonus;
                break;
            case 2:
                rollName = "Con Save";
                modifier = initiativeCreature.Creature.ConSaveBonus;
                break;
            case 3:
                rollName = "Int Save";
                modifier = initiativeCreature.Creature.IntSaveBonus;
                break;
            case 4:
                rollName = "Wis Save";
                modifier = initiativeCreature.Creature.WisSaveBonus;
                break;
            case 5:
                rollName = "Cha Save";
                modifier = initiativeCreature.Creature.ChaSaveBonus;
                break;
            case 6:
                rollName = initiativeCreature.Creature.HotKey1Name;
                modifier = initiativeCreature.Creature.HotKey1Bonus;
                break;
            case 7:
                rollName = initiativeCreature.Creature.HotKey2Name;
                modifier = initiativeCreature.Creature.HotKey2Bonus;
                break;
        }

        await Task.Run(() =>
        {
            Random rng = new();
            roll1 = rng.Next(1, 21);
            roll2 = rng.Next(1, 21);

            if (parameters.Item2 == 6 || parameters.Item2 == 7)
            {
                int diceCount1 = 0;
                int diceSize1 = 0;
                int damageBonus1 = 0;
                int diceCount2 = 0;
                int diceSize2 = 0;
                int damageBonus2 = 0;

                if (parameters.Item2 == 6)
                {
                    diceCount1 = initiativeCreature.Creature.HotKey1DamageDiceNumber1;
                    diceSize1 = initiativeCreature.Creature.HotKey1DamageDiceSize1;
                    damageBonus1 = initiativeCreature.Creature.HotKey1DamageBonus1;
                    damageType1 = initiativeCreature.Creature.HotKey1DamageType1;
                    diceCount2 = initiativeCreature.Creature.HotKey1DamageDiceNumber2;
                    diceSize2 = initiativeCreature.Creature.HotKey1DamageDiceSize2;
                    damageBonus2 = initiativeCreature.Creature.HotKey1DamageBonus2;
                    damageType2 = initiativeCreature.Creature.HotKey1DamageType2;
                }
                else if (parameters.Item2 == 7)
                {
                    diceCount1 = initiativeCreature.Creature.HotKey2DamageDiceNumber1;
                    diceSize1 = initiativeCreature.Creature.HotKey2DamageDiceSize1;
                    damageBonus1 = initiativeCreature.Creature.HotKey2DamageBonus1;
                    damageType1 = initiativeCreature.Creature.HotKey2DamageType1;
                    diceCount2 = initiativeCreature.Creature.HotKey2DamageDiceNumber2;
                    diceSize2 = initiativeCreature.Creature.HotKey2DamageDiceSize2;
                    damageBonus2 = initiativeCreature.Creature.HotKey2DamageBonus2;
                    damageType2 = initiativeCreature.Creature.HotKey2DamageType2;
                }

                damage1 = 0;
                for (int i = 0; i < diceCount1; i++)
                {
                    damage1 += rng.Next(1, diceSize1);
                }
                damage1 += damageBonus1;

                damage2 = 0;
                for (int i = 0; i < diceCount2; i++)
                {
                    damage2 += rng.Next(1, diceSize2);
                }
                damage2 += damageBonus2;
            }
        });

        Roll newRoll = new Roll(initiativeCreature.Creature.Name + " " + initiativeCreature.InitiativeCreatureData.NameID, rollName, roll1, roll2, modifier, damage1, damageType1, damage2, damageType2, Combat.RoundCount, Combat.Id);
        Rolls.Insert(0, newRoll);
        await RollService.SaveAsync(newRoll);
    }
}
