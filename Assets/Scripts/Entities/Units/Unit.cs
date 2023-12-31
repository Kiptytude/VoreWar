using OdinSerializer;
using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UnitType
{
    Soldier,
    Leader,
    Mercenary,
    Summon,
    SpecialMercenary,
    Adventurer,
    Spawn,
}

public enum AIClass
{
    Default,
    Melee,
    MeleeVore,
    Ranged,
    RangedVore,
    PureVore,
    MagicMelee,
    MagicRanged,
    Custom
}

public class Unit
{

    [OdinSerialize]
    public int Side;
    [OdinSerialize]
    internal Unit _controller = null;

    internal Unit Controller
    {
        get
        {
            if (_controller != null)
                if (_controller.Controller != null)
                    return _controller.Controller;
            return _controller;
        }
        set
        {
            if (value == null)
                _controller = value;
            else if ((_controller == null) || (_controller.GetStat(Stat.Mind) < value.GetStat(Stat.Mind)))
                if (value == this || value.Controller == this)
                    return;
                else
                    _controller = value;
        }
    }
    [OdinSerialize]
    private int _fixedSide = -1;

    internal bool HasFixedSide() => _fixedSide != -1;
    public int FixedSide
    {
        get
        {
            if (Controller != null)
                return _controller.FixedSide;
            return (_fixedSide == -1) ? Side : _fixedSide;
        }
        set => _fixedSide = value;
    }
    [OdinSerialize]
    public bool hiddenFixedSide = false;

    public static List<Traits> secretTags = new List<Traits>() { Traits.Infiltrator, Traits.Corruption, Traits.Parasite, Traits.Metamorphosis,
        Traits.Possession, Traits.Changeling, Traits.Reincarnation, Traits.InfiniteReincarnation, Traits.Transmigration, Traits.InfiniteTransmigration,
        Traits.Untamable, Traits.GreaterChangeling, Traits.SpiritPossession, Traits.ForcedMetamorphosis, Traits.Shapeshifter, Traits.Skinwalker};

    [OdinSerialize]
    public Race Race;
    [OdinSerialize]
    public int Health;
    [OdinSerialize]
    protected int[] Stats;
    [OdinSerialize]
    protected float experience;
    [OdinSerialize]
    protected int level;
    [OdinSerialize]
    private double _baseScale = 1;
    internal double BaseScale
    {
        get
        {
            if (_baseScale < 1 || HasTrait(Traits.Growth) == false)
                return 1;
            return _baseScale;
        }
        set => _baseScale = value;
    }
    [OdinSerialize]
    public float ExpMultiplier { get; protected set; } = 1;

    [OdinSerialize]
    internal int Mana { get; private set; }

    internal int MaxMana => (int)(GetStatBase(Stat.Mind) + GetStatBase(Stat.Will) * 2 * TraitBoosts.ManaMultiplier);

    private int _maxHealth = 99999;

    public int MaxHealth
    {
        get // Ah yes, a simple getter function uwu. Keeps health percentage consistent after gaining/losing stats mid battle, doesn't break Thrillseeker, doesn't cause prey orphans, doesn't break on save/load... etc. (But apparently it's still buggy)
        {
            if (Stats == null) return 1;
            if (!Config.StatBoostsAffectMaxHP)
            {
                _maxHealth = Stats[(int)Stat.Endurance] * 2 + Stats[(int)Stat.Strength];
                return _maxHealth;
            }

            int oldMax = _maxHealth;
            _maxHealth = GetStat(Stat.Endurance) * 2 + GetStat(Stat.Strength);
            if (oldMax > 1 && oldMax != _maxHealth && _healthPct > 0)
            {
                int healthChange = (int)Math.Round((_maxHealth - oldMax) * _healthPct);
                if (healthChange > 0)
                    Health = Math.Min(_maxHealth, Math.Max(1, Health + healthChange));
            }
            return _maxHealth;
        }
        set => _maxHealth = value;
    }
    private int GetHealthBoosts()
    {
        throw new NotImplementedException();
    }

    [OdinSerialize]
    internal AIClass AIClass;
    [OdinSerialize]
    internal StatWeights StatWeights;
    [OdinSerialize]
    internal bool AutoLeveling;

    #region Customizations
    [OdinSerialize]
    public int HairColor;
    [OdinSerialize]
    public int HairStyle;
    [OdinSerialize]
    public int BeardStyle;
    [OdinSerialize]
    public int SkinColor;
    [OdinSerialize]
    public int AccessoryColor;
    [OdinSerialize]
    public int EyeColor;
    [OdinSerialize]
    public int ExtraColor1;
    [OdinSerialize]
    public int ExtraColor2;
    [OdinSerialize]
    public int ExtraColor3;
    [OdinSerialize]
    public int ExtraColor4;
    [OdinSerialize]
    public int EyeType;
    [OdinSerialize]
    public int MouthType;
    [OdinSerialize]
    public int BreastSize;
    [OdinSerialize]
    public int DickSize;
    [OdinSerialize]
    public bool HasVagina;
    [OdinSerialize]
    internal int BodySize;
    [OdinSerialize]
    internal int SpecialAccessoryType;
    [OdinSerialize]
    internal bool BodySizeManuallyChanged;
    [OdinSerialize]
    internal int DefaultBreastSize;
    [OdinSerialize]
    internal int ClothingType;
    [OdinSerialize]
    internal int ClothingType2;
    [OdinSerialize]
    internal int ClothingHatType;
    [OdinSerialize]
    internal int ClothingAccessoryType;
    [OdinSerialize]
    internal int ClothingExtraType1;
    [OdinSerialize]
    internal int ClothingExtraType2;
    [OdinSerialize]
    internal int ClothingExtraType3;
    [OdinSerialize]
    internal int ClothingExtraType4;
    [OdinSerialize]
    internal int ClothingExtraType5;
    [OdinSerialize]
    internal int ClothingColor;
    [OdinSerialize]
    internal int ClothingColor2;
    [OdinSerialize]
    internal int ClothingColor3;
    [OdinSerialize]
    internal bool Furry;
    [OdinSerialize]
    public int HeadType;
    [OdinSerialize]
    public int TailType;
    [OdinSerialize]
    public int FurType;
    [OdinSerialize]
    public int EarType;
    [OdinSerialize]
    public int BodyAccentType1;
    [OdinSerialize]
    public int BodyAccentType2;
    [OdinSerialize]
    public int BodyAccentType3;
    [OdinSerialize]
    public int BodyAccentType4;
    [OdinSerialize]
    public int BodyAccentType5;
    [OdinSerialize]
    public int BallsSize;
    [OdinSerialize]
    public int VulvaType;
    [OdinSerialize]
    public int BasicMeleeWeaponType;
    [OdinSerialize]
    public int AdvancedMeleeWeaponType;
    [OdinSerialize]
    public int BasicRangedWeaponType;
    [OdinSerialize]
    public int AdvancedRangedWeaponType;
    #endregion

    [OdinSerialize]
    internal int DigestedUnits;
    [OdinSerialize]
    internal int KilledUnits;

    [OdinSerialize]
    internal int TimesKilled;


    [OdinSerialize]
    public Item[] Items;
    [OdinSerialize]
    public string Name { get; set; }
    [OdinSerialize]
    public List<string> Pronouns;

    [OdinSerialize]
    public Action OnDiscard;

    public string GetPronoun(int num)
    {
        if (Pronouns == null)
            GeneratePronouns();
        return Pronouns[num];
    }
    [OdinSerialize]
    public UnitType Type;
    [OdinSerialize]
    public bool Predator;
    [OdinSerialize]
    public bool ImmuneToDefections;
    [OdinSerialize]
    public bool FixedGear;

    [OdinSerialize]
    public EnumIndexedArray<SingleUnitContext, Unit> RelatedUnits = new EnumIndexedArray<SingleUnitContext, Unit>();


    [OdinSerialize]
    internal VoreType PreferredVoreType;

    [OdinSerialize]
    internal Unit SavedCopy;
    [OdinSerialize]
    internal Village SavedVillage;

    [OdinSerialize]
    private List<StatusEffect> _statusEffects;

    [OdinSerialize]
    public bool EarnedMask = false;

    [OdinSerialize]
    public List<Unit> ShifterShapes;

    public override string ToString() => Name;

    /// <summary>
    /// Unit was manually changed to/from pred so it should not be overwritten
    /// </summary>
    [OdinSerialize]
    public bool fixedPredator;

    internal List<StatusEffect> StatusEffects
    {
        get
        {
            if (_statusEffects == null)
                _statusEffects = new List<StatusEffect>();
            return _statusEffects;
        }
        set => _statusEffects = value;
    }

    [OdinSerialize]
    public List<SpellTypes> InnateSpells;

    private List<Spell> _useableSpells;

    [OdinSerialize]
    internal List<SpellTypes> SingleUseSpells = new List<SpellTypes>();

    [OdinSerialize]
    internal List<SpellTypes> MultiUseSpells = new List<SpellTypes>();  // This is so much more straightforward than adding Special Actions

    public Unit HiddenUnit
    {
        get
        {
            return (RelatedUnits[SingleUnitContext.HiddenUnit] == null) ? this : RelatedUnits[SingleUnitContext.HiddenUnit];
        }
    }

