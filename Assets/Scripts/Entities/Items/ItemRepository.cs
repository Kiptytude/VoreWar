using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public enum ItemType
{
    Mace,
    Axe,
    Bow,
    CompoundBow,
    Helmet,
    BodyArmor,
    Gauntlet,
    Gloves,
    Shoes,

    FireBall,
    PowerBolt,
    LightningBolt,
    Shield,
    Mending,
    Speed,
    Valor,
    Predation,
    IceBlast,
    Pyre,
    //Warp
    //Magic Wall
    Poison,
    //Quicksand
    PreysCurse,
    Maw,
    Enlarge,
    Charm,
    Summon,
    //Raze,
    Diminishment,
    GateMaw,
    Resurrection,
}


public enum SpecialItems
{
    SeliciaWeapon,
    SuccubusWeapon,
    VisionWeapon,
    KiWeapon,
    ScorchWeapon,
    DRACOWeapon,
    ZoeyWeapon,
    ZeraWeapon,
    AbakWeapon,
    AurilikaWeapon,
    SalixWeapon,
    ErinWeapon,
    ErinWings,
}


public class ItemRepository
{
    [OdinSerialize]
    List<Item> items;
    [OdinSerialize]
    List<Item> specialItems;
    [OdinSerialize]
    List<Item> monsterItems;
    [OdinSerialize]
    public Weapon Claws;
    [OdinSerialize]
    public Weapon Bite;

    [OdinSerialize]
    List<Item> AllItems;

