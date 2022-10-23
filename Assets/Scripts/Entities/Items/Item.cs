using OdinSerializer;

public abstract class Item
{
    [OdinSerialize]
    public string Name { get; protected set; }
    [OdinSerialize]
    public int Cost { get; protected set; }
    [OdinSerialize]
    public string Description { get; protected set; }
    [OdinSerialize]
    public bool LockedItem { get; protected set; }
}
