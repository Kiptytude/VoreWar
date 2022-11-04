using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum PreyLocation
{
    breasts,
    balls,
    stomach,
    stomach2,
    womb,
    tail,
    anal,
    leftBreast,
    rightBreast
}

public class PredatorComponent
{
    [OdinSerialize]
    List<Prey> prey;
    [OdinSerialize]
    List<Prey> womb;
    [OdinSerialize]
    List<Prey> breasts;
    [OdinSerialize]
    List<Prey> leftBreast;
    [OdinSerialize]
    List<Prey> rightBreast;
    [OdinSerialize]
    List<Prey> balls;
    [OdinSerialize]
    List<Prey> stomach;
    [OdinSerialize]
    List<Prey> stomach2;
    [OdinSerialize]
    List<Prey> tail;

    Transition StomachTransition;
    Transition BallsTransition;
    Transition LeftBreastTransition;
    Transition RightBreastTransition;

    public static PreyLocation[] preyLocationOrder =
    {
        PreyLocation.stomach,
        PreyLocation.stomach2,
        PreyLocation.womb,
        PreyLocation.balls,
        PreyLocation.breasts,
        PreyLocation.leftBreast,
        PreyLocation.rightBreast,
        PreyLocation.tail,
        PreyLocation.anal
    };

    internal struct Transition
    {
        internal float transitionTime;
        internal float transitionLength;
        internal float transitionStart;
        internal float transitionEnd;

        public Transition(float transitionLength, float transitionStart, float transitionEnd)
        {
            transitionTime = 0;
            this.transitionLength = transitionLength;
            this.transitionStart = transitionStart;
            this.transitionEnd = transitionEnd;
        }
    }

    [OdinSerialize]
    public float VisibleFullness { get; set; }
    [OdinSerialize]
    public float Fullness { get; set; }
    [OdinSerialize]
    public float BreastFullness { get; set; }
    [OdinSerialize]
    public float BallsFullness { get; set; }
    [OdinSerialize]
    public float TailFullness { get; set; }
    [OdinSerialize]
    public float Stomach2ndFullness { get; set; } // A second stomach fullness used for units with the trait DualStomach.
    [OdinSerialize]
    public float CombinedStomachFullness { get; set; } // Used when DualStomach unit has no separate sprites for second one or they are turned off.
    [OdinSerialize]
    public float LeftBreastFullness { get; set; }
    [OdinSerialize]
    public float RightBreastFullness { get; set; }
    /// <summary>
    /// Only includes the stomach, and not the womb
    /// </summary>
    [OdinSerialize]
    public float ExclusiveStomachFullness { get; set; }
    /// <summary>
    /// Only includes the womb and not the stomach
    /// </summary>
    [OdinSerialize]
    public float WombFullness { get; set; }
    [OdinSerialize]
    Actor_Unit actor;
    [OdinSerialize]
    Unit unit;
    [OdinSerialize]
    public int birthStatBoost;


    public int AlivePrey { get; set; }

    // lumps prey in similar locations together

    public int PreyNearLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.balls:
                foreach (Prey unit in balls)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach:
            case PreyLocation.stomach2:
            case PreyLocation.womb:
            case PreyLocation.anal:
            case PreyLocation.tail:
                foreach (Prey unit in stomach)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                foreach (Prey unit in stomach2)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                foreach (Prey unit in womb)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.leftBreast:
                foreach (Prey unit in leftBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.rightBreast:
                foreach (Prey unit in rightBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.breasts:
                foreach (Prey unit in breasts)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
        }
        return prey;
    }
    public int PreyInLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.balls:
                foreach (Prey unit in balls)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach:
            case PreyLocation.anal:
                foreach (Prey unit in stomach)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach2:
                foreach (Prey unit in stomach2)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.tail:
                foreach (Prey unit in tail)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.womb:
                foreach (Prey unit in womb)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.leftBreast:
                foreach (Prey unit in leftBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.rightBreast:
                foreach (Prey unit in rightBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.breasts:
                foreach (Prey unit in breasts)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
        }
        return prey;
    }


    internal void UpdateAlivePrey()
    {
        AlivePrey = 0;
        foreach (Prey unit in prey)
        {
            if (unit.Unit.IsDead == false)
                AlivePrey += 1;
        }
    }

    internal List<Prey> GetDirectPrey()
    {
        return prey;
    }

    /// <summary>
    /// Gets all prey in this unit, including all sub prey and on down
    /// </summary>
    internal List<Prey> GetAllPrey()
    {
        List<Prey> allPrey = new List<Prey>();
        List<Prey> tempPrey = new List<Prey>();
        tempPrey.AddRange(prey);
        int counter = 0;
        while (tempPrey.Count > 0)
        {
            counter++;
            if (counter > 200)
            {
                Debug.LogWarning("Error Gathering all prey");
                break;
            }
            if (tempPrey[0].SubPrey != null)
                tempPrey.AddRange(tempPrey[0].SubPrey);
            allPrey.Add(tempPrey[0]);
            tempPrey.Remove(tempPrey[0]);
        }
        return allPrey;
    }

    public PredatorComponent(Actor_Unit actor, Unit unit)
    {
        this.actor = actor;
        this.unit = unit;
        Fullness = 0.0F;
        prey = new List<Prey>();
        womb = new List<Prey>();
        breasts = new List<Prey>();
        balls = new List<Prey>();
        stomach = new List<Prey>();
        stomach2 = new List<Prey>();
        tail = new List<Prey>();
        leftBreast = new List<Prey>();
        rightBreast = new List<Prey>();
        birthStatBoost = 0;
    }

    public void UpdateVersion()
    {
        if (tail == null)
            tail = new List<Prey>();
        if (stomach2 == null)
            stomach2 = new List<Prey>();
        if (leftBreast == null)
            leftBreast = new List<Prey>();
        if (rightBreast == null)
            rightBreast = new List<Prey>();
    }

    public int PreyCount => prey.Count;

    public int DigestingUnitCount
    {
        get
        {
            if (unit.HasTrait(Traits.Endosoma))
                return prey.Where(s => s.Unit.Side != unit.Side || s.Unit.IsDead).Count();
            return prey.Count;
        }
    }

    public bool OnlyOnePreyAndLiving() => prey.Count == 1 && prey[0].Unit.IsDead == false;

    public bool IsActorInPrey(Actor_Unit actor)
    {
        foreach (Prey p in prey)
        {
            if (p.Actor == actor)
                return true;
        }
        return false;
    }
    /// <summary>
    /// Checks if a specific Actor_Unit is in the specified part of the predator.
    /// </summary>
    /// <param name="unit">The Actor_Unit being looked for.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitInPrey(Actor_Unit unit, params PreyLocation[] locations)
    {
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
                if (p.Actor == unit) return true;

        return false;
    }

    /// <summary>
    /// Used to check for whether a unit of specified race is present in eaten prey.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <returns></returns>
    //public bool IsUnitOfSpecificationInPrey(Race race)
    //{
    //    foreach (Prey p in prey)
    //    {
    //        if (p.Unit.Race == race)
    //            return true;
    //    }
    //    return false;
    //}

