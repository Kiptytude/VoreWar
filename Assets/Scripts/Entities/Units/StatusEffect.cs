using OdinSerializer;
using System.Collections.Generic;
using System.Linq;

public class StatusEffect
{
    [OdinSerialize]
    internal StatusEffectType Type;
    [OdinSerialize]
    internal float Strength;
    [OdinSerialize]
    internal int Duration;

    public void OnApply(Unit unit)
    {
        switch (Type)
        {
            case StatusEffectType.Polymorphed:
                {
                    Actor_Unit actor = TacticalUtilities.GetActorOf(unit);
                    var AvailableRaces = new List<Race>();
                    foreach (Race race in (Race[])System.Enum.GetValues(typeof(Race)))
                    {
                        if (race >= Race.Vagrants && race < Race.Selicia && (Config.World.GetValue($"Merc {race}") || (Config.SpawnerInfoWithoutGeneration(race)?.Enabled ?? false)))
                            AvailableRaces.Add(race);
                    }
                    AvailableRaces.Remove(unit.Race);
                    if (AvailableRaces.Any() == false)
                        AvailableRaces.Add(Race.FeralAnts);
                    Race polyRace = AvailableRaces[State.Rand.Next(AvailableRaces.Count())];
                    Unit form = unit.CreateRaceShape(polyRace);
                    StrategicUtilities.SpendLevelUps(form);
                    unit.RelatedUnits[SingleUnitContext.OriginalForm] = unit;
                    actor.Shapeshift(form);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{unit.Name}</b> turned into {LogUtilities.GetAorAN(InfoPanel.RaceSingular(form))} {InfoPanel.RaceSingular(form)}.");
                    break;
                }
        }
    }

    public void OnExpire(Unit unit)
    {
        switch (Type)
        {
            case StatusEffectType.Polymorphed:
                {
                    Actor_Unit actor = TacticalUtilities.GetActorOf(unit);
                    unit.RelatedUnits[SingleUnitContext.OriginalForm].SetExp(unit.Experience);
                    actor.Shapeshift(actor.Unit.RelatedUnits[SingleUnitContext.OriginalForm]);
                    break;
                }
            case StatusEffectType.Diminished:
                {
                    Actor_Unit actor = TacticalUtilities.GetActorOf(unit);
                    Actor_Unit pred = actor.SelfPrey?.Predator;
                    if (actor != null)
                    {
                        if (pred != null)
                        {
                            State.GameManager.TacticalMode.Log.RegisterDiminishmentExpiration(pred.Unit, unit, actor.SelfPrey.Location);
                        }

                    }
                    break;
                }
            case StatusEffectType.WillingPrey:
                {
                    Actor_Unit actor = TacticalUtilities.GetActorOf(unit);
                    Actor_Unit pred = actor.SelfPrey?.Predator;
                    if (actor != null)
                    {
                        if (pred != null)
                        {
                            State.GameManager.TacticalMode.Log.RegisterCurseExpiration(pred.Unit, unit, actor.SelfPrey.Location);
                        }

                    }
                    break;
                }
        }
    }

    public StatusEffect(StatusEffectType type, float strength, int duration)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
    }

}

