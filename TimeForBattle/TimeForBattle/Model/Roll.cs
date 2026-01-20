using System.Text.RegularExpressions;

namespace TimeForBattle.Model;

public partial class Roll : DatabaseObject
{
    [ObservableProperty] string creatureName;
    [ObservableProperty] int? creatureID;
    [ObservableProperty] string rollName;
    [ObservableProperty] int rollValue1;
    [ObservableProperty] int rollValue2;
    [ObservableProperty] int modifier;
    [ObservableProperty] string modifierString;
    [ObservableProperty] int? damage1;
    [ObservableProperty] string? damageType1;
    [ObservableProperty] int? damage2;
    [ObservableProperty] string? damageType2;
    [ObservableProperty] int round;

    public Roll(string creatureName, string rollName, int rollValue1, int rollValue2, int modifier, int? damage1, string? damageType1, int? damage2, string? damageType2, int round, int combatID)
    {
        CreatureName = creatureName.TrimEnd(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        var result = Regex.Match(creatureName, @"\d+$", RegexOptions.RightToLeft);
        if (result.Success) {
            CreatureID = int.TryParse(result.Value, out int ID) ? ID : null;
        } else
        {
            CreatureID = null;
        }

        RollName = rollName;
        RollValue1 = rollValue1;
        RollValue2 = rollValue2;
        Modifier = modifier;
        Damage1 = damage1;
        DamageType1 = damageType1;
        Damage2 = damage2;
        DamageType2 = damageType2;
        if (Modifier < 0)
            ModifierString = Modifier.ToString();
        else
            ModifierString = "+" + Modifier.ToString();
        Round = round;
        CombatID = combatID;
    }

    public Roll() { }
}