using System;
using System.Collections.Generic;

public enum RaceAI
{
    Standard,
    Hedonist,
    ServantRace,
    NonCombatant
}
public static class RaceAIType
{
    public static Dictionary<RaceAI, Type> Dict = new Dictionary<RaceAI, Type>()
    {
        { RaceAI.Standard, typeof(StandardTacticalAI)},
        { RaceAI.Hedonist, typeof(HedonistTacticalAI)},
        { RaceAI.ServantRace, typeof(RaceServantTacticalAI)},
        { RaceAI.NonCombatant, typeof(NonCombatantTacticalAI)},
    };


}
