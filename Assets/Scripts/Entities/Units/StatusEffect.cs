using OdinSerializer;


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

