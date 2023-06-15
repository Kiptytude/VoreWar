using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;

class ScatInfo
{
    [OdinSerialize]
    public string name = "";
    [OdinSerialize]
    public int color = -1;
    [OdinSerialize]
    public Race predRace;
    [OdinSerialize]
    public Race preyRace;
    [OdinSerialize]
    public int preySize;
    [OdinSerialize]
    public List<BoneInfo> bonesInfos;

    public ScatInfo(Unit pred, Prey preyUnit)
    {
        name = preyUnit.Unit.Name;
        predRace = pred.Race;
        preyRace = preyUnit.Unit.Race;
        preySize = Mathf.RoundToInt(preyUnit.Actor.BodySize());

        if (pred.Race == Race.Slimes)
        {
            color = pred.AccessoryColor;
        }

        if (pred.Race == Race.Selicia || preyUnit.Unit.Race == Race.Selicia)
        {
            color = 3;
        }

        bonesInfos = preyUnit.GetBoneTypes();
        if (Config.ScatBones == false)
        {
            //empty list
            bonesInfos = new List<BoneInfo>();
        }
        else
        {
            //Randomizing
            List<BoneInfo> result = new List<BoneInfo>();
            foreach (BoneInfo bonesInfo in bonesInfos)
            {
                if (Random.Range(0, 100) > 45)
                {
                    result.Add(bonesInfo);
                }
            }
            bonesInfos = result;
        }
    }
    public string GetDescription()
    {
        return $"Remains of {name}";
    }
}
