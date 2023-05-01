using OdinSerializer;
using System.Collections.Generic;
namespace LegacyAI
{
    public class LegacyTacticalAI : ITacticalAI
    {
        [OdinSerialize]
        List<Actor_Unit> actors;
        [OdinSerialize]
        TacticalTileType[,] tiles;

        bool didAction;
        bool foundPath;
        [OdinSerialize]
        readonly int AISide;
        List<PathNode> path;
        Actor_Unit pathIsFor;

        public StandardTacticalAI.RetreatConditions RetreatPlan
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        [OdinSerialize]
        public bool foreignTurn;

        bool ITacticalAI.ForeignTurn
        {
            get
            {
                return foreignTurn;
            }
            set => foreignTurn = value;
        }

        public LegacyTacticalAI(List<Actor_Unit> actors, TacticalTileType[,] tiles, int AIteam)
        {
            AISide = AIteam;
            this.tiles = tiles;
            this.actors = actors;


        }

        public bool RunAI()
        {
            foreach (Actor_Unit actor in actors)
            {
                if (actor.Targetable == true && actor.Unit.Side == AISide && actor.Movement > 0)
                {
                    if (path != null && pathIsFor == actor)
                    {
                        if (path.Count == 0)
                        {
                            path = null;
                            continue;
                        }
                        Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
                        path.RemoveAt(0);
                        if (actor.MoveTo(newLoc, tiles, (State.GameManager.TacticalMode.RunningFriendlyAI ? Config.TacticalFriendlyAIMovementDelay : Config.TacticalAIMovementDelay)) == false)
                        {
                            //Can't move -- most likely a multiple movement point tile when on low MP
                            actor.Movement = 0;
                            path = null;
                            return true;
                        }
                        if (actor.Movement == 1 && IsRanged(actor) && TacticalUtilities.TileContainsMoreThanOneUnit(actor.Position.x, actor.Position.y) == false)
                        {
                            path = null;
                        }
                        else if (path.Count == 0 || actor.Movement == 0)
                        {
                            path = null;
                        }
                        return true;
                    }
                    else
                    {
                        foundPath = false;
                        didAction = false;
                        //do action
                        RunPred(actor);
                        pathIsFor = actor;
                        if (foundPath || didAction) return true;
                        if (IsRanged(actor))
                            RunRanged(actor);
                        else
                            RunMelee(actor);
                        if (foundPath || didAction) return true;
                        //If no path to any targets, will sit out its turn
                        actor.ClearMovement();
                        return true;
                    }
                }
            }

            return false;
        }


        public bool RunPred(Actor_Unit actor)
        {
            if (actor.Unit.Predator == false)
                return false;
            int index = -1;
            int chance = 0;
            float distance = 64;
            float cap = actor.PredatorComponent.FreeCap();
            if (cap >= 8)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    float d = actors[i].Position.GetDistance(actor.Position);
                    if (actors[i].Targetable == true && d < 8)
                    {
                        Actor_Unit unit = actors[i];
                        if (unit.Unit.Side != AISide && unit.Bulk() <= cap)
                        {
                            int c = (int)(100 * unit.GetDevourChance(actor, true));
                            if (c > 50 && c > chance && TacticalUtilities.FreeSpaceAroundTarget(actors[i].Position, actor) && unit.AIAvoidEat <= 0)
                            {
                                chance = c;
                                index = i;
                                distance = d;
                            }



                        }
                    }

                }
                if (index != -1)
                {
                    if (distance < 2)
                    {
                        actor.PredatorComponent.UsePreferredVore(actors[index]);
                        didAction = true;
                        return true;
                    }
                    else if (distance < 8)
                    {
                        return Walkto(actor, actors[index].Position, 8);
                    }


                }
            }
            return false;
        }

        bool Walkto(Actor_Unit actor, Vec2i p)
        {
            path = TacticalPathfinder.GetPath(actor.Position, p, 1, actor);
            if (path == null || path.Count == 0)
            {
                return false;
            }
            foundPath = true;
            return true;
        }

        bool Walkto(Actor_Unit actor, Vec2i p, int maxDistance)
        {
            path = TacticalPathfinder.GetPath(actor.Position, p, 1, actor, maxDistance);
            if (path == null || path.Count == 0)
            {
                return false;
            }
            foundPath = true;
            return true;
        }


        bool RandomWalk(Actor_Unit actor)
        {
            int r = State.Rand.Next(8);
            int d = 8;
            while (!actor.Move(r, tiles))
            {
                r++;
                d--;
                if (r > 7)
                {
                    r = 0;
                }
                if (d < 1)
                {
                    actor.Movement = 0;
                    break;
                }
            }
            didAction = true;
            return true;
        }
        bool RunRanged(Actor_Unit actor)
        {
            float distance = 64;
            int index = -1;
            for (int i = 0; i < actors.Count; i++)
            {
                float d = actors[i].Position.GetNumberOfMovesDistance(actor.Position);
                if (actors[i].Targetable == true)
                {
                    Actor_Unit unit = actors[i];
                    if (unit.Unit.Side != AISide && d < distance && (d > 1 || (actor.BestRanged.Omni && d > 0)))
                    {
                        index = i;
                        distance = d;
                    }
                }
            }
            if (index == -1)
            {
                bool walked = RandomWalk(actor);
                if (walked)
                {
                    didAction = true;
                    return true;
                }
                didAction = true;
                RunMelee(actor); // Surrounded
                actor.ClearMovement();
                return true;
            }
            else
            {
                distance = actors[index].Position.GetDistance(actor.Position);
                if (distance >= actor.Unit.GetBestRanged().Range)
                {
                    return Walkto(actor, actors[index].Position);
                }
                else
                {
                    if (distance < 2)
                    {
                        bool walked = RandomWalk(actor);
                        if (walked)
                        {
                            didAction = true;
                            return true;
                        }
                        didAction = true;
                        RunMelee(actor); // Surrounded
                        actor.ClearMovement();
                        return true;
                    }
                    else
                    {
                        didAction = true;
                        actor.Attack(actors[index], true);
                    }
                    return true;
                }
            }

        }
        bool RunMelee(Actor_Unit actor)
        {
            //move towards closest target
            float distance = 64;
            int index = -1;
            for (int i = 0; i < actors.Count; i++)
            {
                float d = actors[i].Position.GetDistance(actor.Position);
                if (actors[i].Targetable == true)
                {
                    Actor_Unit unit = actors[i];
                    if (unit.Unit.Side != AISide)
                    {
                        if (d < distance)
                        {
                            index = i;
                            distance = d;
                        }
                    }
                }
            }
            if (index == -1)
            {
                bool walked = RandomWalk(actor);
                if (walked)
                {
                    didAction = true;
                    return true;
                }
                didAction = true;
                actor.ClearMovement();
                return true;
            }
            else
            {
                distance = actors[index].Position.GetDistance(actor.Position);
                if (distance < 2)
                {
                    if (actors[index] == null)
                    {
                        actor.ClearMovement();
                        return true;
                    }
                    didAction = true;
                    actor.Attack(actors[index], false);
                    return true;
                }
                else
                {
                    return Walkto(actor, actors[index].Position);
                }
            }

        }




        bool IsRanged(Actor_Unit actor) => actor.Unit.GetBestRanged() != null;


    }
}
