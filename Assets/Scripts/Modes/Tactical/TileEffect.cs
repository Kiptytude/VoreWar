class TileEffect
{
    internal int RemainingDuration;
    internal float Strength;
    internal TileEffectType Type;

    public TileEffect(int remainingDuration, float strength, TileEffectType type)
    {
        RemainingDuration = remainingDuration;
        Strength = strength;
        Type = type;
    }
}
