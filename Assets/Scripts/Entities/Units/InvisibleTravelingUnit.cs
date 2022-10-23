using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

