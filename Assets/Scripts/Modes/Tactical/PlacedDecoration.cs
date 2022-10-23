using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

