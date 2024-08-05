internal class StrategicMoveUndo
{

    internal Army Army;
    internal int MP;
    internal int SCD;
    internal Vec2i PreviousPosition;

    public StrategicMoveUndo(Army army, int mP, int scd, Vec2i previousPosition)
    {
        Army = army;
        MP = mP;
        SCD = scd;
        PreviousPosition = previousPosition;
    }

    internal void Undo()
    {
        State.GameManager.StrategyMode.Translator.ClearTranslator();
        State.GameManager.StrategyMode.QueuedPath = null;
        Army.SetPosition(PreviousPosition);
        Army.RemainingMP = MP;
        Army.SCooldown = SCD;
        State.GameManager.StrategyMode.Regenerate();


    }
}