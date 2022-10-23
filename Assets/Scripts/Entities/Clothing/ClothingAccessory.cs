using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A lighter weight version of the clothing, designed for accessory types that don't need to really modify the character
/// </summary>
abstract class ClothingAccessory
{

    /// <summary>Only wearable by units with the leader type</summary>
    protected bool leaderOnly = false;
    /// <summary>Only wearable by females / herms</summary>
    protected bool femaleOnly = false;
    /// <summary>Only wearable by males</summary>
    protected bool maleOnly = false;
    protected SpriteExtraInfo clothing1;
    protected SpriteExtraInfo clothing2;
    protected bool ReqWinterHoliday;

    //protected SpriteExtraInfo clothing3; //There for easy expansion if there are complicated accessories
    //protected SpriteExtraInfo clothing4;

    protected Color WhiteColored(Actor_Unit actor) => Color.white;
    protected Color DefaultClothingColor(Actor_Unit actor) => Races.GetRace(actor.Unit).ClothingColor(actor);

    public virtual bool CanWear(Unit unit)
    {
        if (maleOnly && unit.HasBreasts && unit.HasDick == false)
            return false;

        if (femaleOnly && unit.HasDick && unit.HasBreasts == false)
            return false;

        if (leaderOnly && unit.Type != UnitType.Leader)
            return false;

        if (ReqWinterHoliday && Config.WinterActive() == false)
            return false;

        return true;
    }


    public virtual void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        Apply(sprite, actor);
    }


    protected void Apply(CompleteSprite sprite, Actor_Unit actor)
    {
        if (clothing1 != null)
        {
            sprite.SetNextClothingSprite(clothing1);
        }

        if (clothing2 != null)
        {
            sprite.SetNextClothingSprite(clothing2);
        }

        //if (clothing3 != null)
        //{
        //    sprite.SetNextClothingSprite(clothing3);
        //}

        //if (clothing4 != null)
        //{
        //    sprite.SetNextClothingSprite(clothing4);
        //}

    }
}