    public Race HiddenRace
    {
        get
        {
            return (RelatedUnits[SingleUnitContext.HiddenUnit] == null) ? Race : RelatedUnits[SingleUnitContext.HiddenUnit].Race;
        }
    }

    public int[] HiddenStats
    {
        get
        {
            return (RelatedUnits[SingleUnitContext.HiddenUnit] == null) ? Stats : RelatedUnits[SingleUnitContext.HiddenUnit].Stats;
        }
    }

    [OdinSerialize]
    private Race _spawnRace;
    public Race SpawnRace
    {
        get
        {
            return (_spawnRace == Race.none) ? Race : _spawnRace;
        }
        set => _spawnRace = value;
    }
    [OdinSerialize]
    private Race _conversionRace;
    public Race ConversionRace
    {
        get
        {
            return (_conversionRace == Race.none) ? Race : _conversionRace;
        }
        set => _conversionRace = value;
    }

    internal List<Spell> UseableSpells
    {
        get
        {
            if (_useableSpells == null)
            {
                _useableSpells = new List<Spell>();
            }
            return _useableSpells;
        }
        set => _useableSpells = value;
    }

    internal bool HasDick => DickSize > -1;
    internal bool HasBreasts => DefaultBreastSize > -1;

    public bool IsInfiltratingSide(int side)
    {
        return side == Side && Side != FixedSide && hiddenFixedSide;
    }

    internal Gender GetGender()
    {
        if (HasBreasts && HasDick && (HasVagina || Config.HermsCanUB == false))
            return Gender.Hermaphrodite;
        else if (HasBreasts && HasDick && !HasVagina)
            return Gender.Gynomorph;
        else if (HasBreasts && !HasDick && HasVagina)
            return Gender.Female;
        else if (HasBreasts && !HasDick && !HasVagina)
            return Gender.Agenic;
        else if (!HasBreasts && HasDick && HasVagina)
            return Gender.Maleherm;
        else if (!HasBreasts && HasDick && !HasVagina)
            return Gender.Male;
        else if (!HasBreasts && !HasDick && HasVagina)
            return Gender.Andromorph;
        return Gender.None;
    }

    internal bool CanBeConverted()
    {
        return Type != UnitType.Summon && Type != UnitType.Leader && Type != UnitType.SpecialMercenary && HasTrait(Traits.Eternal) == false && SavedCopy == null && Level > 0;
    }

    internal bool CanUnbirth => Config.Unbirth && HasVagina;
    internal bool CanCockVore => Config.CockVore && HasDick;
    internal bool CanBreastVore => Config.BreastVore && HasBreasts;
    internal bool CanAnalVore => Config.AnalVore;
    internal bool CanTailVore => Config.TailVore;

