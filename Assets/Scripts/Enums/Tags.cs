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
    /// <summary>Unit gains +1 melee attack and +1 ranged attack, reducing the MP used by a melee or ranged attack.</summary>
    DoubleAttack,
    /// <summary>Unit requires 30% less experience to level up.</summary>
    Clever,
    /// <summary>Unit requires 40% more experience to level up.</summary>
    Foolish,
    /// <summary>Unit takes 1 less damage from all attacks.</summary>
    Resilient,
    /// <summary>Unit deals 20% more melee damage.</summary>
    StrongMelee,
    /// <summary>Unit deals 20% less melee damage.</summary>
    WeakAttack,
    /// <summary>Unit is 25% easier to vore.</summary>
    EasyToVore,
    /// <summary>Unit's Strength is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackStrength,
    /// <summary>Unit's Dexterity is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackDexterity,
    /// <summary>Unit's Voracity is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackVoracity,
    /// <summary>Unit's Defense is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackDefense,
    /// <summary>Unit's Will is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackWill,
    /// <summary>Unit's Mind is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackMind,
    /// <summary>Unit's Stomach is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackStomach,

    /// <summary>Unit's stats are boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackTactics,
    /// <summary>Doubles unit's escape rate.</summary>
    EscapeArtist = 20,
    /// <summary>Doubles unit's digestion rate.</summary>
    FastDigestion,
    /// <summary>Halves unit's digestion rate.</summary>
    SlowDigestion,
    /// <summary>Unit gains 20% redution to damage taken and is 33% harder to vore after ending turn with more than 0 MP.</summary>
    DefensiveStance,
    /// <summary>Adjacent units' dodge chances are reduced by 20%.</summary>
    Intimidating,
    /// <summary>Unit's stats increase when health is lower (max 11% boost).</summary>
    ThrillSeeker,
    /// <summary>Unit has 75% increased vore chance when without prey.</summary>
    Ravenous,
    /// <summary>Unit's stats are boosted by 10% per kill each battle.</summary>
    Frenzy,
    /// <summary>Unit gains 10% global dodge chance.</summary>
    ArtfulDodge,
    /// <summary>When levelling up, any stat can be boosted and 2 random stats are increased by 1 extra point.</summary>
    AdeptLearner,
    /// <summary>Unit takes 30% less damage from ranged attacks and 30% more damage from melee attacks.</summary>
    Tempered,
    /// <summary>Doubles chance for prey to escape from this unit.</summary>
    Nauseous,
    /// <summary>Healing provided from digesting unit is doubled.</summary>
    Tasty,
    /// <summary>Healing provided from digesting unit is reduced by 80%.</summary>
    Disgusting,
    /// <summary>Unit can flee from battle on 4th turn.</summary>
    EvasiveBattler,
    /// <summary>Breeding rate decreased by 30%.</summary>
    SlowBreeder,
    /// <summary>Breeding rate increased by 75%.</summary>
    ProlificBreeder,
    /// <summary>Unit can fly through other units otherwise impassable tiles and only consumes 1 MP per tile.</summary>
    Flight,
    /// <summary>Allows unit to pounce to attack or vore nearby units.</summary>
    Pounce,
    /// <summary>Unit has 50% chance to slime target for 1 turn when attacking, halving their MP.</summary>
    BoggingSlime,
    /// <summary>Unit takes 25% reduced range damage and 20% reduced melee damage, but is 15% easier to vore.</summary>
    GelatinousBody,
    /// <summary>Unit gains +1 melee attack, reducing the MP used by a melee attack.</summary>
    Maul,
    /// <summary>When unit fails a vore attack, unit instead bites target, dealing damage equivalent to that of the basic melee weapon.</summary>
    Biter,
    /// <summary>Unit cannot vore, but experience gained is increased by 15% and healing while in towns is doubled.</summary>
    Prey,
    /// <summary>Unit may stun target for 1 turn when attacking. First stun is 10% chance, with lower chances the more time the unit has been stunned.</summary>
    Paralyzer,
    /// <summary>If at least half of an army has this trait, all non-water tiles can be traversed by the army for 1 MP.</summary>
    Pathfinder,
    /// <summary>Unit has 1/8 chance to summon additional units of race at start of battle.</summary>
    AstralCall,
    /// <summary>Reduces chance to vore unit by 30%, and halves digestion damage taken by unit.</summary>
    MetalBody,
    /// <summary>Adjacent units' stats are reduced by 8%.</summary>
    TentacleHarassment,
    /// <summary>Nullifies movement and dodge penalites from having prey.</summary>
    BornToMove,
    /// <summary>Unit has an additional equipment slot.</summary>
    Resourceful,
    /// <summary>Unit knocks back melee targets or deals 20% more damage if target has no room to be knocked back.</summary>
    ForcefulBlow,
    /// <summary>Halves digestion damage taken by unit, and unit is immune to first turn of digestion damage.</summary>
    AcidResistant,
    /// <summary>Doubles digestion damage taken by unit.</summary>
    SoftBody,
    /// <summary>Unit has a much greater chance to evade ranged attacks. (The exact math is complicated, but should decrease hit chance by up to 50%.)</summary>
    KeenReflexes,
    /// <summary>Tree tiles are treated as valid movement tiles for unit.</summary>
    NimbleClimber,
    /// <summary>Unit gains +1 vore attack, reducing the MP used by a vore attack.</summary>
    StrongGullet,
    /// <summary>Unit deals trple melee damage and is forced to use <b>Claws</b> weapon, which has 2 base damage.</summary>
    Feral,
    /// <summary>Unit has two stomachs. Oral/anal prey will become trapped in second stomach after 2 turns, escaping second stomach moves prey to first stomach and allows 7 turns to escape again before being moved back to second stomach.</summary>
    DualStomach,
    /// <summary>Attacks against unit may fail based on attacker's and unit's will score (max 80% fail chance).</summary>
    Dazzle,
    /// <summary>Unit gains +4 movement for the first two turns of a battle.</summary>
    Charge,
    /// <summary>Allows unit to cast a random spell once per battle.</summary>
    MadScience,
    /// <summary>Halves unit's escape chance.</summary>
    Lethargic,
    /// <summary>Allows usage of a strong attack that can vore.</summary>
    ShunGokuSatsu,
    /// <summary>Unit will respawn after battle if killed.</summary>
    Eternal,
    /// <summary>Unit is immune to first 20 turns of digestion damage.</summary>
    AcidImmunity,
    /// <summary>When killed, unit will respawn as new unit of same race with half of the original unit's experience.</summary>
    Replaceable,
    /// <summary>Unit will not regurgitate prey, and Regurgitate command is disabled.</summary>
    Greedy,
    /// <summary>Unit can vore units up to 4 tiles away, with lowers odds the further away the target is.</summary>
    RangedVore,
    /// <summary>Pounces deal additional damage (max 2x) when unit has prey, as well as debuffing unit's dodge chance (max -80%).</summary>
    HeavyPounce,
    /// <summary>Allows unit to eat non-surrendered allies.</summary>
    Cruel,
    /// <summary>Increases unit's scale by 50%.</summary>
    Large,
    /// <summary>Reduces unit's scale by 2/3.</summary>
    Small,
    /// <summary>Increases unit's chance to evade spells by 20%.</summary>
    MagicResistance,
    /// <summary>Increases unit's spell success rate by 20%.</summary>
    MagicProwess,
    /// <summary>Unit gains <b>Empowered</b> buff after digesting a victim.</summary>
    MetabolicSurge,
    /// <summary>Doubles prey absorption rate.</summary>
    FastAbsorption,
    /// <summary>Halves prey absorption rate.</summary>
    SlowAbsorption,
    /// <summary>Prey is afflicted with <b>Prey's Curse</b>.</summary>
    EnthrallingDepths,
    /// <summary>Debuffs nearby foes when voring a unit.</summary>
    FearsomeAppetite,
    /// <summary>Allows a weaker, 3-tile attack.</summary>
    TailStrike,
    /// <summary>Unit does not digest friendly units.</summary>
    Endosoma,
    /// <summary>Halves chance for unit's prey to escape.</summary>
    IronGut,
    /// <summary>Reduces chance for unit's prey to escape by 15%.</summary>
    SteadyStomach,
    /// <summary>Attacks from unit may inflict a non-stackable poison effect.</summary>
    Stinger,
    /// <summary>Unit's race suffers a permanent <b>Prey's Curse</b> effect.</summary>
    WillingRace,
    /// <summary>Unit's bulk is increased by 75%. Does not boost stats.</summary>
    Bulky,
    /// <summary>Allows unit to use <b>Pollen Cloud</b>, a 3x3 status-inflicting attack once per battle.</summary>
    PollenProjector,
    /// <summary>Allows unit to use <b>Web</b>, an attack that reduces movement and stats once per battle.</summary>
    Webber,
    /// <summary>Prevents unit from defecting.</summary>
    Camaraderie,
    /// <summary>Forces unit to always defect to its race's side.</summary>
    RaceLoyal,
    /// <summary>Unit's movement is reduced by 25%, but movement penalties from prey are halved and minimum movement is raised to 2.</summary>
    SlowMovement,
    /// <summary>Unit's movement is reduced by 45%, but movement penalties from prey are nullified and minimum movement is raised to 2.</summary>
    VerySlowMovement,
    /// <summary>No healing is provided when absordbing this unit, and there is a 1/8 chance of being poisoned when attacking this unit.</summary>
    Toxic,
    /// <summary>Allows unit to use <b>Glue Bomb</b>, an attack that reduces movement once per battle.</summary>
    GlueBomb,
    /// <summary>Unit takes 25% less ranged damage.</summary>
    HardSkin,
    /// <summary>Unit recovers health when melee attacking equivalent to 12% of damage dealt, minimum 1.</summary>
    Vampirism,
    /// <summary>When killing another unit, this unit will recieve one of the following buffs: Valor, Mending, Fast, Shielded, or Predation.</summary>
    TasteForBlood,
    /// <summary>When this unit rubs a unit's belly, the effect is doubled.</summary>
    PleasurableTouch,
    /// <summary>Doubles unit's max capacity.</summary>
    StretchyInsides,
    /// <summary>Unit gains +1 ranged attack, reducing the MP used by a ranged attack.</summary>
    QuickShooter,
    /// <summary>Unit gains +1 spell attack, reducing the MP used by a spell attack.</summary>
    FastCaster,
    /// <summary>Unit deals 20% less damage with ranged attacks.</summary>
    RangedIneptitude,
    /// <summary>Unit deals 20% more damage with ranged attacks.</summary>
    KeenShot,
    /// <summary>Unit takes 25% less damage from fire attacks.</summary>
    HotBlooded,
    /// <summary>When levelling up, any stat can be boosted.</summary>
    FocusedDevelopment,
    /// <summary>Unit's maximum mana is increased by 50%, and units absorbing this unit will recover mana equivalent to 40% of the digestion damage to this unit.</summary>
    ManaRich,
    /// <summary>When absorbing prey, this unit will recover mana equivalent to 40% of the digestion damage dealt, but healing from the absorbed unit is reducued by 40%.</summary>
    ManaDrain,
    /// <summary>
    /// Unit deals 40% more damage with attacks, takes 25% less damage from melee and ranged attacks, and is half as likely to miss,
    ///  but unit's movement is reduced by 20% and escape chance by 75%, and unit is 2.5x easier to vore. This trait is spread to the predator when this unit is digested.
    /// </summary>
    CursedMark,
    /// <summary>Unit is 20% harder to vore, but 60% less likely to escape.</summary>
    Slippery,
    /// <summary>Unit regenerates 2 HP per turn, but provides triple the healing when absorbed.</summary>
    HealingBlood,
    /// <summary>Allows unit to use <b>Poison Spit</b>, an attack deals damage and inflicts a short, strong damage-over-time effect once per battle.</summary>
    PoisonSpit,
    /// <summary>Unit digests and absorbs prey 75% slower.</summary>
    SlowMetabolism,
    /// <summary>Unit has a 4/5 chance to respawn after battle if killed.</summary>
    LuckySurvival,
    /// <summary>Unit deals up to 40% more damage with weapons based on how low target's health is, as well as an additional 10% per negative status effect the target has.</summary>
    SenseWeakness,
    /// <summary>Unit gains a stack of Blade Dance when landing a melee attack and loses a stack when hit with a melee attck. Each stack boosts Strength by 2 and Agility by 1.</summary>
    BladeDance,
    /// <summary>Unit gains +1 melee attack when without prey, but takes 25% more damage from attacks.</summary>
    LightFrame,
    /// <summary>Unit gains +1 movement and 25% melee and vore dodge chance, but takes 20% more damage from melee attacks.</summary>
    Featherweight,
    /// <summary>Unit's stats are boosted by 50%.</summary>
    PeakCondition,
    /// <summary>Unit's stats are boosted by 20%.</summary>
    Fit,
    /// <summary>Unit's stats are lowered by 20%.</summary>
    Illness,
    /// <summary>Unit's stats are lowered by 50%.</summary>
    Diseased,
    /// <summary>Unit has 1/8 chance to summon ants at start of battle.</summary>
    AntPheromones,
    /// <summary>Unit cannot flee or surrender.</summary>
    Fearless,
    /// <summary>Unit's movement is reduced by 15%, melee damage by 10% and melee accuracy by 30%.</summary>
    Clumsy,
    /// <summary>Unit will respawn after battle if digested.</summary>
    Reformer,
    /// <summary>Unit will respawn after battle unless digested.</summary>
    Reanimator,
    /// <summary>Unit's first attack will deal 5x damage, or first vore attack will have 3.25 odds each battle.</summary>
    AllOutFirstStrike,
    /// <summary>If a missed vore from this unit triggers a bite, the tharget will also be poisoned and inflicted with Shaken.</summary>
    VenomousBite,
    /// <summary>Allows unit to use <b>Petrify</b>, an attack that petrifies the target once per battle.</summary>
    Petrifier,
    /// <summary>Unit deals 50% more melee damage and has a 50% higher chance to vore poisoned targets.</summary>
    VenomShock,
    /// <summary>Unit will not escape predators once vored.</summary>
    Submissive,
    /// <summary>Odds to vore this unit are increased by 1000x.</summary>
    Defenseless,
    /// <summary>Unit gains a stack of Tenacity when hit with or missing an attack and loses 5 stacks when landing an attack or voring a target. Each stack boosts Strength, Agility and Voracity by 10%.</summary>
    Tenacious,
    /// <summary>Unit will never convert or rebirth unbirthed prey.</summary>
    PredGusher,
    /// <summary>When unit breastfeeds, experience gain will be boosted up to 100% based on prey's level and body size.</summary>
    Honeymaker,
    /// <summary>When unit feeds, health gain will be boosted and AP used will be decreased.</summary>
    WetNurse,
    /// <summary>Unit will be counted as fled if digested.</summary>
    TheGreatEscape,
    /// <summary>Unit is afflicted by <b>Berserk</b> status when fallling below half health.</summary>
    Berserk,
    /// <summary>Unit is absorbed 50% quicker.</summary>
    HighlyAbsorbable,
    /// <summary>Unit is absorbed 100% quicker and provides 50% more healing.</summary>
    IdealSustenance,
    /// <summary>Unit recieves 50% more healing when absorbing prey.</summary>
    EfficientGuts,
    /// <summary>Unit absorbs prey 50% faster, but healing recieved from absorption is halved.</summary>
    WastefulProcessing,
    /// <summary>Allows unit to use <b>Charm</b>, an attack changes a target's allegience once per battle.</summary>
    Charmer,
    /// <summary>Unit can only cock vore or unbirth pery if the prey is 1/3 the size of this unit, but Diminishment does not fade while prey is inside this unit's cock or womb.</summary>
    TightNethers,


    //Everything after this is a cheat trait
    /// <summary>Cheat Trait: Unit gains +10 movement and +5 attacks of any type.</summary>
    LightningSpeed = 242,
    /// <summary>Cheat Trait: Unit is digested 75% slower, is immune to first 5 turns of digestion damage, takes 25% less damage from melee and ranged attacks, gains +2 attacks of any type, +3 to all stats and +7 to 2 random stats on level up and any stat can be boosted when levelling up.</summary>
    DivineBloodline,
    /// <summary>Cheat Trait: Unit gains 5x experience from vore and 5x experience from absorption.</summary>   
    GeneEater,
    /// <summary>Cheat Trait: Unit fully digests prey by the end of the turn they are vored.</summary>   
    InstantDigestion,
    /// <summary>Cheat Trait: Unit fully absorbs prey by the end of the turn they are digested.</summary>   
    InstantAbsorption,
    /// <summary>Cheat Trait: Prey cannot escape from unit.</summary>   
    Inescapable,
    /// <summary>Cheat Trait: Unit's vore attacks have 100% success rate.</summary>   
    Irresistable,
    /// <summary>Cheat Trait: Increases unit's scale by 3x.</summary>
    Titanic,
    /// <summary>Cheat Trait: Decreases unit's scale by 1/3.</summary>
    Tiny,
    /// <summary>Cheat Trait: Heals 10% of friendly prey's HP each turn.</summary>
    HealingBelly,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption. When this occurs, unit loses this trait unless unit had less than 5 traits beforehand.</summary>
    Assimilate,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption. If unit has already gained 3 traits this way, the new trait replace the oldest trait.</summary>
    AdaptiveBiology,
    /// <summary>Cheat Trait: Increases unit's scale by 3x.</summary>
    Huge,
    /// <summary>Cheat Trait: Increases unit's scale by 2.5x.</summary>
    Colossal,
    /// <summary>Cheat Trait: Doubles experience gained by unit.</summary>
    AdaptiveTactics,
    /// <summary>Cheat Trait: Unit gains +1 to all stats for every 4 non-vore kills.</summary>
    KillerKnowledge,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption.</summary>
    InfiniteAssimilation,
    /// <summary>Cheat Trait: Unit grants random trait from prey to unit's race upon absorption.</summary>
    SynchronizedEvolution,
    /// <summary>Cheat Trait: Unit has 50% chance to convert digested prey to its side.</summary>
    DigestionConversion,
    /// <summary>Cheat Trait: Unit has 50% chance to rebirth digested prey to its race and side.</summary>
    DigestionRebirth,
    /// <summary>Cheat Trait: Unit gains +1 to all stats for every 4 digestions.</summary>
    EssenceAbsorption,
    /// <summary>Cheat Trait: Increases unit's max capacity by 1000x.</summary>
    UnlimitedCapacity,
    /// <summary>Cheat Trait: Unit is unable to be vored.</summary>
    Inedible,
    /// <summary>Unit will always convert unbirthed prey.</summary>
    PredConverter,
    /// <summary>Unit will always rebirth unbirthed prey.</summary>
    PredRebirther,
    /// <summary>When this unit rubs the belly of a foe that has not been attacked in the last turn, there is a chance for that foe to be stunned or switch sides.</summary>
    SeductiveTouch,

    // Growth-related section
    /// <summary>Unit increases in size when absorbing prey.</summary>
	Growth,
    /// <summary>Unit's absorption growth is 50% faster.</summary>
    IncreasedGrowth,
    /// <summary>Unit's absorption growth is 2x faster.</summary>
    DoubleGrowth,
    /// <summary>Unit's absorption growth decays 50% slower.</summary>
    PersistentGrowth,
    /// <summary>Unit's absorption growth does not decay.</summary>
    PermanentGrowth,
    /// <summary>Unit's absorption growth is 50% slower.</summary>
    MinorGrowth,
    /// <summary>Unit's absorption growth is 20% slower.</summary>
    SlowedGrowth,
    /// <summary>Unit's absorption growth decays 2x as fast.</summary>
    FleetingGrowth,
    /// <summary>Doubles healing provided when absorbing unit and increases growth provided by 50%.</summary>
    ProteinRich,
}
