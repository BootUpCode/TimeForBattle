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
        int result;

        this.Name = String.IsNullOrEmpty(creatureData[0]) ? string.Empty : creatureData[0];
        this.Size = String.IsNullOrEmpty(creatureData[1]) ? string.Empty : creatureData[1];
        this.Type = String.IsNullOrEmpty(creatureData[2]) ? string.Empty : creatureData[2];
        this.Alignment = String.IsNullOrEmpty(creatureData[3]) ? string.Empty : creatureData[3];

        this.ArmorClass = int.TryParse(creatureData[4], out result) ? result : 0;
        this.MaximumHitPoints = int.TryParse(creatureData[5], out result) ? result : 0;
        this.ChallengeRating = String.IsNullOrEmpty(creatureData[6]) ? string.Empty : creatureData[6];
        this.InitiativeBonus = int.TryParse(creatureData[7], out result) ? result : 0;
        this.Speed = String.IsNullOrEmpty(creatureData[8]) ? string.Empty : creatureData[8];

        this.StrScore = int.TryParse(creatureData[9], out result) ? result : 0;
        this.StrSaveBonus = int.TryParse(creatureData[10], out result) ? result : 0;
        this.DexScore = int.TryParse(creatureData[11], out result) ? result : 0;
        this.DexSaveBonus = int.TryParse(creatureData[12], out result) ? result : 0;
        this.ConScore = int.TryParse(creatureData[13], out result) ? result : 0;
        this.ConSaveBonus = int.TryParse(creatureData[14], out result) ? result : 0;
        this.IntScore = int.TryParse(creatureData[15], out result) ? result : 0;
        this.IntSaveBonus = int.TryParse(creatureData[16], out result) ? result : 0;
        this.WisScore = int.TryParse(creatureData[17], out result) ? result : 0;
        this.WisSaveBonus = int.TryParse(creatureData[18], out result) ? result : 0;
        this.ChaScore = int.TryParse(creatureData[19], out result) ? result : 0;
        this.ChaSaveBonus = int.TryParse(creatureData[20], out result) ? result : 0;

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
        this.HotKey1Bonus = int.TryParse(creatureData[33], out result) ? result : 0;
        this.HotKey1DamageDiceNumber = int.TryParse(creatureData[34], out result) ? result : 0;
        this.HotKey1DamageDiceSize = int.TryParse(creatureData[35], out result) ? result : 0;
        this.HotKey1DamageBonus = int.TryParse(creatureData[36], out result) ? result : 0;
        this.HotKey1DamageType = String.IsNullOrEmpty(creatureData[37]) ? string.Empty : creatureData[37];

        this.HotKey2Name = String.IsNullOrEmpty(creatureData[38]) ? string.Empty : creatureData[38];
        this.HotKey2Bonus = int.TryParse(creatureData[39], out result) ? result : 0;
        this.HotKey2DamageDiceNumber = int.TryParse(creatureData[40], out result) ? result : 0;
        this.HotKey2DamageDiceSize = int.TryParse(creatureData[41], out result) ? result : 0;
        this.HotKey2DamageBonus = int.TryParse(creatureData[42], out result) ? result : 0;
        this.HotKey2DamageType = String.IsNullOrEmpty(creatureData[43]) ? string.Empty : creatureData[43];
    }
}