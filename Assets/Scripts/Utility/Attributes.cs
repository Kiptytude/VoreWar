using System;


public sealed class AllowEditing : Attribute
{

}

public sealed class ProperNameAttribute : Attribute
{
    public string Name;

    public ProperNameAttribute(string name)
    {
        Name = name;
    }
}

public sealed class DescriptionAttribute : Attribute
{
    public string Description;

    public DescriptionAttribute(string description)
    {
        Description = description;
    }
}

public sealed class FloatRangeAttribute : Attribute
{
    public float Min;
    public float Max;

    public FloatRangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

public sealed class IntegerRangeAttribute : Attribute
{
    public int Min;
    public int Max;

    public IntegerRangeAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }
}

