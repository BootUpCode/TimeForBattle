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

    [ObservableProperty] public int strScore;
    [ObservableProperty] public int dexScore;
    [ObservableProperty] public int conScore;
    [ObservableProperty] public int intScore;
    [ObservableProperty] public int wisScore;
    [ObservableProperty] public int chaScore;

    [ObservableProperty] public int strSaveBonus;
    [ObservableProperty] public int dexSaveBonus;
    [ObservableProperty] public int conSaveBonus;
    [ObservableProperty] public int intSaveBonus;
    [ObservableProperty] public int wisSaveBonus;
    [ObservableProperty] public int chaSaveBonus;

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
    [ObservableProperty] public int hotKey1Bonus;
    [ObservableProperty] public int hotKey1DamageDiceNumber;
    [ObservableProperty] public int hotKey1DamageDiceSize;
    [ObservableProperty] public int hotKey1DamageBonus;
    [ObservableProperty] public string hotKey1DamageType;

    [ObservableProperty] public string hotKey2Name;
    [ObservableProperty] public int hotKey2Bonus;
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

        this.StrScore = String.IsNullOrEmpty(creatureData[9]) ? 0 : int.Parse(creatureData[9]);
        this.StrSaveBonus = String.IsNullOrEmpty(creatureData[10]) ? 0 : int.Parse(creatureData[10]);
        this.DexScore = String.IsNullOrEmpty(creatureData[11]) ? 0 : int.Parse(creatureData[11]);
        this.DexSaveBonus = String.IsNullOrEmpty(creatureData[12]) ? 0 : int.Parse(creatureData[12]);
        this.ConScore = String.IsNullOrEmpty(creatureData[13]) ? 0 : int.Parse(creatureData[13]);
        this.ConSaveBonus = String.IsNullOrEmpty(creatureData[14]) ? 0 : int.Parse(creatureData[14]);
        this.IntScore = String.IsNullOrEmpty(creatureData[15]) ? 0 : int.Parse(creatureData[15]);
        this.IntSaveBonus = String.IsNullOrEmpty(creatureData[16]) ? 0 : int.Parse(creatureData[16]);
        this.WisScore = String.IsNullOrEmpty(creatureData[17]) ? 0 : int.Parse(creatureData[17]);
        this.WisSaveBonus = String.IsNullOrEmpty(creatureData[18]) ? 0 : int.Parse(creatureData[18]);
        this.ChaScore = String.IsNullOrEmpty(creatureData[19]) ? 0 : int.Parse(creatureData[19]);
        this.ChaSaveBonus = String.IsNullOrEmpty(creatureData[20]) ? 0 : int.Parse(creatureData[20]);

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

        this.HotKey1Name = String.IsNullOrEmpty(creatureData[32]) ? string.Empty : creatureData[32];
        this.HotKey1Bonus = String.IsNullOrEmpty(creatureData[33]) ? 0 : int.Parse(creatureData[33]);
        this.HotKey1DamageDiceNumber = String.IsNullOrEmpty(creatureData[34]) ? 0 : int.Parse(creatureData[34]);
        this.HotKey1DamageDiceSize = String.IsNullOrEmpty(creatureData[35]) ? 0 : int.Parse(creatureData[35]);
        this.HotKey1DamageBonus = String.IsNullOrEmpty(creatureData[36]) ? 0 : int.Parse(creatureData[36]);
        this.HotKey1DamageType = String.IsNullOrEmpty(creatureData[37]) ? string.Empty : creatureData[37];

        this.HotKey2Name = String.IsNullOrEmpty(creatureData[38]) ? string.Empty : creatureData[38];
        this.HotKey2Bonus = String.IsNullOrEmpty(creatureData[39]) ? 0 : int.Parse(creatureData[39]);
        this.HotKey2DamageDiceNumber = String.IsNullOrEmpty(creatureData[40]) ? 0 : int.Parse(creatureData[40]);
        this.HotKey2DamageDiceSize = String.IsNullOrEmpty(creatureData[41]) ? 0 : int.Parse(creatureData[41]);
        this.HotKey2DamageBonus = String.IsNullOrEmpty(creatureData[42]) ? 0 : int.Parse(creatureData[42]);
        this.HotKey2DamageType = String.IsNullOrEmpty(creatureData[43]) ? string.Empty : creatureData[43];
    }
}