namespace TimeForBattle.Model;

public partial class Roll : DatabaseObject
{
    [ObservableProperty] string creatureName;
    [ObservableProperty] string rollName;
    [ObservableProperty] int rollValue1;
    [ObservableProperty] int rollValue2;
    [ObservableProperty] int modifier;
    [ObservableProperty] string modifierString;
    [ObservableProperty] int? damage;
    [ObservableProperty] string? damageType;
    [ObservableProperty] int round;

    public Roll(string creatureName, string rollName, int rollValue1, int rollValue2, int modifier, int? damage, string? damageType, int round, int combatID)
    {
        CreatureName = creatureName;
        RollName = rollName;
        RollValue1 = rollValue1;
        RollValue2 = rollValue2;
        Modifier = modifier;
        Damage = damage;
        DamageType = damageType;
        if (Modifier < 0)
            ModifierString = Modifier.ToString();
        else
            ModifierString = "+" + Modifier.ToString();
        Round = round;
        CombatID = combatID;
    }

    public Roll() { }
}