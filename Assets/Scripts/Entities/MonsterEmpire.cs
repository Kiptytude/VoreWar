using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MonsterEmpire : Empire
{
    //[OdinSerialize]
    //public List<Army> Armies;

    //[OdinSerialize]
    //public IStrategicAI StrategicAI;
    //[OdinSerialize]
    //public TacticalAIType TacticalAIType;

    

    public MonsterEmpire(ConstructionArgs args) : base(args)
    {
        if (args.strategicAI == StrategyAIType.Monster)
            StrategicAI = new MonsterStrategicAI(this);
        else if (args.strategicAI == StrategyAIType.Goblin)
            StrategicAI = new GoblinAI(this);
    }

    new public void Regenerate()
    {
        for (int i = 0; i < Armies.Count; i++)
        {
            Armies[i].Refresh();
        }

    }


    new public int Income => 0;
    new public int StartingXP => 0;

    new public void LoadFix()
    {
    }

    new public void CalcIncome(Village[] villages, bool AddToStats = false)
    {
    }


}

