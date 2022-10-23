internal class StrategicMoveUndo
{

    internal Army Army;
    internal int MP;
    internal Vec2i PreviousPosition;

    public StrategicMoveUndo(Army army, int mP, Vec2i previousPosition)
    {
        Army = army;
        MP = mP;
        PreviousPosition = previousPosition;
    }

    internal void Undo()
    {
        State.GameManager.StrategyMode.Translator.ClearTranslator();
        State.GameManager.StrategyMode.QueuedPath = null;
        Army.SetPosition(PreviousPosition);
        Army.RemainingMP = MP;
        State.GameManager.StrategyMode.Regenerate();
        
        
    }
}