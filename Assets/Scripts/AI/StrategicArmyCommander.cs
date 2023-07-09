using System;
using System.Collections.Generic;
using System.Linq;

public enum AIMode
{
    Default,
    Sneak,
    Heal,
    Resupply,
    //HuntStrong,
    //HeavyTraining
}


class StrategicArmyCommander
{
    Village[] Villages => State.World.Villages;
    Empire empire;
    int maxArmySize;

    bool smarterAI;

    List<PathNode> path;
    Army pathIsFor;

    internal float StrongestArmyRatio { get; private set; }

    int AISide => empire.Side;

    public StrategicArmyCommander(Empire empire, int maxSize, bool smarterAI)
    {
        this.empire = empire;
        maxArmySize = maxSize;
        this.smarterAI = smarterAI;
    }

    internal void UpdateStrongestArmyRatio()
    {
        if (empire.Armies.Count < 2)
            return;
        var strongestArmy = empire.Armies.OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestArmy == null) return;

        var strongestEnemy = StrategicUtilities.GetAllHostileArmies(empire).OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestEnemy != null)
        {
            StrongestArmyRatio = (float)(strongestArmy.ArmyPower / strongestEnemy.ArmyPower);
        }
    }

    internal void ResetPath() => pathIsFor = null; //If there is only one army, this forces it to generate a new path each turn

    internal bool GiveOrder()
    {
        foreach (Army army in empire.Armies.ToList())
        {
            if (army.RemainingMP < 1)
                continue;
            DevourCheck(army);
            if (army.RemainingMP < 1)
                continue;
            if (path != null && pathIsFor == army)
            {
                if (army.InVillageIndex > -1)
                {
                    UpdateEquipmentAndRecruit(army);
                }
                if (path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
                Vec2i position = army.Position;
#if UNITY_EDITOR
                if (newLoc.GetNumberOfMovesDistance(position) != 1)
                {
                    UnityEngine.Debug.LogWarning($"Army tried to move from {position.x} {position.y} to {newLoc.x} {newLoc.y}");
                }

#endif
                path.RemoveAt(0);

                if (army.MoveTo(newLoc))
                    StrategicUtilities.StartBattle(army);
                else if (position == army.Position)
                    army.RemainingMP = 0; //This prevents the army from wasting time trying to move into a forest with 1 mp repeatedly
                return true;

            }
            else
            {
                GenerateTaskForArmy(army);
                if (path == null || path.Count == 0)
                    army.RemainingMP = 0;
                return true;
            }
        }
        SpendExpAndRecruit(); //At the end of the turn, restock troops
        return false;
    }

    internal void SpendExpAndRecruit()
    {
        foreach (Army army in empire.Armies)
        {
            var infiltrators = new List<Unit>();
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
                if (unit.HasTrait(Traits.Infiltrator) && unit.Type != UnitType.Leader && unit.FixedSide == army.Side)
                    infiltrators.Add(unit);
            }
            infiltrators.ForEach(u => StrategicUtilities.TryInfiltrateRandom(army, u));

            if (army.InVillageIndex > -1)
            {
                UpdateEquipmentAndRecruit(army);
            }
            if (army.AIMode == AIMode.Resupply && army.Units.Count == maxArmySize && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0)
                army.AIMode = AIMode.Default;
        }
    }




    private void GenerateTaskForArmy(Army army)
    {
        path = null;
        pathIsFor = army;
        UpdateArmyStatus(army);
        switch (army.AIMode)
        {
            case AIMode.Default:
                Attack(army, 1.6f);
                break;
            case AIMode.Sneak:
                Attack(army, 0.8f);
                break;
            case AIMode.Heal:
                if (army.InVillageIndex == -1)
                {
                    if (NavigateToFriendlyVillage(army, false))
                        break;
                    Attack(army, 1);
                    break;
                }
                else
                {
                    DevourCheck(army);
                    army.RemainingMP = 0;
                    break;
                }
            case AIMode.Resupply:
                if (empire.Income < 20)
                    army.AIMode = AIMode.Default;

                Village villageArmyIsIn = null;
                if (army.InVillageIndex != -1)
                {
                    villageArmyIsIn = State.World.Villages[army.InVillageIndex];
                }


                if (army.InVillageIndex != -1 && empire.Gold > 4500 && Config.AICanHireSpecialMercs && MercenaryHouse.UniqueMercs.Count > 0 && army.Units.Count() < empire.MaxArmySize && NavigateToMercenaries(army, (int)(3f * army.GetMaxMovement())))
                {
                    break;
                }

                if (army.InVillageIndex != -1 && empire.Gold > 1500 && army.Units.Count() < empire.MaxArmySize && NavigateToMercenaries(army, (int)(2f * army.GetMaxMovement())))
                {
                    break;
                }


                if (army.InVillageIndex != -1 && empire.Gold > 500 && army.Units.Count() < empire.MaxArmySize && NavigateToMercenaries(army, (int)(1f * army.GetMaxMovement())))
                {
                    break;
                }

                MercenaryHouse mercHouseArmyIsIn = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                if (mercHouseArmyIsIn != null)
                {
                    if (Config.AICanHireSpecialMercs)
                    {
                        foreach (var merc in MercenaryHouse.UniqueMercs.OrderByDescending(s => s.Cost))
                        {
                            HireSpecialMerc(army, merc);
                        }
                    }
                    foreach (var merc in mercHouseArmyIsIn.Mercenaries.OrderByDescending(s => s.Unit.Experience / s.Cost))
                    {
                        HireMerc(army, mercHouseArmyIsIn, merc);
                    }
                }

                if (villageArmyIsIn == null || villageArmyIsIn.GetTotalPop() < 12)
                {
                    if (NavigateToFriendlyVillage(army, army.Units.Count != maxArmySize))
                        break;
                    Attack(army, 1);
                    break;
                }
                UpdateEquipmentAndRecruit(army);
                if (army.Units.Count == maxArmySize && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0)
                    army.AIMode = AIMode.Default;
                else
                    army.RemainingMP = 0;
                break;
                //       case AIMode.HuntStrong:
                //           {
                //               AttackStrongestArmy(army);
                //               break;
                //           }
                //       case AIMode.HeavyTraining:
                //           {
                //if (army.InVillageIndex > -1)
                //{
                //	Village trainingVillage = State.World.Villages[army.InVillageIndex];
                //	var maxTrainLevel = trainingVillage.NetBoosts.MaximumTrainingLevelAdd;
                //	if (trainingVillage != null && maxTrainLevel > 0)
                //	{
                //		Train(army);
                //		double highestPower = 0;
                //		foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
                //		{
                //			double p = hostileArmy.ArmyPower;
                //			if (p > highestPower)
                //			{
                //				highestPower = p;
                //			}

                //		}
                //		if (highestPower * 1.3f + 256 < army.ArmyPower)
                //			army.AIMode = AIMode.HuntStrong;
                //	}
                //	else
                //		NavigateToTrainArmy(army);
                //}
                //               break;
                //           }
        }
    }

    void HireSpecialMerc(Army army, MercenaryContainer merc)
    {
        if (empire.Gold >= merc.Cost * 2)
        {
            if (army.Units.Count < army.MaxSize)
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(merc.Cost);
                MercenaryHouse.UniqueMercs.Remove(merc);
            }
        }
    }

    void HireMerc(Army army, MercenaryHouse house, MercenaryContainer merc)
    {
        if (empire.Gold >= merc.Cost)
        {
            if (army.Units.Count < army.MaxSize)
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(merc.Cost);
                house.Mercenaries.Remove(merc);
                MercenaryHouse.UniqueMercs.Remove(merc);
            }
        }
    }

    //void Train(Army army)
    //{
    //    if (army.RemainingMP > 0)
    //    {
    //        for (int i = 5 - 1; i >= 0; i--)
    //        {
    //var trainingCost = army.TrainingGetCost(i);
    //            if (empire.Gold > trainingCost)
    //            {
    //                State.World.Stats.SpentGoldOnArmyTraining(trainingCost, empire.Side);
    //                army.Train(i);
    //                army.RemainingMP = 0;
    //                return;
    //            }
    //        }

    //    }
    //}

    //void NavigateToTrainArmy(Army army)
    //{
    //    Vec2i[] locations = State.World.Villages.Where(s => s.Side == army.Side && s.NetBoosts.MaximumTrainingLevelAdd > 0).Select(s => s.Position).ToArray();
    //    if (locations != null && locations.Length > 0)
    //    {
    //        SetClosestPath(army, locations);
    //    }
    //    else
    //    {
    //        army.AIMode = AIMode.Sneak;
    //        return;
    //    }


    //}

    //void AttackStrongestArmy(Army army)
    //{
    //    Vec2i targetPosition = null;
    //    double highestPower = 0;
    //    foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
    //    {
    //        double p = hostileArmy.ArmyPower;
    //        if (p > highestPower)
    //        {
    //            highestPower = p;
    //            targetPosition = hostileArmy.Position;
    //        }
    //        if (p > army.ArmyPower * 1.1f)
    //            army.AIMode = AIMode.HeavyTraining;
    //    }

    //    SetPath(army, targetPosition);
    //}

    void Attack(Army army, float MaxDefenderStrength)
    {
        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(empire).Where(s => s.ArmyPower > 2 * army.ArmyPower).Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 4 && !s.Units.All(u => u.HasTrait(Traits.Infiltrator))))
        {
            Vec2i[] closeVillagePositions = Villages.Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 7 && StrategicUtilities.ArmyAt(s.Position) == null).Select(s => s.Position).ToArray();
            if (closeVillagePositions != null && closeVillagePositions.Length > 0)
            {
                int oldMp = army.RemainingMP; //If there's no close town, then ignore it, instead of eating remaining MP
                SetClosestPath(army, closeVillagePositions, 6);
                if (path != null)
                    return;
                army.RemainingMP = oldMp;
            }

        }

        Vec2i capitalPosition = empire.CapitalCity?.Position ?? army.Position; //Shouldn't really ever be null, but just in case

        List<Vec2i> potentialTargets = new List<Vec2i>();
        List<int> potentialTargetValue = new List<int>();

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (Villages[i].Empire.IsEnemy(empire))
            {
                if (StrategicUtilities.TileThreat(Villages[i].Position) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                {
                    potentialTargets.Add(Villages[i].Position);
                    int value = Villages[i].Race == empire.ReplacedRace ? 45 : ((State.World.GetEmpireOfRace(Villages[i].Race)?.IsAlly(empire) ?? false) ? 40 : 35);
                    if (Villages[i].GetTotalPop() == 0)
                        value = 30;
                    value -= Villages[i].Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                    potentialTargetValue.Add(value);
                }
            }
        }

        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(empire))
        {
            if (!hostileArmy.Units.All(u => u.HasTrait(Traits.Infiltrator)) && StrategicUtilities.ArmyPower(hostileArmy) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army) && hostileArmy.InVillageIndex == -1)
            {
                potentialTargets.Add(hostileArmy.Position);
                if (hostileArmy.Side >= 100 || hostileArmy.Side == (int)Race.Goblins) //If Monster
                    potentialTargetValue.Add(12);
                else
                    potentialTargetValue.Add(42 - hostileArmy.Position.GetNumberOfMovesDistance(capitalPosition) / 3);
            }
        }

        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable.Owner == null || empire.IsEnemy(claimable.Owner))
            {
                Army defender = StrategicUtilities.ArmyAt(claimable.Position);
                if (defender != null && StrategicUtilities.ArmyPower(defender) > MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                    continue;
                potentialTargets.Add(claimable.Position);
                int value = 38;
                value -= claimable.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                potentialTargetValue.Add(value);
            }
        }

        SetClosestPathWithPriority(army, potentialTargets.ToArray(), potentialTargetValue.ToArray());
    }



    void UpdateArmyStatus(Army army)
    {
        var healthPct = army.GetHealthPercentage();
        if (healthPct < 60)
            army.AIMode = AIMode.Heal;

        if (army.InVillageIndex != -1 && healthPct < 80)
            army.AIMode = AIMode.Heal;

        if (army.AIMode == AIMode.Heal && healthPct > 95)
            if ((army.InVillageIndex > -1 && StrategicUtilities.NumberOfDesiredUpgrades(army) > 0) == false)
                army.AIMode = AIMode.Default;

        float need = 32 * (((float)maxArmySize - army.Units.Count()) / maxArmySize) + StrategicUtilities.NumberOfDesiredUpgrades(army);
        if (need > 4 && empire.Gold >= 40 && empire.Income > 25)
        {
            var path = StrategyPathfinder.GetPathToClosestObject(empire, army, Villages.Where(s => s.Side == army.Side).Select(s => s.Position).ToArray(), army.RemainingMP, 5, army.movementMode == Army.MovementMode.Flight);
            if (path != null && path.Count() < need / 2)
                army.AIMode = AIMode.Resupply;

        }


    }

    bool NavigateToMercenaries(Army army, int maxRange)
    {
        Vec2i[] mercPositions = StrategicUtilities.GetUnoccupiedMercCamp(empire).Select(s => s.Position).ToArray();
        if (mercPositions == null || mercPositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, mercPositions, maxRange);
            if (path == null)
                return false;
            return true;
        }

    }


    bool NavigateToFriendlyVillage(Army army, bool canRecruitFrom)
    {
        Vec2i[] villagePositions = StrategicUtilities.GetUnoccupiedFriendlyVillages(empire).Select(s => s.Position).ToArray();
        if (villagePositions == null || villagePositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, villagePositions);
            return true;
        }

    }

    void DevourCheck(Army army)
    {
        if (army.GetHealthPercentage() > 88)
            return;
        if (army.Units.Where(s => s.Predator && 100 * s.HealthPct < 70).Any() == false)
            return;
        if (army.RemainingMP < 1)
            return;
        if (army.InVillageIndex > -1)
        {
            Village village = State.World.Villages[army.InVillageIndex];
            int range;
            int minimumheal;
            //Could check the relative strength but it's probably fine for now.
            if (village.Empire.IsAlly(empire))
            {
                minimumheal = 7;
                range = 8;
                if (village.GetTotalPop() < 22)
                {
                    minimumheal = 9;
                }
            }
            else
            {
                minimumheal = 2;
                range = 20;
            }

            if (StrategicUtilities.EnemyArmyWithinXTiles(army, range))
            {
                State.GameManager.StrategyMode.Devour(army, Math.Min(army.GetDevourmentCapacity(minimumheal), village.GetTotalPop() - 3)); //Don't completely devour villages
            }

        }
    }

    private void UpdateEquipmentAndRecruit(Army army)
    {
        Village village = State.World.Villages[army.InVillageIndex];
        army.ItemStock.SellAllWeaponsAndAccessories(empire);
        StrategicUtilities.UpgradeUnitsIfAtLeastLevel(army, village, 4);
        if (army.Units.Count != maxArmySize)
        {
            int goldPerTroop = empire.Gold / (maxArmySize - army.Units.Count());
            for (int i = 0; i < maxArmySize; i++)
            {
                if (smarterAI && empire.Gold > 40)
                    RecruitUnitAndEquip(army, village, 2);
                else if (goldPerTroop > 40 && army.Units.Count < maxArmySize && village.GetTotalPop() > 3 && empire.Income > 15)
                    RecruitUnitAndEquip(army, village, 2);
                else if (empire.Gold > 16 && army.Units.Count < maxArmySize && village.GetTotalPop() > 3 && empire.Income > 5)
                    RecruitUnitAndEquip(army, village, 1);
                else
                    break;
            }
            if (army.AIMode == AIMode.Resupply && army.Units.Count() == maxArmySize)
                army.AIMode = AIMode.Default;

        }
    }


    internal Unit RecruitUnitAndEquip(Army army, Village village, int tier)
    {
        if (village.GetTotalPop() < 4)
            return null;
        if (army.Units.Count >= army.MaxSize)
            return null;
        if (empire.Leader?.Health <= 0)
            return ResurrectLeader(army, village);
        if (tier == 2 && empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost)
            return null;
        if (tier == 1 && empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.Bow).Cost)
            return null;
        Unit unit = village.RecruitAIUnit(empire, army);
        if (unit == null) //Catches army size
            return null;
        if (unit.HasTrait(Traits.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
        {
            unit.OnDiscard = () =>
            {
                village.VillagePopulation.AddHireable(unit);
                UnityEngine.Debug.Log(unit.Name + " is returning to " +  village.Name);
            };
        }
        if (unit.FixedGear == false)
        {
            if (unit.HasTrait(Traits.Feral))
            {
                Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Gauntlet));
            }
            else if (unit.Items[0] == null)
            {
                if (tier == 1)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Bow));
                    else
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Mace));
                }
                else if (tier == 2)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                    else
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                }
            }
        }

        StrategicUtilities.SetAIClass(unit, .1f);

        StrategicUtilities.SpendLevelUps(unit);
        army.RefreshMovementMode();
        return unit;
    }

    Unit ResurrectLeader(Army army, Village village)
    {
        empire.SpendGold(100);
        empire.Leader.Side = AISide;
        empire.Leader.FixedSide = AISide;
        empire.Leader.Type = UnitType.Leader;
        empire.Leader.LeaderLevelDown();
        empire.Leader.Health = empire.Leader.MaxHealth;
        if (village.GetStartingXp() > empire.Leader.Experience)
        {
            empire.Leader.SetExp(village.GetStartingXp());
            StrategicUtilities.SpendLevelUps(empire.Leader);

        }
        army.Units.Add(empire.Leader);
        army.RefreshMovementMode();
        State.World.Stats.ResurrectedLeader(empire.Side);
        if (Config.LeadersRerandomizeOnDeath)
        {
            empire.Leader.TotalRandomizeAppearance();
            empire.Leader.ReloadTraits();
            empire.Leader.InitializeTraits();
        }

        return empire.Leader;
    }

    private void SetPath(Army army, Vec2i targetPosition, int maxDistance)
    {
        if (targetPosition != null)
        {
            path = StrategyPathfinder.GetPath(empire, army, targetPosition, army.RemainingMP, army.movementMode == Army.MovementMode.Flight, maxDistance);
            return;
        }
        army.RemainingMP = 0;
    }

    private void SetClosestPath(Army army, Vec2i[] targetPositions, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            path = StrategyPathfinder.GetPathToClosestObject(empire, army, targetPositions, army.RemainingMP, maxDistance, army.movementMode == Army.MovementMode.Flight);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0], maxDistance);
        }
        else
            army.RemainingMP = 0;
    }

    private void SetClosestPathWithPriority(Army army, Vec2i[] targetPositions, int[] targetPriorities, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            path = StrategyPathfinder.GetPathToClosestObject(empire, army, targetPositions, army.RemainingMP, maxDistance, army.movementMode == Army.MovementMode.Flight, targetPriorities);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0], maxDistance);
        }
        else
            army.RemainingMP = 0;
    }

}