    public bool CanVore(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.womb:
                return CanUnbirth;
            case PreyLocation.balls:
                return CanCockVore;
            case PreyLocation.breasts:
                return CanBreastVore;
            case PreyLocation.anal:
                return CanAnalVore;
            case PreyLocation.tail:
                return CanTailVore;
            default:
                return true;
        }
    }

    internal bool HasWeapon
    {
        get
        {
            if (Items == null)
                return false;
            if (HasTrait(Traits.Feral))
                return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is Weapon)
                    return true;
            }
            return false;
        }
    }

    internal bool HasSpecificWeapon(params ItemType[] types)
    {
        if (Items == null)
            return false;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                continue;
            if (types.Contains(State.World.ItemRepository.GetItemType(Items[i])))
                return true;
        }
        return false;
    }

    internal bool HasBook
    {
        get
        {
            if (Items == null)
                return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is SpellBook)
                    return true;
            }
            return false;
        }
    }

    public bool BestSuitedForRanged() => Stats[(int)Stat.Dexterity] * TraitBoosts.VirtualDexMult > Stats[(int)Stat.Strength] * TraitBoosts.VirtualStrMult;

    public void SetLevel(int level) => this.level = level;

    internal bool SpendMana(int amount)
    {
        if (Mana >= amount)
        {
            Mana -= amount;
            return true;
        }
        return false;
    }

    internal void RestoreManaPct(float pct)
    {
        Mana += (int)(MaxMana * pct);
        if (Mana > MaxMana)
            Mana = MaxMana;
    }

    internal void RestoreMana(int amt)
    {
        Mana += amt;
        if (Mana > MaxMana)
            Mana = MaxMana;
    }


    internal int NearbyFriendlies = 0;
    internal bool Harassed = false;

    public bool IsDead => (Health < 1);
    private PermanentBoosts _traitBoosts;
    internal PermanentBoosts TraitBoosts
    {
        get
        {
            if (_traitBoosts == null)
                InitializeTraits();
            return _traitBoosts;
        }
        set => _traitBoosts = value;
    }

    [OdinSerialize]
    protected List<Traits> Tags; //For some reason, renaming this to anything else results in an infinite loop in serialization, so it is staying tags for now

    [OdinSerialize]
    protected List<Traits> TemporaryTraits;
    [OdinSerialize]
    protected List<Traits> SharedTraits;
    [OdinSerialize]
    protected List<Traits> PersistentSharedTraits;

    /// <summary>
    /// Traits that are considered to be permanent, i.e. do not disappear during refreshes
    /// </summary>
    [OdinSerialize]
    protected List<Traits> PermanentTraits;

    /// <summary>
    /// Traits that are explicitly removed, done so that they aren't added back in at some point by version changes or the like.
    /// </summary>
    [OdinSerialize]
    protected List<Traits> RemovedTraits;

    //internal List<Trait> TraitsList = new List<Trait>();
    internal List<IStatBoost> StatBoosts;
    internal List<IAttackStatusEffect> AttackStatusEffects;
    internal List<IVoreAttackOdds> VoreAttackOdds;
    internal List<IVoreDefenseOdds> VoreDefenseOdds;
    internal List<IPhysicalDefenseOdds> PhysicalDefenseOdds;

    [OdinSerialize]
    internal int EnemiesKilledThisBattle;

    internal Unit CurrentLeader;


    /// <summary>
    /// Creates an empty unit for various purposes
    /// </summary>
    public Unit(Race race)
    {
        Race = race;
        Stats = new int[(int)Stat.None];
        Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
        Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
        Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
        Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        Health = MaxHealth;
        Mana = MaxMana;
    }

    public Unit(int side, Race race, int startingXP, bool predator, UnitType type = UnitType.Soldier, bool immuneToDefectons = false)
    {
        Stats = new int[(int)Stat.None];
        Race = race;
        Side = side;
        experience = startingXP;
        level = 1;
        Tags = new List<Traits>();
        PermanentTraits = new List<Traits>();
        RemovedTraits = new List<Traits>();
        Type = type;

        Predator = predator;
        if (predator == false)
            fixedPredator = true;
        ImmuneToDefections = immuneToDefectons;


        var raceData = Races.GetRace(this);
        RandomizeNameAndGender(race, raceData, true);

        DefaultBreastSize = BreastSize;
        Items = new Item[Config.ItemSlots];


        ReloadTraits();
        raceData.RandomCustom(this);
        InitializeTraits();
        RefreshSecrecy();
        InitializeFixedSide(side);

        if (HasTrait(Traits.Resourceful))
        {
            Items = new Item[3];
        }

        SetGear(race);

        InnateSpells = new List<SpellTypes>();
        RelatedUnits = new EnumIndexedArray<SingleUnitContext, Unit>();
        ShifterShapes = new List<Unit>();

        if (race == Race.Dragon)
        {
            int rand = State.Rand.Next(3);
            if (rand == 0) InnateSpells.Add(SpellTypes.IceBlast);
            if (rand == 1) InnateSpells.Add(SpellTypes.Pyre);
            if (rand == 2) InnateSpells.Add(SpellTypes.LightningBolt);
        }
        if (race == Race.Fairies)
        {
            FairyUtil.SetSeason(this, FairyUtil.GetSeason(this)); //To establish the spell properly
        }

        RandomSkills();
        Health = MaxHealth;
        Mana = MaxMana;

        if (UniformDataStorer.GetUniformOdds(race) >= State.Rand.NextDouble())
        {
            var available = UniformDataStorer.GetCompatibleCustomizations(this);
            if (available != null && available.Any())
            {
                UniformDataStorer.ExternalCopyToUnit(available[State.Rand.Next(available.Count)], this);
            }
        }
        _spawnRace = Race.none;
        _conversionRace = Race.none;

        SetForcedPermanentTraits();
        ReincarnateCheck();
    }

    private void SetForcedPermanentTraits()
    {
        if (Tags.Contains(Traits.InfiniteAssimilation))
        {
            RemoveTrait(Traits.InfiniteAssimilation);
            AddPermanentTrait(Traits.InfiniteAssimilation);
        }
        if (Tags.Contains(Traits.InfiniteReincarnation))
        {
            RemoveTrait(Traits.InfiniteReincarnation);
            AddPermanentTrait(Traits.InfiniteReincarnation);
        }
        if (Tags.Contains(Traits.InfiniteTransmigration))
        {
            RemoveTrait(Traits.InfiniteTransmigration);
            AddPermanentTrait(Traits.InfiniteTransmigration);
        }
        if (Tags.Contains(Traits.Shapeshifter))
        {
            RemoveTrait(Traits.Shapeshifter);
            AddPermanentTrait(Traits.Shapeshifter);
        }
        if (Tags.Contains(Traits.Skinwalker))
        {
            RemoveTrait(Traits.Skinwalker);
            AddPermanentTrait(Traits.Skinwalker);
        }
        if (Tags.Contains(Traits.Extraction))
        {
            RemoveTrait(Traits.Extraction);
            AddPermanentTrait(Traits.Extraction);
        }
    }

    private void ReincarnateCheck()
    {
        if (State.World != null && State.World.Reincarnators != null && State.World.Reincarnators?.Count > 0 && Type != UnitType.SpecialMercenary && Type != UnitType.Summon)
        {
            if (State.World.Reincarnators.Where(r => r.Race == Race).Count() > 0 && State.Rand.Next(3) == 0)
                Reincarnate(State.World.Reincarnators.Where(r => r.Race == Race).First());
        }
    }

    private void Reincarnate(Reincarnator reinc)
    {
        Unit pastLife = reinc.PastLife;
        Name = pastLife.Name;
        experience = pastLife.Experience;
        foreach (Traits trait in pastLife.PermanentTraits)
        {
            AddPermanentTrait(trait);
        }
        InnateSpells.AddRange(pastLife.InnateSpells);
        ShifterShapes = pastLife.ShifterShapes;
        if (ShifterShapes == null)
            ShifterShapes = new List<Unit>();
        ShifterShapes.Add(pastLife);
        FixedSide = pastLife.FixedSide;
        hiddenFixedSide = true;
        SavedCopy = pastLife.SavedCopy;
        SavedVillage = pastLife.SavedVillage;
        Race race = reinc.Race;
        OnDiscard = () =>
        {
            Race targetRace = race;
            if (!reinc.RaceLocked)
            {
                List<Race> activeRaces = StrategicUtilities.GetAllUnits(false).ConvertAll(u => u.Race)
                .Distinct().ToList();
                if (activeRaces.Any())
                {
                    targetRace = activeRaces[State.Rand.Next(activeRaces.Count)];
                }
            }
            State.World.Reincarnators?.Add(new Reincarnator(pastLife, targetRace, reinc.RaceLocked));
            Debug.Log(pastLife.Name + " is re-reincarnating");
        };
        State.World.Reincarnators?.Remove(reinc);
        Debug.Log(pastLife.Name + " reincarnated as a " + Race);
        StrategicUtilities.SpendLevelUps(this);
    }

    internal void SetGear(Race race, bool skipTraitItems = false)
    {
        if (race >= Race.Vagrants && race < Race.Selicia)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetMonsterItem((int)Race - 100);
        }
        else if (race == Race.Selicia)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SeliciaWeapon);
        }
        else if (race == Race.Vision)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.VisionWeapon);
        }
        else if (race == Race.Ki)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.KiWeapon);
        }
        else if (race == Race.Scorch)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ScorchWeapon);
        }
        else if (race == Race.Succubi)
        {
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SuccubusWeapon);
        }
        else if (race == Race.Asura)
        {
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
        }
        else if (race == Race.DRACO)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.DRACOWeapon);
        }
        else if (race == Race.Zoey)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZoeyWeapon);
        }
        else if (race == Race.Abakhanskya)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AbakWeapon);
        }
        else if (race == Race.Zera)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZeraWeapon);
        }
        else if (race == Race.Auri)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AurilikaWeapon);
        }
        else if (race == Race.Salix)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SalixWeapon);
        }
        else if (race == Race.Erin)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWeapon);
            Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWings);
        }
        else
        {
            FixedGear = false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null && State.World.ItemRepository.ItemIsUnique(Items[i]))
                    Items[i] = null;
            }
            if (RaceParameters.GetRaceTraits(race).CanUseRangedWeapons == false)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null && State.World.ItemRepository.ItemIsRangedWeapon(Items[i]))
                    {
                        if (Items[i] is Weapon weapon)
                        {
                            if (weapon.Damage > 4)
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Axe);
                            else
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Mace);
                        }
                    }
                }
            }
            if (!skipTraitItems)
                GiveTraitBooks();
        }
    }

    internal void GiveTraitBooks(int specificTier = 0)
    {
        if (State.World?.ItemRepository == null) return;
        var tiers = new List<int>();
        if (specificTier == 1 || (specificTier == 0 && HasTrait(Traits.BookWormI)))
        {
            tiers.Add(1);
        }
        if (specificTier == 2 || (specificTier == 0 && HasTrait(Traits.BookWormII)))
        {
            tiers.Add(2);
        }
        if (specificTier == 3 || (specificTier == 0 && HasTrait(Traits.BookWormIII)))
        {
            tiers.Add(3);
        }
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null && Items[i] is SpellBook)
            {
                if (((SpellBook)Items[i]).Tier == 4)
                    tiers.Remove(3);
                else
                    tiers.Remove(((SpellBook)Items[i]).Tier);
            }
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (tiers.Count > 0)
            {
                if (Items[i] == null)
                {
                    int t = tiers.Count > 1 ? tiers[State.Rand.Next(tiers.Count - 1)] : tiers[0];
                    Items[i] = State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true);
                    tiers.Remove(t);
                }
            }
        }
        if (tiers.Count > 0 && HasTrait(Traits.BookEater))
        {
            foreach (int t in tiers)
            {
                SpellTypes spell = ((SpellBook)State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true)).ContainedSpell;
                if (!InnateSpells.Contains(spell))
                    InnateSpells.Add(spell);
            }
        }
    }

    internal Unit Clone()
    {
        var clone = (Unit)MemberwiseClone();
        clone.Stats = new int[(int)Stat.None];
        for (int i = 0; i < Stats.Length; i++)
        {
            clone.Stats[i] = Stats[i];
        }
        //clone.Items = (Item[])Items.Clone();
        //for (int i = 0; i < Items.Length; i++)
        //{
        //    clone.SetItem(null, i);
        //}
        return clone;
    }

    internal void CopyAppearance(Unit appearance)
    {
        this.HairColor = appearance.HairColor;
        this.HairStyle = appearance.HairStyle;
        this.BeardStyle = appearance.HairStyle;
        this.SkinColor = appearance.SkinColor;
        this.AccessoryColor = appearance.AccessoryColor;
        this.EyeColor = appearance.EyeColor;
        this.ExtraColor1 = appearance.ExtraColor1;
        this.ExtraColor2 = appearance.ExtraColor2;
        this.ExtraColor3 = appearance.ExtraColor3;
        this.ExtraColor4 = appearance.ExtraColor4;
        this.EyeType = appearance.EyeType;
        this.MouthType = appearance.MouthType;
        this.BreastSize = appearance.BreastSize;
        this.DickSize = appearance.DickSize;
        this.HasVagina = appearance.HasVagina;
        this.BodySize = appearance.BodySize;
        this.SpecialAccessoryType = appearance.SpecialAccessoryType;
        this.BodySizeManuallyChanged = appearance.BodySizeManuallyChanged;
        this.DefaultBreastSize = appearance.DefaultBreastSize;
        this.ClothingType = appearance.ClothingType;
        this.ClothingType2 = appearance.ClothingType2;
        this.ClothingHatType = appearance.ClothingHatType;
        this.ClothingAccessoryType = appearance.ClothingAccessoryType;
        this.ClothingExtraType1 = appearance.ClothingExtraType1;
        this.ClothingExtraType2 = appearance.ClothingExtraType2;
        this.ClothingExtraType3 = appearance.ClothingExtraType3;
        this.ClothingExtraType4 = appearance.ClothingExtraType4;
        this.ClothingExtraType5 = appearance.ClothingExtraType5;
        this.ClothingColor = appearance.ClothingColor;
        this.ClothingColor2 = appearance.ClothingColor2;
        this.ClothingColor3 = appearance.ClothingColor3;
        this.Furry = appearance.Furry;
        this.HeadType = appearance.HeadType;
        this.TailType = appearance.TailType;
        this.FurType = appearance.FurType;
        this.EarType = appearance.EarType;
        this.BodyAccentType1 = appearance.BodyAccentType1;
        this.BodyAccentType2 = appearance.BodyAccentType2;
        this.BodyAccentType3 = appearance.BodyAccentType3;
        this.BodyAccentType4 = appearance.BodyAccentType4;
        this.BodyAccentType5 = appearance.BodyAccentType5;
        this.BallsSize = appearance.BallsSize;
        this.VulvaType = appearance.VulvaType;
        this.BasicMeleeWeaponType = appearance.BasicMeleeWeaponType;
        this.AdvancedMeleeWeaponType = appearance.AdvancedMeleeWeaponType;
        this.BasicRangedWeaponType = appearance.BasicRangedWeaponType;
        this.AdvancedRangedWeaponType = appearance.AdvancedRangedWeaponType;
    }

    internal void SetGenderRandomizeName(Race race, Gender gender)
    {
        var raceData = Races.GetRace(this);
        var isMale = false;
        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (gender == Gender.Hermaphrodite && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((gender == Gender.Male && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            isMale = false;
        }

        if (race >= Race.Vagrants)
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }



        ReloadTraits();
        raceData.RandomCustom(this);
        InitializeTraits();
    }

    internal void TotalRandomizeAppearance()
    {
        var raceData = Races.GetRace(this);
        RandomizeNameAndGender(Race, raceData, true);
        raceData.RandomCustom(this);

    }

    internal void RandomizeAppearance()
    {
        var raceData = Races.GetRace(this);
        raceData.RandomCustom(this);
    }

    internal void RandomizeNameAndGender(Race race, DefaultRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        bool isMale = false;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.maleFraction;
            hermFraction = raceStats.hermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = Config.HermsCanUB;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
            isMale = false;
        }

        if (race >= Race.Vagrants)
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }

    }

    internal void GeneratePronouns()
    {
        if (GetGender() == Gender.Female)
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        else if (GetGender() == Gender.Male)
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        else
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
    }

    internal void RandomizeGender(Race race, DefaultRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.maleFraction;
            hermFraction = raceStats.hermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = true;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }

    }

    public void GiveExp(float exp, bool voreSource = false, bool isKill = false)
    {
        exp *= TraitBoosts.ExpGain;

        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }

        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        experience += exp;
    }

    public void GiveRawExp(int exp) => experience += exp;

    public bool IsDeadAndOverkilledBy(int overkill)
    {
        return Health < (1 - overkill);
    }

    public bool IsThisCloseToDeath(int lessThanThisDamageAwayFromDeath)
    {
        return Health < (1 + lessThanThisDamageAwayFromDeath) && !IsDead;
    }


    public void Kill()
    {
        TimesKilled++;
        if (SavedCopy != null)
            SavedCopy.TimesKilled++;
    }

    public void DrainExp(float exp)
    {
        experience -= exp;
    }

    public void GiveScaledExp(float exp, int attackerLevelAdvantage, bool voreSource = false, bool isKill = false)
    {
        if (Config.FlatExperience)
        {
            GiveExp(exp, voreSource, isKill);
            return;
        }
        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }
        exp *= TraitBoosts.ExpGain;
        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        if (attackerLevelAdvantage > 0)
            exp = Math.Max(exp * (1 - ((float)Math.Pow(attackerLevelAdvantage, 1.2) / 24f)), .3f * exp);
        else if (attackerLevelAdvantage < 0)
        {
            exp = Math.Min(exp * (1 + ((float)Math.Pow(-attackerLevelAdvantage, 1.2) / 12f)), 6f * exp);
        }

        experience += exp;
    }

    public static int GetExperienceRequiredForLevel(int level, float expRequiredMod)
    {
        if (level >= Config.HardLevelCap && Config.HardLevelCap > 0)
            return 99999999;
        return (int)(expRequiredMod * level * Config.ExperiencePerLevel + ((level >= Config.SoftLevelCap ? 8 : 0) + (level * Config.AdditionalExperiencePerLevel * (level - 1) / 2)) *
            (level >= Config.SoftLevelCap ? (int)Math.Pow(2, level + 1 - Config.SoftLevelCap) : 1));
    }

    public static int GetLevelFromExperience(int experience)
    {
        for (int i = 0; i < 200; i++)
        {
            if (GetExperienceRequiredForLevel(i, 1) < experience)
                return i;
        }
        return 200;
    }

    public float Experience => experience;
    public int Level => level;
    public int ExperienceRequiredForNextLevel => GetExperienceRequiredForLevel(level);
    public int GetExperienceRequiredForLevel(int lvl) => GetExperienceRequiredForLevel(lvl, ExpRequiredMod);
    float ExpRequiredMod => TraitBoosts.ExpRequired;
    public bool HasEnoughExpToLevelUp() => Experience >= ExperienceRequiredForNextLevel;

    private float _healthPct = 100f;
    public float HealthPct
    {
        get
        {
            _healthPct = (float)Health / MaxHealth;
            return _healthPct;
        }
    }

    internal float GetHealthPctWithoutUpdating() // Important for calculating stat boosts that depend on health percentages, otherwise it's circular.
    {
        return _healthPct;
    }
    internal void ClearAllTraits()
    {
        Tags = new List<Traits>();
        InitializeTraits();
    }

    internal List<Traits> GetTraits
    {
        get
        {
            if (PermanentTraits == null)
                return Tags.ToList();
            if (SharedTraits == null)
                return Tags.Concat(PermanentTraits).ToList();
            return Tags.Concat(PermanentTraits).ToList().Concat(SharedTraits).ToList();
        }
    }

    internal int BaseTraitsCount => Tags.Count + PermanentTraits.Count;

    public int GetStatBase(Stat stat) => Stats[(int)stat];
    public void SetStatBase(Stat stat, int value) => Stats[(int)stat] = value;
    public void SetStatBaseAll(int value)
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = value;
        }
    }
    public int GetLeaderBonus()
    {
        if (CurrentLeader == null)
            return 0;
        if (CurrentLeader.IsDead)
            return 0;
        return CurrentLeader.Stats[(int)Stat.Leadership] / 10;
    }
    public int GetTraitBonus(Stat stat)
    {
        if (StatBoosts == null)
            InitializeTraits();
        int bonus = 0;

        if (Harassed)
            bonus -= (int)(GetStatBase(stat) * 0.08f);

        foreach (IStatBoost trait in StatBoosts)
        {
            bonus += trait.StatBoost(this, stat);
        }

        return bonus;
    }
    public int GetEffectBonus(Stat stat)
    {
        float bonus = 0;
        if (stat == Stat.Voracity)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += (GetStatBase(Stat.Voracity) * .25f);
            }
        }
        else if (stat == Stat.Stomach)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += (effect.Strength);
            }
        }

        // todo: store the original duration of the effect, maybe?
        // can have it wear off over time, no matter the total duration
        if (GetStatusEffect(StatusEffectType.Empowered) != null)
        {
            StatusEffect eff = GetStatusEffect(StatusEffectType.Empowered);
            bonus += GetStatBase(stat) * eff.Strength * eff.Duration / 5;
        }

        if (GetStatusEffect(StatusEffectType.Berserk) != null)
        {
            if (stat == Stat.Voracity)
            {
                bonus += GetStatBase(Stat.Voracity);
            }
            if (stat == Stat.Strength)
            {
                bonus += GetStatBase(Stat.Strength);
            }
        }

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.BladeDance) != null)
            bonus += 2 * GetStatusEffect(StatusEffectType.BladeDance).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.BladeDance) != null)
            bonus += 1 * GetStatusEffect(StatusEffectType.BladeDance).Duration;

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Strength) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Agility) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Voracity && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Voracity) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.Focus) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.Focus).Duration;
            bonus += stacks + (GetStatBase(Stat.Mind) * (stacks / 100));
        }
        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.SpellForce) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.SpellForce).Duration;
            bonus += stacks + (GetStatBase(Stat.Mind) * (stacks / 10));
        }

        bonus -= GetStatBase(stat) * (GetStatusEffect(StatusEffectType.Shaken)?.Strength ?? 0);

        if (GetStatusEffect(StatusEffectType.Webbed) != null)
            bonus -= GetStatBase(stat) * .3f;


        return Mathf.RoundToInt(bonus);

    }
    public int GetStat(Stat stat)
    {
        float total = GetStatBase(stat) + GetLeaderBonus() + GetTraitBonus(stat) + GetEffectBonus(stat);

        total *= GetScale();

        total *= TraitBoosts.StatMult;

        if (total < 1)
            return 1;

        return Mathf.RoundToInt(total);
    }

    public float GetScale(int power = 1)
    {
        float scale = (float)BaseScale;

        scale *= 1.0f + (GetStatusEffect(StatusEffectType.Enlarged)?.Strength ?? 0f);
        scale *= 1.0f - (GetStatusEffect(StatusEffectType.Diminished)?.Strength ?? 0f);

        scale *= TraitBoosts.Scale;

        return Mathf.Pow(scale, power);
    }

    public string GetStatInfo(Stat stat)
    {
        int modStat = GetStat(stat);
        int baseStat = GetStatBase(stat);
        if (modStat > baseStat)
        {
            if (Config.HideBaseStats)
                return $"<color=#007000ff>{modStat}</color>";
            return $"<color=#007000ff>{modStat}</color> ({baseStat})";
        }
        else if (modStat < baseStat)
        {
            if (Config.HideBaseStats)
                return $"<color=red>{modStat}</color>";
            return $"<color=red>{modStat}</color> ({baseStat})";
        }
        else
            return $"{modStat}";
    }

    public void SetExp(float exp) => experience = exp;

    internal void ModifyStat(int stat, int amount) => ModifyStat((Stat)stat, amount);

    internal void ModifyStat(Stat stat, int amount)
    {
        Stats[(int)stat] += amount;
        if (stat == Stat.Endurance)
            Health += 2 * amount;
        if (stat == Stat.Strength)
            Health += 1 * amount;

    }

    internal void InitializeTraits()
    {
        //foreach (IAttackStatusEffect trait in Unit.TraitsList.Where(s => s is IAttackStatusEffect))
        TraitBoosts = new PermanentBoosts();
        //TraitsList = new List<Trait>();
        StatBoosts = new List<IStatBoost>();
        VoreAttackOdds = new List<IVoreAttackOdds>();
        VoreDefenseOdds = new List<IVoreDefenseOdds>();
        AttackStatusEffects = new List<IAttackStatusEffect>();
        PhysicalDefenseOdds = new List<IPhysicalDefenseOdds>();

        RecalculateStatBoosts();
    }

    internal void RefreshSecrecy()
    {
        if (HasTrait(Traits.Infiltrator) || HasTrait(Traits.Corruption))
            hiddenFixedSide = true;
    }
    internal void InitializeFixedSide(int side)
    {
        if (State.World?.ItemRepository == null) return; //protection for the create strat screen
        if (_fixedSide > -1) return;
        if (HasTrait(Traits.Untamable))
        {
            FixedSide = State.World.GetEmpireOfRace(Race)?.Side ?? side;
            return;
        }
        if (HasTrait(Traits.Infiltrator) || HasTrait(Traits.Corruption))
        {
            FixedSide = side;
            return;
        }
        FixedSide = -1;
    }

    public bool HasTrait(Traits tag)
    {
        if (tag == Traits.TheGreatEscape && Race == Race.Erin)
            return true;
        if (Tags != null)
            return Tags.Contains(tag) || (PermanentTraits?.Contains(tag) ?? false);
        return false;
    }

    public void HealPercentageCap(float rate, int cap)
    {
        int heal = (int)(MaxHealth * rate);
        if (heal <= 0) { heal = 1; }
        heal = Math.Min(heal, cap);
        if (heal > MaxHealth - Health)
            heal = MaxHealth - Health;
        var actor = TacticalUtilities.Units.FirstOrDefault(s => s.Unit == this);
        if (actor != null && heal != 0)
            actor.UnitSprite.DisplayDamage(-heal);
        Health += heal;

    }

    public void HealPercentage(float rate)
    {
        rate *= TraitBoosts.PassiveHeal;
        int h = (int)(MaxHealth * rate);
        if (h <= 0) { h = 1; }
        Health += h;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public int Heal(int amount)
    {
        int diff = MaxHealth - Health;
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        int actualHeal = Math.Min(diff, amount);
        State.GameManager.TacticalMode?.TacticalStats?.RegisterHealing(actualHeal, Side);
        return actualHeal;
    }

    void NonFatalDamage(int amount, string type)
    {
        if (Health <= 0)
            return;
        int actual = Math.Min(Health - 1, amount);
        Health -= actual;
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Name}</b> took <color=red>{actual}</color> points of {type} damage");

    }

    internal string ListTraits(bool hideSecret = false)
    {
        if (Tags.Count == 0 && (PermanentTraits == null || PermanentTraits.Count == 0))
            return "";
        string ret = "";
        for (int i = 0; i < Tags.Count; i++)
        {
            if (!(hideSecret && secretTags.Contains(Tags[i])))
            {
                bool color = false;
                string tagString = "";
                if (ret != "")
                    ret += "\n";
                if (TemporaryTraits != null && TemporaryTraits.Count > 0 && TemporaryTraits.Contains(Tags[i]))
                {
                    if (PermanentTraits != null && PermanentTraits.Count > 0 && !PermanentTraits.Contains(Tags[i]))
                        color = true;
                }
                if (State.RandomizeLists.Any(rl => (Traits)rl.id == Tags[i]))
                    tagString = State.RandomizeLists.Where(rl => (Traits)rl.id == Tags[i]).FirstOrDefault().name;
                else
                    tagString = Tags[i].ToString();

                ret += color ? "<color=#402B8Dff>" + tagString + "</color>" : tagString;
            }
        }
        if (PermanentTraits != null && PermanentTraits.Count > 0)
        {
            for (int i = 0; i < PermanentTraits.Count; i++)
            {
                if (Tags.Contains(PermanentTraits[i]))
                    continue;
                if (!(hideSecret && secretTags.Contains(PermanentTraits[i])))
                {
                    if (ret != "")
                        ret += "\n";
                    if (State.RandomizeLists.Any(rl => (Traits)rl.id == PermanentTraits[i]))
                        ret += State.RandomizeLists.Where(rl => (Traits)rl.id == PermanentTraits[i]).FirstOrDefault().name;
                    else
                        ret += PermanentTraits[i].ToString();
                    }
            }
        }
        return ret;
    }

    public bool IsEnemyOfSide(int side)
    {
        return (Side != side);
    }

    public void AddTraits(List<Traits> traitIdsToAdd)
    {
        foreach (var traitId in traitIdsToAdd)
        {
            AddTrait(traitId);
        }

        return;
    }

    public void AddTrait(Traits traitIdToAdd)
    {
        if (Tags == null)
            Tags = new List<Traits>();

        if (HasTrait(traitIdToAdd))
            return;

        Tags.Add(traitIdToAdd);
        RecalculateStatBoosts();
    }

    public bool AddPermanentTrait(Traits traitIdToAdd)
    {
        if (PermanentTraits == null)
            PermanentTraits = new List<Traits>();

        if (traitIdToAdd == Traits.Prey)
        {
            Predator = false;
            fixedPredator = true;
        }

        if (HasTrait(traitIdToAdd))
            return false;

        PermanentTraits.Add(traitIdToAdd);
        RecalculateStatBoosts();
        return true;
    }

    public void RemoveTrait(Traits traitToRemove)
    {
        if (Tags == null)
            return;

        if (RemovedTraits == null)
            RemovedTraits = new List<Traits>();

        if (traitToRemove == Traits.Prey)
        {
            if (RaceParameters.GetTraitData(this).AllowedVoreTypes.Any())
            {
                Predator = true;
                fixedPredator = true;
            }
            else //Cancel removal in this case
                return;

        }

        RemovedTraits.Add(traitToRemove);

        if (HasTrait(traitToRemove))
        {
            Tags.Remove(traitToRemove);
            PermanentTraits?.Remove(traitToRemove);
            RecalculateStatBoosts();
        }

    }

    protected void RecalculateStatBoosts()
    {
        float healthBefore = HealthPct;
        RefreshSecrecy();
        InitializeFixedSide(Side);
        if (Tags == null)
            return;

        TraitBoosts = new PermanentBoosts();
        StatBoosts.Clear();
        VoreAttackOdds.Clear();
        VoreDefenseOdds.Clear();
        PhysicalDefenseOdds.Clear();
        AttackStatusEffects.Clear();
        if (PermanentTraits == null)
        {
            foreach (var trait in Tags)
            {
                Trait ITrait = TraitList.GetTrait(trait);
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }

        if (PermanentTraits != null)
        {
            foreach (var trait in Tags.Concat(PermanentTraits).Distinct())
            {
                Trait ITrait = TraitList.GetTrait(trait);
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }

        if (SharedTraits != null)
        {
            foreach (var trait in SharedTraits)
            {
                Trait ITrait = TraitList.GetTrait(trait);
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }
    }



    internal void SetMaxItems()
    {

        if (HasTrait(Traits.Resourceful) == false)
        {
            if (Items.Count() == 3)
                SetItem(null, 2);
            if (Items.Count() == 2)
                return;
            if (Items.Count() == 0)
            {
                Items = new Item[2];
                return;
            }
            Item[] tempItems = new Item[]
            {
                    Items[0],
                    Items[1],
            };
            Items = tempItems;
        }
        else
        {
            if (Items.Count() == 3)
                return;
            if (Items.Count() == 2)
            {
                Item[] tempItems = new Item[]
                {
                    Items[0],
                    Items[1],
                    null
                };
                Items = tempItems;
            }
            else
                Items = new Item[3];
        }

    }

    internal void AddTemporaryTrait(Traits trait)
    {
        if (TemporaryTraits == null)
            TemporaryTraits = new List<Traits>();
        if (TemporaryTraits.Contains(trait) == false)
            TemporaryTraits.Add(trait);
        if (TemporaryTraits.Count >= 4)
            TemporaryTraits.RemoveAt(0);
    }

    public bool HasSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        return (SharedTraits.Contains(trait));
    }

    public void AddSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        if (!SharedTraits.Contains(trait) && !HasTrait(trait))
            SharedTraits.Add(trait);
        AddTrait(trait);

    }

    public void ResetSharedTraits()
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        foreach (Traits trait in SharedTraits)
        {
            RemoveTrait(trait);
        }
        SharedTraits.Clear();
    }

    public void RemoveSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        if (SharedTraits.Contains(trait) && HasTrait(trait))
            SharedTraits.Remove(trait);
        RemoveTrait(trait);
    }

    public bool HasPersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        return (PersistentSharedTraits.Contains(trait));
    }

    public void AddPersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        if (!PersistentSharedTraits.Contains(trait) && !HasTrait(trait))
            PersistentSharedTraits.Add(trait);
        AddTrait(trait);

    }

    public void ResetPersistentSharedTraits()
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        foreach (Traits trait in PersistentSharedTraits)
        {
            RemoveTrait(trait);
        }
        PersistentSharedTraits.Clear();
    }

    public void RemovePersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        if (PersistentSharedTraits.Contains(trait) && HasTrait(trait))
            PersistentSharedTraits.Remove(trait);
        RemoveTrait(trait);
    }

    internal void ReloadTraits()
    {
        Tags = new List<Traits>();
        if (Config.RaceTraitsEnabled)
            Tags.AddRange(State.RaceSettings.GetRaceTraits(HiddenUnit.Race));
        if (HiddenUnit.HasBreasts && HiddenUnit.HasDick == false)
        {
            var femaleTraits = State.RaceSettings.GetFemaleRaceTraits(HiddenUnit.Race);
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
            femaleTraits = Config.FemaleTraits;
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
        }
        else if (!HiddenUnit.HasBreasts && HiddenUnit.HasDick)
        {
            var maleTraits = State.RaceSettings.GetMaleRaceTraits(HiddenUnit.Race);
            if (maleTraits != null) Tags.AddRange(maleTraits);
            maleTraits = Config.MaleTraits;
            if (maleTraits != null) Tags.AddRange(maleTraits);
        }
        else
        {
            var hermTraits = State.RaceSettings.GetHermRaceTraits(HiddenUnit.Race);
            if (hermTraits != null) Tags.AddRange(hermTraits);
            hermTraits = Config.HermTraits;
            if (hermTraits != null) Tags.AddRange(hermTraits);
        }
        if (Type == UnitType.Leader)
        {
            var leaderTraits = State.RaceSettings.GetLeaderRaceTraits(HiddenUnit.Race);
            if (leaderTraits != null) Tags.AddRange(leaderTraits);
            if (Config.LeaderTraits != null) Tags.AddRange(Config.LeaderTraits);
        }
        else if (Type == UnitType.Spawn)
        {
            var spawnTraits = State.RaceSettings.GetSpawnRaceTraits(HiddenUnit.Race);
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
            spawnTraits = Config.SpawnTraits;
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
        }
        if (TemporaryTraits != null)
            Tags.AddRange(TemporaryTraits);
        if (SharedTraits != null)
            Tags.AddRange(SharedTraits);
        if (PersistentSharedTraits != null)
            Tags.AddRange(PersistentSharedTraits);
        if (RemovedTraits != null)
        {
            foreach (Traits trait in RemovedTraits)
            {
                Tags.Remove(trait);
            }
        }
        if (!State.TutorialMode)
            RandomizeTraits();
        Tags = Tags.Distinct().ToList();
        if (HasTrait(Traits.Prey))
            Predator = false;
        else if (fixedPredator == false)
            Predator = State.World?.GetEmpireOfRace(HiddenUnit.Race)?.CanVore ?? true;
        Tags.RemoveAll(s => s == Traits.Prey);
        if (RaceParameters.GetTraitData(HiddenUnit).AllowedVoreTypes.Any() == false)
            Predator = false;
        if (HiddenUnit.Predator == false && !HasTrait(Traits.Prey))
            Tags.Add(Traits.Prey);
        SetMaxItems();
        if (HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker))
        {
            if (ShifterShapes == null)
                ShifterShapes = new List<Unit>();
        }
    }

    public void ChangeRace(Race race)
    {
        Race = race;
        fixedPredator = false;
    }

    public void HideRace(Race race, Unit appearance = null)
    {
        RelatedUnits[SingleUnitContext.HiddenUnit] = Clone();
        Race = race;
        fixedPredator = false;
        if (appearance != null)
            CopyAppearance(appearance);
        else
        {
            var NewRace = Races.GetRace(race);
            NewRace.RandomCustom(this);
        }

    }

    public void UnhideRace()
    {
        Race = HiddenUnit.Race;
        fixedPredator = false;
        CopyAppearance(HiddenUnit);
        RelatedUnits[SingleUnitContext.HiddenUnit] = null;
    }

    private void RandomizeTraits(bool isLevelingUp = false)
    {
        while (true)
        {
            var customs = Tags.Where(t => State.RandomizeLists.Any(rl => (Traits)rl.id == t && rl.level <= level)).ToList();
            customs.AddRange(PermanentTraits.Where(t => State.RandomizeLists.Any(rl => (Traits)rl.id == t && rl.level <= level)));
            if (!customs.Any())
                break;
            customs.ForEach(ct =>
            {
                    RandomizeList randomizeList = State.RandomizeLists.Single(rl => (Traits)rl.id == ct);
                    var chance = randomizeList.chance;
                    while (chance > 0 && State.Rand.NextDouble() < randomizeList.chance)
                    {
                        List<Traits> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
                        if (gainable.Count() > 0)
                        {
                            var randomPick = gainable[State.Rand.Next(gainable.Count())];
                            RemovedTraits?.Remove(randomPick); // Even if manually removed before, rng-sus' word is law
                            AddPermanentTrait(randomPick);
                            gainable.Remove(randomPick);
                            GivePrerequisiteTraits(randomPick);
                            if (isLevelingUp) 
                                { 
                                    if (randomPick == Traits.BookWormI)
                                        GiveTraitBooks(1);
                                    else
                                    if (randomPick == Traits.BookWormII)
                                        GiveTraitBooks(2);
                                    else
                                    if (randomPick == Traits.BookWormIII)
                                        GiveTraitBooks(3);
                                }
                        }
                        chance -= 1;
                    }
                    if (RemovedTraits == null)
                        RemovedTraits = new List<Traits>();
                    RemovedTraits.Add(ct);
                    foreach (Traits trait in RemovedTraits)
                    {
                        Tags.Remove(trait);
                        PermanentTraits.Remove(trait);
                    }
                
            });
        }

    }

    private void GivePrerequisiteTraits(Traits randomPick)
    {
        Traits prereq = (Traits)(-1);
        if (randomPick > Traits.Growth && randomPick <= Traits.FleetingGrowth)
        {
            prereq = Traits.Growth;
        }
        if (randomPick == Traits.HealingBelly)
        {
            prereq = Traits.Endosoma;
        }
        if (randomPick == Traits.VenomousBite)
        {
            prereq = Traits.Biter;
        }
        if (randomPick == Traits.SynchronizedEvolution)
        {
            prereq = Traits.Assimilate;
        }
        if (randomPick == Traits.PredConverter || randomPick == Traits.PredRebirther || randomPick == Traits.PredGusher)
        {
            if (RaceParameters.GetRaceTraits(Race).AllowedVoreTypes.Contains(VoreType.Unbirth))
                HasVagina = true;
        }
        if (randomPick == Traits.HeavyPounce)
        {
            prereq = Traits.Pounce;
        }
        if (prereq != (Traits)(-1) && !Tags.Contains(prereq) && !PermanentTraits.Contains(prereq))
        {
            PermanentTraits.Add(prereq);
            RemovedTraits?.Remove(prereq);
        }
    }

    public void SetSizeToDefault() => BreastSize = DefaultBreastSize;

    internal void SetDefaultBreastSize(int size, bool update = true)
    {
        DefaultBreastSize = size;
        if (update) BreastSize = DefaultBreastSize;
    }

    public void SetBreastSize(int size)
    {
        if (size > Races.GetRace(this).BreastSizes - 1)
            size = Races.GetRace(this).BreastSizes - 1;
        if (size <= DefaultBreastSize)
            size = DefaultBreastSize;
        BreastSize = size;
    }

    public Stat[] GetLevelUpPossibilities(bool canVore)
    {
        if (Type == UnitType.Leader)
            return Leader.GetLevelUpPossibilities();
        int[] stats = new int[(int)Stat.None - 1];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = i;
        }



        if (canVore == false)
        {
            stats[(int)Stat.Voracity] = -1;
            stats[(int)Stat.Stomach] = -1;
        }
        //stats[(int)Stat.Leadership] = -1; unneeded as the stats already cuts it
        if (TraitBoosts.OnLevelUpAllowAnyStat)
        {
            Stat[] ret2 = new Stat[stats.Length];
            for (int i = 0; i < ret2.Length; i++)
            {
                ret2[i] = (Stat)i;
            }
            return ret2;
        }

        var traits = RaceParameters.GetTraitData(this);
        var favored = State.RaceSettings.GetFavoredStat(Race);

        if (favored != Stat.None)
            stats[(int)favored] = -1;

        stats = stats.Where(s => s >= 0).ToArray();

        for (int i = 0; i < stats.GetUpperBound(0); i++) //Randomize the order
        {
            int j = State.Rand.Next(i, stats.GetUpperBound(0) + 1);
            int temp = stats[i];
            stats[i] = stats[j];
            stats[j] = temp;
        }
        if (favored == Stat.None) //If no favored stat
        {
            Stat[] ret3 = new Stat[3];

            for (int i = 0; i < 3; i++)
            {
                ret3[i] = (Stat)stats[i];
            }
            return ret3;
        }
        Stat[] ret = new Stat[4];

        ret[0] = favored;
        for (int i = 1; i < 4; i++)
        {
            ret[i] = (Stat)stats[i];
        }
        return ret;

    }

    public void GeneralStatIncrease(int amount)
    {
        for (int x = 0; x < Stats.Length; x++)
        {
            if (Stats[x] > 0)
                Stats[x] += amount;
        }
        Health += 3 * amount;
    }

    public void LevelUp(Stat stat)
    {
        GeneralStatIncrease(1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(TraitBoosts.OnLevelUpBonusToAllStats);
        }

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }
        if (Config.LeadersAutoGainLeadership)
        {
            ModifyStat((int)Stat.Leadership, 2);
        }
        level++;
        Stats[(int)stat] += 4;
        if (stat == Stat.Endurance)
        {
            Health += 8;
        }
        if (stat == Stat.Strength)
        {
            Health += 4;
        }
        RandomizeTraits(true);
    }

    public void LeaderLevelDown()
    {
        SubtractLevels(Config.LeaderLossLevels);
        SubtractFraction(Config.LeaderLossExpPct);
        if (Config.WeightGain)
        {
            if (DefaultBreastSize > 0)
                DefaultBreastSize -= 1;
            if (DickSize > 0)
                DickSize -= 1;
            if (BodySize > 0)
                BodySize -= 1;
            if (BodySize > 0)
                BodySize -= 1;
        }
    }

    /// <summary>
    /// Subtracts this fraction from the total unit experience, leveling down as needed (i.e. .25 would cause a unit to lose 25% of its total XP)
    /// </summary>
    internal void SubtractFraction(float fraction)
    {
        if (fraction > 1 || fraction < 0)
        {
            UnityEngine.Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }
        if (fraction == 0)
            return;
        int startingLevel = level;
        if (Config.LeaderLossExpPct > 0)
        {
            experience *= (1 - fraction);

            for (int i = 0; i < startingLevel - 1; i++)
            {
                if (experience < GetExperienceRequiredForLevel(level - 1))
                {
                    LevelDown();
                }
            }

        }
    }

    /// <summary>
    /// Subtracts this many levels from the unit while keeping its % of xp to the next level constant
    /// </summary>
    internal void SubtractLevels(int levelsLost)
    {
        if (levelsLost < 0)
        {
            UnityEngine.Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }
        if (levelsLost == 0)
            return;
        if (levelsLost >= 1 && level > 1)
        {
            for (int i = 0; i < levelsLost; i++)
            {
                experience -= GetExperienceRequiredForLevel(level) - GetExperienceRequiredForLevel(level - 1);
                LevelDown();
                if (level <= 1)
                    break;
            }
            if (experience < 0)
                experience = 0;
        }
    }

    public virtual int GetStatTotal()
    {
        return GetStat(Stat.Agility) + GetStat(Stat.Will) + GetStat(Stat.Mind)
            + GetStat(Stat.Dexterity) + GetStat(Stat.Endurance) + GetStat(Stat.Strength)
            + GetStat(Stat.Voracity) + GetStat(Stat.Stomach);
    }

    public void LevelDown()
    {
        if (level <= 1)
            return;
        int highestType = 0;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] > Stats[highestType])
                highestType = i;
        }
        LevelDown((Stat)highestType);
    }

    public void LevelDown(Stat stat)
    {
        if (level <= 1)
            return;
        GeneralStatIncrease(-1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(-1 * TraitBoosts.OnLevelUpBonusToAllStats);
        }

        level--;
        int loweredStat = (int)stat;

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, -1 * TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }

        Stats[loweredStat] -= 4;
        if (loweredStat == (int)Stat.Endurance)
        {
            Health -= 8;
        }

        if (loweredStat == (int)Stat.Strength)
        {
            Health -= 4;
        }


    }

    public void Feed()
    {
        GiveExp(1, true);
        Health += 10;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    void RandomSkills()
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);
        if (raceStats != null)
        {
            Stats[(int)Stat.Strength] = raceStats.Strength.Minimum + State.Rand.Next(raceStats.Strength.Roll);
            Stats[(int)Stat.Dexterity] = raceStats.Dexterity.Minimum + State.Rand.Next(raceStats.Dexterity.Roll);
            Stats[(int)Stat.Endurance] = raceStats.Endurance.Minimum + State.Rand.Next(raceStats.Endurance.Roll);
            Stats[(int)Stat.Mind] = raceStats.Mind.Minimum + State.Rand.Next(raceStats.Mind.Roll);
            Stats[(int)Stat.Will] = raceStats.Will.Minimum + State.Rand.Next(raceStats.Will.Roll);
            Stats[(int)Stat.Agility] = raceStats.Agility.Minimum + State.Rand.Next(raceStats.Agility.Roll);
            Stats[(int)Stat.Voracity] = raceStats.Voracity.Minimum + State.Rand.Next(raceStats.Voracity.Roll);
            Stats[(int)Stat.Stomach] = raceStats.Stomach.Minimum + State.Rand.Next(raceStats.Stomach.Roll);
        }
        else
        {
            //These should not trigger under almost all circumstances, it uses the data at the bottom of RaceParameters now
            Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
            Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
            Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
            Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        }


    }

    public Weapon GetBestMelee()
    {
        if (HasTrait(Traits.Feral))
        {
            return State.World.ItemRepository.Claws;
        }
        Weapon best = null;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range <= 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Damage > best.Damage)
                        best = weapon;
                }
            }
        }

        if (best == null)
        {
            best = State.World.ItemRepository.Claws;
        }
        return best;
    }

    public Weapon GetBestRanged()
    {
        if (HasTrait(Traits.Feral))
        {
            return null;
        }
        Weapon best = null;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range > 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Range > best.Range)
                        best = weapon;
                }
            }
        }
        return best;
    }

    public Item GetItem(int i) => Items[i];
    public int GetItemSlot(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == item)
                return i;
        }
        return -1;
    }

    public bool HasFreeItemSlot()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return true;
        }
        return false;
    }

    public void UpdateItems(ItemRepository NewRepository)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
                SetItem(NewRepository.GetNewItemType(Items[i]), i);
        }
    }

    void AddAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] += accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance)
            Health += 2 * accessory.StatBonus;
    }

    void RemoveAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] -= accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance)
        {
            Health -= 2 * accessory.StatBonus;
            if (Health < 1)
                Health = 1;
        }
    }

    public void SetItem(Item item, int i, bool fromUnitEditor = false)
    {
        if (item == null && (ShifterShapes?.Any() ?? false))
        {
            ShifterShapes.Where(shape => shape.GetItem(i) == GetItem(i)).ForEach(s => // test what happens if main has resourceful but shape has not
            {
                s.Items[i] = null;
            });
        }
        if (Items.Length <= i)
        {
            UnityEngine.Debug.LogWarning("Tried to assign item to a non-existent slot!");
            return;
        }
        if (item is SpellBook && HasTrait(Traits.BookEater) && !fromUnitEditor && !InnateSpells.Contains(((SpellBook)item).ContainedSpell))
        {
            InnateSpells.Add(((SpellBook)item).ContainedSpell);
            return;
        }
        if (Items[i] != null)
        {
            if (Items[i] is Accessory)
            {
                RemoveAccessory((Accessory)Items[i]);
            }
        }
        Items[i] = item;
        if (Items[i] != null)
        {
            if (item is Accessory)
            {
                AddAccessory((Accessory)item);
            }
        }
    }

    public void UpdateSpells()
    {
        UseableSpells = new List<Spell>();
        if (InnateSpells != null)
        {
            foreach (SpellTypes type in InnateSpells)
            {
                if (SpellList.SpellDict.TryGetValue(type, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        if (SingleUseSpells?.Any() ?? false)
        {
            foreach (var spellType in SingleUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }

        }

        if (MultiUseSpells?.Any() ?? false)
        {
            foreach (var spellType in MultiUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }

        }

        var racePar = RaceParameters.GetTraitData(this);

        if (racePar.InnateSpells?.Any() ?? false)
        {
            foreach (SpellTypes type in racePar.InnateSpells)
            {
                var thisType = type;
                //if (thisType > SpellTypes.Resurrection)
                //    thisType = thisType - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;
                if (SpellList.SpellDict.TryGetValue(thisType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        var grantedSpell = State.RaceSettings.GetInnateSpell(Race);
        if (grantedSpell != SpellTypes.None)
        {
            if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
            {
                UseableSpells.Add(spell);
            }
        }

        foreach (Item item in Items)
        {
            if (item == null)
                continue;
            if (item is SpellBook book)
            {
                if (SpellList.SpellDict.TryGetValue(book.ContainedSpell, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }

            }
        }
    }

    public void ApplyStatusEffect(StatusEffectType type, float strength, int duration)
    {
        if (type == StatusEffectType.Poisoned && HasTrait(Traits.PoisonSpit))
            return;
        RemoveStatus(GetStatusEffect(type));
        StatusEffect status = new StatusEffect(type, strength, duration);
        StatusEffects.Add(status);
        status.OnApply(this);
    }

    public void RemoveStatus(StatusEffect statusEffect)
    {
        if (statusEffect != null)
        {
            statusEffect.OnExpire(this);
            StatusEffects.Remove(statusEffect);
        }
    }

    internal StatusEffect GetStatusEffect(StatusEffectType type)
    {
        if (type == StatusEffectType.WillingPrey && HasTrait(Traits.WillingRace))
            return new StatusEffect(StatusEffectType.WillingPrey, 0, 100);
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Strength).ThenByDescending(s => s.Duration).FirstOrDefault();
    }

    internal int GetNegativeStatusEffects()
    {
        int ret = 0;
        if (HasEffect(StatusEffectType.Clumsiness)) ret++;
        if (HasEffect(StatusEffectType.Diminished)) ret++;
        if (HasEffect(StatusEffectType.Glued)) ret++;
        if (HasEffect(StatusEffectType.Poisoned)) ret++;
        if (HasEffect(StatusEffectType.Shaken)) ret++;
        if (HasEffect(StatusEffectType.Webbed)) ret++;
        if (HasEffect(StatusEffectType.WillingPrey)) ret++;
        if (HasEffect(StatusEffectType.Charmed)) ret++;
        if (HasEffect(StatusEffectType.Hypnotized)) ret++;
        if (HasEffect(StatusEffectType.Sleeping)) ret++;
        if (HasEffect(StatusEffectType.Staggering)) ret++;
        if (HasEffect(StatusEffectType.Virus)) ret++;

        bool HasEffect(StatusEffectType type)
        {
            return GetStatusEffect(type) != null;
        }
        return ret;
    }

    public int GetApparentSide(Unit viewer = null)
    {
        if (viewer != null && TacticalUtilities.UnitCanSeeTrueSideOfTarget(viewer, this))
            return FixedSide;
        return hiddenFixedSide ? Side : FixedSide;
    }

    public Unit CreateRaceShape(Race race)
    {
        var shape = new Unit(Side, race, (int)Experience, true, Type, ImmuneToDefections);
        foreach (Traits trait in PermanentTraits)
        {
            shape.AddPermanentTrait(trait);
        }
        if (Races.GetRace(shape.Race).CanBeGender.Contains(GetGender()))
        {
            shape.SetGenderRandomizeName(race, GetGender());
        }
        shape.Name = Name;
        shape.InnateSpells.AddRange(InnateSpells);
        hiddenFixedSide = false;
        shape._fixedSide = _fixedSide;
        shape.SavedCopy = SavedCopy;
        shape.SavedVillage = SavedVillage;
        shape.RelatedUnits = RelatedUnits;
        shape.ShifterShapes = ShifterShapes;
        return shape;
    }

    internal void AddBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            dance.Duration++;
            dance.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.BladeDance, 1, 1);
        }

    }

    internal void RemoveBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            dance.Duration--;
            dance.Strength--;
            if (dance.Duration == 0)
                RemoveStatus(dance);
        }

    }

    internal void AddTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            ten.Duration++;
            ten.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Tenacious, 1, 1);
        }

    }

    internal void RemoveTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            ten.Duration -= 5;
            ten.Strength -= 5;
            if (ten.Duration <= 0)
                RemoveStatus(ten);
        }

    }

    internal void AddSpellForce()
    {
        var force = GetStatusEffect(StatusEffectType.SpellForce);
        if (force != null)
        {
            force.Duration++;
            force.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.SpellForce, 1, 1);
        }

    }

    internal void AddFocus(int amount)
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            foc.Duration += amount;
            foc.Strength += amount;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Focus, amount, amount);
        }

    }

    internal void RemoveFocus()
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            foc.Duration -= 3;
            foc.Strength -= 3;
            if (foc.Duration == 0)
                RemoveStatus(foc);
        }

    }

    internal void AddStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration++;
            stag.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.SpellForce, 1, 1);
        }

    }

    internal void RemoveStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration--;
            stag.Strength--;
            if (stag.Duration == 0)
                RemoveStatus(stag);
        }
    }

    internal StatusEffect GetLongestStatusEffect(StatusEffectType type)
    {
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Duration).FirstOrDefault();
    }

    internal void TickStatusEffects()
    {
        var effect = GetStatusEffect(StatusEffectType.Mending);
        if (effect != null)
            HealPercentageCap(.2f, (int)(2 + effect.Strength / 4));
        effect = GetStatusEffect(StatusEffectType.Poisoned);
        if (effect != null)
            NonFatalDamage((int)effect.Strength, "poison");
        effect = GetStatusEffect(StatusEffectType.Virus);
        if (effect != null)
            NonFatalDamage((int)effect.Strength, "virus");
        foreach (var eff in StatusEffects.ToList())
        {
            if (eff.Type == StatusEffectType.BladeDance || eff.Type == StatusEffectType.Tenacious || eff.Type == StatusEffectType.Focus)
                continue;
            var actor = TacticalUtilities.Units.Where(s => s.Unit == this).FirstOrDefault();
            var pred = actor.SelfPrey?.Predator;
            if (pred != null && eff.Type == StatusEffectType.Diminished)
            {
                if (pred.Unit.HasTrait(Traits.TightNethers) && (actor.SelfPrey.Location == PreyLocation.balls || actor.SelfPrey.Location == PreyLocation.womb))
                {
                    continue;
                }
            }
            if (eff.Type == StatusEffectType.Staggering || eff.Type == StatusEffectType.SpellForce)
                RemoveStatus(eff);
            eff.Duration -= 1;
            if (eff.Duration <= 0)
            {
                RemoveStatus(eff);
            }
        }
    }

    internal List<Traits> RandomizeOne(RandomizeList randomizeList)
    {
        if (randomizeList.level > Level)
            {
                return new List<Traits>() { (Traits)randomizeList.id };
            }
        var chance = randomizeList.chance;
        var traitsToAdd = new List<Traits>();
        List<Traits> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
        while (State.Rand.NextDouble() < chance)
        {
            if (gainable.Count() > 0)
            {
                var randomPick = gainable[State.Rand.Next(gainable.Count())];
                GivePrerequisiteTraits(randomPick);
                if (randomPick >= (Traits)1000)
                {
                    RandomizeList recursiveRl = State.RandomizeLists.Find(re => (Traits)re.id == randomPick);
                    if (recursiveRl != null)
                    {
                        traitsToAdd.AddRange(RandomizeOne(recursiveRl));
                    }
                }
                else
                    traitsToAdd.Add(randomPick);
                gainable.Remove(randomPick);
            }
            chance -= 1;
        }
        return traitsToAdd;
    }

    internal void AcquireShape(Unit unit, bool forceDirect = false)
    {
        if (ShifterShapes.Any(shape => shape.Race == unit.Race) && !forceDirect) return;
        if (HasTrait(Traits.Skinwalker) || forceDirect)
        {
            Unit shape = unit.Clone();
            shape.Side = Side;
            shape._fixedSide = _fixedSide;
            if (HasTrait(Traits.Skinwalker))
                shape.AddPermanentTrait(Traits.Skinwalker);
            shape.hiddenFixedSide = hiddenFixedSide;
            shape.SavedCopy = SavedCopy;
            shape.SavedVillage = SavedVillage;
            shape.RelatedUnits = RelatedUnits;
            shape.ShifterShapes = ShifterShapes;
            ShifterShapes.Add(shape);
        }
        else if (HasTrait(Traits.Shapeshifter))
        {
            this.ShifterShapes.Add(CreateRaceShape(unit.Race));
        }
    }

    internal List<Traits> GetPermanentTraits()
    {
        return PermanentTraits;
    }

    internal void UpdateShapeExpAndItems(bool spendLevels = true)
    {
        if (!HasTrait(Traits.Skinwalker))
        {
            ShifterShapes.ForEach(shape =>
            {
                shape.SetExp(experience);
                if (spendLevels) 
                    StrategicUtilities.SpendLevelUps(shape);
            });
        }
         // Items are too much of a can of worms. Shape inventories are just separate for now.
        //ShifterShapes.ForEach(shape =>
        //{
        //    if (!shape.FixedGear)
        //    {
        //        shape.Items.ForEach((slot, index) =>
        //        {
        //            if (slot == null)
        //            {
        //                slot = GetItem(index);
        //            }
        //        });
        //    }
        //});
    }

    public void Shrink() // The fuck?
    {
        if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal) && HasTrait(Traits.Huge) && HasTrait(Traits.Large))
            RemoveTrait(Traits.Large);
        else if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal) && HasTrait(Traits.Huge))
            RemoveTrait(Traits.Huge);
        else if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal))
        {
            RemoveTrait(Traits.Titanic);
            AddTrait(Traits.Huge);
        }
        else if (HasTrait(Traits.Colossal) && HasTrait(Traits.Huge))
        {
            RemoveTrait(Traits.Colossal);
            RemoveTrait(Traits.Huge);
            AddTrait(Traits.Titanic);
        }
        else if (HasTrait(Traits.Titanic))
        {
            AddTrait(Traits.Colossal);
            RemoveTrait(Traits.Titanic);
        }
        else if (HasTrait(Traits.Colossal))
        {
            RemoveTrait(Traits.Colossal);
            AddTrait(Traits.Huge);
        }
        else if (HasTrait(Traits.Huge))
        {
            RemoveTrait(Traits.Huge);
            AddTrait(Traits.Large);
        }
        else if (HasTrait(Traits.Large))
            RemoveTrait(Traits.Large);
        else if (HasTrait(Traits.Small))
        {
            RemoveTrait(Traits.Small);
            AddTrait(Traits.Tiny);
        }
        else
            AddTrait(Traits.Small);
    }

    public Race DetermineSpawnRace()
    {
        if (SpawnRace != Race)
        {
            return SpawnRace;
        }

        else
            return State.RaceSettings.GetSpawnRace(Race);
    }

    public Race DetermineConversionRace()
    {
        if (ConversionRace != Race)
            return ConversionRace;
        else
            return State.RaceSettings.GetConversionRace(Race);
    }

    internal bool HasShapeshiftingTrait()
    {
        return HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker);
    }

    internal void ClearStatus()
    {
        StatusEffects.ForEach(se =>
        {
            se.OnExpire(this);
        });
        StatusEffects.Clear();
    }
}
