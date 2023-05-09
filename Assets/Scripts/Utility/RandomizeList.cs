using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeList
{
    public bool Initialized = false;
    int RaceInt = -1;
    [AllowEditing]
    internal Dictionary<Traits, bool> CanRandomize;


    internal bool CanGet(Traits trait, int raceInt = -1)
    {
        if (Initialized == false)
            Initialize(raceInt);
        if (CanRandomize.TryGetValue(trait, out bool value))
        {
            return value;
        }
        return false;
    }

    internal void Initialize(int raceInt = -1)
    {
        Initialized = true;
        RaceInt = raceInt;
        CanRandomize = new Dictionary<Traits, bool>();
        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (PlayerPrefs.HasKey($"R{RaceInt.ToString()}{trait}"))
                CanRandomize[trait] = PlayerPrefs.GetInt($"R{RaceInt.ToString()}{trait}", 1) == 1;
            else
                CanRandomize[trait] = PlayerPrefs.GetInt($"R{trait}", 1) == 1;
        }
    }

    internal void Uninitialize()
    {
        Initialized = false;
    }

    internal void Save()
    {
        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            PlayerPrefs.SetInt($"R{(RaceInt == -1 ? "" : RaceInt.ToString())}{trait}", CanRandomize[trait] ? 1 : 0);
        }
    }
}