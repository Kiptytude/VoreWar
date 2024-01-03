using System;
using System.Collections.Generic;
using System.Linq;
using OdinSerializer;
using static UnityEngine.UI.CanvasScaler;

internal class Shape
{
    [OdinSerialize]
    public Unit Unit { get; set; }

    [OdinSerialize]
    public Traits OriginTrait { get; private set; }

    [OdinSerialize]
    public bool UseUnitStats { get; private set; }

    [OdinSerialize]
    public bool SharedExp { get; private set; }

    [OdinSerialize]
    public TraitSharingStrategy TraitSharing { get; private set; }

    public Shape(Unit unit, Traits originTrait)
    {
        Unit = unit;
        OriginTrait = originTrait;

        switch (originTrait)
        {
            case Traits.Shapeshifter:
                {
                    UseUnitStats = false;
                    SharedExp = true;
                    TraitSharing = TraitSharingStrategy.UseUnitPermanent;
                    break;
                }
            case Traits.Skinwalker:
                {
                    UseUnitStats = false;
                    SharedExp = false;
                    TraitSharing = TraitSharingStrategy.Separate;
                    break;
                }
        }
    }
    public Shape(Unit unit, bool useUnitStats, bool sharedExp, TraitSharingStrategy traitSharing)
    {
        Unit = unit;
        UseUnitStats = useUnitStats;
        SharedExp = sharedExp;
        TraitSharing = traitSharing;
    }
}

public enum TraitSharingStrategy
{
    Separate,
    UseUnitPermanent,
    MergePermanent,
    ShareAll
}