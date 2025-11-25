using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

public partial class MainMenuViewModel : BaseViewModel
{
    public CreatureService<Combat> CombatService;
    public DialogService DialogService;
    public ObservableCollection<Combat> Combats { get; }
    [ObservableProperty] bool isRefreshing;
    [ObservableProperty] bool isNamingNewCombat = false;
    [ObservableProperty] string newCombatName = "";

    public MainMenuViewModel(CreatureService<Combat> combatService)
    {
        Title = "Main Menu";
        this.CombatService = combatService;
        Combats = [];
    }

    [RelayCommand]
    public async Task NewCombat(string name)
    {
        IsNamingNewCombat = true;
    }

    [RelayCommand]
    public async Task CancelNewCombat(string name)
    {
        NewCombatName = "";
        IsNamingNewCombat = false;
    }

    [RelayCommand]
    public async Task AddCombat(string name)
    {
        IsNamingNewCombat = false;

        Combat combat = new();

        Combats.Add(combat);
        await CombatService.SaveAsync(combat);

        if(!String.IsNullOrEmpty(NewCombatName))
        {
            combat.Name = NewCombatName;
        } else
        {
            combat.Name = "Encounter #" + combat.Id.ToString();
        }

        NewCombatName = "";

        await CombatService.SaveAsync(combat);

        await Shell.Current.GoToAsync($"{nameof(InitiativePage)}", true,
            new Dictionary<string, object>
            {
                {"Combat", combat}
            });
    }

    [RelayCommand]
    public async Task GoToCombatListPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CombatListPage)}", true);
    }


    [RelayCommand]
    public async Task GoToCreatureListAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CreatureListPage)}", true);
    }
}