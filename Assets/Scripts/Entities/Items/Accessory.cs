using OdinSerializer;

public class Accessory : Item
{
    [OdinSerialize]
    public int ChangedStat { get; private set; }
    [OdinSerialize]
    public int StatBonus { get; private set; }
    [OdinSerialize]
    public int ChangedStat2 { get; private set; }
    [OdinSerialize]
    public int Stat2Bonus { get; private set; }

    public Accessory(string name, string description, int cost, int changedStat, int statBonus, bool lockedItem = false)
    {
        Name = name;
        Cost = cost;
        Description = description;
        ChangedStat = changedStat;
        StatBonus = statBonus;
        LockedItem = lockedItem;
    }
}
