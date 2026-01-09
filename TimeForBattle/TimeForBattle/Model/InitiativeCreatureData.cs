
namespace TimeForBattle.Model;

public partial class InitiativeCreatureData : DatabaseObject
{
    [ObservableProperty] public int currentHitPoints;
    [ObservableProperty] public int? initiative;
    [ObservableProperty] public int? nameID;
    [ObservableProperty] public bool isTurn;
    [ObservableProperty] public bool isExpanded;
    [ObservableProperty] public bool isBlinded;
    [ObservableProperty] public bool isCharmed;
    [ObservableProperty] public bool isDeafened;
    [ObservableProperty] public bool isFrightened;
    [ObservableProperty] public bool isGrappled;
    [ObservableProperty] public bool isIncapacitated;
    [ObservableProperty] public bool isInvisible;
    [ObservableProperty] public bool isParalysed;
    [ObservableProperty] public bool isPetrified;
    [ObservableProperty] public bool isPoisoned;
    [ObservableProperty] public bool isProne;
    [ObservableProperty] public bool isRestrained;
    [ObservableProperty] public bool isStunned;
    [ObservableProperty] public bool isUnconscious;
    [ObservableProperty] public bool isOtherA;
    [ObservableProperty] public bool isOtherB;
    [ObservableProperty] public int activeTab;

    public InitiativeCreatureData(Creature creature, int combatID)
    {
        this.CreatureID = creature.Id;
        this.CombatID = combatID;
        this.NameID = null;
        this.Initiative = null;
        this.CurrentHitPoints = creature.MaximumHitPoints;
        this.IsTurn = false;
        this.IsExpanded = false;
        this.IsBlinded = false;
        this.IsCharmed = false;
        this.IsDeafened = false;
        this.IsFrightened = false;
        this.IsGrappled = false;
        this.IsIncapacitated = false;
        this.IsInvisible = false;
        this.IsParalysed = false;
        this.IsPetrified = false;
        this.IsPoisoned = false;
        this.IsProne = false;
        this.IsRestrained = false;
        this.IsStunned = false;
        this.IsUnconscious = false;
        this.IsOtherA = false;
        this.IsOtherB = false;
        this.ActiveTab = 0;
    }

    public InitiativeCreatureData() { }
}


