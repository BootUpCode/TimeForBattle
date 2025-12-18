
namespace TimeForBattle.Model;

public partial class InitiativeCreatureData : DatabaseObject
{
    [ObservableProperty] public int currentHitPoints;
    [ObservableProperty] public int? initiative;
    [ObservableProperty] public int? nameID;
    [ObservableProperty] public bool isTurn;
    [ObservableProperty] public bool isExpanded;

    public InitiativeCreatureData(Creature creature, int combatID)
    {
        this.CreatureID = creature.Id;
        this.CombatID = combatID;
        this.NameID = null;
        this.Initiative = null;
        this.CurrentHitPoints = creature.MaximumHitPoints;
        this.IsTurn = false;
        this.IsExpanded = false;
    }

    public InitiativeCreatureData() { }
}


