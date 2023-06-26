/// <summary>
/// The list of possible unit traits
/// </summary>
public enum Traits
{
    /// <summary>Unit gains +1 melee attack and +1 ranged attack, reducing the MP used by a melee or ranged attack.</summary>
    DoubleAttack = 0,
    /// <summary>Unit requires 30% less experience to level up.</summary>
    Clever = 1,
    /// <summary>Unit requires 40% more experience to level up.</summary>
    Foolish = 2,
    /// <summary>Unit takes 1 less damage from all attacks.</summary>
    Resilient = 3,
    /// <summary>Unit deals 20% more melee damage.</summary>
    StrongMelee = 4,
    /// <summary>Unit deals 20% less melee damage.</summary>
    WeakAttack = 5,
    /// <summary>Unit is 25% easier to vore.</summary>
    EasyToVore = 6,
    /// <summary>Unit's Strength is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackStrength = 7,
    /// <summary>Unit's Dexterity is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackDexterity = 8,
    /// <summary>Unit's Voracity is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackVoracity = 9,
    /// <summary>Unit's Defense is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackDefense = 10,
    /// <summary>Unit's Will is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackWill = 11,
    /// <summary>Unit's Mind is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackMind = 12,
    /// <summary>Unit's Stomach is boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackStomach = 13,
    /// <summary>Unit's stats are boosted when adajcent to an ally. A second adjacent ally will increase the boost.</summary>
    PackTactics = 14,
    /// <summary>Doubles unit's escape rate.</summary>
    EscapeArtist = 20,
    /// <summary>Doubles unit's digestion rate.</summary>
    FastDigestion = 21,
    /// <summary>Halves unit's digestion rate.</summary>
    SlowDigestion = 22,
    /// <summary>Unit gains 20% redution to damage taken and is 33% harder to vore after ending turn with more than 0 MP.</summary>
    DefensiveStance = 23,
    /// <summary>Adjacent units' dodge chances are reduced by 20%.</summary>
    Intimidating = 24,
    /// <summary>Unit's stats increase when health is lower (max 11% boost).</summary>
    ThrillSeeker = 25,
    /// <summary>Unit has 75% increased vore chance when without prey.</summary>
    Ravenous = 26,
    /// <summary>Unit's stats are boosted by 10% per kill each battle.</summary>
    Frenzy = 27,
    /// <summary>Unit gains 10% global dodge chance.</summary>
    ArtfulDodge = 28,
    /// <summary>When levelling up, any stat can be boosted and 2 random stats are increased by 1 extra point.</summary>
    AdeptLearner = 29,
    /// <summary>Unit takes 30% less damage from ranged attacks and 30% more damage from melee attacks.</summary>
    Tempered = 30,
    /// <summary>Doubles chance for prey to escape from this unit.</summary>
    Nauseous = 31,
    /// <summary>Healing provided from digesting unit is doubled.</summary>
    Tasty = 32,
    /// <summary>Healing provided from digesting unit is reduced by 80%.</summary>
    Disgusting = 33,
    /// <summary>Unit can flee from battle on 4th turn.</summary>
    EvasiveBattler = 34,
    /// <summary>Breeding rate decreased by 30%.</summary>
    SlowBreeder = 35,
    /// <summary>Breeding rate increased by 75%.</summary>
    ProlificBreeder = 36,
    /// <summary>Unit can fly through other units otherwise impassable tiles and only consumes 1 MP per tile.</summary>
    Flight = 37,
    /// <summary>Allows unit to pounce to attack or vore nearby units.</summary>
    Pounce = 38,
    /// <summary>Unit has 50% chance to slime target for 1 turn when attacking, halving their MP.</summary>
    BoggingSlime = 39,
    /// <summary>Unit takes 25% reduced range damage and 20% reduced melee damage, but is 15% easier to vore.</summary>
    GelatinousBody = 40,
    /// <summary>Unit gains +1 melee attack, reducing the MP used by a melee attack.</summary>
    Maul = 41,
    /// <summary>When unit fails a vore attack, unit instead bites target, dealing damage equivalent to that of the basic melee weapon.</summary>
    Biter = 42,
    /// <summary>Unit cannot vore, but experience gained is increased by 15% and healing while in towns is doubled.</summary>
    Prey = 43,
    /// <summary>Unit may stun target for 1 turn when attacking. First stun is 10% chance, with lower chances the more time the unit has been stunned.</summary>
    Paralyzer = 44,
    /// <summary>If at least half of an army has this trait, all non-water tiles can be traversed by the army for 1 MP.</summary>
    Pathfinder = 45,
    /// <summary>Unit has 1/8 chance to summon additional units of race at start of battle.</summary>
    AstralCall = 46,
    /// <summary>Reduces chance to vore unit by 30%, and halves digestion damage taken by unit.</summary>
    MetalBody = 47,
    /// <summary>Adjacent units' stats are reduced by 8%.</summary>
    TentacleHarassment = 48,
    /// <summary>Nullifies movement and dodge penalites from having prey.</summary>
    BornToMove = 49,
    /// <summary>Unit has an additional equipment slot.</summary>
    Resourceful = 50,
    /// <summary>Unit knocks back melee targets or deals 20% more damage if target has no room to be knocked back.</summary>
    ForcefulBlow = 51,
    /// <summary>Halves digestion damage taken by unit, and unit is immune to first turn of digestion damage.</summary>
    AcidResistant = 52,
    /// <summary>Doubles digestion damage taken by unit.</summary>
    SoftBody = 53,
    /// <summary>Unit has a much greater chance to evade ranged attacks. (The exact math is complicated, but should decrease hit chance by up to 50%.)</summary>
    KeenReflexes = 54,
    /// <summary>Tree tiles are treated as valid movement tiles for unit.</summary>
    NimbleClimber = 55,
    /// <summary>Unit gains +1 vore attack, reducing the MP used by a vore attack.</summary>
    StrongGullet = 56,
    /// <summary>Unit deals trple melee damage and is forced to use <b>Claws</b> weapon, which has 2 base damage.</summary>
    Feral = 57,
    /// <summary>Unit has two stomachs. Oral/anal prey will become trapped in second stomach after 2 turns, escaping second stomach moves prey to first stomach and allows 7 turns to escape again before being moved back to second stomach.</summary>
    DualStomach = 58,
    /// <summary>Attacks against unit may fail based on attacker's and unit's will score (max 80% fail chance).</summary>
    Dazzle = 59,
    /// <summary>Unit gains +4 movement for the first two turns of a battle.</summary>
    Charge = 60,
    /// <summary>Allows unit to cast a random spell once per battle.</summary>
    MadScience = 61,
    /// <summary>Halves unit's escape chance.</summary>
    Lethargic = 62,
    /// <summary>Allows usage of a strong attack that can vore.</summary>
    ShunGokuSatsu = 63,
    /// <summary>Unit will respawn after battle if killed.</summary>
    Eternal = 64,
    /// <summary>Unit is immune to first 20 turns of digestion damage.</summary>
    AcidImmunity = 65,
    /// <summary>When killed, unit will respawn as new unit of same race with half of the original unit's experience.</summary>
    Replaceable = 66,
    /// <summary>Unit will not regurgitate prey, and Regurgitate command is disabled.</summary>
    Greedy = 67,
    /// <summary>Unit can vore units up to 4 tiles away, with lowers odds the further away the target is.</summary>
    RangedVore = 68,
    /// <summary>Pounces deal additional damage (max 2x) when unit has prey, as well as debuffing unit's dodge chance (max -80%).</summary>
    HeavyPounce = 69,
    /// <summary>Allows unit to eat non-surrendered allies.</summary>
    Cruel = 70,
    /// <summary>Increases unit's scale by 50%.</summary>
    Large = 71,
    /// <summary>Reduces unit's scale by 2/3.</summary>
    Small = 72,
    /// <summary>Increases unit's chance to evade spells by 20%.</summary>
    MagicResistance = 73,
    /// <summary>Increases unit's spell success rate by 20%.</summary>
    MagicProwess = 74,
    /// <summary>Unit gains <b>Empowered</b> buff after digesting a victim.</summary>
    MetabolicSurge = 75,
    /// <summary>Doubles prey absorption rate.</summary>
    FastAbsorption = 76,
    /// <summary>Halves prey absorption rate.</summary>
    SlowAbsorption = 77,
    /// <summary>Prey is afflicted with <b>Prey's Curse</b>.</summary>
    EnthrallingDepths = 78,
    /// <summary>Debuffs nearby foes when voring a unit.</summary>
    FearsomeAppetite = 79,
    /// <summary>Allows a weaker, 3-tile attack.</summary>
    TailStrike = 80,
    /// <summary>Unit does not digest friendly units.</summary>
    Endosoma = 81,
    /// <summary>Halves chance for unit's prey to escape.</summary>
    IronGut = 82,
    /// <summary>Reduces chance for unit's prey to escape by 15%.</summary>
    SteadyStomach = 83,
    /// <summary>Attacks from unit may inflict a non-stackable poison effect.</summary>
    Stinger = 84,
    /// <summary>Unit's race suffers a permanent <b>Prey's Curse</b> effect.</summary>
    WillingRace = 85,
    /// <summary>Unit's bulk is increased by 75%. Does not boost stats.</summary>
    Bulky = 86,
    /// <summary>Allows unit to use <b>Pollen Cloud</b>, a 3x3 status-inflicting attack once per battle.</summary>
    PollenProjector = 87,
    /// <summary>Allows unit to use <b>Web</b>, an attack that reduces movement and stats once per battle.</summary>
    Webber = 88,
    /// <summary>Prevents unit from defecting.</summary>
    Camaraderie = 89,
    /// <summary>Forces unit to always defect to its race's side.</summary>
    RaceLoyal = 144,
    /// <summary>Unit's movement is reduced by 25%, but movement penalties from prey are halved and minimum movement is raised to 2.</summary>
    SlowMovement = 90,
    /// <summary>Unit's movement is reduced by 45%, but movement penalties from prey are nullified and minimum movement is raised to 2.</summary>
    VerySlowMovement = 91,
    /// <summary>No healing is provided when absordbing this unit, and there is a 1/8 chance of being poisoned when attacking this unit.</summary>
    Toxic = 92,
    /// <summary>Allows unit to use <b>Glue Bomb</b>, an attack that reduces movement once per battle.</summary>
    GlueBomb = 93,
    /// <summary>Unit takes 25% less ranged damage.</summary>
    HardSkin = 94,
    /// <summary>Unit recovers health when melee attacking equivalent to 12% of damage dealt, minimum 1.</summary>
    Vampirism = 95,
    /// <summary>When killing another unit, this unit will recieve one of the following buffs: Valor, Mending, Fast, Shielded, or Predation.</summary>
    TasteForBlood = 96,
    /// <summary>When this unit rubs a unit's belly, the effect is doubled.</summary>
    PleasurableTouch = 97,
    /// <summary>Doubles unit's max capacity.</summary>
    StretchyInsides = 98,
    /// <summary>Unit gains +1 ranged attack, reducing the MP used by a ranged attack.</summary>
    QuickShooter = 99,
    /// <summary>Unit gains +1 spell attack, reducing the MP used by a spell attack.</summary>
    FastCaster = 100,
    /// <summary>Unit deals 20% less damage with ranged attacks.</summary>
    RangedIneptitude = 101,
    /// <summary>Unit deals 20% more damage with ranged attacks.</summary>
    KeenShot = 102,
    /// <summary>Unit takes 25% less damage from fire attacks.</summary>
    HotBlooded = 103,
    /// <summary>When levelling up, any stat can be boosted.</summary>
    FocusedDevelopment = 104,
    /// <summary>Unit's maximum mana is increased by 50%, and units absorbing this unit will recover mana equivalent to 40% of the digestion damage to this unit.</summary>
    ManaRich = 105,
    /// <summary>When absorbing prey, this unit will recover mana equivalent to 40% of the digestion damage dealt, but healing from the absorbed unit is reducued by 40%.</summary>
    ManaDrain = 106,
    /// <summary>
    /// Unit deals 40% more damage with attacks, takes 25% less damage from melee and ranged attacks, and is half as likely to miss,
    ///  but unit's movement is reduced by 20% and escape chance by 75%, and unit is 2.5x easier to vore. This trait is spread to the predator when this unit is digested.
    /// </summary>
    CursedMark = 107,
    /// <summary>Unit is 20% harder to vore, but 60% less likely to escape.</summary>
    Slippery = 108,
    /// <summary>Unit regenerates 2 HP per turn, but provides triple the healing when absorbed.</summary>
    HealingBlood = 109,
    /// <summary>Allows unit to use <b>Poison Spit</b>, an attack deals damage and inflicts a short, strong damage-over-time effect once per battle.</summary>
    PoisonSpit = 110,
    /// <summary>Unit digests and absorbs prey 75% slower.</summary>
    SlowMetabolism = 111,
    /// <summary>Unit has a 4/5 chance to respawn after battle if killed.</summary>
    LuckySurvival = 112,
    /// <summary>Unit deals up to 40% more damage with weapons based on how low target's health is, as well as an additional 10% per negative status effect the target has.</summary>
    SenseWeakness = 113,
    /// <summary>Unit gains a stack of Blade Dance when landing a melee attack and loses a stack when hit with a melee attck. Each stack boosts Strength by 2 and Agility by 1.</summary>
    BladeDance = 114,
    /// <summary>Unit gains +1 melee attack when without prey, but takes 25% more damage from attacks.</summary>
    LightFrame = 115,
    /// <summary>Unit gains +1 movement and 25% melee and vore dodge chance, but takes 20% more damage from melee attacks.</summary>
    Featherweight = 116,
    /// <summary>Unit's stats are boosted by 50%.</summary>
    PeakCondition = 117,
    /// <summary>Unit's stats are boosted by 20%.</summary>
    Fit = 118,
    /// <summary>Unit's stats are lowered by 20%.</summary>
    Illness = 119,
    /// <summary>Unit's stats are lowered by 50%.</summary>
    Diseased = 120,
    /// <summary>Unit has 1/8 chance to summon ants at start of battle.</summary>
    AntPheromones = 121,
    /// <summary>Unit cannot flee or surrender.</summary>
    Fearless = 122,
    /// <summary>Unit's movement is reduced by 15%, melee damage by 10% and melee accuracy by 30%.</summary>
    Clumsy = 123,
    /// <summary>Unit will respawn after battle if digested.</summary>
    Reformer = 124,
    /// <summary>Unit will respawn after battle unless digested.</summary>
    Revenant = 125,
    /// <summary>Unit's first attack will deal 5x damage, or first vore attack will have 3.25 odds each battle.</summary>
    AllOutFirstStrike = 126,
    /// <summary>If a missed vore from this unit triggers a bite, the tharget will also be poisoned and inflicted with Shaken.</summary>
    VenomousBite = 127,
    /// <summary>Allows unit to use <b>Petrify</b>, an attack that petrifies the target once per battle.</summary>
    Petrifier = 128,
    /// <summary>Unit deals 50% more melee damage and has a 50% higher chance to vore poisoned targets.</summary>
    VenomShock = 129,
    /// <summary>Unit will not escape predators once vored.</summary>
    Submissive = 130,
    /// <summary>Odds to vore this unit are increased by 1000x.</summary>
    Defenseless = 131,
    /// <summary>Unit gains a stack of Tenacity when hit with or missing an attack and loses 5 stacks when landing an attack or voring a target. Each stack boosts Strength, Agility and Voracity by 10%.</summary>
    Tenacious = 132,
    /// <summary>Unit will never convert or rebirth unbirthed prey.</summary>
    PredGusher = 133,
    /// <summary>When unit breastfeeds, experience gain will be boosted up to 100% based on prey's level and body size.</summary>
    Honeymaker = 134,
    /// <summary>When unit feeds, health gain will be boosted and AP used will be decreased.</summary>
    WetNurse = 135,
    /// <summary>Unit will be counted as fled if digested.</summary>
    TheGreatEscape = 136,
    /// <summary>Unit is afflicted by <b>Berserk</b> status when fallling below half health.</summary>
    Berserk = 137,
    /// <summary>Unit is absorbed 50% quicker.</summary>
    HighlyAbsorbable = 138,
    /// <summary>Unit is absorbed 100% quicker and provides 50% more healing.</summary>
    IdealSustenance = 139,
    /// <summary>Unit recieves 50% more healing when absorbing prey.</summary>
    EfficientGuts = 140,
    /// <summary>Unit absorbs prey 50% faster, but healing recieved from absorption is halved.</summary>
    WastefulProcessing = 141,
    /// <summary>Allows unit to use <b>Charm</b>, an attack changes a target's allegience once per battle.</summary>
    Charmer = 143,
    /// <summary>Allows unit to attempt force-feeding itself to another unit at will.</summary>
    ForceFeeder = 145,
    /// <summary>Allows unit to use <b>Reanimate</b>, an attack that brings any unit back to life as the caster's summon, once per battle.</summary>
    Reanimator = 146,
    /// <summary>Allows unit to either take control of any summon, or re-summon the most recently bound one once a battle.</summary>
    Binder = 147,
    /// <summary>Unit generates with a random Tier 1 Book.</summary>
    BookWormI = 148,
    /// <summary>Unit generates with a random Tier 2 Book.</summary>
    BookWormII = 149,
    /// <summary>Unit generates with a random Tier 3-4 Book.</summary>
    BookWormIII = 150,
    /// <summary>Units that are put under a mindcontrol (e.g. Charm, Hypnosis) effect by this unit want to force-feed themselves to it or its close allies.</summary>
    Temptation = 151,
    /// <summary>Unit heals for half Max HP when taken below half Max HP</summary>
	SecondWind = 152, 
    /// <summary>Unit heals after not taking damage for a bit, scaling higer with each turn without damage.</summary>
	Perseverance = 153, //
    /// <summary>Unit needs mana to survive, uses a bit of their mana every turn. Becomes shaken every turn they don't have mana</summary>
	ManaDependant = 154,
    /// <summary>Unit heals after not taking damage for a bit, scaling higer with each turn without damage.</summary>
    NightEye = 155,
    /// <summary>Unit heals after not taking damage for a bit, scaling higer with each turn without damage.</summary>
    KeenEye = 156,
    /// <summary>Unit heals after not taking damage for a bit, scaling higer with each turn without damage.</summary>
	AccuteDodge = 157,
    //Not yet implemented
    AlertStance = 158, // Increased graze chance if MP remaining
    CheapShot = 159, // Critical hits stun
    StaggeringForce = 160, // Critical hits stagger
    SavageSortilege = 161, //Unit Can critically strike magic attacks
    ArcaneBloodline = 162, //Unit can use health as mana, Gain 3 Mana Based spells


