

namespace TimeForBattle.Model;

public partial class InitiativeCreature : ObservableObject
{
    [ObservableProperty] public Creature creature;
    [ObservableProperty] public InitiativeCreatureData initiativeCreatureData;

    //Constructor for new creature in initiative
    public InitiativeCreature(Creature creature, int combatID)
    {
        this.InitiativeCreatureData = new InitiativeCreatureData(creature, combatID);
        this.Creature = creature;
    }

    //Constructor for existing creature in initiative loaded from sql data
    public InitiativeCreature(Creature creature, InitiativeCreatureData initiativeCreatureData)
    {
        this.InitiativeCreatureData = initiativeCreatureData;
        this.Creature = creature;
    }
}