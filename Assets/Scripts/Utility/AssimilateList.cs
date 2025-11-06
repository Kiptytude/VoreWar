using System;
using System.Collections.Generic;
using UnityEngine;

public class AssimilateList
{
    public bool Initialized = false;
    [AllowEditing]
    internal Dictionary<Traits, bool> CanAssimilate;


    internal bool CanGet(Traits trait)
    {
        if (Initialized == false)
            Initialize();
        if (CanAssimilate.TryGetValue(trait, out bool value))
        {
            return value;
        }
        return false;
    }

    internal void Initialize()
    {
        Initialized = true;
        CanAssimilate = new Dictionary<Traits, bool>();
        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (trait == Traits.Prey ||trait == Traits.Brainless)
                continue;
            CanAssimilate[trait] = PlayerPrefs.GetInt($"A{trait}", 1) == 1;
        }
    }

    internal void Save()
    {
        foreach (var entry in CanAssimilate)
        {
            PlayerPrefs.SetInt($"A{entry.Key}", entry.Value ? 1 : 0);
        }
    }
}