    /// <summary>
    /// Used to check for whether a unit of specified race is present in eaten prey and either alive or dead depending on the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        foreach (Prey p in prey)
        {
            if (p.Unit.Race == race)
                if (p.Unit.IsDead != alive)
                    return true;
        }
        return false;
    }

    /// <summary>
    /// Used to check for whether a unit of specified race is present inside specified part of the pred and either alive or dead depending on the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        return false;
    }

    public bool IsUnitOfSpecificationInPrey(Race race, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        return false;
    }

    public void FreeAnyAlivePrey()
    {
        List<Prey> preyUnits = new List<Prey>();
        preyUnits.AddRange(prey);
        while (preyUnits.Any())
        {
            if (preyUnits[0].Unit.IsDead == false)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterFreed(unit.Side);
                TacticalUtilities.Log.RegisterFreed(unit, preyUnits[0].Unit, Location(preyUnits[0]));
                preyUnits[0].Predator.PredatorComponent.FreePrey(preyUnits[0], true);
                preyUnits.RemoveAt(0);
            }
            else
            {
                if (preyUnits[0].SubPrey != null)
                    foreach (Prey subUnit in preyUnits[0].SubPrey)
                    {
                        preyUnits.Add(subUnit);
                    }
                preyUnits.RemoveAt(0);
            }
        }
    }

    internal void PurgePrey()
    {
        FreeAnyAlivePrey();
        prey.Clear();
        womb.Clear();
        breasts.Clear();
        leftBreast.Clear();
        rightBreast.Clear();
        balls.Clear();
        stomach.Clear();
        stomach2.Clear();
        tail.Clear();
    }

    public Vec2i GetCurrentLocation()
    {
        Actor_Unit located = actor;
        for (int i = 0; i < 200; i++)
        {
            Actor_Unit newActor = TacticalUtilities.FindPredator(located);
            if (newActor != null)
            {
                located = newActor;
            }
            else
                break;
        }
        return located?.Position;
    }

    public float GetBulkOfPrey(int count = 0)
    {
        count++;
        if (count > 300)
        {
            Debug.LogWarning("Infinite prey chain seemingly detected, handling it but you should check out the source");
            return 0;
        }

        float ret = 0;
        for (int i = 0; i < prey.Count; i++)
        {
            if (prey[i].Unit == actor.Unit)
            {
                Debug.Log("Prey inside of itself!");
                FreePrey(prey[i], true);
                continue;
            }
            ret += prey[i].Actor.Bulk(count);
        }
        return ret;
    }

    public float TotalCapacity()
    {
        float c = State.RaceSettings.GetStomachSize(unit.Race);

        c *= unit.GetStat(Stat.Stomach) / 12f * unit.TraitBoosts.CapacityMult;

        //c *= unit.GetScale(3);  It may be more realistic, but having huge resulting in 81 times the base stomach capacity is just overkill

        return c;
    }

    public float FreeCap()
    {
        float totalBulk = 0;
        for (int i = 0; i < prey.Count; i++)
        {
            totalBulk += prey[i].Actor.Bulk();
        }

        return TotalCapacity() - totalBulk;
    }

    /// <summary>
    /// Designed to be used for manual regurgitation.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    internal bool FreeRandomPreyToSquare(Vec2i location)
    {
        if (actor.Position.GetNumberOfMovesDistance(location) > 1)
            return false;
        if (actor.Movement == 0)
            return false;
        if (actor.PredatorComponent == null)
            return false;
        var alives = prey.Where(s => s.Unit.IsDead == false && s.Unit.Side == unit.Side).ToArray();
        if (alives.Length == 0)
            alives = prey.Where(s => s.Unit.IsDead == false).ToArray();
        if (alives.Length == 0)
            return false;
        var target = alives[State.Rand.Next(alives.Length)];
        if (TacticalUtilities.OpenTile(location, target.Actor) == false)
            return false;
        if (!unit.HasTrait(Traits.Endosoma) || target.Unit.Side != actor.Unit.Side)
            unit.GiveScaledExp(-4, unit.Level - target.Unit.Level, true);
        target.Actor.SetPos(location);
        target.Actor.Visible = true;
        target.Actor.Targetable = true;
        TacticalUtilities.UpdateActorLocations();
        target.Actor.UnitSprite.DisplayEscape();
        RemovePrey(target);
        UpdateFullness();
        return true;


    }

    internal void FreeGreatEscapePrey(Prey preyUnit)
    {
        TacticalUtilities.Log.LogGreatEscapeFlee(unit, preyUnit.Unit, Location(preyUnit));
        RemovePrey(preyUnit);        
        preyUnit.Actor.SelfPrey = null;        
    }

    void FreePrey(Prey preyUnit, bool forcePop)
    {

        if (actor.Visible || forcePop || actor.Fled)
        {
            preyUnit.Actor.SelfPrey = null;
            if (PlacedPrey(preyUnit.Actor))
            {
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;

                if (Config.DisorientedPrey)
                {
                    preyUnit.Actor.WasJustFreed = true;
                    preyUnit.Actor.Movement = Mathf.Min(Mathf.Max(2, (int)(.2f * preyUnit.Actor.CurrentMaxMovement())), 5);
                }
                else
                    preyUnit.Actor.Movement = preyUnit.Actor.CurrentMaxMovement();


                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
            else
            {
                Debug.Log("Couldn't place prey anywhere in a 5x5 ... killing unit");
                preyUnit.Unit.Health = -99;
                return;
            }

        }
        else
        {
            Actor_Unit predator = actor.SelfPrey?.Predator;
            if (predator != null)
            {
                var loc = actor.SelfPrey.Location;
                Prey prey = new Prey(preyUnit.Actor, predator, preyUnit.SubPrey);
                predator.PredatorComponent.AddPrey(prey, loc);
                preyUnit.Actor.SelfPrey = prey;
            }
            else
            {
                preyUnit.Actor.SelfPrey = null;
                Debug.Log($"Couldn't find predator for {unit.Name}, freeing sub prey");
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;
                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
        }
        RemovePrey(preyUnit);
        UpdateFullness();

    }

    void AddPrey(Prey preyUnit, PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.breasts:
                breasts.Add(preyUnit);
                break;
            case PreyLocation.balls:
                balls.Add(preyUnit);
                break;
            case PreyLocation.stomach:
                stomach.Add(preyUnit);
                break;
            case PreyLocation.stomach2:
                stomach2.Add(preyUnit);
                break;
            case PreyLocation.womb:
                womb.Add(preyUnit);
                break;
            case PreyLocation.tail:
                tail.Add(preyUnit);
                break;
            case PreyLocation.anal:
                stomach.Add(preyUnit);
                break;
            case PreyLocation.leftBreast:
                leftBreast.Add(preyUnit);
                break;
            case PreyLocation.rightBreast:
                rightBreast.Add(preyUnit);
                break;
        }
        prey.Add(preyUnit);
        UpdateAlivePrey();
    }

    internal PreyLocation Location(Prey preyUnit)
    {
        if (womb.Contains(preyUnit))
        {
            return PreyLocation.womb;
        }
        if (breasts.Contains(preyUnit))
        {
            return PreyLocation.breasts;
        }
        if (balls.Contains(preyUnit))
        {
            return PreyLocation.balls;
        }
        if (tail.Contains(preyUnit))
        {
            return PreyLocation.tail;
        }
        if (stomach2.Contains(preyUnit))
        {
            return PreyLocation.stomach2;
        }
        if (leftBreast.Contains(preyUnit))
        {
            return PreyLocation.leftBreast;
        }
        if (rightBreast.Contains(preyUnit))
        {
            return PreyLocation.rightBreast;
        }
        else
        {
            return PreyLocation.stomach;
        }
    }

    private void RemovePrey(Prey preyUnit)
    {
        womb.Remove(preyUnit);
        breasts.Remove(preyUnit);
        balls.Remove(preyUnit);
        stomach.Remove(preyUnit);
        stomach2.Remove(preyUnit);
        tail.Remove(preyUnit);
        leftBreast.Remove(preyUnit);
        rightBreast.Remove(preyUnit);
        prey.Remove(preyUnit);
        UpdateAlivePrey();
    }

    public void Digest(string feedType = "")
    {
        AlivePrey = 0;
        int totalHeal = 0;
        foreach (Prey preyUnit in prey.ToList())
        {
            if ((preyUnit.Location != PreyLocation.breasts && feedType == "breastfeed") || (preyUnit.Location != PreyLocation.balls && feedType == "cumfeed"))
                break;
            if (unit.HasTrait(Traits.EnthrallingDepths))
            {
                preyUnit.Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, 3);
            }
            int preyDamage = CalculateDigestionDamage(preyUnit);
            if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns)
                preyDamage = 0;
            if (tail.Contains(preyUnit))
            {
                if (preyUnit.TurnsBeingSwallowed >= 1)
                {
                    tail.Remove(preyUnit);
                    stomach.Add(preyUnit);
                    preyUnit.TurnsBeingSwallowed = 0;
                }
                else
                {
                    preyUnit.TurnsBeingSwallowed++;
                    preyDamage = 0;
                }
            }

            if (unit.HasTrait(Traits.DualStomach))
            {
                if (stomach.Contains(preyUnit))
                {
                    if (preyUnit.TurnsBeingSwallowed >= 2)
                    {
                        stomach.Remove(preyUnit);
                        stomach2.Add(preyUnit);
                    }
                    else
                    {
                        preyUnit.TurnsBeingSwallowed++;
                    }
                }
            }

            if (actor.PredatorComponent.IsUnitInPrey(preyUnit.Actor, PreyLocation.womb) && actor.PredatorComponent.birthStatBoost > 0 && preyUnit.Unit.Side == unit.Side && Config.KuroTenkoEnabled && Config.CumGestation)
            {
                while (actor.PredatorComponent.birthStatBoost > 0)
                {
                    int stat = UnityEngine.Random.Range(0, 8);
                    preyUnit.Unit.ModifyStat(stat, 1);
                    if (stat == 6)
                    {
                        preyUnit.Unit.Health += 2;
                    }
                    if (stat == 0)
                    {
                        preyUnit.Unit.Health += 1;
                    }
                    actor.PredatorComponent.birthStatBoost--;
                }
            }
            if (unit.HasTrait(Traits.Endosoma) && preyUnit.Unit.Side == unit.Side && preyUnit.Unit.IsDead == false)
            {
                if (unit.HasTrait(Traits.HealingBelly))
                    preyDamage = Math.Min(unit.MaxHealth / -10, -1);
                else
                    preyDamage = 0;
                if (unit.HasTrait(Traits.InfiniteAssimilation) && unit.HasTrait(Traits.Endosoma) && !preyUnit.Unit.HasTrait(Traits.InfiniteAssimilation) && Config.KuroTenkoEnabled)
                    preyUnit.Unit.AddPermanentTrait(Traits.InfiniteAssimilation);
                preyUnit.Unit.ReloadTraits();
                preyUnit.Unit.InitializeTraits();

            }
            else if (Config.FriendlyRegurgitation && unit.HasTrait(Traits.Greedy) == false && !TacticalUtilities.TreatAsHostile(actor, preyUnit.Actor) && preyUnit.Unit.Health > 0 && preyUnit.Actor.Surrendered == false)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(unit.Side);
                TacticalUtilities.Log.RegisterRegurgitated(unit, preyUnit.Unit, Location(preyUnit));
                FreePrey(preyUnit, false);
                continue;
            }

            if (preyDamage > 0)
                totalHeal += DigestOneUnit(preyUnit, preyDamage);
            else
                DigestOneUnit(preyUnit, preyDamage);
            if (unit.HasTrait(Traits.Growth))
            {
                unit.BaseScale += ((float)totalHeal / preyUnit.Unit.MaxHealth * .2d) * CalculateGrowthValue(preyUnit);
                if (unit.BaseScale > 5)
                    unit.BaseScale = 5.0d;
            }
        }
        if (!(unit.Health < unit.MaxHealth))
        {
            totalHeal = 0;
        }
        if (totalHeal > 0)
        {
            unit.Heal(totalHeal);
            actor.UnitSprite.DisplayDamage(-totalHeal);
            TacticalUtilities.Log.RegisterHeal(unit, new int[] { totalHeal, 0 });
        }
        UpdateFullness();
    }

    float CalculateGrowthValue(Prey preyUnit)
    {
        float preyMass = preyUnit.Unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(preyUnit.Unit.Race);
        float predMass = unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(unit.Race);
        float sizeDiff = (preyUnit.Unit.GetScale(2) * preyMass) / (unit.GetScale(2) * predMass);
        float preyBoosts = (((preyUnit.Unit.TraitBoosts.Outgoing.Nutrition - 1) * .2f) + 1f) * preyUnit.Unit.TraitBoosts.Outgoing.GrowthRate;
        float predBoosts = (((unit.TraitBoosts.Incoming.Nutrition - 1) * .2f) + 1f) * unit.TraitBoosts.Incoming.GrowthRate;
        return sizeDiff * preyBoosts * predBoosts;
    }

    int CalculateDigestionDamage(Prey preyUnit)
    {
        if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns || preyUnit.Unit.HasTrait(Traits.TheGreatEscape))
            return 0;

        float usedCapacity = TotalCapacity() - FreeCap();
        float PredStomach = Mathf.Pow(unit.GetStat(Stat.Stomach) + 15, 1.5f);
        float PredVoracity = Mathf.Pow(unit.GetStat(Stat.Voracity) + 15, 1.5f);
        float preyEndurance = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Endurance) + 15, 1.5f);
        float preyWill = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Will) + 15, 1.5f);
        float predScore = 2 * (40 + PredStomach * 3 + PredVoracity) / (1f + ((usedCapacity - preyUnit.Actor.Bulk()) / usedCapacity));
        float preyScore = ((preyEndurance + preyWill * .75f) / (1 + preyUnit.Actor.BodySize() / 40) + 300) / (1 + preyUnit.TurnsDigested / 5f);
        predScore *= unit.TraitBoosts.Outgoing.DigestionRate;
        preyScore /= preyUnit.Unit.TraitBoosts.Incoming.DigestionRate;
        int damage = (int)Math.Round(predScore / preyScore);
        if (damage < 1)
            damage = 1;

        return damage;
    }

    /// <summary>
    /// Returns the % of the stomach used.  i.e. 50% is .5, can be over 100%
    /// </summary>
    internal float UsageFraction => (TotalCapacity() - FreeCap()) / TotalCapacity();

    internal void FreeUnit(Actor_Unit target, bool forcePop = false)
    {
        var preyUnit = prey.Where(s => s.Actor == target).FirstOrDefault();
        if (preyUnit != null)
        {
            RemovePrey(preyUnit);
            FreePrey(preyUnit, forcePop);
        }

    }

    private int DigestOneUnit(Prey preyUnit, int preyDamage)
    {
        int totalHeal = 0;
        bool freshKill = false;

        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            if ((Location(preyUnit) == PreyLocation.womb || Config.KuroTenkoConvertsAllTypes) && preyUnit.Unit.CanBeConverted() && Config.KuroTenkoEnabled && unit.HasTrait(Traits.PredGusher) == false && (Config.UBConversion == UBConversion.Both || Config.UBConversion == UBConversion.ConversionOnly) && preyUnit.Unit.Type != UnitType.Summon && preyUnit.Unit.Type != UnitType.Leader && preyUnit.Unit.Type != UnitType.SpecialMercenary && preyUnit.Unit.HasTrait(Traits.Eternal) == false && preyUnit.Unit.SavedCopy == null)
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;
                if (preyUnit.Unit.Side != unit.Side)
                    State.GameManager.TacticalMode.SwitchAlignment(preyUnit.Actor);
                preyUnit.Actor.Surrendered = false;
                FreeUnit(preyUnit.Actor);
                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f);
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetBirthMode();
                return 0;
            }
            if (Location(preyUnit) == PreyLocation.womb && preyUnit.Unit.CanBeConverted() && preyUnit.Unit.Type != UnitType.Summon && preyUnit.Unit.Type != UnitType.Leader && preyUnit.Unit.Type != UnitType.SpecialMercenary && preyUnit.Unit.HasTrait(Traits.Eternal) == false && preyUnit.Unit.SavedCopy == null && unit.HasTrait(Traits.PredConverter) && unit.HasTrait(Traits.PredRebirther) == false && unit.HasTrait(Traits.PredGusher) == false)
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;
                if (preyUnit.Unit.Side != unit.Side)
                    State.GameManager.TacticalMode.SwitchAlignment(preyUnit.Actor);
                preyUnit.Actor.Surrendered = false;
                FreeUnit(preyUnit.Actor);
                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f);
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetBirthMode();
                return 0;
            }
            State.GameManager.TacticalMode.TacticalStats.RegisterDigestion(unit.Side);
            TacticalUtilities.Log.RegisterDigest(unit, preyUnit.Unit, Location(preyUnit));
            if (!State.GameManager.TacticalMode.turboMode)
                actor.SetDigestionMode();
            if (State.GameManager.TacticalMode.turboMode == false && Config.DigestionSkulls)
                GameObject.Instantiate(State.GameManager.TacticalMode.SkullPrefab, new Vector3(actor.Position.x + UnityEngine.Random.Range(-0.2F, 0.2F), actor.Position.y + 0.1F + UnityEngine.Random.Range(-0.1F, 0.1F)), new Quaternion());
            Actor_Unit existingPredator = actor;
            freshKill = true;
            if (unit.HasTrait(Traits.DigestionConversion) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted())
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;
                if (preyUnit.Unit.Side != unit.Side)
                    State.GameManager.TacticalMode.SwitchAlignment(preyUnit.Actor);
                if (Config.FriendlyRegurgitation)
                {
                    State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(unit.Side);
                    TacticalUtilities.Log.RegisterRegurgitated(unit, preyUnit.Unit, Location(preyUnit));
                    FreeUnit(preyUnit.Actor);
                }

                preyUnit.Actor.Surrendered = false;
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} converted from one side to another thanks to {unit.Name}'s converting digestion trait.");
                return 0;
            }
            if (unit.HasTrait(Traits.DigestionRebirth) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted() && (Config.SpecialMercsCanConvert || unit.Race < Race.Selicia))
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                HashSet<Gender> set = new HashSet<Gender>(Races.GetRace(preyUnit.Unit.Race).CanBeGender);
                bool equals = set.SetEquals(Races.GetRace(unit.Race).CanBeGender);
                preyUnit.Unit.Race = unit.Race;
                preyUnit.Unit.SetGear(unit.Race);
                if (equals == false || Config.AlwaysRandomizeConverted)
                    preyUnit.Unit.TotalRandomizeAppearance();
                else
                {
                    var race = Races.GetRace(unit.Race);
                    race.RandomCustom(preyUnit.Unit);
                }
                preyUnit.Actor.Movement = 0;
                if (preyUnit.Unit.Side != unit.Side)
                    State.GameManager.TacticalMode.SwitchAlignment(preyUnit.Actor);
                if (Config.FriendlyRegurgitation)
                {
                    State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(unit.Side);
                    TacticalUtilities.Log.RegisterRegurgitated(unit, preyUnit.Unit, Location(preyUnit));
                    FreeUnit(preyUnit.Actor);
                }
                preyUnit.Actor.AnimationController = new AnimationController();
                preyUnit.Actor.Surrendered = false;
                preyUnit.Actor.Unit.ReloadTraits();
                preyUnit.Actor.Unit.InitializeTraits();
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} converted from one side to another and changed race thanks to {unit.Name}'s converting digestion rebirth trait.");
                return 0;
            }
            preyUnit.Actor.KilledByDigestion = true;
            if (preyUnit.Unit.HasTrait(Traits.CursedMark))
            {
                unit.AddPermanentTrait(Traits.CursedMark);
                preyUnit.Unit.RemoveTrait(Traits.CursedMark);
            }
            unit.DigestedUnits++;
            if (unit.HasTrait(Traits.EssenceAbsorption) && unit.DigestedUnits % 4 == 0)
                unit.GeneralStatIncrease(1);
            if (preyUnit.Unit.Race == Race.Asura)
                unit.EarnedMask = true;
            if (unit.HasTrait(Traits.TasteForBlood))
                actor.GiveRandomBoost();
            unit.EnemiesKilledThisBattle++;
            preyUnit.Unit.Kill();
            for (int i = 0; i < 20; i++)
            {
                Actor_Unit next = TacticalUtilities.FindPredator(existingPredator);
                if (next == null)
                    break;
                existingPredator = next;
            }
            DigestionEffect(preyUnit, existingPredator);

            if (unit.HasTrait(Traits.MetabolicSurge))
            {
                unit.ApplyStatusEffect(StatusEffectType.Empowered, 1.0f, 5);
            }
        }

        if (preyUnit.Unit.IsDead == false)
        {
            preyUnit.Actor.SubtractHealth(preyDamage);
            preyUnit.TurnsSinceLastDamage = 0;
        }

        if (freshKill)
            preyUnit.Actor.Unit.Health = 0;

        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            TacticalUtilities.Log.RegisterNearDigestion(unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage > 0)
        {
            TacticalUtilities.Log.LogDigestionRandom(unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage == 0 && preyUnit.Unit.HasTrait(Traits.TheGreatEscape))
        {
            TacticalUtilities.Log.LogGreatEscapeKeep(unit, preyUnit.Unit, Location(preyUnit));
        }

        if (preyUnit.Unit.IsDead)
        {
            float speedFactor;
            speedFactor = (float)Math.Sqrt(actor.BodySize() / preyUnit.Actor.BodySize());

            speedFactor *= unit.TraitBoosts.Outgoing.AbsorptionRate;
            speedFactor *= preyUnit.Unit.TraitBoosts.Incoming.AbsorptionRate;

            if (speedFactor > 4f && speedFactor < 1000)
                speedFactor = 4f;

            int healthReduction = (int)Math.Max(Math.Round(preyUnit.Unit.MaxHealth * speedFactor / 15), 1);
            if (healthReduction >= preyUnit.Unit.MaxHealth + preyUnit.Unit.Health)
                healthReduction = preyUnit.Unit.MaxHealth + preyUnit.Unit.Health + 1;
            preyUnit.Actor.SubtractHealth(healthReduction);
            totalHeal += Math.Max((int)(healthReduction / 2 * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition * unit.TraitBoosts.Incoming.Nutrition), 1);
            var baseManaGain = healthReduction * (preyUnit.Unit.TraitBoosts.Outgoing.ManaAbsorbHundreths + unit.TraitBoosts.Incoming.ManaAbsorbHundreths);
            var totalManaGain = baseManaGain / 100 + (State.Rand.Next(100) < (baseManaGain % 100) ? 1 : 0);
            unit.RestoreMana(totalManaGain);
            if (preyUnit.Unit.IsDeadAndOverkilledBy(healthReduction * 2) && preyUnit.SubPrey?.Count() > 0)
            {
                Prey[] aliveSubUnits = preyUnit.GetAliveSubPrey();
                for (int i = 0; i < aliveSubUnits.Length; i++)
                {
                    Prey newPrey = new Prey(aliveSubUnits[i].Actor, actor, aliveSubUnits[i].SubPrey);
                    prey.Add(newPrey);
                    switch (Location(preyUnit))
                    {
                        case PreyLocation.womb:
                            womb.Add(newPrey);
                            break;
                        case PreyLocation.balls:
                            balls.Add(newPrey);
                            break;
                        case PreyLocation.breasts:
                            breasts.Add(newPrey);
                            break;
                        case PreyLocation.stomach:
                            stomach.Add(newPrey);
                            break;
                        case PreyLocation.tail:
                            tail.Add(newPrey);
                            break;
                        case PreyLocation.leftBreast:
                            leftBreast.Add(newPrey);
                            break;
                        case PreyLocation.rightBreast:
                            rightBreast.Add(newPrey);
                            break;
                        case PreyLocation.stomach2:
                            stomach2.Add(newPrey);
                            break;
                        default:
                            stomach.Add(newPrey);
                            break;
                    }
                    preyUnit.SubPrey.Remove(aliveSubUnits[i]);
                }
            }
            if (preyUnit.Unit.IsDeadAndOverkilledBy(preyUnit.Unit.MaxHealth))
            {
                if (((Location(preyUnit) == PreyLocation.womb || Config.KuroTenkoConvertsAllTypes) && preyUnit.Unit.CanBeConverted() && Config.KuroTenkoEnabled && (Config.UBConversion == UBConversion.Both || Config.UBConversion == UBConversion.RebirthOnly) && (Config.SpecialMercsCanConvert || unit.Race < Race.Selicia) && unit.HasTrait(Traits.PredGusher) == false)
                    ||
                ((Location(preyUnit) == PreyLocation.womb || Config.KuroTenkoConvertsAllTypes) && preyUnit.Unit.CanBeConverted() && (Config.SpecialMercsCanConvert || unit.Race < Race.Selicia) && unit.HasTrait(Traits.PredRebirther) && unit.HasTrait(Traits.PredGusher) == false))
                {
                    preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                    preyUnit.Actor.Movement = 0;
                    if (preyUnit.Unit.Side != unit.Side)
                        State.GameManager.TacticalMode.SwitchAlignment(preyUnit.Actor);
                    preyUnit.Actor.Surrendered = false;
                    if (preyUnit.Unit.Race != unit.Race)
                    {
                        HashSet<Gender> set = new HashSet<Gender>(Races.GetRace(preyUnit.Unit.Race).CanBeGender);
                        bool equals = set.SetEquals(Races.GetRace(unit.Race).CanBeGender);
                        preyUnit.Unit.Race = unit.Race;
                        preyUnit.Unit.SetGear(unit.Race);
                        if (equals == false || Config.AlwaysRandomizeConverted)
                            preyUnit.Unit.TotalRandomizeAppearance();
                        else
                        {
                            var race = Races.GetRace(unit.Race);
                            race.RandomCustom(preyUnit.Unit);
                        }
                        preyUnit.Actor.AnimationController = new AnimationController();
                        preyUnit.Actor.Unit.ReloadTraits();
                        preyUnit.Actor.Unit.InitializeTraits();
                    }
                    FreeUnit(preyUnit.Actor);
                    TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f);
                    if (!State.GameManager.TacticalMode.turboMode)
                        actor.SetBirthMode();
                    RemovePrey(preyUnit);
                    return 0;
                }
                else
                {
                    TacticalUtilities.Log.RegisterAbsorb(unit, preyUnit.Unit, Location(preyUnit));
                }

                unit.GiveScaledExp(8 * preyUnit.Unit.ExpMultiplier, unit.Level - preyUnit.Unit.Level, true);
                if (Config.WeightGain)
                {
                    if (Location(preyUnit) == PreyLocation.balls)
                    {
                        if (unit.HasDick)
                        {
                            unit.DickSize = Math.Min(unit.DickSize + 1, Races.GetRace(unit).DickSizes - 1);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(unit.Race))
                                unit.DickSize = Math.Min(unit.DickSize, State.RaceSettings.Get(unit.Race).MaxDick);
                        }
                    }
                    else if (Location(preyUnit) == PreyLocation.breasts || Location(preyUnit) == PreyLocation.leftBreast || Location(preyUnit) == PreyLocation.rightBreast)
                    {
                        if (unit.HasBreasts)
                        {
                            unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize + 1, Races.GetRace(unit).BreastSizes - 1), unit.BreastSize == unit.DefaultBreastSize);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(unit.Race))
                                unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize, State.RaceSettings.Get(unit.Race).MaxBoob));
                        }
                    }
                    else
                    {
                        if (Config.AltVoreOralGain && State.Rand.NextDouble() < .4)
                        {
                            if (unit.HasDick)
                            {
                                unit.DickSize = Math.Min(unit.DickSize + 1, Races.GetRace(unit).DickSizes - 1);
                                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(unit.Race))
                                    unit.DickSize = Math.Min(unit.DickSize, State.RaceSettings.Get(unit.Race).MaxDick);
                            }
                        }

                        if (Config.AltVoreOralGain && State.Rand.NextDouble() < .4)
                        {
                            if (unit.HasBreasts)
                            {
                                unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize + 1, Races.GetRace(unit).BreastSizes - 1), unit.BreastSize == unit.DefaultBreastSize);
                                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(unit.Race))
                                    unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize, State.RaceSettings.Get(unit.Race).MaxBoob));
                            }
                        }

                        if (State.Rand.NextDouble() < .5 && Races.GetRace(unit).WeightGainDisabled == false)
                        {
                            unit.BodySize = Math.Max(Math.Min(unit.BodySize + 1, Races.GetRace(unit).BodySizes - 1), 0);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideWeight(unit.Race))
                                unit.BodySize = Math.Min(unit.BodySize, State.RaceSettings.Get(unit.Race).MaxWeight);
                        }

                    }
                }
                AbsorptionEffect(preyUnit, Location(preyUnit));
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetAbsorbtionMode();
                CheckPredTraitAbsorption(preyUnit);

                if (preyUnit.SubPrey?.Count() > 0) //Catches any dead prey that weren't already properly moved
                {
                    Prey[] subUnits = preyUnit.SubPrey.ToArray();
                    for (int i = 0; i < subUnits.Length; i++)
                    {
                        Prey newPrey = new Prey(subUnits[i].Actor, actor, subUnits[i].SubPrey);
                        prey.Add(newPrey);
                        switch (Location(preyUnit))
                        {
                            case PreyLocation.womb:
                                womb.Add(newPrey);
                                break;
                            case PreyLocation.balls:
                                balls.Add(newPrey);
                                break;
                            case PreyLocation.breasts:
                                breasts.Add(newPrey);
                                break;
                            case PreyLocation.stomach:
                                stomach.Add(newPrey);
                                break;
                            case PreyLocation.stomach2:
                                stomach2.Add(newPrey);
                                break;
                            case PreyLocation.tail:
                                tail.Add(newPrey);
                                break;
                            case PreyLocation.leftBreast:
                                leftBreast.Add(newPrey);
                                break;
                            case PreyLocation.rightBreast:
                                rightBreast.Add(newPrey);
                                break;
                            default:
                                stomach.Add(newPrey);
                                break;
                        }
                        preyUnit.SubPrey.Remove(subUnits[i]);
                    }
                }
                RemovePrey(preyUnit);

            }
        }
        else
        {
            preyUnit.TurnsDigested++;
            AlivePrey++;
            preyUnit.UpdateEscapeRate();
            float escapeMult = 1;
            if (FreeCap() < 0)
            {
                float cap = TotalCapacity();
                escapeMult = 1.4f + 2 * ((Fullness / cap) - 1);
            }
            if (State.Rand.NextDouble() < preyUnit.EscapeRate * escapeMult && preyUnit.Actor.Surrendered == false)
            {
                if (stomach2.Contains(preyUnit))
                {
                    stomach.Add(preyUnit);
                    stomach2.Remove(preyUnit);
                    TacticalUtilities.Log.RegisterPartialEscape(unit, preyUnit.Unit, Location(preyUnit));
                    preyUnit.TurnsBeingSwallowed = -5; //Gets 7 turns before it gets forced down again, so it needs to escape twice in 7 turns
                }
                else
                {
                    State.GameManager.TacticalMode.TacticalStats.RegisterEscape(unit.Side);
                    AlivePrey--;
                    TacticalUtilities.Log.RegisterEscape(unit, preyUnit.Unit, Location(preyUnit));
                    FreePrey(preyUnit, false);
                }

            }
        }
        return totalHeal;
    }

    //public List<Actor_Unit> Birth()
    //{
    //    List<Actor_Unit> released = new List<Actor_Unit>();
    //    foreach (Prey preyUnit in womb.ToList())
    //    {
    //        if (preyUnit.Unit.Health < (preyUnit.Unit.Level * -10) && Config.KuroTenkoEnabled)
    //        {
    //            int offset = 0 - (int)preyUnit.Unit.Experience;
    //            preyUnit.Unit.GiveExp(offset);
    //            Prey newPrey = null;
    //            if (!(preyUnit.Unit.ImmuneToDefections || preyUnit.Unit.Type == UnitType.Leader) && preyUnit.Unit.IsDead == false)
    //            {
    //                preyUnit.Unit.Side = unit.Side;
    //            }
    //            else
    //            {
    //                Unit newUnit = new Unit(unit.Side, unit.Race, 0, unit.Predator);
    //                Actor_Unit newActor = new Actor_Unit(preyUnit.Actor.Position, newUnit);
    //                newActor.UpdateBestWeapons();
    //                newActor.Visible = false;
    //                newActor.Targetable = false;
    //                State.GameManager.TacticalMode.DirtyPack = true;
    //                State.GameManager.TacticalMode.AddUnit(newActor);
    //                newActor.UnitSprite.UpdateSprites(newActor, false);

    //                newPrey = new Prey(newActor, actor, newActor.PredatorComponent?.prey);
    //                while (actor.PredatorComponent.birthStatBoost > 0)
    //                {
    //                    int stat = UnityEngine.Random.Range(0, 8);
    //                    newPrey.Unit.ModifyStat(stat, 1);
    //                    if (stat == 6)
    //                    {
    //                        preyUnit.Unit.Health += 2;
    //                    }
    //                    if (stat == 0)
    //                    {
    //                        preyUnit.Unit.Health += 1;
    //                    }
    //                    actor.PredatorComponent.birthStatBoost--;
    //                }
    //                prey.Add(preyUnit);
    //            }
    //            if (newPrey != null)
    //            {
    //                preyUnit.Actor.SubtractHealth(999);
    //                RemovePrey(preyUnit);
    //                FreePrey(newPrey, true);
    //                released.Add(newPrey.Actor);
    //            }
    //            else
    //            {
    //                preyUnit.Actor.Surrendered = false;
    //                FreePrey(preyUnit, true);
    //                released.Add(preyUnit.Actor);
    //            }
    //            TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f);
    //        }
    //    }
    //    UpdateFullness();
    //    return released;
    //}

    void CheckPredTraitAbsorption(Prey preyUnit)
    {
        bool updated = false;
        bool raceUpdated = true;
        if (unit.HasTrait(Traits.InfiniteAssimilation))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                if (unit.HasTrait(Traits.SynchronizedEvolution))
                {
                    RaceSettingsItem item = State.RaceSettings.Get(unit.Race);
                    item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                    raceUpdated = true;
                } else { 
                 unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                 updated = true;
                }
            }
        }
        else if (unit.HasTrait(Traits.Assimilate))
        {
            if (unit.BaseTraitsCount < 5)
            {
                var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

                if (possibleTraits.Any())
                {
                    if (unit.HasTrait(Traits.SynchronizedEvolution))
                    {
                        RaceSettingsItem item = State.RaceSettings.Get(unit.Race);
                        item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        raceUpdated = true;
                    }
                    else
                    {
                        unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        updated = true;
                    }
                }
            }
            else if (unit.BaseTraitsCount == 5)
            {
                var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

                if (possibleTraits.Any())
                {
                    unit.RemoveTrait(Traits.Assimilate);
                    if (unit.HasTrait(Traits.SynchronizedEvolution))
                    {
                        RaceSettingsItem item = State.RaceSettings.Get(unit.Race);
                        item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        raceUpdated = true;
                    }
                    else
                    {
                        unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        updated = true;
                    }
                }
            }
        }
        if (unit.HasTrait(Traits.AdaptiveBiology) && updated == false)
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                unit.AddTemporaryTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                updated = true;
            }
        }
        if (updated)
        {
            unit.ReloadTraits();
            unit.InitializeTraits();
        }
        if (raceUpdated)
        {
            if (State.World.Villages != null)
            {
                var units = StrategicUtilities.GetAllUnits();
                foreach (Unit unit in units)
                {
                    unit.ReloadTraits();
                }
            }
            else
            {
                foreach (Actor_Unit actor in TacticalUtilities.Units)
                {
                    actor.Unit.ReloadTraits();
                    actor.Unit.InitializeTraits();
                    actor.Unit.UpdateSpells();
                }
            }
            if (State.World.AllActiveEmpires != null)
            {
                foreach (Empire emp in State.World.AllActiveEmpires)
                {
                    if (emp.Side > 300)
                        continue;
                    var raceFlags = State.RaceSettings.GetRaceTraits(emp.ReplacedRace);
                    if (raceFlags != null)
                    {
                        if (raceFlags.Contains(Traits.Prey))
                            emp.CanVore = false;
                        else
                            emp.CanVore = true;
                    }
                }
            }
        }
    }

    private void DigestionEffect(Prey preyUnit, Actor_Unit existingPredator)
    {
        if (Config.BurpOnDigest)
        {
            if (stomach.Contains(preyUnit) || stomach2.Contains(preyUnit))
            {
                if (State.Rand.NextDouble() < Config.BurpFraction)
                {
                    actor.SetBurpMode();
                    State.GameManager.SoundManager.PlayBurp(actor);
                }
                else
                    State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
            }
            else
                State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
        }
        else
        {
            State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
        }

        DefaultRaceData preyRace = Races.GetRace(preyUnit.Unit);

        if (Config.ClothingDiscards)
        {
            if (preyRace.AllowedMainClothingTypes.Count >= preyUnit.Unit.ClothingType && preyUnit.Unit.ClothingType > 0)
            {
                int clothingType = preyRace.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].Type;
                int color;
                if (preyRace.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.AllowedWaistTypes.Count >= preyUnit.Unit.ClothingType2 && preyUnit.Unit.ClothingType2 > 0)
            {
                int clothingType2 = preyRace.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].Type;
                int color;
                if (preyRace.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType2, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing1Types.Count >= preyUnit.Unit.ClothingExtraType1 && preyUnit.Unit.ClothingExtraType1 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing2Types.Count >= preyUnit.Unit.ClothingExtraType2 && preyUnit.Unit.ClothingExtraType2 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing3Types.Count >= preyUnit.Unit.ClothingExtraType3 && preyUnit.Unit.ClothingExtraType3 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing4Types.Count >= preyUnit.Unit.ClothingExtraType4 && preyUnit.Unit.ClothingExtraType4 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing5Types.Count >= preyUnit.Unit.ClothingExtraType5 && preyUnit.Unit.ClothingExtraType5 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
        }



    }

    private void AbsorptionEffect(Prey preyUnit, PreyLocation location)
    {
        if (location == PreyLocation.stomach || location == PreyLocation.stomach2)
        {
            if (!Config.BurpOnDigest && State.Rand.NextDouble() < Config.BurpFraction)
            {
                actor.SetBurpMode();
                State.GameManager.SoundManager.PlayBurp(actor);
            }
            else
            {
                State.GameManager.SoundManager.PlayAbsorb(location, actor);
            }
            if (Config.FartOnAbsorb && (location == PreyLocation.stomach || location == PreyLocation.stomach2 || location == PreyLocation.anal) && State.Rand.NextDouble() < Config.FartFraction)
            {
                State.GameManager.SoundManager.PlayFart(actor);
            }

            if (Config.Scat && preyUnit.ScatDisabled == false)
            {
                State.GameManager.SoundManager.PlayAbsorb(location, actor);
                if (preyUnit.Unit.Race == Race.Slimes)
                {
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.SlimePile, preyUnit.Unit.Name, preyUnit.Unit.AccessoryColor);
                }
                else
                    State.GameManager.TacticalMode.CreateScat(GetCurrentLocation(), new ScatInfo(unit, preyUnit));
            }
            else
            {
                GenerateBones(preyUnit);
            }
        }
        else if (location == PreyLocation.balls || location == PreyLocation.womb || location == PreyLocation.breasts || location == PreyLocation.leftBreast || location == PreyLocation.rightBreast)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, actor);
            if (Config.Cumstains)
            {
                if (unit.Race == Race.Selicia)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name, 0);
                else
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name);
            }
        }
    }

    private void GenerateBones(Prey preyUnit)
    {
        if (Config.Bones)
        {
            List<BoneInfo> bonesInfos = preyUnit.GetBoneTypes();
            foreach (BoneInfo bonesInfo in bonesInfos)
            {
                State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), bonesInfo.boneTypes, bonesInfo.name, bonesInfo.accessoryColor);
            }
        }
    }

    internal void UpdateTransition()
    {
        if (StomachTransition.transitionTime < StomachTransition.transitionLength)
        {
            if (unit.Race == Race.FeralFrogs && actor.IsOralVoring && actor.IsOralVoringHalfOver == false)
                return;
            StomachTransition.transitionTime += Time.deltaTime;
            VisibleFullness = Mathf.Lerp(StomachTransition.transitionStart, StomachTransition.transitionEnd, StomachTransition.transitionTime / StomachTransition.transitionLength);
        }
        if (BallsTransition.transitionTime < BallsTransition.transitionLength)
        {
            BallsTransition.transitionTime += Time.deltaTime;
            BallsFullness = Mathf.Lerp(BallsTransition.transitionStart, BallsTransition.transitionEnd, BallsTransition.transitionTime / BallsTransition.transitionLength);
        }
        if (LeftBreastTransition.transitionTime < LeftBreastTransition.transitionLength)
        {
            LeftBreastTransition.transitionTime += Time.deltaTime;
            LeftBreastFullness = Mathf.Lerp(LeftBreastTransition.transitionStart, LeftBreastTransition.transitionEnd, LeftBreastTransition.transitionTime / LeftBreastTransition.transitionLength);
        }
        if (RightBreastTransition.transitionTime < RightBreastTransition.transitionLength)
        {
            RightBreastTransition.transitionTime += Time.deltaTime;
            RightBreastFullness = Mathf.Lerp(RightBreastTransition.transitionStart, RightBreastTransition.transitionEnd, RightBreastTransition.transitionTime / RightBreastTransition.transitionLength);
        }

    }

    internal void UpdateFullness()
    {
        float fullnessFactor = 2.25f / unit.GetScale(2);
        float fullness = 0;
        float stomachFullness = 0;
        float breastFullness = 0;
        float leftBreastFullness = 0;
        float rightBreastFullness = 0;
        float ballsFullness = 0;
        float tailFullness = 0;
        float stomach2ndFullness = 0;
        float wombFullness = 0;
        float exclusiveStomachFullness = 0;
        foreach (Prey preyUnit in prey.ToList()) //ToList to cover the rare case it needs to do the pop unit out of itself condition in the bulk function. (It has happened, once at least)
        {
            fullness += preyUnit.Actor.Bulk();
            if (breasts.Contains(preyUnit))
            {
                breastFullness += preyUnit.Actor.Bulk();
            }
            else if (balls.Contains(preyUnit))
            {
                ballsFullness += preyUnit.Actor.Bulk();
            }
            else if (stomach2.Contains(preyUnit))
            {
                stomach2ndFullness += preyUnit.Actor.Bulk();
            }
            else if (tail.Contains(preyUnit))
            {
                tailFullness += preyUnit.Actor.Bulk();
            }
            else if (leftBreast.Contains(preyUnit))
            {
                leftBreastFullness += preyUnit.Actor.Bulk();
            }
            else if (rightBreast.Contains(preyUnit))
            {
                rightBreastFullness += preyUnit.Actor.Bulk();
            }
            else
            {
                if (womb.Contains(preyUnit))
                    wombFullness += preyUnit.Actor.Bulk();
                else
                    exclusiveStomachFullness += preyUnit.Actor.Bulk();
                stomachFullness += preyUnit.Actor.Bulk();
            }

        }

        float stomachSize = State.RaceSettings.GetStomachSize(unit.Race);
        if (State.RaceSettings.Exists(unit.Race))
        {
            stomachSize = State.RaceSettings.GetStomachSize(unit.Race);
        }
        float newStomach = fullnessFactor * stomachFullness / stomachSize;
        if (newStomach > 0)
            StomachTransition = new Transition(Math.Abs(newStomach - VisibleFullness) / 4, VisibleFullness, newStomach);
        else
        {
            StomachTransition = new Transition(0, 0, 0);
            VisibleFullness = 0;
        }

        float newBalls = fullnessFactor * ballsFullness / stomachSize;
        if (newBalls > 0)
            BallsTransition = new Transition(Math.Abs(newBalls - BallsFullness) / 4, BallsFullness, newBalls);
        else
        {
            BallsTransition = new Transition(0, 0, 0);
            BallsFullness = 0;
        }

        Fullness = fullnessFactor * fullness / stomachSize;

        TailFullness = fullnessFactor * tailFullness / stomachSize;


        WombFullness = fullnessFactor * wombFullness / stomachSize;

        ExclusiveStomachFullness = fullnessFactor * exclusiveStomachFullness / stomachSize;


        Stomach2ndFullness = fullnessFactor * stomach2ndFullness / stomachSize;
        CombinedStomachFullness = fullnessFactor * (stomach2ndFullness + stomachFullness) / stomachSize;
        if (breastFullness <= 0) breastFullness = -1;
        BreastFullness = breastFullness;

        float newLeftBreast = fullnessFactor * leftBreastFullness / stomachSize;
        float newRightBreast = fullnessFactor * rightBreastFullness / stomachSize;
        if (Config.FairyBVType == FairyBVType.Shared)
        {
            newLeftBreast = (newLeftBreast + newRightBreast) / 2;
            newRightBreast = newLeftBreast;
        }
        if (newLeftBreast > 0)
            LeftBreastTransition = new Transition(Math.Abs(newLeftBreast - LeftBreastFullness) / 4, LeftBreastFullness, newLeftBreast);
        else
        {
            LeftBreastTransition = new Transition(0, 0, 0);
            LeftBreastFullness = 0;
        }
        if (newRightBreast > 0)
            RightBreastTransition = new Transition(Math.Abs(newRightBreast - RightBreastFullness) / 4, RightBreastFullness, newRightBreast);
        else
        {
            RightBreastTransition = new Transition(0, 0, 0);
            RightBreastFullness = 0;
        }

        var data = Races.GetRace(unit.Race);
        if (data.ExtendedBreastSprites == false)
            actor.Unit.SetBreastSize(unit.DefaultBreastSize + (int)(BreastFullness * 8));
        //actor.Unit.SetDickSize(unit.DefaultDickSize + (int)(BallsFullness * 8));
    }

    bool PlacedPrey(Actor_Unit prey)
    {
        Vec2i p = new Vec2i(0, 0);
        for (int i = 0; i < 8; i++)
        {
            p = actor.GetPos(i);
            if (TacticalUtilities.OpenTile(p.x, p.y, actor))
            {
                prey.SetPos(p);
                return true;
            }
        }
        for (int x = -2; x <= 2; x++) //Inefficient but should be rarely needed
        {
            for (int y = -2; y <= 2; y++)
            {
                p.x = actor.Position.x + x;
                p.y = actor.Position.y + y;
                if (TacticalUtilities.OpenTile(p.x, p.y, actor))
                {
                    prey.SetPos(p);
                    return true;
                }
            }
        }
        return false;
    }

    public string GetPreyInformation()
    {
        string ret = "";
        for (int x = 0; x < prey.Count; x++)
        {
            AddPreyInformation(ref ret, prey[x], 0);
        }
        return ret;
    }

    string AddPreyInformation(ref string ret, Prey prey, int indent)
    {
        if (indent > 12)
            return "";
        if (prey.Unit == null)
            return "";
        var loc = prey.Location.ToString();
        if (unit.Race == Race.Terrorbird && prey.Location == PreyLocation.tail)
            loc = "Crop";
        if (prey.Unit.IsDead == false && unit.HasTrait(Traits.DualStomach) && stomach.Contains(prey))
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Pushing {prey.Unit.Name} deeper\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";
            }

        }
        else if (prey.Unit.IsDead == false)
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Digesting {prey.Unit.Name}\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";
            }

        }
        else
        {
            if (indent > 0) ret += $"L:{indent}";
            ret += ($"Absorbing {prey.Unit.Name}\n");
            if (Config.ExtraTacticalInfo)
                ret += $" loc: {loc}\n absorbed: {Math.Round((float)-prey.Unit.Health / prey.Unit.MaxHealth * 100, 1)}%\n";
        }
        if (prey.SubPrey != null)
        {
            foreach (Prey sub in prey.SubPrey)
            {
                AddPreyInformation(ref ret, sub, indent + 1);
            }
        }
        return ret;
    }

    private void AddToStomach(Prey preyref, float v)
    {
        stomach.Add(preyref);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
    }

    public bool UsePreferredVore(Actor_Unit target)
    {
        List<VoreType> allowedVoreTypes = State.RaceSettings.GetVoreTypes(unit.Race);

        switch (unit.PreferredVoreType)
        {
            case VoreType.All:
                break;
            case VoreType.Oral:
                if (allowedVoreTypes.Contains(VoreType.Oral))
                    return Consume(target, Devour, PreyLocation.stomach);
                break;
            case VoreType.Unbirth:
                if (allowedVoreTypes.Contains(VoreType.Unbirth) && unit.CanUnbirth)
                    return Consume(target, Unbirth, PreyLocation.womb);
                break;
            case VoreType.CockVore:
                if (allowedVoreTypes.Contains(VoreType.CockVore) && unit.CanCockVore)
                    return Consume(target, CockVore, PreyLocation.balls);
                break;
            case VoreType.BreastVore:
                if (allowedVoreTypes.Contains(VoreType.BreastVore) && unit.CanBreastVore)
                    return Consume(target, BreastVore, PreyLocation.breasts);
                break;
            case VoreType.TailVore:
                if (allowedVoreTypes.Contains(VoreType.TailVore) && unit.CanTailVore)
                    return Consume(target, TailVore, PreyLocation.tail);
                break;
            case VoreType.Anal:
                if (allowedVoreTypes.Contains(VoreType.Anal) && unit.CanAnalVore)
                    return Consume(target, AnalVore, PreyLocation.anal);
                break;
        }
        if (State.GameManager.TacticalMode.turboMode) //When turboing, just pick the fast solution.
            return Consume(target, Devour, PreyLocation.stomach);

        WeightedList<VoreType> options = new WeightedList<VoreType>();

        List<VoreType> voreTypes = new List<VoreType>();
        if (allowedVoreTypes.Contains(VoreType.Oral) && Config.OralWeight > 0)
            options.Add(VoreType.Oral, Config.OralWeight);
        if (allowedVoreTypes.Contains(VoreType.Unbirth) && unit.CanUnbirth && Config.UnbirthWeight > 0)
            options.Add(VoreType.Unbirth, Config.UnbirthWeight);
        if (allowedVoreTypes.Contains(VoreType.CockVore) && unit.CanCockVore && Config.CockWeight > 0)
            options.Add(VoreType.CockVore, Config.CockWeight);
        if (allowedVoreTypes.Contains(VoreType.BreastVore) && unit.CanBreastVore && Config.BreastWeight > 0)
            options.Add(VoreType.BreastVore, Config.BreastWeight);
        if (allowedVoreTypes.Contains(VoreType.TailVore) && unit.CanTailVore && Config.TailWeight > 0)
            options.Add(VoreType.TailVore, Config.TailWeight);
        if (allowedVoreTypes.Contains(VoreType.Anal) && unit.CanAnalVore && Config.AnalWeight > 0)
            options.Add(VoreType.Anal, Config.AnalWeight);

        var type = options.GetResult();

        if (type == VoreType.All || type == VoreType.Oral)
            return Consume(target, Devour, PreyLocation.stomach);

        if (type == VoreType.Unbirth)
            return Consume(target, Unbirth, PreyLocation.womb);
        if (type == VoreType.CockVore)
            return Consume(target, CockVore, PreyLocation.balls);
        if (type == VoreType.BreastVore)
            return Consume(target, BreastVore, PreyLocation.breasts);
        if (type == VoreType.TailVore)
            return Consume(target, TailVore, PreyLocation.tail);
        if (type == VoreType.Anal)
            return Consume(target, AnalVore, PreyLocation.anal);
        return Consume(target, Devour, PreyLocation.stomach);
    }

    internal bool Devour(Actor_Unit target, float delay = 0)
    {
        if (delay == 0)
            return Consume(target, Devour, PreyLocation.stomach);
        else
            return Consume(target, Devour, PreyLocation.stomach, delay);
    }

    internal bool Unbirth(Actor_Unit target)
    {
        return Consume(target, Unbirth, PreyLocation.womb);
    }

    internal bool BreastVore(Actor_Unit target)
    {
        return Consume(target, BreastVore, PreyLocation.breasts);
    }

    internal bool CockVore(Actor_Unit target)
    {
        return Consume(target, CockVore, PreyLocation.balls);
    }

    internal bool TailVore(Actor_Unit target)
    {
        return Consume(target, TailVore, PreyLocation.tail);
    }

    internal bool AnalVore(Actor_Unit target)
    {
        return Consume(target, AnalVore, PreyLocation.anal);
    }

    internal bool MagicConsume(Spell spell, Actor_Unit target)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(target.Position);
        if (target.Unit == unit)
            return false;
        if (target.DefendSpellCheck(spell, actor, out float chance) == false)
        {
            State.GameManager.TacticalMode.Log.RegisterSpellMiss(unit, target.Unit, spell.SpellType, chance);
            return false;
        }

        if (target.Bulk() <= FreeCap())
        {
            //actor.SetPredMode(preyType);
            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(actor, skillBoost: actor.Unit.GetStat(Stat.Mind));
            if (r < v)
            {
                if (target.Unit.IsDead == false)
                    AlivePrey++;
                State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

                if (target.Unit.Side == unit.Side)
                    State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
                if (target.Unit.Side != unit.Side || !unit.HasTrait(Traits.Endosoma))
                {
                    unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, unit.Level - target.Unit.Level, true);
                }
                target.Visible = false;
                target.Targetable = false;
                State.GameManager.TacticalMode.Log.RegisterSpellHit(unit, target.Unit, spell.SpellType, 0, chance);
                State.GameManager.TacticalMode.DirtyPack = true;

                target.Movement = 0;
                Prey preyref = new Prey(target, actor, target.PredatorComponent?.prey);
                prey.Add(preyref);
                MagicDevour(target, v, preyref);
                UpdateFullness();
                return true;
            }
            else
            {
                State.GameManager.TacticalMode.Log.RegisterSpellMiss(unit, target.Unit, spell.SpellType, chance);
                return false;
            }


        }

        return false;

    }

    bool Consume(Actor_Unit target, Action<Actor_Unit, float, Prey, float> action, PreyLocation preyType, float delay = 0)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        int boost = 0;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(target.Position);
        if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false)
        {
            return false;
        }
        if (unit.HasTrait(Traits.RangedVore))
        {
            int dist = target.Position.GetNumberOfMovesDistance(actor.Position);
            if (dist > 4)
                return false;
            boost = -3 * (dist - 1);
        }
        else if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (target.Unit == unit)
            return false;


        if (target.Bulk() <= FreeCap())
        {
            bool bit = false;
            if (target.Unit.HasTrait(Traits.Dazzle) && target.Surrendered == false)
            {
                float chance = actor.WillCheckOdds(actor, target);
                if (State.Rand.NextDouble() < chance)
                {
                    actor.Movement = 0;
                    actor.UnitSprite.DisplayDazzle();
                    actor.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.3f, 1);
                    TacticalUtilities.Log.RegisterDazzle(actor.Unit, target.Unit, chance);
                    return false;
                }
            }
            actor.SetPredMode(preyType);

            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(actor, skillBoost: boost);
            if (r < v)
            {
                PerformConsume(target, action, preyType, v, delay);
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetVoreSuccessMode();
                if (unit.HasTrait(Traits.Tenacious))
                    unit.RemoveTenacious();
                if (unit.HasTrait(Traits.FearsomeAppetite))
                {
                    foreach (Actor_Unit victim in TacticalUtilities.UnitsWithinTiles(actor.Position, 3).Where(s => s.Unit.IsEnemyOfSide(unit.Side)))
                    {
                        victim.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.2f, 3);
                    }
                }
            }
            else
            {
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetVoreFailMode();
                if (actor.Unit.HasTrait(Traits.Biter))
                {
                    int oldMP = actor.Movement;
                    actor.Attack(target, false, true, canKill: false);
                    actor.Movement = oldMP;
                    bit = true; //Used because killed units are considered to be surrendered, so this prevents a bite that kills a unit being counted as 2 mp
                }
                else
                {
                    target.UnitSprite.DisplayResist();
                    if (unit.HasTrait(Traits.Tenacious))
                        unit.AddTenacious();
                }

            }

            if (bit == false && (target.Surrendered || (unit.HasTrait(Traits.Endosoma) && target.Unit.Side == unit.Side)))
                actor.Movement = Math.Max(actor.Movement - 2, 0);
            else
            {
                SpendVoreMP();
            }



            return r < v;
        }
        return false;
    }

    private void SpendVoreMP()
    {
        if (unit.TraitBoosts.VoreAttacks > 1)
        {
            int movementFraction = 1 + actor.MaxMovement() / unit.TraitBoosts.VoreAttacks;
            if (actor.Movement > movementFraction)
                actor.Movement -= movementFraction;
            else
                actor.Movement = 0;
        }
        else
            actor.Movement = 0;
    }

    void PerformConsume(Actor_Unit target, Action<Actor_Unit, float, Prey, float> action, PreyLocation preyType, float odds = 1f, float delay = 0f)
    {
        if (target.Unit.IsDead == false)
            AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

        if (target.Unit.Side == unit.Side)
            State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
        target.Visible = false;
        target.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (target.Unit.Side != unit.Side || !unit.HasTrait(Traits.Endosoma))
        {
            unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, unit.Level - target.Unit.Level, true);
        }
        target.Movement = 0;
        Prey preyref = new Prey(target, actor, target.PredatorComponent?.prey);
        prey.Add(preyref);
        action(target, odds, preyref, delay);
        UpdateFullness();
    }

    void MagicDevour(Actor_Unit target, float v, Prey preyref)
    {
        //State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        //TacticalUtilities.Log.RegisterVore(unit, target.Unit, v);
        AddToStomach(preyref, v);
    }

    void Devour(Actor_Unit target, float v, Prey preyref, float delay)
    {
        if (delay > 0)
            MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor), delay);
        else
            State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        TacticalUtilities.Log.RegisterVore(unit, target.Unit, v);
        AddToStomach(preyref, v);
    }

    void Unbirth(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.womb, actor);
        TacticalUtilities.Log.RegisterUnbirth(unit, target.Unit, v);
        AddToWomb(preyref, v);
    }

    void BreastVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
        TacticalUtilities.Log.RegisterBreastVore(unit, target.Unit, v);
        var data = Races.GetRace(unit.Race);
        if (data.ExtendedBreastSprites)
        {
            if (Config.FairyBVType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
            {
                var box = State.GameManager.CreateDialogBox();
                box.SetData(() => { rightBreast.Add(preyref); UpdateFullness(); }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () => { leftBreast.Add(preyref); UpdateFullness(); });
                return;
            }
            if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                leftBreast.Add(preyref);
            else
                rightBreast.Add(preyref);
        }
        else
            breasts.Add(preyref);
    }

    void CockVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.balls, actor);
        TacticalUtilities.Log.RegisterCockVore(unit, target.Unit, v);
        balls.Add(preyref);
    }

    void TailVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        TacticalUtilities.Log.RegisterTailVore(unit, target.Unit, v);
        tail.Add(preyref);
    }

    void AnalVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.anal, actor);
        TacticalUtilities.Log.RegisterAnalVore(unit, target.Unit, v);
        stomach.Add(preyref);
    }

    public bool CanFeed()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.BreastfeedOnly))
        {
            var race = Races.GetRace(actor.Unit.Race);
            if (race.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in leftBreast)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
                foreach (Prey preyUnit in rightBreast)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
            }
            else
            {
                foreach (Prey preyUnit in breasts)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
            }
        }
        return false;
    }

    public bool CanFeedCum()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.CumFeedOnly))
        {
            foreach (Prey preyUnit in balls)
            {
                if (preyUnit.Unit.IsDead)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanSuckle()
    {
        return (Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None);
    }

    public bool CanTransfer()
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false)
            return false;
        return GetTransfer() != null;
    }

    public bool CanVoreSteal(Actor_Unit target)
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false)
            return false;
        return GetVoreSteal(target) != null;
    }

    private Prey GetTransfer()
    {
        foreach (Prey preyUnit in balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit;
            //}
        }
        return null;
    }

    public float GetTransferBulk()
    {
        foreach (Prey preyUnit in balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit.Actor.Bulk();
            //}
        }
        return 0;
    }

    public float GetSuckleChance(Actor_Unit target, bool includeSecondaries = false)
    {
        if (target.Surrendered || actor.Unit.Side == target.Unit.Side)
            return 1f;
        float predVoracity = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float predStrength = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Strength), 1.5f);
        float preyStrength = Mathf.Pow(15 + target.Unit.GetStat(Stat.Strength), 1.5f);
        float preyWill = Mathf.Pow(15 + target.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (predVoracity + 2 * predStrength) * (actor.Unit.HealthPct + 1) * (30 + actor.BodySize()) / 16 / (1 + 2 * Mathf.Pow(actor.PredatorComponent.UsageFraction, 2));
        float defenderScore = 20 + (2 * (preyStrength + 2 * preyWill) * target.Unit.HealthPct * target.Unit.HealthPct * ((10 + target.BodySize()) / 2));

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= target.Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (target.Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - target.WillCheckOdds(actor, target);
            }
        }
        return odds / 100;
    }

    public int[] GetSuckle(Actor_Unit target)
    {
        if (actor.PredatorComponent == null || target.PredatorComponent == null)
            return new int[] { 0, 0 };
        if (Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None)
        {
            if (target.PredatorComponent.PreyInLocation(PreyLocation.breasts, false) + target.PredatorComponent.PreyInLocation(PreyLocation.leftBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.rightBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.balls, false) == 0)
                return new int[] { 0, 0 };
            bool rng = State.Rand.NextDouble() >= 0.5f;
            if (Config.SucklingPermission == SucklingPermission.AlwaysBreast || Config.FeedingType == FeedingType.BreastfeedOnly)
                return GetBreastSuckle(target);
            if (Config.SucklingPermission == SucklingPermission.AlwaysCock || Config.FeedingType == FeedingType.CumFeedOnly)
                return GetCockSuckle(target);
            int[] breasts = GetBreastSuckle(target);
            int[] balls = GetCockSuckle(target);
            if (breasts[0] > balls[0] && Config.SucklingPermission == SucklingPermission.Auto || Config.SucklingPermission == SucklingPermission.Random && !rng)
                return breasts;
            else
                return GetCockSuckle(target);

        }
        return new int[] { 0, 0, 0 };
    }

    public bool Suckle(Actor_Unit target)
    {
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1 || target.PredatorComponent == null)
        {
            return false;
        }

        float r = (float)State.Rand.NextDouble();
        float v = GetSuckleChance(target);
        int[] suckle = GetSuckle(target);
        if (suckle.Length < 3)
            return false;
        int halfMovement = 1 + actor.MaxMovement() / 2;
        if (actor.Movement > halfMovement)
            actor.Movement -= halfMovement;
        else
            actor.Movement = 0;
        int actorMaxHeal = actor.Unit.MaxHealth - actor.Unit.Health;
        if (r > v)
        {
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.breasts, v);
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.leftBreast, v);
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.rightBreast, v);
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.balls, v);
                    break;
            }
            return false;
        }
        else
        {
            suckle[1] = GetSuckleBonus(suckle[1], suckle[0], actorMaxHeal, actor);
            actor.Unit.GiveRawExp(suckle[1]);
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.breasts, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.leftBreast, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.rightBreast, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.balls, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "cumfeeding");
                    target.DigestCheck("cumfeed");
                    break;
            }
            actor.Unit.Heal(suckle[0]);
            actor.UnitSprite.DisplayDamage(-suckle[0]);
            actor.UnitSprite.UpdateSprites(target, true);
            target.PredatorComponent.UpdateFullness();
            return true;
        }
    }

    private int[] GetBreastSuckle(Actor_Unit target)
    {
        var targetRace = Races.GetRace(target.Unit.Race);
        if (targetRace.ExtendedBreastSprites)
        {
            int leftHeal = 0;
            int rightHeal = 0;
            int leftExp = 0;
            int rightExp = 0;
            foreach (Prey preyUnit in target.PredatorComponent.leftBreast)
            {
                if (preyUnit.Unit.IsDead)
                    leftHeal += CalcFeedValue(preyUnit, actor);
                leftExp += CalcFeedBonus(preyUnit);
            }
            foreach (Prey preyUnit in target.PredatorComponent.rightBreast)
            {
                if (preyUnit.Unit.IsDead)
                    rightHeal += CalcFeedValue(preyUnit, actor);
                rightExp += CalcFeedBonus(preyUnit);
            }
            if (leftHeal + rightHeal > 0 && !State.GameManager.TacticalMode.turboMode)
            {
                target.SetSuckledMode();
                actor.SetSuckleMode();
            }
            if (leftHeal > rightHeal)
                return new int[] { leftHeal, leftExp, 1 };
            else
                return new int[] { rightHeal, rightExp, 2 };
        }
        else
        {
            int heal = 0;
            int exp = 0;
            foreach (Prey preyUnit in target.PredatorComponent.breasts)
            {
                if (preyUnit.Unit.IsDead)
                    heal += CalcFeedValue(preyUnit, actor);
                exp += CalcFeedBonus(preyUnit);
            }
            if (heal > 0 && !State.GameManager.TacticalMode.turboMode)
            {
                target.SetSuckledMode();
                actor.SetSuckleMode();
            }
            return new int[] { heal, exp, 0 };
        }
    }

    private int[] GetCockSuckle(Actor_Unit target)
    {
        int heal = 0;
        int exp = 0;
        foreach (Prey preyUnit in target.PredatorComponent.balls)
        {
            if (preyUnit.Unit.IsDead)
                heal += CalcFeedValue(preyUnit, actor);
            exp += CalcFeedBonus(preyUnit);
        }
        if (heal > 0) {
            if (!State.GameManager.TacticalMode.turboMode)
            {
                target.SetSuckledMode();
                actor.SetSuckleMode();
            }   
        }
        return new int[] { heal, exp, 3 };
    }

    public float GetVoreStealChance(Actor_Unit attacker, bool includeSecondaries = false)
    {
        Prey prey = GetVoreSteal(actor);
        if (prey == null)
        {
            return 0f;
        }
        if (attacker.PredatorComponent == null)
        {
            Debug.Log("This shouldn't have happened");
            return 0;
        }
        if (actor.Surrendered || actor.Unit.Side == attacker.Unit.Side)
            return 1f;

        if (prey.Unit.HasTrait(Traits.Irresistable))
            return 1f;

        float recipientVoracity = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Voracity), 1.5f);
        float recipientStrength = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Strength), 1.5f);
        float donorVoracity = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float donorStrength = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Strength), 1.5f);
        float donorWill = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (recipientVoracity + recipientStrength) * (attacker.Unit.HealthPct + 1) * (30 + attacker.BodySize()) / 16 / (1 + 2 * Mathf.Pow(attacker.PredatorComponent.UsageFraction, 2)) - prey.Actor.Bulk();
        float defenderHealthPct = actor.Unit.HealthPct;
        float defenderScore = 20 + (2 * (donorStrength + 1.5f * donorVoracity + 2 * donorWill) * defenderHealthPct * defenderHealthPct * ((10 + actor.BodySize()) / 2));

        defenderScore /= actor.Unit.TraitBoosts.Incoming.VoreOddsMult;
        attackerScore *= attacker.Unit.TraitBoosts.Outgoing.VoreOddsMult;

        if (prey.Unit.HasTrait(Traits.GelatinousBody))
            attackerScore *= 1.15f;

        if (prey.Unit.HasTrait(Traits.MetalBody))
            attackerScore *= .7f;

        if (prey.Unit.HasTrait(Traits.Slippery))
            attackerScore *= 1.5f;

        if (attacker.Unit.HasTrait(Traits.AllOutFirstStrike) && attacker.HasAttackedThisCombat == false)
            attackerScore *= 3.25f;

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= actor.Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (actor.Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - actor.WillCheckOdds(attacker, actor);
            }
        }
        return odds / 100;
    }

    private Prey GetVoreSteal(Actor_Unit target)
    {
        if (actor.PredatorComponent == null || target.PredatorComponent == null)
            return null;

        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return null;
        }

        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral) || State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Unbirth))
        {
            foreach (Prey preyUnit in target.PredatorComponent.balls)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        var data = Races.GetRace(target.Unit.Race);
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            if (data.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in target.PredatorComponent.leftBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
                foreach (Prey preyUnit in target.PredatorComponent.rightBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }
            else
            {
                foreach (Prey preyUnit in target.PredatorComponent.breasts)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }

        }
        return null;
    }

    private void TransferFail(Prey transfer)
    {
        FreePrey(transfer, true);
        foreach (Prey preyUnit in balls)
        {
            if (preyUnit.Unit.IsDead == false)
            {
                FreePrey(preyUnit, true);
                break;
            }
        }
        TacticalUtilities.Log.RegisterTransferFail(unit, transfer.Unit, .3f);
    }

    public float TransferBulk()
    {
        Prey transfer = GetTransfer();
        if (transfer == null)
        {
            return 999f;
        }
        return transfer.Actor.Bulk();
    }

    public float StealBulk()
    {
        Prey transfer = GetVoreSteal(actor);
        if (transfer == null)
        {
            return 999f;
        }
        return transfer.Actor.Bulk();
    }

    private int[] BreastFeed(Actor_Unit target, int level)
    {
        int baseHeal = Math.Max(actor.Unit.MaxHealth / 16, 1);
        int standardHeal = 0;
        int leftHeal = 0;
        int rightHeal = 0;
        int standardExp = 0;
        int leftExp = 0;
        int rightExp = 0;
        int targetMaxHeal = target.Unit.MaxHealth - target.Unit.Health;
        var deadPrey = new List<Prey>();
        foreach (Prey preyUnit in actor.PredatorComponent.breasts.ToList())
        {
            if (preyUnit.Unit.IsDead)
            {
                deadPrey.Add(preyUnit);
                standardHeal += CalcFeedValue(preyUnit, target); ;
                standardExp += CalcFeedBonus(preyUnit);
                DigestOneUnit(preyUnit, CalculateDigestionDamage(preyUnit) / (2 + (actor.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0)));
            }
        }
        foreach (Prey preyUnit in actor.PredatorComponent.leftBreast.ToList())
        {
            if (preyUnit.Unit.IsDead)
            {
                deadPrey.Add(preyUnit);
                leftHeal += CalcFeedValue(preyUnit, target); ;
                leftExp += CalcFeedBonus(preyUnit);
                DigestOneUnit(preyUnit, CalculateDigestionDamage(preyUnit) / (2 + (actor.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0)));
            }
        }
        foreach (Prey preyUnit in actor.PredatorComponent.rightBreast.ToList())
        {
            if (preyUnit.Unit.IsDead)
            {
                deadPrey.Add(preyUnit);
                rightHeal += CalcFeedValue(preyUnit, target); ;
                rightExp += CalcFeedBonus(preyUnit);
                DigestOneUnit(preyUnit, CalculateDigestionDamage(preyUnit) / (2 + (actor.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0)));
            }
        }
        int totalHeal = baseHeal + Math.Max(standardHeal, Math.Max(leftHeal, rightHeal));

        TacticalUtilities.Log.RegisterFeed(unit, target.Unit, deadPrey[0].Unit, 1f);
        return new int[] { totalHeal, GetSuckleBonus(Math.Max(standardExp, Math.Max(leftExp, rightExp)), Math.Max(standardHeal, Math.Max(leftHeal, rightHeal)), targetMaxHeal, target) };
    }
    private int[] CumFeed(Actor_Unit target)
    {
        int baseHeal = Math.Max((actor.Unit.MaxHealth / 16), 1);
        int cumHeal = 0;
        int expBonus = 0;
        int targetMaxHeal = target.Unit.MaxHealth - target.Unit.Health;
        var deadPrey = new List<Prey>();

        foreach (Prey preyUnit in actor.PredatorComponent.balls)
        {
            if (preyUnit.Unit.IsDead)
            {
                deadPrey.Add(preyUnit);
                cumHeal += CalcFeedValue(preyUnit, target);
                expBonus += CalcFeedBonus(preyUnit);
            }
        }
        int totalHeal = baseHeal + cumHeal;

        TacticalUtilities.Log.RegisterCumFeed(unit, target.Unit, deadPrey[0].Unit, 1f);
        return new int[] { totalHeal, GetSuckleBonus(expBonus, cumHeal, targetMaxHeal, target) };
    }

    private int CalcFeedValue(Prey preyUnit, Actor_Unit target)
    {
        return Mathf.RoundToInt((preyUnit.Unit.MaxHealth / 10 + (((preyUnit.Unit.MaxHealth / 10) * 9) / 20) * Math.Max(0, preyUnit.Unit.Level - target.Unit.Level / (actor.Unit.HasTrait(Traits.WetNurse) ? 2 : 1)) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition));
    }

    private int CalcFeedBonus(Prey preyUnit)
    {
        int cap = State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race);
        int bonus = Mathf.Clamp(preyUnit.Unit.Level, 1, cap);
        if (preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker))
            bonus = Mathf.RoundToInt(bonus * (1f + (Mathf.Min((float)preyUnit.Unit.Level, (float)cap) / cap)));
        return bonus;
        // return Mathf.RoundToInt(Mathf.Clamp(preyUnit.Unit.Experience / 20, 0, (State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race) * ((preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0) + 1))) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition);
    }

    private int GetSuckleBonus(int expBonus, int healValue, int healCap, Actor_Unit target)
    {
        if (healValue > healCap && Config.OverhealEXP)
        {
            float multiplier = ((healValue - healCap) / Math.Max(healValue, 1)) * target.Unit.TraitBoosts.ExpGain;
            expBonus = Convert.ToInt32(Mathf.Round(expBonus * multiplier));
            return expBonus;
        }
        else
            return 0;
    }

    public bool Feed(Actor_Unit target)
    {
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered || target.Position.GetNumberOfMovesDistance(actor.Position) > 1 || actor.Movement == 0 || !CanFeed())
            return false;
        int[] nurseHeal = BreastFeed(target, actor.Unit.Level);
        target.Unit.GiveRawExp(nurseHeal[1]);
        actor.DigestCheck("breastfeed");
        //int totalHeal = BreastFeed(target, damage);
        if (nurseHeal[0] > 0)
        {
            TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "breastfeeding", actor.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-(nurseHeal[0] + nurseHeal[1]));
            target.UnitSprite.UpdateSprites(actor, true);
        }
        int halfMovement = 1 + actor.MaxMovement() / (2 + (actor.Unit.HasTrait(Traits.WetNurse) ? 1 : 0));
        if (actor.Movement > halfMovement)
            actor.Movement -= halfMovement;
        else
            actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    public bool FeedCum(Actor_Unit target)
    {
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered)
        {
            return false;
        }
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanFeedCum())
        {
            return false;
        }
        int[] nurseHeal = CumFeed(target);
        target.Unit.GiveRawExp(nurseHeal[1]);
        actor.DigestCheck("cumfeed");
        if (nurseHeal[0] > 0)
        {
            TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "cumfeeding");
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-(nurseHeal[0] + nurseHeal[1]));
            target.UnitSprite.UpdateSprites(actor, true);
        }
        int quarterMovement = 1 + actor.MaxMovement() / (4 + (actor.Unit.HasTrait(Traits.WetNurse) ? 1 : 0));
        if (actor.Movement > quarterMovement)
            actor.Movement -= quarterMovement;
        else
            actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    private bool Transfer(Actor_Unit target, Prey preyUnit)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.GetSpecialChance(SpecialAction.CockVore);
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (r > v && !preyUnit.Unit.IsDead)
        {
            return false;
        }
        Prey preyref = new Prey(preyUnit.Actor, target, preyUnit.Actor.PredatorComponent?.prey);
        if (target.Unit.HasVagina && RaceParameters.GetTraitData(target.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth))
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => TransferFinalize(target, preyUnit, preyref, PreyLocation.stomach), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => TransferFinalize(target, preyUnit, preyref, PreyLocation.womb));
        }
        // Hopefully there's no circumstance where a vaginal trasfer is possible but an oral isn't
        //else if (target.Unit.HasBreasts && !target.Unit.HasDick)
        //{
        //    TransferFinalize(target, preyUnit, preyref, PreyLocation.womb);
        //}
        else
        {
            TransferFinalize(target, preyUnit, preyref, PreyLocation.stomach);
        }
        if (target.UnitSprite != null)
        {
            target.UnitSprite.UpdateSprites(actor, true);
            target.UnitSprite.AnimateBellyEnter();
        }
        return true;
    }

    private bool TransferFinalize(Actor_Unit recipient, Prey preyUnit, Prey preyref, PreyLocation destination)
    {
        // Empowerment handling
        if (destination == PreyLocation.womb && recipient.Unit.HasVagina && Config.CumGestation)
        {
            bool alreadyPregnant = false;
            Unit alreadyChild = null;
            foreach (Prey existingPrey in recipient.PredatorComponent.womb)
            {
                if ((existingPrey.Unit.IsDead && existingPrey.Unit.Side != recipient.Unit.Side) || (!existingPrey.Unit.IsDead && existingPrey.Unit.Side == recipient.Unit.Side))
                {
                    alreadyPregnant = true;
                    alreadyChild = existingPrey.Unit;
                }
            }
            if (alreadyPregnant)
            {
                recipient.PredatorComponent.birthStatBoost += preyUnit.Unit.Level;
                recipient.PredatorComponent.RemovePrey(preyUnit);
                actor.PredatorComponent.RemovePrey(preyUnit);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps what remains of <b>{preyUnit.Unit.Name}</b> into <b>{recipient.Unit.Name}</b>'s womb, providing nutrients to strengthen <b>{alreadyChild.Name}</b>.");
                UpdateFullness();
                unit.UpdateSpells();
                return true;
            }
        }
        recipient.PredatorComponent.prey.Add(preyref);
        if (preyUnit.Unit.IsDead == false)
            recipient.PredatorComponent.AlivePrey++;
        actor.PredatorComponent.AlivePrey--;
        switch (destination)
        {
            case PreyLocation.womb:
                recipient.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.womb);
                break;
            case PreyLocation.stomach:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach);
                break;
            default:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach);
                break;
        }
        recipient.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, recipient.Unit.Level - preyUnit.Unit.Level, true);
        if (preyUnit.Unit.IsDead == false && Config.NoScatForDeadTransfers)
            preyUnit.ScatDisabled = true;
        recipient.PredatorComponent.UpdateFullness();
        actor.PredatorComponent.RemovePrey(preyUnit);
        UpdateFullness();
        return true;
    }

    public bool TransferAttempt(Actor_Unit target)
    {
        if (actor.PredatorComponent == null || target.PredatorComponent == null)
            return false;
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered)
        {
            return false;
        }
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanTransfer())
        {
            return false;
        }
        Prey transfer = GetTransfer();
        if (target.PredatorComponent.FreeCap() < transfer.Actor.Bulk() && !(target.Unit == actor.Unit))
        {
            return false;
        }
        if (!Transfer(target, transfer))
            TransferFail(transfer);
        int thirdMovement = (int)Math.Ceiling(actor.MaxMovement() / 3.0f);
        if (actor.Movement > thirdMovement)
            actor.Movement -= thirdMovement;
        else
            actor.Movement = 0;
        return true;
    }

    public bool VoreStealAttempt(Actor_Unit target)
    {
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanVoreSteal(target))
        {
            return false;
        }
        Prey transfer = GetVoreSteal(target);
        if (transfer == null)
            return false;
        if (FreeCap() < transfer.Actor.Bulk() && target.Unit != actor.Unit)
        {
            return false;
        }
        if (!VoreSteal(target, transfer, transfer.Location))
            VoreStealFail(target, transfer, transfer.Location);
        int thirdMovement = (int)Math.Ceiling(actor.MaxMovement() / 3.0f);
        if (actor.Movement > thirdMovement)
            actor.Movement -= thirdMovement;
        else
            actor.Movement = 0;
        return true;
    }

    private bool VoreSteal(Actor_Unit target, Prey preyUnit, PreyLocation oldLocation)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.PredatorComponent.GetVoreStealChance(actor);
        if (r >= v)
        {
            return false;
        }
        Prey preyref = new Prey(preyUnit.Actor, actor, preyUnit.Actor.PredatorComponent?.prey);
        if (actor.Unit.HasVagina && RaceParameters.GetTraitData(actor.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth) && oldLocation == PreyLocation.balls)
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.stomach, oldLocation), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.womb, oldLocation));
        }
        else
        {
            VoreStealFinalize(target, preyUnit, preyref, PreyLocation.stomach, oldLocation);
        }
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
        return true;
    }

    private void VoreStealFail(Actor_Unit target, Prey preyUnit, PreyLocation oldLocation)
    {
        TacticalUtilities.Log.RegisterVoreStealFail(target.Unit, actor.Unit, preyUnit.Unit, oldLocation);
    }

    private bool VoreStealFinalize(Actor_Unit donor, Prey preyUnit, Prey preyref, PreyLocation destination, PreyLocation oldLocation)
    {
        actor.PredatorComponent.prey.Add(preyref);
        actor.PredatorComponent.AlivePrey++;
        donor.PredatorComponent.AlivePrey--;
        switch (destination)
        {
            case PreyLocation.womb:
                actor.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.womb, oldLocation);
                break;
            case PreyLocation.stomach:
                actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach, oldLocation);
                break;
            default:
                actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach, oldLocation);
                break;
        }
        actor.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, actor.Unit.Level - preyUnit.Unit.Level, true);
        UpdateFullness();
        donor.PredatorComponent.RemovePrey(preyUnit);
        donor.PredatorComponent.UpdateFullness();
        return true;
    }

    private void AddToWomb(Prey preyref, float v)
    {
        womb.Add(preyref);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
    }


}

