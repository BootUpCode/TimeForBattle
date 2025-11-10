using System.Text.RegularExpressions;
using TimeForBattle.Services;

namespace TimeForBattle.ViewModel;

[QueryProperty("Creature", "Creature")]
public partial class AddCreatureViewModel : BaseViewModel
{
    public CreatureService<Creature> CreatureService;
    public InitiativeService<InitiativeCreatureData> InitiativeService;
    public DialogService DialogService;
    [ObservableProperty] public static ObservableCollection<string> attributeNames = ["Str", "Dex", "Con", "Int", "Wis", "Cha"];
    [ObservableProperty] public static ObservableCollection<string> damageTypes = ["acid", "bludgeoning", "cold", "fire", "force", "lightning", "necrotic", "piercing", "poison", "psychic", "radiant", "slashing", "thunder"];
    [ObservableProperty] public string importCreatureText = "";

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

    [RelayCommand]
    public async Task ImportFromText()
    {
        string[] regexStrings =
        [
            @"([a-zA-Z ]+)\r?\n?",
            @"(?<size>Small|Medium|Large|Huge|Gargantuan)",
            @"[a-zA-Z ]+\r?\n?[a-zA-Z]+ ([a-zA-Z() ]+)",
            @"[a-zA-Z]+\r?\n?[a-zA-Z]+ [a-zA-Z() ]+, ([a-zA-Z ]+)",
            @"AC ([0-9]+)",
            @"HP ([0-9]+)",
            @"CR ([0-9]+)",
            @"Initiative ([\+|\-][0-9]+)",
            @"Speed ([0-9a-zA-Z .,]+)",
            @"Str\r?\n?([0-9]+)",
            @"Str\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Dex\r?\n?([0-9]+)",
            @"Dex\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Con\r?\n?([0-9]+)",
            @"Con\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Int\r?\n?([0-9]+)",
            @"Int\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Wis\r?\n?([0-9]+)",
            @"Wis\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Cha\r?\n?([0-9]+)",
            @"Cha\r?\n?[0-9]+\n?\r?[\+\-0-9]+\r?\n?([\+\-0-9]+)",
            @"Skills ([a-zA-Z0-9 ,+]+)",
            @"Vulnerabilities ([a-zA-Z ,;]+)",
            @"Resistances ([a-zA-Z ,;]+)",
            @"Immunities ([a-zA-Z ,;]+)",
            @"Senses ([a-zA-Z0-9. ,;]+)",
            @"Languages ([a-zA-Z ,]+)",
            @"Traits\s*((?:(?!Actions|Bonus actions|Reactions|Legendary actions).)*)",
            @"Actions\s*((?:(?!Bonus actions|Reactions|Legendary actions).)*)",
            @"Bonus actions\s*((?:(?!Reactions|Legendary actions).)*)",
            @"Reactions\s*((?:(?!Legendary actions).)*)",
            @"Legendary actions\s*(.*)"
        ];

        string[] matches = new string[regexStrings.Length];

        await Task.Run(() =>
        {
            RegexOptions options = RegexOptions.Singleline;
            ImportCreatureText = Regex.Replace(ImportCreatureText, "–", "-", options);

            for (int i = 0; i < regexStrings.Length; i++)
            {
                Regex pattern = new Regex(regexStrings[i], options);
                Match match = pattern.Match(ImportCreatureText);

                matches[i] = "";
                if (!String.IsNullOrEmpty(match.Groups[1].Value))
                {
                    matches[i] = match.Groups[1].Value.TrimEnd();
                }
            }

            this.Creature.Import(matches);
        });
    }
}
