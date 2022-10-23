using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MonsterSpawnerLocation
{
    internal Vec2i Location;

    internal Race Race;

    public MonsterSpawnerLocation(Vec2i location, Race race)
    {
        Location = location;
        Race = race;
    }
}

