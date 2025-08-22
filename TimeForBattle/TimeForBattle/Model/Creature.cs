namespace TimeForBattle.Model;

public partial class Creature : DatabaseObject
{
    [ObservableProperty] public bool isPlayer;

    [ObservableProperty] public string name;
    [ObservableProperty] public string size;
    [ObservableProperty] public string type;
    [ObservableProperty] public string alignment;

    [ObservableProperty] public int armorClass;
    [ObservableProperty] public int initiativeBonus;
    [ObservableProperty] public int maximumHitPoints;
    [ObservableProperty] public string speed;
    [ObservableProperty] public string challengeRating;
    [ObservableProperty] public int proficiencyBonus;

    [ObservableProperty] public int strScore;
    [ObservableProperty] public int dexScore;
    [ObservableProperty] public int conScore;
    [ObservableProperty] public int intScore;
    [ObservableProperty] public int wisScore;
    [ObservableProperty] public int chaScore;

    [ObservableProperty] public bool strSaveProf;
    [ObservableProperty] public bool dexSaveProf;
    [ObservableProperty] public bool conSaveProf;
    [ObservableProperty] public bool intSaveProf;
    [ObservableProperty] public bool wisSaveProf;
    [ObservableProperty] public bool chaSaveProf;

    [ObservableProperty] public string skills;
    [ObservableProperty] public string resistances;
    [ObservableProperty] public string vulnerabilities;
    [ObservableProperty] public string immunities;
    [ObservableProperty] public string senses;
    [ObservableProperty] public string languages;
    [ObservableProperty] public string gear;

    [ObservableProperty] public string traits;

    [ObservableProperty] public string actions;
    [ObservableProperty] public string bonusActions;
    [ObservableProperty] public string reactions;
    [ObservableProperty] public string legendaryActions;

    [ObservableProperty] public string hotKey1Name;
    [ObservableProperty] public string hotKey1Attribute;
    [ObservableProperty] public bool hotKey1Proficiency;
    [ObservableProperty] public int hotKey1DamageDiceNumber;
    [ObservableProperty] public int hotKey1DamageDiceSize;
    [ObservableProperty] public int hotKey1DamageBonus;
    [ObservableProperty] public string hotKey1DamageType;

    [ObservableProperty] public string hotKey2Name;
    [ObservableProperty] public string hotKey2Attribute;
    [ObservableProperty] public bool hotKey2Proficiency;
    [ObservableProperty] public int hotKey2DamageDiceNumber;
    [ObservableProperty] public int hotKey2DamageDiceSize;
    [ObservableProperty] public int hotKey2DamageBonus;
    [ObservableProperty] public string hotKey2DamageType;
}