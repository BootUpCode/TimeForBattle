using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

public partial class CombatListViewModel : BaseViewModel
{
    public CreatureService<Combat> CombatService;
    public CreatureService<InitiativeCreatureData> InitiativeService;
    public DialogService DialogService;
    public ObservableCollection<Combat> Combats { get; }
    [ObservableProperty] bool isRefreshing;

    public CombatListViewModel(CreatureService<Combat> combatService, CreatureService<InitiativeCreatureData> initiativeService, DialogService dialogService)
    {
        Title = "Encounters";
        this.CombatService = combatService;
        this.InitiativeService = initiativeService;
        this.DialogService = dialogService;
        Combats = [];
    }

    [RelayCommand]
    public async Task GoToMainMenuAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(MainMenuPage)}", true);
    }

    [RelayCommand]
    public async Task RefreshCombats()
    {
        List<Combat> combatData = await CombatService.GetAllAsync();
        Combats.Clear();

        foreach (Combat combat in combatData)
        {
            Combats.Add(combat);
        }
    }

    [RelayCommand]
    public async Task LoadCombatAsync(Combat combat)
    {
        if (combat is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(InitiativePage)}", true,
            new Dictionary<string, object>
            {
                {"Combat", combat}
            });
    }

    [RelayCommand]
    public async Task RenameCombatAsync(Combat combat)
    {
        if (combat is null)
            return;

        string newCombatName = await Shell.Current.CurrentPage.DisplayPromptAsync(
            "Name",
            "Enter a new name for the encounter");

        if (String.IsNullOrWhiteSpace(newCombatName))
        {
            return;
        }
        else
        {
            combat.Name = newCombatName;
        }

        await CombatService.SaveAsync(combat);
    }

    [RelayCommand]
    public async Task DeleteCombatAsync(Combat combat)
    {
        if (combat is null)
            return;

        bool answer = await DialogService.ShowConfirmationAsync((ContentPage)AppShell.Current.CurrentPage, "Delete?", "Are you sure you want to delete the encounter \"" + combat.Name + "\"?", "Yes", "No");
        if (answer)
            if (await CombatService.DeleteAsync(combat) > 0)
            {
                List<InitiativeCreatureData> deleteList = await InitiativeService.GetAllByCombatAsync(combat.Id);
                foreach (InitiativeCreatureData deleteCreature in deleteList)
                {
                    await InitiativeService.DeleteAsync(deleteCreature);
                }
            }
        
        await RefreshCombats();
    }
}
