using System.Text.RegularExpressions;
using TimeForBattle.Services;

namespace TimeForBattle.ViewModel;

[QueryProperty("Creature", "Creature")]
public partial class AddCreatureViewModel : BaseViewModel
{
    public CreatureService<Creature> CreatureService;
    public CreatureService<InitiativeCreatureData> InitiativeService;
    public DialogService DialogService;
    [ObservableProperty] public static ObservableCollection<string> attributeNames = ["Str", "Dex", "Con", "Int", "Wis", "Cha"];
    [ObservableProperty] public static ObservableCollection<string> damageTypes = ["Acid", "Bludgeoning", "Cold", "Fire", "Force", "Lightning", "Necrotic", "Piercing", "Poison", "Psychic", "Radiant", "Slashing", "Thunder"];
    [ObservableProperty] public string importCreatureText = "";

    public AddCreatureViewModel(CreatureService<Creature> characterService, CreatureService<InitiativeCreatureData> initiativeService, DialogService dialogService)
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

        bool answer = await DialogService.ShowConfirmationAsync((ContentPage)AppShell.Current.CurrentPage, "Delete?", "Are you sure you want to delete the creature \"" + Creature.Name + "\"?", "Yes", "No");
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
    public async Task PasteImportText()
    {
        await Task.Run(() => {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Clipboard.GetTextAsync().Result != null)
                {
                    ImportCreatureText = Clipboard.GetTextAsync().Result;
                }
            });
        });
    }

        [RelayCommand]
    public async Task ImportText()
    {
        if (String.IsNullOrEmpty(ImportCreatureText))
        {
            return;
        }

        string[] regexStrings =
        [
            @"([a-zA-Z ]+)\r?\n?",
            @"(Small|Medium|Large|Huge|Gargantuan)",
            @"(?:Small|Medium|Large|Huge|Gargantuan) ([a-zA-Z() ]+)",
            @"[a-zA-Z]+\r?\n?[a-zA-Z]+ [a-zA-Z() ]+, ([a-zA-Z ]+)",
            @"(?:AC|Armor Class) ([0-9]+)",
            @"(?:HP|Hit Points) ([0-9]+)",
            @"(?:CR|Challenge) ([0-9/]+)",
            @"Initiative ([\+|\-][0-9]+)",
            @"Speed ([0-9a-zA-Z .,]+)",
            @"(?:Str|STR)\s*?([0-9]+)",
            @"(?:Str|STR)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"(?:Dex|DEX)\s*?([0-9]+)",
            @"(?:Dex|DEX)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"(?:Con|CON)\s*?([0-9]+)",
            @"(?:Con|CON)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"(?:Int|INT)\s*?([0-9]+)",
            @"(?:Int|INT)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"(?:Wis|WIS)\s*?([0-9]+)",
            @"(?:Wis|WIS)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"(?:Cha|CHA)\s*?([0-9]+)",
            @"(?:Cha|CHA)\s*?[0-9]+\s*?[\+\-0-9]+\s*?([\+\-0-9]+)",
            @"Skills ([a-zA-Z0-9 ,+]+)",
            @"Vulnerabilities ([a-zA-Z ,;]+)",
            @"(?:Resistances|Resistance) ([a-zA-Z ,;]+)",
            @"Immunities ([a-zA-Z ,;]+)",
            @"Senses ([a-zA-Z0-9. ,;]+)",
            @"Languages ([a-zA-Z ,]+)",
            @"Traits\s*((?:(?!Actions|Bonus actions|Reactions|Legendary actions|Habitat|Treasure|Source).)*)",
            @"Actions\s*((?:(?!Bonus actions|Bonus Actions|Reactions|Legendary actions|Legendary Actions|Habitat|Treasure|Source).)*)",
            @"(?:Bonus actions|Bonus Actions)\s*((?:(?!Reactions|Legendary actions|Legendary Actions|Habitat|Treasure|Source).)*)",
            @"Reactions\s*((?:(?!Legendary actions|Habitat|Treasure|Source).)*)",
            @"(?:Legendary actions|Legendary Actions)\s*((?:(?!Habitat|Treasure|Source).)*)",
            @"Actions.*?([A-Za-z ]+)\. [A-Za-z ]+: \+",
            @"Actions.*?: \+([0-9]+)",
            @"Actions.*?\(([0-9]+)d[0-9]+ \+ [0-9]+\)",
            @"Actions.*?\([0-9]+d([0-9]+) \+ [0-9]+\)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ ([0-9]+)\)",
            @"Actions.*?(Acid|Bludgeoning|Cold|Fire|Force|Lightning|Necrotic|Piercing|Poison|Psychic|Radiant|Slashing|Thunder)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?([A-Za-z ]+)\. [A-Za-z ]+: \+",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?: \+([0-9]+)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?\(([0-9]+)d[0-9]+ \+ [0-9]+\)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?\([0-9]+d([0-9]+) \+ [0-9]+\)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?\([0-9]+d[0-9]+ \+ ([0-9]+)\)",
            @"Actions.*?\([0-9]+d[0-9]+ \+ [0-9]+\) [A-Za-z]+ damage\..*?(Acid|Bludgeoning|Cold|Fire|Force|Lightning|Necrotic|Piercing|Poison|Psychic|Radiant|Slashing|Thunder)",
            @"Damage Immunities ([a-zA-Z ,;]+)",
            @"Condition Immunities ([a-zA-Z ,;]+)",
            @"(?:Challenge) (?:[0-9/]+) \((?:[0-9,]+ XP)\)(.*?)(?:Actions)"
        ];

        string[] matches = new string[regexStrings.Length];
        int fieldsDetected = 0;

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
                    fieldsDetected++;
                }
            }
        });

        bool answer = await DialogService.ShowConfirmationAsync((ContentPage)AppShell.Current.CurrentPage, "Import?", "Are you sure you want to import the creature \'" + matches[0] + "\'? Statblock fields detected: " + fieldsDetected + ".", "Yes", "No");
        if (answer)
        {
            this.Creature.Import(matches);
            ImportCreatureText = "";
        }
    }

    [RelayCommand]
    public async Task ClearImportText()
    {
        await Task.Run(() =>
        {
            ImportCreatureText = "";
        });
    }
}
