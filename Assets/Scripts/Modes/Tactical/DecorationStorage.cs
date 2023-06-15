using OdinSerializer;
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

