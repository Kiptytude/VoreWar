using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The list of possible unit traits
/// </summary>
public enum Traits
{
    /// <summary>Unit can attack twice</summary>
    DoubleAttack,
    /// <summary>Unit requires less exp to level up</summary>
    Clever,
    /// <summary>Unit requires extra exp to level up</summary>
    Foolish,
    /// <summary>Unit takes one less damage from all attacks</summary>
    Resilient,
    /// <summary>Unit does extra melee damage</summary>
    StrongMelee,
    /// <summary>Unit does reduced melee damage</summary>
    WeakAttack,
    /// <summary>Unit is easier to vore</summary>
    EasyToVore,
    PackStrength,
    PackDexterity,
    PackVoracity,
    PackDefense,
    PackWill,
    PackMind,
    PackStomach,

    /// <summary> combines all the other pack traits into one </summary>
    PackTactics,
    /// <summary>Unit is more likely to escape from vore</summary>
    EscapeArtist = 20,
    /// <summary>Unit digests prey faster</summary>
    FastDigestion,
    /// <summary>Unit digests prey slower</summary>
    SlowDigestion,
    /// <summary>Unit is harder to hit if it still has MP</summary>
    DefensiveStance,
    /// <summary>Units adjacent suffer reduced accuracy</summary>
    Intimidating,
    /// <summary>Stats go up as health goes down</summary>
    ThrillSeeker,
    /// <summary>Bonus Vore chance when stomach is empty</summary>
    Ravenous,
    /// <summary>Unit becomes stronger for every kill in a battle</summary>
    Frenzy,
    /// <summary>Flat Physical/Vore dodge</summary>
    ArtfulDodge,
    /// <summary>Allows picking of any stat and +1 to 2 random stats</summary>
    AdeptLearner,
    /// <summary>Reduced damage from ranged, bonus from melee</summary>
    Tempered,
    /// <summary>Increased Prey Escape Rate</summary>
    Nauseous,
    /// <summary>Increased healing from this unit when prey</summary>
    Tasty,
    /// <summary>Decreased healing from this unit when prey</summary>
    Disgusting,
    /// <summary>Unit can flee from battle earlier</summary>
    EvasiveBattler,
    /// <summary>Race population increases slower</summary>
    SlowBreeder,
    /// <summary>Race population increases faster</summary>
    ProlificBreeder,
    /// <summary>Can fly over enemy units and obstacles in tactical mode</summary>
    Flight,
    /// <summary>Can pounce on nearby targets</summary>
    Pounce,
    /// <summary>Chance to slow targets</summary>
    BoggingSlime,
    /// <summary>Reduced physical damage and increased chance to be vored</summary>
    GelatinousBody,
    /// <summary>Can melee attack twice</summary>
    Maul,
    /// <summary>does weak melee attack on vore miss</summary>
    Biter,
    /// <summary>Bonus to earned exp and standby healing rate</summary>
    Prey,
    /// <summary>Chance to completely paralyze a target for 1 turn</summary>
    Paralyzer,
    /// <summary>Moves faster on strategic map</summary>
    Pathfinder,
    /// <summary>Chance to summon additional units of race</summary>
    AstralCall,
    /// <summary>Harder to vore, and provides less nutrition</summary>
    MetalBody,
    /// <summary>Stat penalty to adjacent units</summary>
    TentacleHarassment,
    /// <summary>No penalties to dodge/movement speed with prey</summary>
    BornToMove,
    /// <summary>Gain additional item slot</summary>
    Resourceful,
    /// <summary>Melee knockback with extra damage if blocked</summary>
    ForcefulBlow,
    /// <summary>Reduced acid damage and a small grace period</summary>
    AcidResistant,
    /// <summary>Takes additional acid damage</summary>
    SoftBody,
    /// <summary>Additional dodge chance against ranged attacks</summary>
    KeenReflexes,
    /// <summary>Can move through / climb trees</summary>
    NimbleClimber,
    /// <summary>Can make an additional vore attack</summary>
    StrongGullet,
    /// <summary>Unit is prevented from using weapons but gets a melee damage boost</summary>
    Feral,
    /// <summary>Unit has two stomachs, one deeper than another</summary>
    DualStomach,
    /// <summary>Units have to make a will check to attack this unit, or be dazzled (do nothing)</summary>
    Dazzle,
    /// <summary>Unit gets a significant speed boost for the first two turns</summary>
    Charge,
    /// <summary>Allows Casting a random spell once per battle</summary>
    MadScience,
    /// <summary>Reduced Escape odds</summary>
    Lethargic,
    /// <summary>Allows usage of the multi-attack vore ability</summary>
    ShunGokuSatsu,
    /// <summary>Unit doesn't perish</summary>
    Eternal,
    /// <summary>20 turn immunity</summary>
    AcidImmunity,
    /// <summary>Unit replaced by similar unit if killed</summary>
    Replaceable,
    /// <summary>Unit does not want to give up prey</summary>
    Greedy,
    /// <summary>Can Vore at Range</summary>
    RangedVore,
    /// <summary>Pounce does extra damage with prey, but lowers defense for a turn</summary>
    HeavyPounce,
    /// <summary>Unit can eat non-surrendered allies</summary>
    Cruel,
    /// <summary>Unit is larger than normal</summary>
    Large,
    /// <summary>Unit is smaller than normal</summary>
    Small,
    /// <summary>Unit is harder to hit with magic</summary>
    MagicResistance,
    /// <summary>Unit's spells are more accurate</summary>
    MagicProwess,
    /// <summary>Unit gains a burst of power after digesting a victim</summary>
    MetabolicSurge,
    /// <summary>Unit absorbs dead prey quickly</summary>
    FastAbsorption,
    /// <summary>Unit absorbs dead prey slowly</summary>
    SlowAbsorption,
    EnthrallingDepths,
    FearsomeAppetite,
    TailStrike,
    Endosoma,
    IronGut,
    SteadyStomach,
    Stinger,
    WillingRace,
    Bulky,
    PollenProjector,
    Webber,
    Camaraderie,
    RaceLoyal,
    SlowMovement,
    VerySlowMovement,
    Toxic,
    GlueBomb,
    HardSkin,
    Vampirism,
    TasteForBlood,
    PleasurableTouch,
    StretchyInsides,
    QuickShooter,
    FastCaster,
    RangedIneptitude,
    KeenShot,
    HotBlooded,
    FocusedDevelopment,
    ManaRich,
    ManaDrain,
    CursedMark,
    Slippery,
    HealingBlood,
    PoisonSpit,
    SlowMetabolism,
    LuckySurvival,
    SenseWeakness,
    BladeDance,
    LightFrame,
    Featherweight,
    PeakCondition,
    Fit,
    Illness,
    Diseased,
    AntPheromones,
    Fearless,
    Clumsy,
    Reformer,
    Reanimator,
    AllOutFirstStrike,
    VenomousBite,
    Petrifier,
    VenomShock,
    Submissive,
    Defenseless,
    Tenacious,
    PredGusher,
    Honeymaker,
    WetNurse,
    TheGreatEscape,
    Berserk,
    HighlyAbsorbable,
    IdealSustenance,
    EfficientGuts,
    WastefulProcessing,
    Charmer,


    //Everything after this is a cheat trait
    /// <summary>Cheat trait</summary>
    LightningSpeed = 242,
    /// <summary>Cheat trait</summary>
    DivineBloodline,
    /// <summary>Cheat trait</summary>   
    GeneEater,
    InstantDigestion,
    InstantAbsorption,
    Inescapable,
    Irresistable,
    Titanic,
    Tiny,
    HealingBelly,
    Assimilate,
    AdaptiveBiology,
    Huge,
    Colossal,
    AdaptiveTactics,
    KillerKnowledge,
    InfiniteAssimilation,
    SynchronizedEvolution,
    DigestionConversion,
    DigestionRebirth,
    EssenceAbsorption,
    UnlimitedCapacity,
    Inedible,	
    PredConverter,
    PredRebirther,
    SeductiveTouch,

    // Growth-related section
	Growth,
    IncreasedGrowth,
    DoubleGrowth,
    PersistentGrowth,
    PermanentGrowth,
    MinorGrowth,
    SlowedGrowth,
    FleetingGrowth,
    ProteinRich,
}
