using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class StrategicUtilities
{
    public static Army[] GetAllArmies(bool excludeMonsters = false)
    {
        List<Army> armies = new List<Army>();
        if (excludeMonsters)
        {
            foreach (Empire empire in State.World.MainEmpires)
            {
                foreach (Army army in empire.Armies)
                {
                    armies.Add(army);
                }
            }
        }
        else
        {
            if (State.World.AllActiveEmpires == null)
                return armies.ToArray();
            foreach (Empire empire in State.World.AllActiveEmpires)
            {
                foreach (Army army in empire.Armies)
                {
                    armies.Add(army);
                }
            }
        }

        return armies.ToArray();
    }

    public static Army[] GetAllMonsterArmies()
    {
        List<Army> armies = new List<Army>();

        foreach (Empire empire in State.World.MonsterEmpires)
        {
            foreach (Army army in empire.Armies)
            {
                armies.Add(army);
            }
        }
        return armies.ToArray();
    }

    public static Army[] GetAllHostileArmies(Empire empire, bool includeGoblins = false)
    {
        List<Army> hostileArmies = new List<Army>();
        foreach (Empire hostileEmpire in State.World.AllActiveEmpires)
        {
            if (empire.IsEnemy(hostileEmpire) == false || (includeGoblins == false && hostileEmpire.Team == -200))
                continue;
            foreach (Army army in hostileEmpire.Armies)
            {
                hostileArmies.Add(army);
            }
        }
        return hostileArmies.ToArray();
    }

    public static Army ArmyAt(Vec2i location)
    {
        foreach (Army army in GetAllArmies())
        {
            if (army.Position.Matches(location))
                return army;
        }
        return null;
    }

    public static Village GetVillageAt(Vec2 location)
    {
        foreach (Village village in State.World.Villages)
        {
            if (village.Position.Matches(location.x, location.y))
            {
                return village;
            }
        }
        return null;
    }

    public static Village GetVillageAt(Vec2i location)
    {
        foreach (Village village in State.World.Villages)
        {
            if (village.Position.Matches(location))
            {
                return village;
            }
        }
        return null;
    }

    public static MercenaryHouse GetMercenaryHouseAt(Vec2i location)
    {
        foreach (MercenaryHouse house in State.World.MercenaryHouses)
        {
            if (house.Position.Matches(location))
            {
                return house;
            }
        }
        return null;
    }

    public static ClaimableBuilding GetClaimableAt(Vec2i location)
    {
        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable.Position.Matches(location))
            {
                return claimable;
            }
        }
        return null;
    }

    public static void TryClaim(Vec2i location, Empire empire)
    {
        if (empire.Race == Race.Goblins)
            return;

        ClaimableBuilding claimable = GetClaimableAt(location);
        if (claimable != null)
        {
            if (empire.Race >= Race.Vagrants)
            {
                claimable.Owner = null;
            }
            else
            {
                if (claimable.Owner != null && RelationsManager.GetRelation(claimable.Owner.Side, empire.Side).Type != RelationState.Enemies)
                {
                    return;
                }
                if (claimable.Owner != empire)
                {
                    State.GameManager.StrategyMode.UndoMoves.Clear();
                    RelationsManager.GoldMineTaken(empire, claimable.Owner);
                    claimable.Owner = empire;
                }

            }
            State.GameManager.StrategyMode.RedrawVillages();
        }
    }

    public static List<Unit> GetAllUnits(bool excludeMonsters = false)
    {
        if (State.World == null)
        {
            Debug.LogWarning("This really should not have happened!");
            return new List<Unit>();
        }

        List<Unit> units = new List<Unit>();
        foreach (Army army in GetAllArmies(excludeMonsters))
        {
            units.AddRange(army.Units);
        }
        if (State.World.MainEmpires != null)
        {
            foreach (Village village in State.World.Villages)
            {
                if (village == null)
                    continue;
                units.AddRange(village.GetRecruitables());
                if (village.travelers != null)
                    units.AddRange(village.travelers.Select(s => s.unit));
                if (village.Mercenaries?.Any() ?? false)
                {
                    units.AddRange(village.Mercenaries.Select(s => s.Unit));
                }
                if (village.Adventurers?.Any() ?? false)
                {
                    units.AddRange(village.Adventurers.Select(s => s.Unit));
                }
            }
            if (State.World.MercenaryHouses != null && State.World.MercenaryHouses.Any())
            {
                foreach (var house in State.World.MercenaryHouses)
                {
                    if (house.Mercenaries?.Any() ?? false)
                    {
                        units.AddRange(house.Mercenaries.Select(s => s.Unit));
                    }
                }
            }
        }
        return units;
    }

    public static int Get80thExperiencePercentile()
    {
        int highestExp = 4;
        var allUnits = GetAllUnits(true).Where(s => s.Type != UnitType.Leader).OrderBy(s => s.Experience).ToArray();
        if (allUnits.Count() > 0)
        {
            highestExp = (int)allUnits[(int)(allUnits.Count() * .80f)].Experience;
        }

        return highestExp;
    }

    public static Village[] GetAllHostileVillages(Empire empire)
    {
        List<Village> hostileVillages = new List<Village>();
        foreach (Village village in State.World.Villages)
        {
            if (village.Empire.IsEnemy(empire))
                hostileVillages.Add(village);

        }
        return hostileVillages.ToArray();
    }

    public static int SideVillageCount(int side)
    {
        int count = 0;
        foreach (Village village in State.World.Villages)
        {
            if (village.Side == side)
                count++;
        }
        return count;
    }

    public static void StartBattle(Army army)
    {
        Army[] hostileArmies = GetAllHostileArmies(army.Empire, true);
        Army enemy = null;
        Village village = null;
        for (int i = 0; i < hostileArmies.Length; i++)
        {
            if (hostileArmies[i].Position.Matches(army.Position) && hostileArmies[i].Units.Count > 0)
            {
                enemy = hostileArmies[i];
                break;
            }
        }
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Empire.IsEnemy(army.Empire) && army.Position.Matches(State.World.Villages[i].Position))
            {
                village = State.World.Villages[i];
                break;
            }
        }
        if (army != null)
        {
            army.RemainingMP = 0;
        }
        if (village == null && enemy != null && enemy.Units.Count == 0)
            return;
        if (village != null)
        {
            SpawnerInfo spawner = Config.SpawnerInfo((Race)army.Side);
            Config.MonsterConquestType spawnerType;
            if (spawner != null)
                spawnerType = spawner.GetConquestType();
            else
                spawnerType = Config.MonsterConquest;
            if (enemy == null && village.Garrison == 0)
            {
                if (army.Side >= 100)
                {
                    if (spawnerType == Config.MonsterConquestType.IgnoreTowns)
                    {
                        army.Units = new List<Unit>();
                        army.Prune();
                    }
                    else if (spawnerType == Config.MonsterConquestType.DevourAndDisperse)
                    {
                        if (village.GetTotalPop() > 2)
                        {
                            army.RemainingMP = 1;
                            State.GameManager.StrategyMode.Devour(army, Mathf.Min(2 * army.Units.Count, village.Population - 2));
                        }
                        army.Units = new List<Unit>();
                        army.Prune();
                    }
                    else if (spawnerType == Config.MonsterConquestType.DevourAndHold)
                    {
                        if (village.GetTotalPop() > village.Maxpop / 2)
                        {
                            army.RemainingMP = 1;
                            State.GameManager.StrategyMode.Devour(army, village.GetTotalPop() - village.Maxpop / 2);
                        }
                        Claim();
                    }
                    else //if (Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndHold || Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndMoveOn)
                    {
                        if (village.GetTotalPop() > 0)
                        {

                            if (Config.MonsterConquestTurns == 0)
                            {
                                army.RemainingMP = 1;
                                State.GameManager.StrategyMode.Devour(army, village.GetTotalPop());
                            }
                            else
                                army.MonsterTurnsRemaining = Config.MonsterConquestTurns;
                        }
                        Claim();
                    }
                }
                else
                    Claim();
            }
            else
            {
                if (army.Side >= 100)
                {
                    if (spawnerType == Config.MonsterConquestType.IgnoreTowns)
                    {
                        army.Units = new List<Unit>();
                        army.Prune();
                        return;
                    }
                }
                State.GameManager.ActivateTacticalWithDelay(State.World.Tiles[army.Position.x, army.Position.y], village, army, enemy);
            }
        }
        else
        {
            State.GameManager.ActivateTacticalWithDelay(State.World.Tiles[army.Position.x, army.Position.y], village, army, enemy);
        }

        void Claim()
        {
            village.ChangeOwner(army.Side);
        }

    }



    public static int NumberOfDesiredUpgrades(Army army)
    {
        int upgrades = 0;
        foreach (Unit unit in army.Units)
        {
            if (unit.Level < 4)
                continue;
            if (unit.FixedGear)
                continue;
            Item UpgradeTo = State.World.ItemRepository.GetUpgrade(unit.Items[0]);
            if (UpgradeTo != null)
                upgrades++;
            if (unit.GetItem(1) == null)
                upgrades++;
        }
        return upgrades;
    }

    public static void UpgradeUnitsIfAtLeastLevel(Army army, Village village, int level)
    {
        //Done this way to prioritize any weapon upgrades first before any accessories
        Empire empire = State.World.GetEmpireOfSide(army.Side);
        foreach (Unit unit in army.Units)
        {
            if (unit.Level >= level)
            {
                UpgradeWeaponIfPossible(empire, unit);
            }
        }

        foreach (Unit unit in army.Units)
        {
            if (unit.Level >= level)
            {
                PurchaseAccessories(empire, village, unit);
            }
        }
    }

    static void UpgradeWeaponIfPossible(Empire empire, Unit unit)
    {
        Item currentWeapon = unit.Items[0];
        Item UpgradeTo = State.World.ItemRepository.GetUpgrade(currentWeapon);
        if (UpgradeTo == null)
            return;
        if (empire.Gold >= UpgradeTo.Cost - (currentWeapon.Cost / 2))
        {
            Shop.SellItem(empire, unit, 0);
            Shop.BuyItem(empire, unit, UpgradeTo);
        }
    }

    static void PurchaseAccessories(Empire empire, Village village, Unit unit)
    {
        Item itemToPurchase = null;
        if (unit.BestSuitedForRanged() && unit.HasTrait(Traits.Feral) == false)
        {
            itemToPurchase = State.World.ItemRepository.GetItem(ItemType.Gloves);
        }
        else
        {
            int r = State.Rand.Next(4);
            switch (r)
            {
                case 0:
                    itemToPurchase = State.World.ItemRepository.GetItem(ItemType.Shoes);
                    break;

                case 1:
                    itemToPurchase = State.World.ItemRepository.GetItem(ItemType.Helmet);
                    break;

                case 2:
                    itemToPurchase = State.World.ItemRepository.GetItem(ItemType.BodyArmor);
                    break;

                case 3:
                    itemToPurchase = State.World.ItemRepository.GetItem(ItemType.Gauntlet);
                    break;

            }
        }
        if (unit.AIClass == AIClass.MagicMelee || unit.AIClass == AIClass.MagicRanged)
        {
            if (village.buildings.Contains(VillageBuilding.MagicGuild))
                itemToPurchase = State.World.ItemRepository.GetRandomBook(1, 2);
            else
                itemToPurchase = State.World.ItemRepository.GetRandomBook(1, 1);
        }
        if (itemToPurchase == null || unit.Items.Contains(itemToPurchase))
        {
            return;
        }
        if (empire.Gold >= itemToPurchase.Cost)
        {
            Shop.BuyItem(empire, unit, itemToPurchase); //Will not buy if there are no free slots, so don't need to check that here
        }
    }

    internal static void SetAIClass(Unit unit, float magicChance = 0)
    {
        if (unit.Race == Race.Fairies)
            unit.AIClass = AIClass.MagicRanged;
        else if ((unit.Race == Race.Succubi || unit.FixedGear == false) && unit.BestSuitedForRanged() ||
            (unit.FixedGear && unit.GetBestRanged() != null))
        {
            if (unit.Items[1] is SpellBook || State.Rand.NextDouble() < magicChance)
                unit.AIClass = AIClass.MagicRanged;
            else if (unit.Predator && State.Rand.Next(3) == 0)
                unit.AIClass = AIClass.RangedVore;
            else
                unit.AIClass = AIClass.Ranged;
        }
        else
        {
            if (unit.HasTrait(Traits.Feral) == false && (unit.Items[1] is SpellBook || State.Rand.NextDouble() < magicChance))
                unit.AIClass = AIClass.MagicMelee;
            else if (unit.Predator && State.Rand.Next(3) == 0)
                unit.AIClass = AIClass.MeleeVore;
            else
                unit.AIClass = AIClass.Melee;
        }
    }

    public static double TileThreat(Vec2i location)
    {
        //This system doesn't fully take into account the full exponential power of high powered units

        return VillagePower(location) + ArmyPower(location);

    }

    private static double VillagePower(Vec2i location)
    {
        double power = 0;
        Village village = GetVillageAt(location);
        if (village != null)
        {
            power = village.Garrison * village.Garrison;
        }
        return power;
    }

    private static double ArmyPower(Vec2i location)
    {
        Army army = ArmyAt(location);
        return ArmyPower(army);
    }

    public static double ArmyPower(Army army)
    {
        double finalPower = 0;
        if (army != null)
        {
            int count = army.Units.Count;
            double power = 0;
            double effectiveLevelBoost = (army.LeaderIfInArmy()?.GetStatBase(Stat.Leadership) ?? 0) / 10 * .5;
            foreach (Unit unit in army.Units)
            {
                var racePar = RaceParameters.GetTraitData(unit);
                double weaponFactor;
                if (unit.GetBestRanged() != null) weaponFactor = 1.5 / 4 * unit.GetBestRanged().Damage;
                else weaponFactor = unit.GetBestMelee().Damage / 4f;
                power += weaponFactor * racePar.PowerAdjustment * Math.Pow(1.2, unit.Level - 1 + effectiveLevelBoost + (unit.GetStatBase(Stat.Leadership) > 0 ? 3 : 0));

            }
            finalPower = count * power;

        }

        return finalPower;
    }

    public static double ArmyPower(List<Unit> units)
    {
        double finalPower = 0;
        if (units != null)
        {
            int count = units.Count;
            double power = 0;

            double effectiveLevelBoost = (units.Where(s => s.GetStat(Stat.Leadership) > 0).FirstOrDefault()?.GetStatBase(Stat.Leadership) ?? 0) / 10 * .5f;
            foreach (Unit unit in units)
            {
                var racePar = RaceParameters.GetTraitData(unit);
                double weaponFactor;
                if (unit.GetBestRanged() != null) weaponFactor = 1.5 / 4 * unit.GetBestRanged().Damage;
                else weaponFactor = unit.GetBestMelee().Damage / 4f;
                power += weaponFactor * racePar.PowerAdjustment * Math.Pow(1.2, unit.Level - 1 + effectiveLevelBoost + (unit.GetStatBase(Stat.Leadership) > 0 ? 3 : 0));
            }
            finalPower = count * power;
        }
        return finalPower;
    }

    /// <summary>
    /// Used for getting the absolute power for a cluster of units, involves no exponents, so it's more of unit value
    /// </summary>
    /// <param name="units"></param>
    /// <returns></returns>
    public static double UnitValue(List<Unit> units)
    {
        double value = 0;
        if (units != null)
        {
            foreach (Unit unit in units)
            {
                var racePar = RaceParameters.GetTraitData(unit);
                double weaponFactor;
                if (unit.GetBestRanged() != null) weaponFactor = 1.5 / 4 * unit.GetBestRanged().Damage;
                else weaponFactor = unit.GetBestMelee().Damage / 4;
                value += weaponFactor * racePar.PowerAdjustment * Math.Pow(1.2, unit.Level - 1 + (unit.GetStatBase(Stat.Leadership) > 0 ? 3 : 0));
            }
        }
        return value;
    }

    internal static Village[] GetUnoccupiedFriendlyVillages(Empire empire)
    {
        Village[] villages = State.World.Villages;
        List<Village> retVillages = new List<Village>();
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (villages[i].Side == empire.Side && villages[i].GetTotalPop() > 8)
            {
                if (IsVillageOccupied(empire, i) == false)
                {
                    retVillages.Add(villages[i]);

                }
            }
        }
        return retVillages.ToArray();
    }

    internal static MercenaryHouse[] GetUnoccupiedMercCamp(Empire empire)
    {
        MercenaryHouse[] mercs = State.World.MercenaryHouses;
        List<MercenaryHouse> retMercs = new List<MercenaryHouse>();
        for (int i = 0; i < State.World.MercenaryHouses.Length; i++)
        {

            if (mercs[i].Mercenaries.Count < 8)
                continue;
            if (ArmyAt(mercs[i].Position) == null)
            {
                retMercs.Add(mercs[i]);

            }
        }
        return retMercs.ToArray();
    }

    internal static bool IsVillageOccupied(Empire empire, int i)
    {
        bool occupied = false;
        Army[] allArmies = StrategicUtilities.GetAllArmies();
        for (int j = 0; j < allArmies.Length; j++)
        {
            if (allArmies[j].Position.Matches(State.World.Villages[i].Position))
            {
                occupied = true;
                break;
            }
        }
        return occupied;
    }

    internal static bool EnemyArmyWithinXTiles(Army army, int tiles, bool includeGoblins = false)
    {
        foreach (Army enemyArmy in GetAllHostileArmies(army.Empire, includeGoblins))
        {
            if (enemyArmy.Position.GetNumberOfMovesDistance(army.Position) <= tiles)
                return true;
        }
        return false;
    }

    internal static bool EnemyArmyWithinXTiles(Village village, int tiles)
    {
        foreach (Army enemyArmy in GetAllHostileArmies(village.Empire))
        {
            if (enemyArmy.Position.GetNumberOfMovesDistance(village.Position) <= tiles)
                return true;
        }
        return false;
    }

    internal static void BuyBasicWeapons(Village village)
    {
        int neededWeapons = Math.Min(village.MaxGarrisonSize, village.GetTotalPop() - 3) - village.Garrison;
        if (neededWeapons <= 0)
            return;
        int maces = neededWeapons / 2;
        int bows = neededWeapons - maces;
        Empire empire = State.World.GetEmpireOfSide(village.Side);
        while (maces > 0 && empire.Gold > 3)
        {
            BuyWeapon(village, ItemType.Mace);
            maces -= 1;
        }
        while (bows > 0 && empire.Gold > 5)
        {
            BuyWeapon(village, ItemType.Bow);
            bows -= 1;
        }

    }

    internal static void BuyWeapon(Village village, ItemType weapon)
    {
        village.BuyWeapon(weapon);
    }

    internal static void SellWeapon(Village village, ItemType weapon)
    {
        village.SellWeapon(weapon);
    }

    internal static bool IsTileClear(Vec2i p)
    {
        return IsTileClear((Vec2)p);
    }

    internal static bool IsTileClear(Vec2 p)
    {
        if (p.x < 0 || p.y < 0 || p.x >= Config.StrategicWorldSizeX || p.y >= Config.StrategicWorldSizeY)
            return false;
        if (StrategicTileInfo.CanWalkInto(p.x, p.y))
        {

            foreach (Army army in GetAllArmies())
            {
                if (army.Position.Matches(p.x, p.y))
                    return false;
            }
            if (GetVillageAt(p) != null)
            {
                return false;
            }

            return true;
        }
        return false;
    }


    internal static void ProcessTravelingUnits(List<Unit> travelingUnits, Army army)
    {
        var loc = StrategyPathfinder.GetPathToClosestObject(null, army.Position, State.World.Villages.Where(s => travelingUnits[0].Side == s.Side).Select(s => s.Position).ToArray(), army.GetMaxMovement(), 999, army.movementMode == Army.MovementMode.Flight);

        int turns = 9999;
        int flightTurns = 9999;
        Vec2i destination = null;
        if (travelingUnits.Where(s => s.Type == UnitType.SpecialMercenary).Any())
            travelingUnits = travelingUnits.Where(s => s.Type != UnitType.SpecialMercenary || s.HasTrait(Traits.Eternal)).ToList();
        if (travelingUnits.Count() == 0)
            return;
        bool flyersExist = travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).Count() > 0;
        if (loc != null && loc.Count > 0)
        {
            destination = new Vec2i(loc.Last().X, loc.Last().Y);
            turns = StrategyPathfinder.TurnsToReach(null, army.Position, destination, army.GetMaxMovement(), false);
            if (flyersExist)
                flightTurns = StrategyPathfinder.TurnsToReach(null, army.Position, destination, army.GetMaxMovement(), true);
        }
        if (turns < 999)
        {
            Village village = GetVillageAt(destination);
            if (village.Side != army.Side)
                Debug.Log("Sent traveling units to someone else's village...");
            if (flyersExist)
                CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).ToList(), village, flightTurns);
            CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder) == false).ToList(), village, turns);
        }


    }


    static internal void CreateInvisibleTravelingArmy(List<Unit> travelingUnits, Village village, int turns)
    {
        if (village.travelers == null)
            village.travelers = new List<InvisibleTravelingUnit>();
        foreach (Unit unit in travelingUnits)
        {
            if (village.travelers.Where(s => s.unit == unit).Any()) //Avoid doubling up
                continue;
            village.travelers.Add(new InvisibleTravelingUnit(unit, turns));
        }
    }

    internal static void ProcessTravelingUnit(Unit travelingUnit, Army army)
    {
        if (travelingUnit.Type == UnitType.SpecialMercenary && travelingUnit.HasTrait(Traits.Eternal) == false)
            return;
        var loc = StrategyPathfinder.GetPathToClosestObject(null, army.Position, State.World.Villages.Where(s => travelingUnit.Side == s.Side).Select(s => s.Position).ToArray(), army.GetMaxMovement(), 999, army.movementMode == Army.MovementMode.Flight);
        int turns = 9999;
        Vec2i destination = null;
        if (loc != null && loc.Count > 0)
        {
            destination = new Vec2i(loc.Last().X, loc.Last().Y);
            turns = StrategyPathfinder.TurnsToReach(null, army.Position, destination, army.GetMaxMovement(), travelingUnit.HasTrait(Traits.Pathfinder));
        }
        if (turns < 999)
            CreateInvisibleTravelingArmy(travelingUnit, GetVillageAt(destination), turns);
    }

    static internal void CreateInvisibleTravelingArmy(Unit travelingUnit, Village village, int turns)
    {
        if (village.travelers == null)
            village.travelers = new List<InvisibleTravelingUnit>();
        if (village.travelers.Where(s => s.unit == travelingUnit).Any()) //Avoid doubling up
            return;
        village.travelers.Add(new InvisibleTravelingUnit(travelingUnit, turns));
    }

    internal static void CheatForceLevelUps(Unit unit, int count)
    {
        Stat[] stats;

        for (int k = 0; k < count; k++)
        {
            stats = unit.GetLevelUpPossibilities(unit.Predator);
            unit.LevelUp(PickBest(unit, stats));
        }
    }

    internal static void SpendLevelUps(Unit unit)
    {
        if (unit.HasEnoughExpToLevelUp() == false)
            return;
        Stat[] stats;

        for (int k = 0; k < 1000; k++)
        {
            stats = unit.GetLevelUpPossibilities(unit.Predator);
            if (unit.HasEnoughExpToLevelUp() == false)
                break;
            unit.LevelUp(PickBest(unit, stats));
        }
    }

    static Stat PickBest(Unit unit, Stat[] stats)
    {

        float[] weight = new float[(int)Stat.None];
        float[] priority = new float[(int)Stat.None];

        switch (unit.AIClass)
        {
            case AIClass.Default:
                if ((unit.BestSuitedForRanged() && unit.FixedGear == false) || unit.GetBestRanged() != null)
                {
                    weight[(int)Stat.Strength] = 0;
                    weight[(int)Stat.Dexterity] = 3;
                    weight[(int)Stat.Voracity] = 2;
                    weight[(int)Stat.Agility] = 2;
                    weight[(int)Stat.Will] = 1.25f;
                    weight[(int)Stat.Mind] = 0f;
                    weight[(int)Stat.Endurance] = 1.5f;
                    weight[(int)Stat.Stomach] = 2;
                    weight[(int)Stat.Leadership] = 0;
                }
                else
                {
                    weight[(int)Stat.Strength] = 3;
                    weight[(int)Stat.Dexterity] = 0;
                    weight[(int)Stat.Voracity] = 3;
                    weight[(int)Stat.Agility] = 3;
                    weight[(int)Stat.Will] = 2;
                    weight[(int)Stat.Mind] = 0;
                    weight[(int)Stat.Endurance] = 2;
                    weight[(int)Stat.Stomach] = 2;
                    weight[(int)Stat.Leadership] = 0;
                }
                break;
            case AIClass.Melee:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 2;
                weight[(int)Stat.Agility] = 3;
                weight[(int)Stat.Will] = 2;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.5f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MeleeVore:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 3;
                weight[(int)Stat.Agility] = 3;
                weight[(int)Stat.Will] = 2;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 2.5f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.Ranged:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 1.25f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 1.25f;
                weight[(int)Stat.Mind] = 0f;
                weight[(int)Stat.Endurance] = 1.5f;
                weight[(int)Stat.Stomach] = .75f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.RangedVore:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 2.25f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 1.25f;
                weight[(int)Stat.Mind] = 0f;
                weight[(int)Stat.Endurance] = 1.5f;
                weight[(int)Stat.Stomach] = 2;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.PureVore:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 4;
                weight[(int)Stat.Agility] = 1;
                weight[(int)Stat.Will] = 1;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 4;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MagicMelee:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 2;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 2.25f;
                weight[(int)Stat.Mind] = 2.75f;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.25f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MagicRanged:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 1.5f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 2.25f;
                weight[(int)Stat.Mind] = 2.75f;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.25f;
                weight[(int)Stat.Leadership] = 0;
                break;
        }

        if (unit.Type == UnitType.Leader && Config.LeadersAutoGainLeadership == false)
        {
            weight[(int)Stat.Leadership] = 4;
        }
        if (unit.AIClass == AIClass.Custom && unit.StatWeights != null)
        {
            for (int i = 0; i < (int)Stat.None; i++)
            {
                weight[i] = unit.StatWeights.Weight[i];
            }
        }
        int highest = 0;
        for (int i = 0; i < priority.Length; i++)
        {
            if (unit.GetStatBase((Stat)i) != 0 && stats.Contains((Stat)i))
                priority[i] = weight[i] / unit.GetStatBase((Stat)i);
            if (priority[i] > priority[highest])
                highest = i;
        }
        return (Stat)highest;

    }

}

