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

    public void Import(string[] creatureData)
    {
        this.Name = String.IsNullOrEmpty(creatureData[0]) ? string.Empty : creatureData[0];
        this.Size = String.IsNullOrEmpty(creatureData[1]) ? string.Empty : creatureData[1];
        this.Type = String.IsNullOrEmpty(creatureData[2]) ? string.Empty : creatureData[2];
        this.Alignment = String.IsNullOrEmpty(creatureData[3]) ? string.Empty : creatureData[3];
        this.ArmorClass = String.IsNullOrEmpty(creatureData[4]) ? 0 : int.Parse(creatureData[4]);
        this.MaximumHitPoints = String.IsNullOrEmpty(creatureData[5]) ? 0 : int.Parse(creatureData[5]);
        this.ChallengeRating = String.IsNullOrEmpty(creatureData[6]) ? string.Empty : creatureData[6];
        this.InitiativeBonus = String.IsNullOrEmpty(creatureData[7]) ? 0 : int.Parse(creatureData[7]);
        this.Speed = String.IsNullOrEmpty(creatureData[8]) ? string.Empty : creatureData[8];
        this.ProficiencyBonus = 0;

        this.StrScore = String.IsNullOrEmpty(creatureData[9]) ? 0 : int.Parse(creatureData[9]);
        this.StrSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[10]) && ((this.StrScore - 10) / 2 ) < int.Parse(creatureData[10]))
        {
            this.StrSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[10]) - ((this.StrScore - 10) / 2);
        }

        this.DexScore = String.IsNullOrEmpty(creatureData[11]) ? 0 : int.Parse(creatureData[11]);
        this.DexSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[12]) && ((this.DexScore - 10) / 2) < int.Parse(creatureData[12]))
        {
            this.DexSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[12]) - ((this.DexScore - 10) / 2);
        }

        this.ConScore = String.IsNullOrEmpty(creatureData[13]) ? 0 : int.Parse(creatureData[13]);
        this.ConSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[14]) && ((this.ConScore - 10) / 2) < int.Parse(creatureData[14]))
        {
            this.ConSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[14]) - ((this.ConScore - 10) / 2);
        }

        this.IntScore = String.IsNullOrEmpty(creatureData[15]) ? 0 : int.Parse(creatureData[15]);
        this.IntSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[16]) && ((this.IntScore - 10) / 2) < int.Parse(creatureData[16]))
        {
            this.IntSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[16]) - ((this.IntScore - 10) / 2);
        }

        this.WisScore = String.IsNullOrEmpty(creatureData[17]) ? 0 : int.Parse(creatureData[17]);
        this.WisSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[18]) && ((this.WisScore - 10) / 2) < int.Parse(creatureData[18]))
        {
            this.WisSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[18]) - ((this.WisScore - 10) / 2);
        }

        this.ChaScore = String.IsNullOrEmpty(creatureData[19]) ? 0 : int.Parse(creatureData[19]);
        this.ChaSaveProf = false;
        if (!String.IsNullOrEmpty(creatureData[20]) && ((this.ChaScore - 10) / 2) < int.Parse(creatureData[20]))
        {
            this.ChaSaveProf = true;
            this.ProficiencyBonus = int.Parse(creatureData[20]) - ((this.ChaScore - 10) / 2);
        }

        this.Skills = String.IsNullOrEmpty(creatureData[21]) ? string.Empty : creatureData[21];
        this.Vulnerabilities = String.IsNullOrEmpty(creatureData[22]) ? string.Empty : creatureData[22];
        this.Resistances = String.IsNullOrEmpty(creatureData[23]) ? string.Empty : creatureData[23];
        this.Immunities = String.IsNullOrEmpty(creatureData[24]) ? string.Empty : creatureData[24];
        this.Senses = String.IsNullOrEmpty(creatureData[25]) ? string.Empty : creatureData[25];
        this.Languages = String.IsNullOrEmpty(creatureData[26]) ? string.Empty : creatureData[26];
        this.Traits = String.IsNullOrEmpty(creatureData[27]) ? string.Empty : creatureData[27];
        this.Actions = String.IsNullOrEmpty(creatureData[28]) ? string.Empty : creatureData[28];
        this.BonusActions = String.IsNullOrEmpty(creatureData[29]) ? string.Empty : creatureData[29];
        this.Reactions = String.IsNullOrEmpty(creatureData[30]) ? string.Empty : creatureData[30];
        this.LegendaryActions = String.IsNullOrEmpty(creatureData[31]) ? string.Empty : creatureData[31];

        this.HotKey1Name = null;
        this.HotKey1Attribute = null;
        this.HotKey1Proficiency = false;
        this.HotKey1DamageDiceNumber = 0;
        this.HotKey1DamageDiceSize = 0;
        this.HotKey1DamageBonus = 0;
        this.HotKey1DamageType = null;

        this.HotKey2Name = null;
        this.HotKey2Attribute = null;
        this.HotKey2Proficiency = false;
        this.HotKey2DamageDiceNumber = 0;
        this.HotKey2DamageDiceSize = 0;
        this.HotKey2DamageBonus = 0;
        this.HotKey2DamageType = null;
    }
}