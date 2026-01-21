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
    [ObservableProperty] public int challengeRating;

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
    [ObservableProperty] public int hotKey1DamageDiceNumber1;
    [ObservableProperty] public int hotKey1DamageDiceSize1;
    [ObservableProperty] public int hotKey1DamageBonus1;
    [ObservableProperty] public string hotKey1DamageType1;
    [ObservableProperty] public int hotKey1DamageDiceNumber2;
    [ObservableProperty] public int hotKey1DamageDiceSize2;
    [ObservableProperty] public int hotKey1DamageBonus2;
    [ObservableProperty] public string hotKey1DamageType2;

    [ObservableProperty] public string hotKey2Name;
    [ObservableProperty] public int hotKey2Bonus;
    [ObservableProperty] public int hotKey2DamageDiceNumber1;
    [ObservableProperty] public int hotKey2DamageDiceSize1;
    [ObservableProperty] public int hotKey2DamageBonus1;
    [ObservableProperty] public string hotKey2DamageType1;
    [ObservableProperty] public int hotKey2DamageDiceNumber2;
    [ObservableProperty] public int hotKey2DamageDiceSize2;
    [ObservableProperty] public int hotKey2DamageBonus2;
    [ObservableProperty] public string hotKey2DamageType2;

    public void Import(string[] creatureData)
    {
        int result;

        this.Name = String.IsNullOrEmpty(creatureData[0]) ? string.Empty : creatureData[0];
        this.Size = String.IsNullOrEmpty(creatureData[1]) ? string.Empty : creatureData[1];
        this.Type = String.IsNullOrEmpty(creatureData[2]) ? string.Empty : creatureData[2];
        this.Alignment = String.IsNullOrEmpty(creatureData[3]) ? string.Empty : creatureData[3];

        this.StrScore = int.TryParse(creatureData[9], out result) ? result : 0;
        this.StrSaveBonus = int.TryParse(creatureData[10], out result) ? result : (int)Math.Floor(((double)this.StrScore - 10) / 2);
        this.StrSaveBonus = int.TryParse(creatureData[47], out result) ? result : this.StrSaveBonus;
        this.DexScore = int.TryParse(creatureData[11], out result) ? result : 0;
        this.DexSaveBonus = int.TryParse(creatureData[12], out result) ? result : (int)Math.Floor(((double)this.DexScore - 10) / 2);
        this.DexSaveBonus = int.TryParse(creatureData[48], out result) ? result : this.DexSaveBonus;
        this.ConScore = int.TryParse(creatureData[13], out result) ? result : 0;
        this.ConSaveBonus = int.TryParse(creatureData[14], out result) ? result : (int)Math.Floor(((double)this.ConScore - 10) / 2);
        this.ConSaveBonus = int.TryParse(creatureData[49], out result) ? result : this.ConSaveBonus;
        this.IntScore = int.TryParse(creatureData[15], out result) ? result : 0;
        this.IntSaveBonus = int.TryParse(creatureData[16], out result) ? result : (int)Math.Floor(((double)this.IntScore - 10) / 2);
        this.IntSaveBonus = int.TryParse(creatureData[50], out result) ? result : this.IntSaveBonus;
        this.WisScore = int.TryParse(creatureData[17], out result) ? result : 0;
        this.WisSaveBonus = int.TryParse(creatureData[18], out result) ? result : (int)Math.Floor(((double)this.WisScore - 10) / 2);
        this.WisSaveBonus = int.TryParse(creatureData[51], out result) ? result : this.WisSaveBonus;
        this.ChaScore = int.TryParse(creatureData[19], out result) ? result : 0;
        this.ChaSaveBonus = int.TryParse(creatureData[20], out result) ? result : (int)Math.Floor(((double)this.ChaScore - 10) / 2);
        this.ChaSaveBonus = int.TryParse(creatureData[52], out result) ? result : this.ChaSaveBonus;

        this.ArmorClass = int.TryParse(creatureData[4], out result) ? result : 0;
        this.MaximumHitPoints = int.TryParse(creatureData[5], out result) ? result : 0;
        this.ChallengeRating = int.TryParse(creatureData[6], out result) ? (result * 8) : 0;
        if (this.ChallengeRating == 0)
        {
            string[] split = creatureData[6].Split(new char[] { '/' });
            if (split.Length == 2)
            {
                int a, b;
                if (int.TryParse(split[0].Trim(), out a) && int.TryParse(split[1].Trim(), out b))
                {
                    if (split.Length == 2)
                    {
                        this.ChallengeRating = 8 * a / b;
                    }
                }
            }
        }
        this.InitiativeBonus = int.TryParse(creatureData[7], out result) ? result : (int)Math.Floor(((double)this.DexScore - 10) / 2);
        this.Speed = String.IsNullOrEmpty(creatureData[8]) ? string.Empty : creatureData[8];

        this.Skills = String.IsNullOrEmpty(creatureData[21]) ? string.Empty : creatureData[21];
        this.Vulnerabilities = String.IsNullOrEmpty(creatureData[22]) ? string.Empty : creatureData[22];
        this.Resistances = String.IsNullOrEmpty(creatureData[23]) ? string.Empty : creatureData[23];
        this.Immunities = String.IsNullOrEmpty(creatureData[24]) ? string.Empty : creatureData[24];
        if (!String.IsNullOrEmpty(creatureData[44]) && !String.IsNullOrEmpty(creatureData[45])) { this.Immunities = creatureData[44] + "; " + creatureData[45]; }
        this.Senses = String.IsNullOrEmpty(creatureData[25]) ? string.Empty : creatureData[25];
        this.Languages = String.IsNullOrEmpty(creatureData[26]) ? string.Empty : creatureData[26];

        this.Traits = String.IsNullOrEmpty(creatureData[27]) ? string.Empty : creatureData[27];
        if (String.IsNullOrEmpty(this.Traits) && !String.IsNullOrEmpty(creatureData[46])) { this.Traits = creatureData[46].Trim().Replace("    ", "\n\n"); }
        this.Actions = String.IsNullOrEmpty(creatureData[28]) ? string.Empty : creatureData[28].Replace("    ", "\n\n");
        this.BonusActions = String.IsNullOrEmpty(creatureData[29]) ? string.Empty : creatureData[29].Replace("    ", "\n\n");
        this.Reactions = String.IsNullOrEmpty(creatureData[30]) ? string.Empty : creatureData[30].Replace("    ", "\n\n");
        this.LegendaryActions = String.IsNullOrEmpty(creatureData[31]) ? string.Empty : creatureData[31].Replace("    ", "\n\n");

        this.HotKey1Name = String.IsNullOrEmpty(creatureData[32]) ? string.Empty : creatureData[32].Trim();
        this.HotKey1Bonus = int.TryParse(creatureData[33], out result) ? result : 0;
        this.HotKey1DamageDiceNumber1 = int.TryParse(creatureData[34], out result) ? result : 0;
        this.HotKey1DamageDiceSize1 = int.TryParse(creatureData[35], out result) ? result : 0;
        this.HotKey1DamageBonus1 = int.TryParse(creatureData[36].Replace(" ", string.Empty), out result) ? result : 0;
        this.HotKey1DamageType1 = String.IsNullOrEmpty(creatureData[37]) ? string.Empty : char.ToUpper(creatureData[37][0]) + creatureData[37].Substring(1);
        this.HotKey1DamageDiceNumber2 = int.TryParse(creatureData[53], out result) ? result : 0;
        this.HotKey1DamageDiceSize2 = int.TryParse(creatureData[54], out result) ? result : 0;
        this.HotKey1DamageBonus2 = int.TryParse(creatureData[55].Replace(" ", string.Empty), out result) ? result : 0;
        this.HotKey1DamageType2 = String.IsNullOrEmpty(creatureData[56]) ? string.Empty : char.ToUpper(creatureData[56][0]) + creatureData[56].Substring(1);

        this.HotKey2Name = String.IsNullOrEmpty(creatureData[38]) ? string.Empty : creatureData[38].Trim();
        this.HotKey2Bonus = int.TryParse(creatureData[39], out result) ? result : 0;
        this.HotKey2DamageDiceNumber1 = int.TryParse(creatureData[40], out result) ? result : 0;
        this.HotKey2DamageDiceSize1 = int.TryParse(creatureData[41], out result) ? result : 0;
        this.HotKey2DamageBonus1 = int.TryParse(creatureData[42].Replace(" ", string.Empty), out result) ? result : 0;
        this.HotKey2DamageType1 = String.IsNullOrEmpty(creatureData[43]) ? string.Empty : char.ToUpper(creatureData[43][0]) + creatureData[43].Substring(1);
        this.HotKey2DamageDiceNumber2 = int.TryParse(creatureData[57], out result) ? result : 0;
        this.HotKey2DamageDiceSize2 = int.TryParse(creatureData[58], out result) ? result : 0;
        this.HotKey2DamageBonus2 = int.TryParse(creatureData[59].Replace(" ", string.Empty), out result) ? result : 0;
        this.HotKey2DamageType2 = String.IsNullOrEmpty(creatureData[60]) ? string.Empty : char.ToUpper(creatureData[56][0]) + creatureData[56].Substring(1);
    }
}