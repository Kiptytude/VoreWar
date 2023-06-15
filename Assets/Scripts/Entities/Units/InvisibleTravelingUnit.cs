using OdinSerializer;


class InvisibleTravelingUnit
{
    [OdinSerialize]
    internal Unit unit;

    [OdinSerialize]
    internal int remainingTurns;

    public InvisibleTravelingUnit(Unit unit, int remainingTurns)
    {
        this.unit = unit;
        this.remainingTurns = remainingTurns;
    }
}

