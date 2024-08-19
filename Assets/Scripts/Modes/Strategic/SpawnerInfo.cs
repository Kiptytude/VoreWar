using OdinSerializer;


class SpawnerInfo
{
    [OdinSerialize]
    internal bool Enabled;
    [OdinSerialize]
    internal int MaxArmies;
    [OdinSerialize]
    internal float Confidence;
    [OdinSerialize]
    internal int MinArmySize;
    [OdinSerialize]
    internal int MaxArmySize;
    [OdinSerialize]
    internal float spawnRate;
    [OdinSerialize]
    internal float scalingFactor;
    [OdinSerialize]
    internal int Team;
    [OdinSerialize]
    internal int SpawnAttempts;
    [OdinSerialize]
    internal int TurnOrder;
    [OdinSerialize]
    internal bool AddOnRace;
    [OdinSerialize]
    internal bool UsingCustomType;
    [OdinSerialize]
    internal bool MonsterScoutMP;
    [OdinSerialize]
    internal Config.MonsterConquestType ConquestType;
    [OdinSerialize]
    internal bool DNRestrictOn;
    [OdinSerialize]
    internal Config.DayNightMovemntType DayNightMovemntType;

    internal Config.MonsterConquestType GetConquestType()
    {
        return UsingCustomType ? ConquestType : Config.MonsterConquest;
    }

    internal Config.DayNightMovemntType GetDNMoveType()
    {
        return DNRestrictOn ? DayNightMovemntType : Config.NightMoveMonsters ? Config.DayNightMovemntType.Night : Config.DayNightMovemntType.Off;
    }

    public SpawnerInfo(bool enabled, int maxArmies, float spawnRate, float scalingFactor, int team, int spawnAttempts, bool addOnRace, float confidence, int minArmySize, int maxArmySize, int turnOrder, bool monsterScoutMP)
    {
        Enabled = enabled;
        MaxArmies = maxArmies;
        this.spawnRate = spawnRate;
        this.scalingFactor = scalingFactor;
        Team = team;
        SpawnAttempts = spawnAttempts;
        AddOnRace = addOnRace;
        Confidence = confidence == 0 ? 6 : confidence;
        MinArmySize = minArmySize;
        MaxArmySize = maxArmySize;
        TurnOrder = turnOrder;
        MonsterScoutMP = monsterScoutMP;

    }

    public void SetSpawnerType(Config.MonsterConquestType conquestType)
    {
        ConquestType = conquestType;
        UsingCustomType = true;
    }

    public void SetSpawnerCycleMoveType(Config.DayNightMovemntType moveType)
    {
        DayNightMovemntType = moveType;
        DNRestrictOn = true;
    }
}