    public ItemRepository()
    {
        Claws = new Weapon("Claw", "innate claws", 0, 0, 2, 1);
        Bite = new Weapon("Bite", "bite attack", 0, 0, 4, 1);
        items = new List<Item>
        {
            new Weapon(name:"Mace",description:"Moderate melee weapon", cost:4, graphic:0, damage:4, range:1, accuracyModifier: 1.25f ),
            new Weapon(name:"Axe", description:"Strong melee weapon", cost:12, graphic:2, damage:8, range:1, accuracyModifier: 1),
            new Weapon(name:"Simple Bow", description:"Ranged weapon", cost:6, graphic:4, damage:4, range:5, accuracyModifier: 1.25f),
            new Weapon(name:"Compound Bow", description:"Advanced Ranged weapon", cost:12, graphic:6, damage:6, range:7, accuracyModifier: 1),
            new Accessory(name:"Helmet", description:"+8 agility", cost:6, changedStat:(int)Stat.Agility, statBonus:8 ),
            new Accessory(name:"Body Armor", description:"+6 endurance", cost:6, changedStat:(int)Stat.Endurance, statBonus:6 ),
            new Accessory(name:"Gauntlet", description:"+6 strength", cost:8, changedStat:(int)Stat.Strength, statBonus:6 ),
            new Accessory(name:"Gloves", description:"+6 dexterity", cost:10, changedStat:(int)Stat.Dexterity, statBonus:6 ),
            new Accessory(name:"Shoes", description:"+2 agility, +1 movement tile", cost:6, changedStat:(int)Stat.Agility, statBonus:2),


            new SpellBook("Fireball Book", "Allows the casting of Fireball", 30, 1, SpellTypes.Fireball),
            new SpellBook("Power Bolt Book", "Allows the casting of Power Bolt", 30, 1, SpellTypes.PowerBolt),
            new SpellBook("Lightning Bolt Book", "Allows the casting of Lightning Bolt", 30, 1, SpellTypes.LightningBolt),
            new SpellBook("Shield Book", "Allows the casting of Shield", 30, 1, SpellTypes.Shield),
            new SpellBook("Mending Book", "Allows the casting of Mending", 30, 1, SpellTypes.Mending),
            new SpellBook("Speed Book", "Allows the casting of Speed", 30, 1, SpellTypes.Speed),
            new SpellBook("Valor Book", "Allows the casting of Valor", 30, 1, SpellTypes.Valor),
            new SpellBook("Predation Book", "Allows the casting of Predation", 30, 1, SpellTypes.Predation),
            new SpellBook("Ice Blast Book", "Allows the casting of Ice Blast", 60, 2, SpellTypes.IceBlast),
            new SpellBook("Pyre Book", "Allows the casting of Pyre", 60, 2, SpellTypes.Pyre),
            //new SpellBook("Warp Book", "Allows the casting of Warp", 60, 2, SpellTypes.Warp),
            //new SpellBook("Magic Wall Book", "Allows the casting of Magic Wall", 60, 2, SpellTypes.MagicWall),           
            new SpellBook("Poison Book", "Allows the casting of Poison", 60, 2, SpellTypes.Poison),            
            //new SpellBook("Quicksand Book", "Allows the casting of Quicksand", 90, 3, SpellTypes.Quicksand),
            new SpellBook("Prey's Curse Book", "Allows the casting of Prey's Curse", 90, 3, SpellTypes.PreysCurse),
            new SpellBook("Maw Book", "Allows the casting of Maw", 90, 3, SpellTypes.Maw),
            new SpellBook("Enlarge Book", "Allows the casting of Enlarge", 90, 3, SpellTypes.Enlarge),
            new SpellBook("Charm Book", "Allows the casting of Charm", 90, 3, SpellTypes.Charm),
            new SpellBook("Polymorph Book", "Allows the casting of Polymorph", 90, 3, SpellTypes.Polymorph),
            new SpellBook("Summon Book", "Allows the casting of Summon", 90, 3, SpellTypes.Summon),
            //new SpellBook("Raze Book", "Allows the casting of Raze", 150, 4, SpellTypes.Raze),
            new SpellBook("Diminishment Book", "Allows the casting of Diminishment", 150, 4, SpellTypes.Diminishment),
            new SpellBook("Gatemaw Book", "Allows the casting of Gatemaw", 150, 4, SpellTypes.GateMaw),
            new SpellBook("Resurrection Book", "Allows the casting of Resurrection", 150, 4, SpellTypes.Resurrection),
            new SpellBook("Reanimate Book", "Allows the casting of Reanimate", 150, 4, SpellTypes.Reanimate),
        };
        monsterItems = new List<Item>()
        {
            new Weapon(name:"Vagrant Stinger", description:"Jellyfish stinger", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Serpent Fangs", description:"Fangs", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Wyvern Claws", description:"Claws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Young Wyvern Claws", description:"Claws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Puny Claws", description:"Puny Claws", cost:4, graphic:0, damage:2, range:1),
            new Weapon(name:"Shark Jaws", description:"Shark Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Wolf Claws", description:"Wolf Claws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Dark Swallower Jaws", description:"Dark Swallower Jaws", cost:4, graphic:0, damage:2, range:1),
            new Weapon(name:"Pointy Teeth", description:"Cake Jaws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Harvester Scythes", description:"Scythes", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Collector Maw", description:"Maw", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Voilin Jaws", description:"Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Bat Jaws", description:"Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Frog Tongue", description:"Tongue", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Dragon Claws", description:"Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Dragonfly Mandibles", description:"Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Plant Bite", description:"Bite", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Fairy Spark", description:"Magical Attack", cost:4, graphic:0, damage:5, range:5, omniWeapon: true, magicWeapon: true),
            new Weapon(name:"Ant Mandibles", description:"Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Gryphon Claws", description:"Gryphon Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Slug Slime", description:"Slug Slime", cost:4, graphic:0, damage:4, range:5, omniWeapon: true),
            new Weapon(name:"Slug Headbash", description:"Headbash", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Slug Body Slam", description:"Body Slam", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Slug Stinger", description:"Venomous Stinger", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Salamander Jaws", description:"Salamander Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Mantis Scythes", description:"Mantis Scythes", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Eastern Dragon Jaws", description:"Eastern Jaws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Catfish Jaws", description:"Catfish Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Raptor Jaws", description:"Raptor Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Warrior Ant Mandibles", description:"Warrior Ant Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Gazelle Headbash", description:"Gazelle Headbash", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Earthworm Maw", description:"Earthworm Maw", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Lizard Jaws", description:"Lizard Jaws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Monitor Lizard Claws", description:"Monitor Lizard Claws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Schiwardez Jaws", description:"Schiwardez Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Terrorbird Beak", description:"Terrorbird Beak", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Dratopyr Jaws", description:"Dratopyr Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Lion Fangs", description:"Serrated and pointy (Feline) Canines", cost:4, graphic:0, damage:6, range:1),
			new Weapon(name:"Goodra Slug Antenna", description:"Goodra's Power Whip", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Whisp fire", description:"Whisp's FoxFire", cost:4, graphic:0, damage:5, range:5, omniWeapon: true, magicWeapon: true),
            new Weapon(name:"Horse Hooves", description:"A terribly painful kick to the gut!", cost:4, graphic:0, damage:4, range:1),
        };

        specialItems = new List<Item>()
        {
            new Weapon(name:"Selicia's Bite", description:"Bite attack", cost:4, graphic:0, damage:10, range:1),
            new Weapon(name:"Summoned Sword", description:"Imp that drops a sword on target", cost:4, graphic:0, damage:4, range:3, omniWeapon:true, lockedItem:true),
            new Weapon(name:"Vision's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Ki's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Scorch's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"DRACO's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Zoey's Limbs", description:"Fist / Tail Attack", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Zera's Claws", description:"Claw Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Abakhanskya's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Aurilika's Gohei", description:"A sacred talisman", cost:4, graphic:0, damage:4, range:1, accuracyModifier: 5f, lockedItem:true),
            new Weapon(name:"Salix's Staff", description:"A weighty magic staff", cost:4, graphic:0, damage:6, range:1, lockedItem:true),
            new Weapon(name:"Nyangel Claws", description:"Near-useless cat scratch!", cost:4, graphic:0, damage:8, range:1),
            new Accessory(name:"Nyangel Wings", description:"The softest, most delicious looking pair of wings you ever did see!\n+5 Willpower", cost:6, changedStat:(int)Stat.Will, statBonus:5 ),
        };


        AllItems = new List<Item>();
        AllItems.AddRange(items);
        AllItems.AddRange(monsterItems);
        AllItems.AddRange(specialItems);


    }

    public int NumItems => items.Count;

    public Item GetItem(int i) => items[i];

    public Item GetMonsterItem(int i) => monsterItems[i];

    public Item GetSpecialItem(SpecialItems i) => specialItems[(int)i];

    public Item GetItem(ItemType i) => items[(int)i];

    public ItemType GetItemType(Item item)
    {
        return (ItemType)items.IndexOf(items.Where(s => s.Name == item.Name).FirstOrDefault());
    }

    public Item GetRandomBook(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        return GetItem(GetRandomBookType(minTier, maxTier, ignoreLimit));
    }

    public int GetRandomBookType(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        if (ignoreLimit == false)
            maxTier = UnityEngine.Mathf.Clamp(maxTier, 1, Config.MaxSpellLevelDrop);
        minTier = UnityEngine.Mathf.Clamp(minTier, 1, maxTier);
        int min = (int)ItemType.FireBall;
        int max = (int)ItemType.Resurrection;
        if (minTier == 1) min = (int)ItemType.FireBall;
        if (minTier == 2) min = (int)ItemType.IceBlast;
        if (minTier == 3) min = (int)ItemType.PreysCurse;
        if (minTier == 4) min = (int)ItemType.Diminishment;
        if (maxTier == 1) max = (int)ItemType.Predation;
        if (maxTier == 2) max = (int)ItemType.Poison;
        if (maxTier == 3) max = (int)ItemType.Summon;
        if (maxTier >= 4) max = (int)ItemType.Resurrection;

        return State.Rand.Next(min, max + 1);
    }

    internal bool ItemIsUnique(Item item)
    {
        return monsterItems.Contains(item) || specialItems.Contains(item);
    }

    public bool ItemIsRangedWeapon(int i)
    {
        if (items[i] is Weapon weapon)
        {
            if (weapon.Range > 1)
                return true;
        }
        return false;
    }

    public bool ItemIsRangedWeapon(Item item)
    {
        if (item is Weapon weapon)
        {
            if (weapon.Range > 1)
                return true;
        }
        return false;
    }

    public Item GetUpgrade(Item item)
    {
        if (item == GetItem(ItemType.Mace))
            return GetItem(ItemType.Axe);
        if (item == GetItem(ItemType.Bow))
            return GetItem(ItemType.CompoundBow);
        return null;
    }

    public Item GetNewItemType(Item item)
    {
        var ret = items.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null)
            ret = monsterItems.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null)
            ret = specialItems.Where(s => s.Name == item.Name).FirstOrDefault();
        if (item.Name.Contains("Frog ???"))
            ret = monsterItems.Where(s => s.Name.Contains("Frog Tongue")).FirstOrDefault();
        if (ret == null)
        {
            if (item is Weapon weap)
            {
                if (weap.AccuracyModifier == 0)
                    weap.ResetAccuracy();
            }
            return item;
        }

        return ret;
    }

    //public Weapon[] GetWeapons()
    //{
    //    return items.Append(Claws).Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    //public Weapon[] GetMonsterWeapons()
    //{
    //    return monsterItems.Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    public List<Item> GetAllItems()
    {
        return AllItems;
    }

}
