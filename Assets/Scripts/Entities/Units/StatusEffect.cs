using OdinSerializer;


public class StatusEffect
{
    [OdinSerialize]
    internal StatusEffectType Type;
    [OdinSerialize]
    internal float Strength;
    [OdinSerialize]
    internal int Duration;
    [OdinSerialize]
    internal Unit Applicator;
    [OdinSerialize]
    internal StatusEffect ExpireEffect;

    public StatusEffect(StatusEffectType type, float strength, int duration, Unit applicator = null, StatusEffect expireEffect = null)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
        Applicator = applicator;
        ExpireEffect = expireEffect;
    }

}

