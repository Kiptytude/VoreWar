using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class ClaimableBuilding
{
    [OdinSerialize]
    internal Empire Owner;

    [OdinSerialize]
    internal Vec2i Position;

    protected ClaimableBuilding(Vec2i location)
    {
        Position = location;
    }

    internal abstract void TurnChanged();


}

