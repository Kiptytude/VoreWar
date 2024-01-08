using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public enum TraitListType
{
    Random,
    RootClass,
    RootRace,
    Invalid,
    Class,
    Race,
}

static class TraitListTypeMethods
{

    static public TraitListType StrToTraitListType(String traitListType)
    {
        if(traitListType.Contains("Random")) return TraitListType.Random;
        if(traitListType.Contains("RootClass")) return TraitListType.RootClass;
        if(traitListType.Contains("Class")) return TraitListType.Class;
        if(traitListType.Contains("RootRace")) return TraitListType.RootRace;
        if(traitListType.Contains("Race")) return TraitListType.Race;
        return TraitListType.Invalid;
    }

    static public String TraitListTypeToStr(TraitListType traitListType)
    {
        switch(traitListType)
        {
            case TraitListType.Class: return "Class";
            case TraitListType.RootClass: return "RootClass";
            case TraitListType.Race: return "Race";
            case TraitListType.RootRace: return "RootRace";
            case TraitListType.Random:
            default:
                return "Random";
        }
    }
}

public class RandomizeList
{
    public int id = -1;
    public TraitListType listtype = TraitListType.Random;
    public string name;
    public float chance;
    public int level;
    public bool permanent;
    [AllowEditing]
    internal List<Traits> RandomTraits;

    public override string ToString()
    {
        string str = id + ", " +  name + ", " + TraitListTypeMethods.TraitListTypeToStr(listtype) + ", " +chance.ToString("N", new CultureInfo("en-US")) + ", " + level + ", " + permanent.ToString(new CultureInfo("en-US")) + ", ";
        RandomTraits.ForEach(rt => str += (int)rt + "|");
        str = str.Remove(str.Length - 1);
        return str;
    }
    // if you look at the decompiled ToString() overloads for boolean, they're kind of funny. They also tell me that we probably DON'T need CultureInfo for booleans, but whaevr
    // I'ma be safe
}

static class RandomizeListUtils
{

    static private int FindNewId()
    {
        bool taken = true;
        int index = 0;
        while (taken)
        {
            index++;
            taken = State.RandomizeLists.Any(rtl => rtl.id == (2000 + index));
        }
        return 2000 + index;
    }

    static public Traits CreateRandomizeListTrait(String name, TraitListType traitListType,  List<Traits> traits, bool isPermanent, int level = 1,  float chance = 1)
    {
        if(State.RandomizeLists.Where(rt => rt.name == name).Count() > 0)
        {
            return (Traits)State.RandomizeLists.Where(rt => rt.name == name).ToList()[0].id;
        }


        RandomizeList custom = new RandomizeList();
        custom.id = FindNewId();
        custom.name = name;
        custom.listtype = traitListType;
        if(custom.listtype == TraitListType.Random)
            custom.chance = chance;
        else
            custom.chance = 1;
        custom.level = level;
        custom.permanent = isPermanent;
        custom.RandomTraits = traits;
        State.RandomizeLists.Add(custom);
        return (Traits)custom.id;
    }

    static public Traits CreateLevelTree(String name, TraitListType traitListType, List<Traits> traits, int traitsPerLvl, bool isPermanent, int levelIncrement = 1, float chance = 1)
    {
        if(traitListType == TraitListType.Class)
            isPermanent = true;//classes are always permanent
        if(traitListType == TraitListType.Race)
            isPermanent = false;//races are never permanent
        if(State.RandomizeLists.Where(rt => rt.name == name).Count() > 0)
        {
            return (Traits)State.RandomizeLists.Where(rt => rt.name == name).ToList()[0].id;
        }
        int level = 1; 
        List<List<Traits>> levelGroups = traits.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / traitsPerLvl)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
        List<Traits> complexTraitsList = new List<Traits>();
        foreach(List<Traits> levelGroup in levelGroups){
            level = level + levelIncrement;
            complexTraitsList.Add(CreateRandomizeListTrait(name + "Lv" + level.ToString(),traitListType,levelGroup,isPermanent,level));
        }
        if(traitListType == TraitListType.Class)
            traitListType = TraitListType.RootClass;
        if(traitListType == TraitListType.Race)
            traitListType = TraitListType.RootRace;
        return CreateRandomizeListTrait(name + "LvTraits",traitListType,complexTraitsList,isPermanent);
    }
}
