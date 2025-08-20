using TimeForBattle.Services;

namespace TimeForBattle.ViewModel;

[QueryProperty("Creature", "Creature")]
public partial class AddCreatureViewModel : BaseViewModel
{
    public CreatureService<Creature> CreatureService;
    public InitiativeService<InitiativeCreatureData> InitiativeService;
    public DialogService DialogService;
    [ObservableProperty] public static ObservableCollection<string> attributeNames = ["Str", "Dex", "Con", "Int", "Wis", "Cha"];

    public AddCreatureViewModel(CreatureService<Creature> characterService, InitiativeService<InitiativeCreatureData> initiativeService, DialogService dialogService)
    {
        this.CreatureService = characterService;
        this.InitiativeService = initiativeService;
        this.DialogService = dialogService;
    }

    [ObservableProperty] Creature creature;

    [RelayCommand]
    public async Task SaveCreatureAsync()
    {
        if (Creature is null)
            return;

        await CreatureService.SaveAsync(Creature);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task DeleteCreatureAsync()
    {
        if (Creature is null)
            return;

        bool answer = await DialogService.ShowConfirmationAsync((ContentPage)AppShell.Current.CurrentPage, "Delete?", "Are you sure you want to delete this creature?", "Yes", "No");
        if (answer)
        {
            if (await CreatureService.DeleteAsync(await CreatureService.GetByIdAsync(Creature.Id)) > 0)
            {
                List<InitiativeCreatureData> deleteList = await InitiativeService.GetAllByCreatureAsync(Creature.Id);
                foreach (InitiativeCreatureData deleteCreature in deleteList)
                {
                    await InitiativeService.DeleteAsync(deleteCreature);
                }
            }

            await Shell.Current.GoToAsync("../..");
        }
    }
}
