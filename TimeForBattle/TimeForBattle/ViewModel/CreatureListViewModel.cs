using System.ComponentModel.Design;
using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

[QueryProperty("Combat", "Combat")]
public partial class CreatureListViewModel : BaseViewModel
{
    [ObservableProperty] public ObservableCollection<Creature> creatures = new();
    public CreatureService<Creature> CreatureService;
    public InitiativeService<InitiativeCreatureData> InitiativeService;
    
    public ObservableCollection<Creature> Monsters { get; }
    public ObservableCollection<Creature> Players { get; }
    public ObservableCollection<InitiativeCreature> Initiative { get; }

    public bool ViewMonsters;
    [ObservableProperty] public Combat combat;

    public CreatureListViewModel(CreatureService<Creature> creatureService, InitiativeService<InitiativeCreatureData> initiativeService)
    {
        Title = "Creatures";
        this.CreatureService = creatureService;
        this.InitiativeService = initiativeService;
        Creatures = [];
        Monsters = [];
        Players = [];
        Initiative = [];
        ViewMonsters = true;
    }

    [ObservableProperty] bool isRefreshing;

    [RelayCommand]
    public async Task RefreshCreatures()
    {
        List<Creature> creatureData = await CreatureService.GetAllAsync();
        Creatures.Clear();
        Monsters.Clear();
        Players.Clear();

        foreach (Creature creature in creatureData)
        {
            if (creature.IsPlayer)
                Players.Add(creature);
            else
                Monsters.Add(creature);
        }

        if (this.Combat != null)
        {
            List<InitiativeCreatureData> initiativeCreatureDataList = await InitiativeService.GetAllByCombatAsync(Combat.Id);

            Initiative.Clear();

            foreach (InitiativeCreatureData initiativeCreatureData in initiativeCreatureDataList)
            {
                InitiativeCreature initiativeCreature = new(await CreatureService.GetByIdAsync(initiativeCreatureData.CreatureID), initiativeCreatureData);
                Initiative.Add(initiativeCreature);
            }
        }

        await Task.Run(() => ChangeView(ViewMonsters));
    }

    [RelayCommand]
    public void ChangeView(bool viewMonsters)
    {
        ViewMonsters = viewMonsters;

        if (ViewMonsters)
            Creatures = Monsters;
        else
            Creatures = Players;
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Creature creature)
    {
        if (creature is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CreatureDetailsPage)}", true,
            new Dictionary<string, object>
            {
                {"Creature", creature}
            });
    }

    [RelayCommand]
    public async Task GoToInitiativeAsync()
    {
        if (this.Initiative is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(InitiativePage)}", true,
            new Dictionary<string, object>
            {
                {"Combat", Combat}
            });
    }

    [RelayCommand]
    public async Task AddToInitiativeAsync(Creature creature)
    {
        if (creature is null | Combat is null)
            return;

        InitiativeCreature newInitiativeCreature = new(creature, Combat.Id);

        int identifier = 1;
        foreach(InitiativeCreature initiativeCreature in Initiative) {
            if (initiativeCreature.Creature.Id == newInitiativeCreature.Creature.Id)
            {
                if (initiativeCreature.InitiativeCreatureData.NameID is null)
                {
                    initiativeCreature.InitiativeCreatureData.NameID = identifier;
                    await InitiativeService.SaveAsync(initiativeCreature.InitiativeCreatureData);
                    identifier++;
                }
                else
                {
                    identifier = (int)initiativeCreature.InitiativeCreatureData.NameID + 1;
                }
            }
        }

        if (identifier > 1)
            newInitiativeCreature.InitiativeCreatureData.NameID = identifier;

        await InitiativeService.SaveAsync(newInitiativeCreature.InitiativeCreatureData);

        Initiative.Add(newInitiativeCreature);
    }

    [RelayCommand]
    public async Task RemoveFromInitiativeAsync(InitiativeCreature initiativeCreature)
    {
        if (initiativeCreature is null)
            return;

        if (await InitiativeService.DeleteAsync(await InitiativeService.GetByIdAsync(initiativeCreature.InitiativeCreatureData.Id)) > 0)
        {
            Initiative.Remove(initiativeCreature);
        }
    }

    [RelayCommand]
    public async Task NewCreatureAsync(bool isPlayer)
    {
        Creature creature = new()
        {
            IsPlayer = isPlayer
        };

        await Shell.Current.GoToAsync($"{nameof(AddCreaturePage)}", true,
            new Dictionary<string, object>
            {
                {"Creature", creature}
            });
    }
}
