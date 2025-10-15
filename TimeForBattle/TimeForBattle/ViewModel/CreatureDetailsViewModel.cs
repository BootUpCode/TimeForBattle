using TimeForBattle.Services;
using TimeForBattle.View;

namespace TimeForBattle.ViewModel;

[QueryProperty("Creature", "Creature")]
public partial class CreatureDetailsViewModel: BaseViewModel
{
    public CreatureService<Creature> creatureService;

    public bool[] Bools { get => [false, true]; }
    public String[] Attributes { get => ["Str", "Dex", "Con", "Int", "Wis", "Cha"]; }

    public CreatureDetailsViewModel(CreatureService<Creature> characterService)
    {
        this.creatureService = characterService;
    }

    [ObservableProperty] Creature creature;

    [RelayCommand]
    public async Task GoToEditAsync()
    {
        if (Creature is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(AddCreaturePage)}", true,
            new Dictionary<string, object>
            {
                {"Creature", Creature}
            });
    }

    [RelayCommand]
    public async Task ReturnAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
