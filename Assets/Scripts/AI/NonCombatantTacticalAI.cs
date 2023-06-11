using System.Collections.Generic;
using System.Linq;

public class NonCombatantTacticalAI : RaceServantTacticalAI
{
    public NonCombatantTacticalAI(List<Actor_Unit> actors, TacticalTileType[,] tiles, int AISide, bool defendingVillage = false) : base(actors, tiles, AISide, defendingVillage)
    {
    }

    protected override void GetNewOrder(Actor_Unit actor)
    {
        foundPath = false;
        didAction = false; // Very important fix: surrounded retreaters sometimes just skipped doing attacks because this was never set to false in or before "fightwithoutmoving"

        path = null;
        List<Actor_Unit> masters = actors.Where(a => RaceAIType.Dict[State.RaceSettings.GetRaceAI(a.Unit.Race)] != typeof(NonCombatantTacticalAI) && !TacticalUtilities.TreatAsHostile(actor, a)).ToList();
        if ((retreating && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.SpecialMercenary && actor.Unit.HasTrait(Traits.Fearless) == false && TacticalUtilities.GetMindControlSide(actor.Unit) == -1 && TacticalUtilities.GetPreferredSide(actor.Unit, AISide, enemySide) == AISide)
            || masters.Count == 0)
        {
            int retreatY;
            if (State.GameManager.TacticalMode.IsDefender(actor) == false)
                retreatY = Config.TacticalSizeY - 1;
            else
                retreatY = 0;
            if (actor.Position.y == retreatY)
            {
                State.GameManager.TacticalMode.AttemptRetreat(actor, true);
                actor.Movement = 0;
                return;
            }
            WalkToYBand(actor, retreatY);
            if (path == null || path.Path.Count == 0)
            {
                actor.Movement = 0;
            }

            return;
        }

        //do action

        int spareMp = CheckActionEconomyOfActorFromPositionWithAP(actor, actor.Position, actor.Movement);
        int thirdMovement = actor.MaxMovement() / 3;
        if (spareMp >= thirdMovement)
        {
            RunBellyRub(actor, spareMp);
            if (path != null)
                return;
            if (didAction) return;
        }

        TryResurrect(actor);

        RunSpells(actor);
        if (path != null)
            return;

        RunBellyRub(actor, actor.Movement);
        if (foundPath || didAction) return;
        //Search for surrendered targets outside of vore range
        //If no path to any targets, will sit out its turn

        actor.ClearMovement();
    }

    protected override List<PotentialTarget> GetListOfPotentialRubTargets(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();

        List<Actor_Unit> masters = actors.Where(a => RaceAIType.Dict[State.RaceSettings.GetRaceAI(a.Unit.Race)] != typeof(NonCombatantTacticalAI) && TacticalUtilities.GetMindControlSide(a.Unit) == -1).ToList();

        foreach (Actor_Unit unit in masters)
        {
            if (unit.Targetable == true && unit.Unit.Predator && !TacticalUtilities.TreatAsHostile(actor, unit) && TacticalUtilities.GetMindControlSide(unit.Unit) == -1 && !unit.Surrendered && unit.PredatorComponent?.PreyCount > 0 && !unit.ReceivedRub)
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

    protected override void RunSpells(Actor_Unit actor)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return;
        var friendlySpells = actor.Unit.UseableSpells.Where(sp => sp != SpellList.Resurrection && sp != SpellList.Reanimate && sp != SpellList.Bind && sp.ManaCost <= actor.Unit.Mana && sp.AcceptibleTargets.Contains(AbilityTargets.Ally)).ToList();

        if (friendlySpells == null || friendlySpells.Any() == false)
            return;

        if (friendlySpells.Any() == false) return;

        Spell spell = friendlySpells[State.Rand.Next(friendlySpells.Count())];

        if ((spell == SpellList.Charm || spell == SpellList.HypnoGas) && TacticalUtilities.GetMindControlSide(actor.Unit) != -1) // Charmed units should not use charm. Trust me.
            return;
        if (spell.ManaCost > actor.Unit.Mana)
            return;

        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible())
            return;

        List<PotentialTarget> targets = GetListOfPotentialSpellTargets(actor, spell, actor.Position);
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= spell.Range.Max)
            {
                if(spell.TryCast(actor, targets[0].actor))
                    didAction = true;
                return;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) <= actor.Movement + spell.Range.Max) //discard the clearly impossible
                {
                    MoveToAndAction(actor, targets[0].actor.Position, spell.Range.Max, actor.Movement, () => spell.TryCast(actor, targets[0].actor));
                    if (foundPath && path.Path.Count() < actor.Movement)
                        return;
                    else
                    {
                        foundPath = false;
                        path = null;
                    }
                }
            }
            targets.RemoveAt(0);
        }
    }

    protected override int CheckActionEconomyOfActorFromPositionWithAP(Actor_Unit actor, Vec2i position, int ap)
    {
        int apRequired = -1;
      
        apRequired = CheckResurrect(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

    
        apRequired = CheckSpells(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        // Everything else is less important than belly rubs.
        return ap;
    }
}
