using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizeList
{
    public int id = -1;
    public string name;
    public float chance;
    [AllowEditing]
    internal List<Traits> RandomTraits;


    internal void Save()
    {
       
    }

    public override string ToString()
    {
        string str = id + ", " + name + ", " + chance + ", ";
        RandomTraits.ForEach(rt => str += (int)rt + "|");
        str = str.Remove(str.Length - 1);
        return str;
    }
}