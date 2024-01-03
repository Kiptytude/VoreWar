using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class RandomizeList
{
    public int id = -1;
    public string name;
    public float chance;
    public int level;
    public bool permanent;
    [AllowEditing]
    internal List<Traits> RandomTraits;

    public override string ToString()
    {
        string str = id + ", " + name + ", " + chance.ToString("N", new CultureInfo("en-US")) + ", " + level + ", " + permanent.ToString(new CultureInfo("en-US")) + ", ";
        RandomTraits.ForEach(rt => str += (int)rt + "|");
        str = str.Remove(str.Length - 1);
        return str;
    }
    // if you look at the decompiled ToString() overloads for boolean, they're kind of funny. They also tell me that we probably DON'T need CultureInfo for booleans, but whaevr
    // I'ma be safe
}