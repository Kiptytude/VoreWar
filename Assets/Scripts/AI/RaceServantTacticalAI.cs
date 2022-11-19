using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public class RaceServantTacticalAI : HedonistTacticalAI
{
    public RaceServantTacticalAI(List<Actor_Unit> actors, TacticalTileType[,] tiles, int AISide, bool defendingVillage = false) : base(actors, tiles, AISide, defendingVillage)
    {
    }

    protected override List<PotentialTarget> GetListOfPotentialRubTargets(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        Race masterRace = GetStrongestRaceOnBattlefield();

        if (actor.Unit.Race == masterRace) return targets; // Don't serve your own race

        List<Actor_Unit> masters = actors.Where(a => a.Unit.Race == GetStrongestRaceOnBattlefield()).ToList();

        foreach (Actor_Unit unit in masters)
        {
            if (unit.Targetable == true && !TacticalUtilities.TreatAsHostile(actor, unit) && unit.Unit.GetStatusEffect(StatusEffectType.Charmed) == null && !unit.Surrendered && unit.PredatorComponent.PreyCount > 0 && !unit.ReceivedRub)
            {
                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance - 1 + (actor.MaxMovement() / 3) <= moves)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                        continue;
                    targets.Add(new PotentialTarget(unit, 100, distance, 4, 100 - (unit == actor ? 100 - unit.Unit.HealthPct + 10 : 100 - unit.Unit.HealthPct)));
                }

            }
        }
        return targets.OrderByDescending(t => t.utility).ToList();
    }

    private Race GetStrongestRaceOnBattlefield()
    {
        return actors.OrderByDescending(a => RaceParameters.GetRaceTraits(a.Unit.Race).PowerAdjustment).First().Unit.Race;
    }
}