    //Hidden Traits
    /// <summary>When an Army consists only of units with this trait, it moves undetected and can infiltrate the enemy by using "exchange" on their village or a mercanary house.</summary>
    Infiltrator = 200,
    /// <summary>No matter which army this unit is in, it is only ever truly aligned with its race.</summary>
    Untamable = 201,
    /// <summary>Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them.</summary>
    Reincarnation = 202,
    /// <summary>Soon after this unit is digested, one of the new Units that come into being for the pred's race will be a reincarnation of them.</summary>
    Transmigration = 203,

    //Hidden Cheat Traits
    /// <summary>If a currupted unit is digested, the pred will build up corruption as a hidden status. Once corrupted prey with a stat total equal to that of the pred has been digested, they are under control of the side of the last-digested corrupted.</summary>
    Corruption = 350,
    /// <summary>Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them. The reincarnation will also have this trait.</summary>
    InfiniteReincarnation = 351,
    /// <summary>Soon after this unit is digested, one of the new Units that come into being as the pred's race will be a reincarnation of them. The reincarnation will also have this trait.</summary>
    InfiniteTransmigration = 352,



    //Everything after this is a cheat trait
    /// <summary>Cheat Trait: Unit gains +10 movement and +5 attacks of any type.</summary>
    LightningSpeed = 242,
    /// <summary>Cheat Trait: Unit is digested 75% slower, is immune to first 5 turns of digestion damage, takes 25% less damage from melee and ranged attacks, gains +2 attacks of any type, +3 to all stats and +7 to 2 random stats on level up and any stat can be boosted when levelling up.</summary>
    DivineBloodline = 243,
    /// <summary>Cheat Trait: Unit gains 5x experience from vore and 5x experience from absorption.</summary>   
    GeneEater = 244,
    /// <summary>Cheat Trait: Unit fully digests prey by the end of the turn they are vored.</summary>   
    InstantDigestion = 245,
    /// <summary>Cheat Trait: Unit fully absorbs prey by the end of the turn they are digested.</summary>   
    InstantAbsorption = 246,
    /// <summary>Cheat Trait: Prey cannot escape from unit.</summary>   
    Inescapable = 247,
    /// <summary>Cheat Trait: Unit's vore attacks have 100% success rate.</summary>   
    Irresistable = 248,
    /// <summary>Cheat Trait: Increases unit's scale by 3x.</summary>
    Titanic = 249,
    /// <summary>Cheat Trait: Decreases unit's scale by 1/3.</summary>
    Tiny = 250,
    /// <summary>Cheat Trait: Heals 10% of friendly prey's HP each turn.</summary>
    HealingBelly = 251,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption. When this occurs, unit loses this trait unless unit had less than 5 traits beforehand.</summary>
    Assimilate = 252,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption. If unit has already gained 3 traits this way, the new trait replace the oldest trait.</summary>
    AdaptiveBiology = 253,
    /// <summary>Cheat Trait: Increases unit's scale by 3x.</summary>
    Huge = 254,
    /// <summary>Cheat Trait: Increases unit's scale by 2.5x.</summary>
    Colossal = 255,
    /// <summary>Cheat Trait: Doubles experience gained by unit.</summary>
    AdaptiveTactics = 256,
    /// <summary>Cheat Trait: Unit gains +1 to all stats for every 4 non-vore kills.</summary>
    KillerKnowledge = 257,
    /// <summary>Cheat Trait: Unit gains random trait from prey upon absorption.</summary>
    InfiniteAssimilation = 258,
    /// <summary>Cheat Trait: Unit grants random trait from prey to unit's race upon absorption.</summary>
    SynchronizedEvolution = 294,
    /// <summary>Cheat Trait: Unit has 50% chance to convert digested prey to its side.</summary>
    DigestionConversion = 259,
    /// <summary>Cheat Trait: Unit has 50% chance to rebirth digested prey to its race and side.</summary>
    DigestionRebirth = 260,
    /// <summary>Cheat Trait: Unit gains +1 to all stats for every 4 digestions.</summary>
    EssenceAbsorption = 261,
    /// <summary>Cheat Trait: Increases unit's max capacity by 1000x.</summary>
    UnlimitedCapacity = 262,
    /// <summary>Cheat Trait: Unit is unable to be vored.</summary>
    Inedible = 263,
    /// <summary>Unit will always convert unbirthed prey.</summary>
    PredConverter = 264,
    /// <summary>Unit will always rebirth unbirthed prey.</summary>
    PredRebirther = 265,
    /// <summary>When this unit rubs the belly of a foe that has not been attacked in the last two turns, there is a chance for that foe to be stunned or switch sides.</summary>
    SeductiveTouch = 266,
    /// <summary>Can emit Gas that turns foes into subservient non-combatants once per battle.</summary>
    HypnoticGas = 295,

    // Growth-related section
    /// <summary>Unit increases in size when absorbing prey.</summary>
	Growth = 267,
    /// <summary>Unit's absorption growth is 50% faster.</summary>
    IncreasedGrowth = 268,
    /// <summary>Unit's absorption growth is 2x faster.</summary>
    DoubleGrowth = 269,
    /// <summary>Unit's absorption growth decays 50% slower.</summary>
    PersistentGrowth = 270,
    /// <summary>Unit's absorption growth does not decay.</summary>
    PermanentGrowth = 271,
    /// <summary>Unit's absorption growth is 50% slower.</summary>
    MinorGrowth = 272,
    /// <summary>Unit's absorption growth is 20% slower.</summary>
    SlowedGrowth = 273,
    /// <summary>Unit's absorption growth decays 2x as fast.</summary>
    FleetingGrowth = 274,
    /// <summary>Doubles healing provided when absorbing unit and increases growth provided by 50%.</summary>
    ProteinRich = 275,
}
