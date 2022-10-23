using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacticalDecorations;

class DecorationStorage
{
    [OdinSerialize]
    internal Vec2 Position;
    [OdinSerialize]
    internal TacDecType Type;

    public DecorationStorage(Vec2 position, TacDecType type)
    {
        Position = position;
        Type = type;
    }
}

