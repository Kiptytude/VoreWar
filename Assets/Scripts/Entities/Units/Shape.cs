using System;
using System.Collections.Generic;
using System.Linq;
using OdinSerializer;
using UnityEngine;

public interface IShapeshift
{
    Unit CreateShape(Unit source, Unit Target);
}

// public class ShapeshiftStatusEffect: StatusEffect
// {
//     public Unit shape;
//     public void ApplyShape(Actor_Unit current)
//     {
//         ShapeUtils.UpdateShapeTree(current.Unit);
//         if(current.Unit.ShifterShapes.Contains(shape)){
//             current.Shapeshift(shape);
//             if(current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] == null)
//                 current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] = shape;
//         }
//     }
//     public void RemoveShape(Actor_Unit current, Unit shape)
//     {
//         ShapeUtils.UpdateShapeTree(current.Unit);
//         if (current.Unit == shape)
//             if(current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] != null)
//                 current.Shapeshift(current.Unit.ShapeData.SrcUnit);
//         if (current.Unit.ShifterShapes.Contains(shape))
//             current.Unit.ShifterShapes.Remove(shape);
//     }
// }

public class Shape
{    
    [OdinSerialize]
    public Unit SrcUnit { get; set; }
    [OdinSerialize]
    public bool KeepShape { get; set; }

    [OdinSerialize]
    public Traits OriginTrait { get; private set; }

    [OdinSerialize]
    public bool UseUnitStats { get; private set; }

    [OdinSerialize]
    public bool SharedExp { get; private set; }

    [OdinSerialize]
    public bool IsUpdated { get; private set; }

    [OdinSerialize]
    public TraitSharingStrategy TraitSharing { get; private set; }

    public Shape(Unit unit, Traits originTrait)
    {
        SrcUnit = unit;
        OriginTrait = originTrait;
        IsUpdated = false;

        switch (originTrait)
        {
            case Traits.Shapeshifter:
                {
                    UseUnitStats = false;
                    SharedExp = true;
                    TraitSharing = TraitSharingStrategy.UseUnitPermanent;
                    KeepShape = true;
                    break;
                }
            case Traits.Skinwalker:
                {
                    UseUnitStats = false;
                    SharedExp = false;
                    TraitSharing = TraitSharingStrategy.Separate;
                    KeepShape = true;
                    break;
                }
        }
    }
    
    public Shape(Unit unit, bool useUnitStats, bool sharedExp, TraitSharingStrategy traitSharing, bool keepShape = true)
    {
        SrcUnit = unit;
        UseUnitStats = useUnitStats;
        SharedExp = sharedExp;
        TraitSharing = traitSharing;
        IsUpdated = false;
        KeepShape = keepShape;
    }

    public void UpdateShape(Unit thisUnit, Unit destUnit)
    {
        if(SharedExp)
        {
            destUnit.SetExp(thisUnit.Experience);
        }
        if(UseUnitStats)
        {
            foreach(Stat stat in Enum.GetValues(typeof(Stat)).Cast<Stat>().Where(stat => stat < Stat.None))
            {
                destUnit.SetStatBase(stat,thisUnit.GetStatBase(stat));
            }
        }
        if(thisUnit.HasShapeshiftingTrait())
        {
            foreach(Traits trait in thisUnit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IShapeshift))
            {
                destUnit.AddPermanentTrait(trait);
            }
            if (thisUnit.HasTrait(Traits.Shapeshifter)) destUnit.AddPermanentTrait(Traits.Shapeshifter);
            if (thisUnit.HasTrait(Traits.Skinwalker)) destUnit.AddPermanentTrait(Traits.Skinwalker);
        }
        if(TraitSharing == TraitSharingStrategy.Separate)
        {
            return;
        }
        foreach (Traits trait in thisUnit.GetPermanentTraits()) destUnit.AddPermanentTrait(trait);
        if(TraitSharing == TraitSharingStrategy.UseUnitFormBound)
        {
            foreach (Traits trait in thisUnit.GetFormBoundTraits().ToList()) destUnit.AddFormBoundTrait(trait);
        }
    }
    public void UpdateSrcShape(Unit thisUnit)
    {
        if(SrcUnit != null){
            Debug.Log(SrcUnit);
            UpdateShape(thisUnit,SrcUnit);
        }
        IsUpdated = true;
        
    }
    public void UpdateCurrentShape(Unit thisUnit)
    {
        if(SrcUnit != null){
            Debug.Log(thisUnit);
            UpdateShape(SrcUnit,thisUnit);
            if(TraitSharing == TraitSharingStrategy.MergeAll){
                foreach(Traits trait in SrcUnit.GetFormBoundTraits())
                    if(!thisUnit.HasTrait(trait))
                        if(thisUnit.AddFormBoundTrait(trait))
                            Debug.Log(trait);
                foreach(Traits trait in SrcUnit.GetTags()){
                    Debug.Log(trait);
                    if(!thisUnit.HasTrait(trait))
                        if(thisUnit.AddFormBoundTrait(trait))
                            Debug.Log(trait);
                }

            }
        }
        foreach(Traits trait in SrcUnit.GetFormBoundTraits())
        {
            Debug.Log(trait);
        }     
        IsUpdated = true;
    }
}

