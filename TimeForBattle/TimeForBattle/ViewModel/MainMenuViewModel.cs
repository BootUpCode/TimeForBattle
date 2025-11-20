using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

public partial class MainMenuViewModel : BaseViewModel
{
    public CreatureService<Combat> CombatService;
    public ObservableCollection<Combat> Combats { get; }
    [ObservableProperty] bool isRefreshing;

    public MainMenuViewModel(CreatureService<Combat> combatService)
    {
        Title = "Main Menu";
        this.CombatService = combatService;
        Combats = [];
    }

    [RelayCommand]
    public async Task NewCombat(string name)
    {
        Combat combat = new();

        Combats.Add(combat);
        await CombatService.SaveAsync(combat);

        combat.Name = "Combat #" + combat.Id.ToString();

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