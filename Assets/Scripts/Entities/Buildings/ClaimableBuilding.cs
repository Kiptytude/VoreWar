using OdinSerializer;


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