public enum TraitSharingStrategy
{
    Separate,
    UseUnitPermanent,
    UseUnitFormBound,
    MergeAll
    //Kuro comment: add more here if there is a use case
}

static class ShapeUtils
{
    private static List<Unit> TraverseToUpdatedNode(Unit thisUnit)
    {
        List<Unit> updateList = new List<Unit>();
        if (thisUnit.ShapeData == null || thisUnit.ShapeData.IsUpdated) return updateList;
        updateList.Add(thisUnit);
        Unit nextUnit = thisUnit.ShapeData.SrcUnit;
        while (nextUnit.ShapeData != null && !nextUnit.ShapeData.IsUpdated && !updateList.Contains(nextUnit))
        {
            updateList.Add(nextUnit);
            nextUnit = nextUnit.ShapeData.SrcUnit;
        }
        return updateList;
    }
    public static void UpdateShapeTree(Unit thisUnit)
    {
        List<Shape> updatedShapes = new List<Shape>();
        List<Unit> updateList = TraverseToUpdatedNode(thisUnit);
        foreach(Unit unit in updateList)
        {
            unit.ShapeData.UpdateSrcShape(unit);
            updatedShapes.Add(unit.ShapeData);
        }
        foreach(Unit shape in thisUnit.ShifterShapes)
        {
            updateList = TraverseToUpdatedNode(shape);
            updateList.Reverse();
            foreach(Unit unit in updateList)
            {
                unit.ShapeData.UpdateCurrentShape(unit);
                updatedShapes.Add(unit.ShapeData);
            }
        }
    }
    public static bool ApplyShape(Actor_Unit current, Unit shape)
    {
        ShapeUtils.UpdateShapeTree(current.Unit);
        if(current.Unit.ShifterShapes.Contains(shape))
        {
            current.Shapeshift(shape);
            if(current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] == null)
                current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] = shape;
            return true;
        }
        return false;
    }

    public static bool RemoveShape(Unit current, Unit shape)
    {
        ShapeUtils.UpdateShapeTree(current);
        if (current.ShifterShapes.Contains(shape))
            current.ShifterShapes.Remove(shape);
        foreach(Unit node in current.ShifterShapes){
            if (node.ShifterShapes.Contains(shape))
                node.ShifterShapes.Remove(shape);
            if (node.ShapeData != null){
                if(node.ShapeData.SrcUnit == shape){
                    if(shape.ShapeData != null) node.ShapeData.SrcUnit = shape.ShapeData.SrcUnit;
                    else node.ShapeData.SrcUnit = null;
                }
            }
        }
        if(current.ShapeData != null){
            if(current.ShapeData.SrcUnit == shape){
                if(shape.ShapeData != null) current.ShapeData.SrcUnit = shape.ShapeData.SrcUnit;
                else current.ShapeData.SrcUnit = null;
            }
        }
        ShapeUtils.UpdateShapeTree(current);

        return true;
    }
    
    public static bool RevertShape(Actor_Unit current)
    {
        if((current.Unit.RelatedUnits[SingleUnitContext.OriginalForm] != null))
        {
            current.Shapeshift(current.Unit.RelatedUnits[SingleUnitContext.OriginalForm]);
            return true;
        } else if (current.Unit.ShapeData != null)
        {
            current.Shapeshift(current.Unit.ShapeData.SrcUnit);
            return true;
        }
        return false;
    }
}