using OdinSerializer;

public class Weapon : Item
{

    [OdinSerialize]
    public int Graphic { get; private set; }
    [OdinSerialize]
    public int Damage { get; private set; }
    [OdinSerialize]
    public int Range { get; private set; }
    [OdinSerialize]
    public bool Omni { get; private set; }

    [OdinSerialize]
    public float AccuracyModifier { get; private set; }

    internal void ResetAccuracy()
    {
        AccuracyModifier = 1;
    }


    public Weapon(string name, string description, int cost, int graphic, int damage, int range, float accuracyModifier = 1f,  bool omniWeapon = false, bool lockedItem = false)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Graphic = graphic;
        Damage = damage;
        Range = range;
        AccuracyModifier = accuracyModifier;
        Omni = omniWeapon;
        LockedItem = lockedItem;
    }
}
