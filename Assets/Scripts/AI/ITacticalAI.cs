public interface ITacticalAI
{
    bool RunAI();
    bool ForeignTurn { get; set; }
    TacticalAI.RetreatConditions RetreatPlan { get; set; }
}
