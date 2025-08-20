using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

[QueryProperty("Combat", "Combat")]
public partial class InitiativeViewModel : BaseViewModel
{
    public CreatureService<Creature> CreatureService;
    public InitiativeService<InitiativeCreatureData> InitiativeService;
    public CreatureService<Combat> CombatService;
    [ObservableProperty] public ObservableCollection<InitiativeCreature> initiative = new();
    [ObservableProperty] Combat? combat;
    [ObservableProperty] public ObservableCollection<Roll> rolls = new();

    public InitiativeViewModel(CreatureService<Creature> creatureService, InitiativeService<InitiativeCreatureData> initiativeService, CreatureService<Combat> combatService)
    {
        this.CreatureService = creatureService;
        this.InitiativeService = initiativeService;
        this.CombatService = combatService;
        Initiative = [];
    }

    [RelayCommand]
    public async Task RefreshInitiativeAsync()
    {
        if (Combat is null)
            return;

        List<InitiativeCreatureData> initiativeCreatureDataList = await InitiativeService.GetAllByCombatAsync(Combat.Id);

        Initiative.Clear();

        foreach (InitiativeCreatureData initiativeCreatureData in initiativeCreatureDataList)
        {
            InitiativeCreature initiativeCreature = new(await CreatureService.GetByIdAsync(initiativeCreatureData.CreatureID), initiativeCreatureData);
            Initiative.Add(initiativeCreature);
        }

        await SortInitiativeAsync();
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
    public async Task RollInitiativeAsync()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        await Task.Run((Action)(() =>
        {
            Random rng = new();

            foreach (InitiativeCreature initiativeCreature in Initiative)
            {
                if (!initiativeCreature.Creature.IsPlayer && !initiativeCreature.InitiativeCreatureData.Initiative.HasValue)
                {
                    int initiative = rng.Next(1, 21) + initiativeCreature.Creature.InitiativeBonus;
                    initiativeCreature.InitiativeCreatureData.Initiative = initiative;
                }

                Task.Run(() => InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData));
            }
        }));

        await SortInitiativeAsync();

        InitiativeCreature? currentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);

        if (currentCreature is null)
        {
            Initiative[0].InitiativeCreatureData.IsTurn = true;
            await InitiativeService.SaveAsync(Initiative[0].InitiativeCreatureData);
        }

        Combat.IsStarted = true;
        await CombatService.SaveAsync(Combat);
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

        InitiativeCreature? currentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
        InitiativeCreature? nextCreature = null;

        if (currentCreature is not null)
        {
            if (Initiative.IndexOf(currentCreature) + 1 < Initiative.Count)
                nextCreature = Initiative[Initiative.IndexOf(currentCreature) + 1];
            else
            {
                nextCreature = Initiative[0];
                Combat.RoundCount++;
                await CombatService.SaveAsync(Combat);
            }

            currentCreature.InitiativeCreatureData.IsTurn = false;
            await Task.Run(() => InitiativeService.SaveAsync(currentCreature.InitiativeCreatureData));
        }
        else
            nextCreature = Initiative[0];

        if (nextCreature is not null)
        {
            nextCreature.InitiativeCreatureData.IsTurn = true;
            await Task.Run(() => InitiativeService.SaveAsync(nextCreature.InitiativeCreatureData));
        }

    }

    [RelayCommand]
    public async Task PreviousCreature()
    {
        if (Combat is null || Initiative is null || Initiative.Count == 0)
            return;

        InitiativeCreature? currentCreature = Initiative.FirstOrDefault(x => x.InitiativeCreatureData.IsTurn == true, null);
        InitiativeCreature? previousCreature = null;

        if (currentCreature is not null)
        {
            if (Initiative.IndexOf(currentCreature) - 1 >= 0)
                previousCreature = Initiative[Initiative.IndexOf(currentCreature) - 1];
            else
            {
                previousCreature = Initiative[^1];
                if (Combat.RoundCount > 0)
                    Combat.RoundCount--;
                await CombatService.SaveAsync(Combat);
            }

            currentCreature.InitiativeCreatureData.IsTurn = false;
            await Task.Run(() => InitiativeService.SaveAsync(currentCreature.InitiativeCreatureData));
        }
        else
            previousCreature = Initiative[0];

        if (previousCreature is not null)
        {
            previousCreature.InitiativeCreatureData.IsTurn = true;
            await Task.Run(() => InitiativeService.SaveAsync(previousCreature.InitiativeCreatureData));
        }
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
    public async Task CurrentHitPointsPlusAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints++;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task CurrentHitPointsMinusAsync(InitiativeCreature initiativeCreature)
    {
        if (Combat is null || Initiative is null || initiativeCreature is null || initiativeCreature.InitiativeCreatureData.CurrentHitPoints <= 0)
            return;

        initiativeCreature.InitiativeCreatureData.CurrentHitPoints--;
        await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
    }

    [RelayCommand]
    public async Task RollSaveAsync(Tuple<int?, string?, string?> parameters)
    {
        if (Combat is null || parameters.Item1 is null || parameters.Item2 is null || parameters.Item3 is null)
            return;

            Random rng = new();
            int roll1 = rng.Next(1, 21);
            int roll2 = rng.Next(1, 21);
            Rolls.Insert(0, new Roll(parameters.Item3, parameters.Item2, roll1, roll2, (int)parameters.Item1, Combat.RoundCount));
    }
}
