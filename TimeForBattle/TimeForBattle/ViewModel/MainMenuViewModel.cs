using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

public partial class MainMenuViewModel : BaseViewModel
{
    public CreatureService<Combat> CombatService;
    public DialogService DialogService;
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
        string newCombatName = await Shell.Current.CurrentPage.DisplayPromptAsync(
            "Name",
            "Enter a name for the new encounter");

        if(newCombatName is null)
        {
            return;
        }

        Combat combat = new();

        Combats.Add(combat);
        await CombatService.SaveAsync(combat);

        if(!String.IsNullOrWhiteSpace(newCombatName))
        {
            combat.Name = newCombatName;
        } else
        {
            combat.Name = "Encounter #" + combat.Id.ToString();
        }

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