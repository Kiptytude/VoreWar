using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyAI
{
    public class LegacyStrategicAI : IStrategicAI
    {
        List<Empire> Empires => State.World.AllActiveEmpires;
        [OdinSerialize]
        int AISide;
        [OdinSerialize]
        int tension = 3;

        List<PathNode> path;
        Army pathIsFor;

        [OdinSerialize]
        int freegold;
        [OdinSerialize]
        float growth;

        private Empire _empire;

        public Empire Empire
        {
            get
            {
                if (_empire == null)
                    _empire = State.World.GetEmpireOfSide(AISide);
                return _empire;
            }
            set { _empire = value; }
        }


        public LegacyStrategicAI(int AISide)
        {
            this.AISide = AISide;
            growth = 2.2F;
            freegold = 0;
        }



        public void RaiseTension(int tension)
        {
            tension += Math.Max(tension, 1);
        }

        public bool RunAI()
        {
            foreach (Army army in Empire.Armies.ToList())
            {
                if (army.RemainingMP < 1)
                    continue;

                if (path != null && pathIsFor == army)
                {
                    if (path.Count == 0)
                    {
                        army.RemainingMP = 0;
                        return true;
                    }

                    Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
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
                    pathIsFor = army;
                    if (ArmyReady(army))
                    {
                        if (army.AIMode == AIMode.Default)
                        {
                            return Attack(army);
                        }
                        else
                        {
                            return Sneak(army);
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool Sneak(Army army)
        {
            float distance = 99;

            Vec2i p = null;
            //find nearest enemy village
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Empire.IsEnemy(Empire))
                {
                    float d = State.World.Villages[i].Position.GetDistance(army.Position) + State.World.Villages[i].Garrison;
                    if (d < distance)
                    {
                        p = State.World.Villages[i].Position;
                        distance = d;
                    }
                }
            }
            int hp = army.GetAbsHealth();
            Army[] hostileArmies = StrategicUtilities.GetAllHostileArmies(Empire);
            for (int i = 0; i < hostileArmies.Length; i++)
            {

                float d = hostileArmies[i].Position.GetDistance(army.Position);
                if (d < distance && hp > hostileArmies[i].GetAbsHealth())
                {
                    distance = d;
                    p = hostileArmies[i].Position;
                }

            }

            //move towards it
            if (p != null)
            {
                path = StrategyPathfinder.GetPath(Empire, army, p, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
                if (path == null)
                    army.RemainingMP = 0;

                return true;
            }
            return false;
        }

        bool Attack(Army army)
        {
            float distance = 99;
            Vec2i p = null;
            //find nearest enemy village
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Empire.IsEnemy(Empire))
                {
                    float d = State.World.Villages[i].Position.GetDistance(army.Position);
                    if (d < distance)
                    {
                        p = State.World.Villages[i].Position;
                        distance = d;
                    }
                }
            }
            foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(Empire))
            {
                float d = hostileArmy.Position.GetNumberOfMovesDistance(army.Position);
                if (d < distance)
                {
                    distance = d;
                    p = hostileArmy.Position;
                }
            }

            //move towards it
            if (p != null)
            {
                path = StrategyPathfinder.GetPath(Empire, army, p, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);

                if (path == null)
                    army.RemainingMP = 0;
                return true;
            }
            return false;
        }



        bool ArmyReady(Army army)
        {
            Empire empire = Empires[AISide];
            //check army health
            if (army.GetHealthPercentage() > 50)
            {
                return true;
            }
            //check if we're on a village
            if (army.InVillageIndex > -1)
            {
                army.RemainingMP = 0;
                //check to perform devouring later


                return false;
            }
            else
            {
                Village village = GetNearestVillage(empire, army.Position);
                if (village == null)
                {
                    army.RemainingMP = 0;
                    return false;
                }
                else
                {
                    path = StrategyPathfinder.GetPath(Empire, army, village.Position, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
                    return false;
                }
            }

        }

        public bool TurnAI()
        {
            foreach (Army army in Empires[AISide].Armies)
            {
                foreach (Unit unit in army.Units)
                {
                    SpendLevelUps(unit);
                }
            }
            RaiseTension(1);
            Empire empire = Empires[AISide];
            empire.AddGold(freegold);
            if (empire.Income > 0)
            {
                for (int i = 0; i < State.World.Villages.Length; i++)
                {
                    if (State.World.Villages[i].Side == AISide)
                    {
                        StrategicUtilities.BuyBasicWeapons(State.World.Villages[i]);
                    }
                }
            }

            if (empire.Gold > 100 && empire.Armies.Count() < Config.MaxArmies)
            {

                Village v = GetVillage(empire);
                if (v != null)
                {
                    Army army = MakeArmy(v);
                    if (army != null)
                    {
                        empire.Armies.Add(army);
                        empire.SpendGold(10 * army.Units.Count);
                        State.World.Stats.SoldiersRecruited(army.Units.Count, empire.Side);
                        return true;
                    }
                }
            }
            return false;
        }

        Village GetVillage(Empire empire)
        {
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Side == empire.Side)
                {
                    //check village isn't occupied by an existing army
                    bool occupied = false;
                    for (int j = 0; j < empire.Armies.Count; j++)
                    {
                        if (empire.Armies[j] != null)
                        {
                            if (empire.Armies[j].Position.Matches(State.World.Villages[i].Position))
                            {
                                occupied = true;
                                break;
                            }
                        }
                    }
                    if (occupied == false)
                    {
                        return State.World.Villages[i];
                    }

                }
            }
            return null;
        }

        Village GetNearestVillage(Empire empire, Vec2i p)
        {
            float distance = 99;
            Village village = null;
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Side == AISide && (int)State.World.Villages[i].Race == AISide)
                {
                    //check village isn't occupied by an existing army
                    bool occupied = false;
                    for (int j = 0; j < empire.Armies.Count; j++)
                    {
                        if (empire.Armies[j].Position.x == State.World.Villages[i].Position.x &&
                            empire.Armies[j].Position.y == State.World.Villages[i].Position.y)
                        {
                            occupied = true;
                            break;
                        }
                    }
                    if (occupied == false)
                    {
                        float d = State.World.Villages[i].Position.GetDistance(p);
                        if (village == null)
                        {
                            village = State.World.Villages[i];
                        }
                        else if (d < distance)
                        {
                            distance = d;
                            village = State.World.Villages[i];
                        }

                    }

                }
            }
            return village;
        }

        void SpendLevelUps(Unit unit)
        {
            for (int i = 0; i < 10; i++)
            {
                if (!unit.HasEnoughExpToLevelUp())
                    return;
                Stat[] stats = unit.GetLevelUpPossibilities(unit.Predator);
                Stat badStat = unit.BestSuitedForRanged() ? Stat.Strength : Stat.Dexterity;
                Stat chosenStat;
                if (stats[0] != badStat) chosenStat = stats[0];
                else chosenStat = stats[1];
                unit.LevelUp(chosenStat);
            }
            return;
        }

        Army MakeArmy(Village village)
        {
            Army army = new Army(Empire, new Vec2i(village.Position.x, village.Position.y), village.Side);
            int size = State.Rand.Next(6) + 1;
            float s = tension;
            s = s / growth;
            size = (int)(size + s);

            if (size > Empires[AISide].MaxArmySize)
            {
                size = Empires[AISide].MaxArmySize;
            }
            //int level = (int)(tension / 5 / growth);
            for (int i = 0; i < size; i++)
            {
                Unit unit = new Unit(village.Side, village.Race, Empire.StartingXP, State.World.GetEmpireOfRace(village.Race)?.CanVore ?? true);
                if (unit.HasWeapon == false)
                {
                    if (unit.BestSuitedForRanged())
                    {
                        unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Bow), 0);
                        //if (tension > 10)
                        //{
                        //    LevelArcher(1 + (level), unit);
                        //}
                    }
                    else
                    {
                        unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Mace), 0);
                        //if (tension > 10)
                        //{
                        //    LevelMelee(1 + (level), unit);
                        //}
                    }
                }

                army.Units.Add(unit);
            }
            army.RemainingMP = 0;
            if (State.Rand.Next(4) == 1)
                army.AIMode = AIMode.Sneak;
            return army;
        }

        void LevelMelee(int levels, Unit unit)
        {

            if (levels > 1)
            {
                int r = State.Rand.Next(2);
                switch (r)
                {
                    case 0:
                        unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Shoes), 1);
                        break;

                    case 1:
                        unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Helmet), 1);
                        break;

                }


            }
            if (levels > 5)
            {
                unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Axe), 0);
            }
            for (int i = 0; i < levels; i++)
            {
                int r = State.Rand.Next(7);
                switch (r)
                {
                    case 0:
                        unit.LevelUp(Stat.Strength);
                        break;
                    case 1:
                        unit.LevelUp(Stat.Will);
                        break;
                    case 2:
                        unit.LevelUp(Stat.Endurance);
                        break;
                    case 3:
                        unit.LevelUp(Stat.Voracity);
                        break;
                    case 4:
                        unit.LevelUp(Stat.Mind);
                        break;
                    case 5:
                        unit.LevelUp(Stat.Agility);
                        break;
                    case 6:
                        unit.LevelUp(Stat.Stomach);
                        break;
                }
            }
        }

        void LevelArcher(int levels, Unit unit)
        {

            if (levels > 2)
            {
                unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gloves), 1);
            }
            if (levels > 5)
            {
                unit.SetItem(State.World.ItemRepository.GetItem(ItemType.CompoundBow), 0);
            }
            for (int i = 0; i < levels; i++)
            {
                int r = State.Rand.Next(7);
                switch (r)
                {
                    case 0:
                        unit.LevelUp(Stat.Dexterity);
                        break;
                    case 1:
                        unit.LevelUp(Stat.Will);
                        break;
                    case 2:
                        unit.LevelUp(Stat.Endurance);
                        break;
                    case 3:
                        unit.LevelUp(Stat.Voracity);
                        break;
                    case 4:
                        unit.LevelUp(Stat.Dexterity);
                        break;
                    case 5:
                        unit.LevelUp(Stat.Agility);
                        break;
                    case 6:
                        unit.LevelUp(Stat.Stomach);
                        break;
                }
            }
        }
    }
}
