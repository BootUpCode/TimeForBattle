
namespace TimeForBattle.Model;

public partial class InitiativeCreatureData : DatabaseObject
{
    [ObservableProperty] public int currentHitPoints;
    [ObservableProperty] public int? initiative;
    [ObservableProperty] public int? nameID;
    [ObservableProperty] public bool isTurn;
    [ObservableProperty] public bool isExpanded;
    [ObservableProperty] public string conditionString1;
    [ObservableProperty] public string conditionTimeString1;
    [ObservableProperty] public string conditionString2;
    [ObservableProperty] public string conditionTimeString2;
    [ObservableProperty] public string conditionString3;
    [ObservableProperty] public string conditionTimeString3;
    [ObservableProperty] public string conditionString4;
    [ObservableProperty] public string conditionTimeString4;
    [ObservableProperty] public string conditionString5;
    [ObservableProperty] public string conditionTimeString5;
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
        this.ConditionString1 = "";
        this.ConditionTimeString1 = "";
        this.ConditionString2 = "";
        this.ConditionTimeString2 = "";
        this.ConditionString3 = "";
        this.ConditionTimeString3 = "";
        this.ConditionString4 = "";
        this.ConditionTimeString4 = "";
        this.ConditionString5 = "";
        this.ConditionTimeString5 = "";
        this.ActiveTab = 0;
    }

    public InitiativeCreatureData() { }
}


