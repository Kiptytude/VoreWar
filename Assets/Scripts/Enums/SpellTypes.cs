public enum SpellTypes
{
    None = 0,
    Fireball = 1,
    PowerBolt = 2,
    LightningBolt = 3,
    Shield = 4,
    Mending = 5,
    Speed = 6,
    Valor = 7,
    Predation = 8,

    IceBlast = 9,
    Pyre = 10,
    //Warp,
    //MagicWall,
    Poison = 11,

    //Quicksand,
    PreysCurse = 12,
    Maw = 13,
    Charm = 14,
    Summon = 15,
    Reanimate = 16,
    Enlarge = 17,

    //Raze,
    Diminishment = 18,
    GateMaw = 19,
    ViralInfection = 20,
    DivinitysEmbrace = 21,

    Resurrection = 22, // This must be the last spell before AlraunePuff because of a line of code who's purpose I don't fully understand.  If you wish to add a new spell, change the number of Resurrection accordingly or weirdness will happen in the right hand menu and give you Web. -Flame Valxsarion

    AlraunePuff = 70,
    Web = 71,
    GlueBomb = 72,
    ViperPoison = 73,
    Petrify = 74,
    HypnoGas = 75,
    Bind = 76,
    Whispers = 77,
    //Corrupt = 78,

    ViperDamage = 110,
    ForceFeed = 111,
    AssumeForm = 112,
    RevertForm = 113,
}

