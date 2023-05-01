using TacticalDecorations;

class PlacedDecoration
{
    internal Vec2 LowerLeftPosition;
    internal TacticalDecoration TacDec;

    public PlacedDecoration(Vec2 lowerLeftPosition, TacticalDecoration tacDec)
    {
        LowerLeftPosition = lowerLeftPosition;
        TacDec = tacDec;
    }
}

