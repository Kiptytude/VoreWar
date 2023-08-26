using OdinSerializer;

public class Accessory : Item
{
    [OdinSerialize]
    public int ChangedStat { get; private set; }
    [OdinSerialize]
    public int StatBonus { get; private set; }

    public Accessory(string name, string description, int cost, int changedStat, int statBonus)
    {
        Name = name;
        Cost = cost;
        Description = description;
        ChangedStat = changedStat;
        StatBonus = statBonus;
        LockedItem = LockedItem;
    }
}
