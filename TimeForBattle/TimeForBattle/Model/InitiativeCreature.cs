
namespace TimeForBattle.Model;

public partial class InitiativeCreature : Creature
{
    public int CreatureID { get; set; }
    public int CombatID { get; set; }
    [ObservableProperty] public int currentHitPoints;
    [ObservableProperty] public int? initiative;
    [ObservableProperty] public int? nameID;
    [ObservableProperty] public bool isTurn;
    [ObservableProperty] public bool isExpanded;

    public InitiativeCreature() { }

    public InitiativeCreature(Creature creature, int combatID)
    {
        this.CreatureID = creature.Id;
        this.CombatID = combatID;
        this.NameID = null;
        this.Initiative = null;
        this.CurrentHitPoints = creature.MaximumHitPoints;
        this.IsTurn = false;
        this.IsExpanded = false;

        this.Name = creature.Name;
        this.IsPlayer = creature.IsPlayer;
        this.Size = creature.Size;
        this.Type = creature.Type;
        this.Alignment = creature.Alignment;

        this.ArmorClass = creature.ArmorClass;
        this.InitiativeBonus = creature.InitiativeBonus;
        this.MaximumHitPoints = creature.MaximumHitPoints;
        this.Speed = creature.Speed;
        this.ChallengeRating = creature.ChallengeRating;
        this.ProficiencyBonus = creature.ProficiencyBonus;

        this.StrScore = creature.StrScore;
        this.DexScore = creature.DexScore;
        this.ConScore = creature.ConScore;
        this.IntScore = creature.IntScore;
        this.WisScore = creature.WisScore;
        this.ChaScore = creature.ChaScore;

        this.StrSaveProf = creature.StrSaveProf;
        this.DexSaveProf = creature.DexSaveProf;
        this.ConSaveProf = creature.ConSaveProf;
        this.IntSaveProf = creature.IntSaveProf;
        this.WisSaveProf = creature.WisSaveProf;
        this.ChaSaveProf = creature.ChaSaveProf;

        this.Skills = creature.Skills;
        this.Resistances = creature.Resistances;
        this.Vulnerabilities = creature.Vulnerabilities;
        this.Immunities = creature.Immunities;
        this.Senses = creature.Senses;
        this.Languages = creature.Languages;
        this.Gear = creature.Gear;

        this.Traits = creature.Traits;

        this.Actions = creature.Actions;
        this.BonusActions = creature.BonusActions;
        this.Reactions = creature.Reactions;
        this.LegendaryActions = creature.LegendaryActions;

        this.hotKeyName = creature.HotKeyName;
        this.hotKeyAttribute = creature.HotKeyAttribute;
        this.hotKeyProficiency = creature.HotKeyProficiency;
    }
}


