using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GoldMine : ClaimableBuilding
{

    public GoldMine(Vec2i location) : base(location)
    {
    }

    internal override void TurnChanged()
    {

    }
}

