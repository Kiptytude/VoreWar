using OdinSerializer;
using System.Collections.Generic;
using System.Linq;

class GoblinAI : IStrategicAI
{
    [OdinSerialize]
    Empire empire;

    List<PathNode> path;
    Army pathIsFor;

    public GoblinAI(Empire empire)
    {
        this.empire = empire;
    }

    public bool RunAI()
    {
        foreach (Army army in empire.Armies.ToList())
        {
            if (army.RemainingMP < 1)
                continue;
            if (path != null && pathIsFor == army)
            {

                if (path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
                if (StrategicUtilities.ArmyAt(newLoc) != null)
                {
                    path = null;
                    army.RemainingMP = 0;
                    continue;
                }
                Vec2i position = army.Position;
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
        foreach (Army army in empire.Armies)
        {
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
            }
        }
        path = null;
        foreach (Army army in empire.Armies)
        {
            var closeVillages = State.World.Villages.Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 4 && s.GetTotalPop() > 0);
            foreach (Village village in closeVillages)
            {
                var emp = State.World.GetEmpireOfSide(village.Side);
                if (emp != null)
                    emp.AddGold(10);
            }

        }
        return false;
    }

    public bool TurnAI()
    {
        if (Config.GoblinCaravans == false)
        {
            empire.SpendGold(empire.Gold);
            return false;
        }
        bool foundSpot = false;
        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * .6f);
        if (empire.Gold < 10000)
            empire.AddGold(10000);
        double mapFactor = (Config.StrategicWorldSizeX + Config.StrategicWorldSizeY) / 20;

        if (State.Rand.NextDouble() < (mapFactor - empire.Armies.Count) / 10)
        {

            int x = 0;
            int y = 0;

            for (int i = 0; i < 10; i++)
            {
                x = State.Rand.Next(Config.StrategicWorldSizeX);
                y = State.Rand.Next(Config.StrategicWorldSizeY);

                if (StrategicUtilities.IsTileClear(new Vec2i(x, y)))
                {
                    foundSpot = true;
                    break;
                }
            }
            if (foundSpot)
            {
                var army = new Army(empire, new Vec2i(x, y), empire.Side);
                empire.Armies.Add(army);

                int num = 0;
                int average = 0;
                foreach (Empire emp in State.World.MainEmpires.Where(s => s.Side < 100 && s.KnockedOut == false))
                {
                    num++;
                    average += emp.MaxArmySize;
                }
                int count = 0;
                if (num > 0)
                {
                    count = State.Rand.Next(3 * average / num / 4, average / num);
                }
                else
                    count = State.Rand.Next(12, 16);

                army.BountyGoods = new BountyGoods((int)(15 * count * (.75f + State.Rand.NextDouble() / 2)));

                for (int i = 0; i < count; i++)
                {
                    Unit unit = new NPC_unit(1, State.Rand.Next(2) == 0, 2, empire.Side, empire.ReplacedRace, RandXp(baseXp), true);
                    if (State.Rand.Next(4) == 0)
                    {
                        if (unit.Level < 3)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 2), 1);
                        else if (unit.Level < 6)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 3), 1);
                        else if (unit.Level < 9)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 4), 1);
                        else
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(2, 4), 1);
                    }
                    army.Units.Add(unit);
                    StrategicUtilities.SetAIClass(unit);
                }
            }

        }

        foreach (Army army in empire.Armies)
        {
            foreach (Unit unit in army.Units)
            {
                if (unit.Experience < .5f * baseXp)
                {
                    unit.SetExp(3 + (unit.Experience * 1.1f));
                }
                StrategicUtilities.SpendLevelUps(unit);
            }
        }

        return foundSpot;



        int RandXp(int exp)
        {
            if (exp < 1)
                exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
    }

    private void GenerateTaskForArmy(Army army)
    {
        if (army.Destination != null)
        {
            if (army.Position.Matches(army.Destination))
                army.Destination = null;
            else
            {
                SetPath(army, army.Destination);
                return;
            }
        }

        List<int> PreferredSides = new List<int>();
        foreach (var relation in State.World.Relations[empire.Side])
        {
            if (relation.Value.Type == RelationState.Neutral && State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0)
                PreferredSides.Add(relation.Key);
            if (relation.Value.Type == RelationState.Allied && State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0)
            {
                PreferredSides.Add(relation.Key);
                PreferredSides.Add(relation.Key);
            }
        }
        if (PreferredSides.Count == 0)
        {
            foreach (var relation in State.World.Relations[empire.Side])
            {
                if (State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0)
                    PreferredSides.Add(relation.Key);
            }
        }

        var villages = State.World.Villages.Where(s => PreferredSides.Contains(s.Side)).ToArray();
        if (villages.Count() == 0)
        {
            army.RemainingMP = 0;
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            Village village = villages[State.Rand.Next(villages.Length)];
            if (village.GetTotalPop() < 4)
                continue;
            if (MoveToNearVillage(army, village))
                break;
        }

    }

    bool MoveToNearVillage(Army army, Village village)
    {
        int x = 0;
        int y = 0;
        bool foundSpot = false;
        for (int i = 0; i < 10; i++)
        {
            x = village.Position.x + State.Rand.Next(-2, 3);
            y = village.Position.y + State.Rand.Next(-2, 3);
            if (StrategicUtilities.IsTileClear(new Vec2i(x, y)))
            {
                foundSpot = true;
                break;
            }
        }
        if (foundSpot == false)
            return false;
        army.Destination = new Vec2i(x, y);
        return true;
    }

    private void SetPath(Army army, Vec2i targetPosition)
    {
        if (targetPosition != null)
        {
            pathIsFor = army;
            path = StrategyPathfinder.GetMonsterPath(empire, army, targetPosition, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
            return;
        }
        army.RemainingMP = 0;
    }



}

