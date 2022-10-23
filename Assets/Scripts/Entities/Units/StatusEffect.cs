using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StatusEffect
{
    [OdinSerialize]
    internal StatusEffectType Type;
    [OdinSerialize]
    internal float Strength;
    [OdinSerialize]
    internal int Duration;

    public StatusEffect(StatusEffectType type, float strength, int duration)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
    }

}

