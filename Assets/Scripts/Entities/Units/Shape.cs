using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Shape
{
    public Unit Unit { get; set; }
    public Traits OriginTrait { get; private set; }

    Shape(Unit unit, Traits originTrait)
    {
        Unit = unit;
        OriginTrait = originTrait;
    }   
}

