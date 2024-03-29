﻿using UnityEngine;
using System;

class Youko : Humans, IVoreRestrictions
{
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Tails = State.GameManager.SpriteDictionary.YoukoTails;

    public Youko()
    {
        FurCapable = true;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, s.Unit.AccessoryColor)); // Ears
        SecondaryAccessory = new SpriteExtraInfo(1, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor)); // Tail;
        Beard = null;
        BeardStyles = 0;
    }

    private int GetNumTails(Actor_Unit actor)
    {
        int StatTotal = actor.Unit.GetStatTotal();
        if (StatTotal < 85)
            return 0;
        return Math.Min((int)(StatTotal - 85) / 15, 7) + 1;

    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites3[8]; //ears
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        int tailCount = GetNumTails(actor);
        if (actor.Unit.Predator && actor.PredatorComponent.TailFullness > 0)
        {
            if(tailCount >= 7)
                return Tails[10];
            return Tails[9];
        }
        return Tails[tailCount];
    }
    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        return null;
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.Furry)
                return Sprites2[47];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[4];
                }
                else
                {
                    return Sprites2[1];
                }
            }
            else
            {
                return Sprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.Furry)
                return Sprites2[49];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[5];
                }
                else
                {
                    return Sprites2[2];
                }
            }
            else
            {
                return Sprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.Furry)
                return Sprites2[45];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[3];
                }
                else
                {
                    return Sprites2[0];
                }
            }
            else
            {
                return Sprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    public bool CheckVore(Actor_Unit actor, Actor_Unit target, PreyLocation location)
    {
        if(location == PreyLocation.tail)
        {
            int tailCount = GetNumTails(actor);
            if ((target != null) && (actor.PredatorComponent.TailFullness < 1))
                if ((float)target.Bulk() < (actor.PredatorComponent.TotalCapacity() / 2))
                    return (tailCount >= 4);
                else
                    return (tailCount >= 7);
            return (tailCount >= 4) && (actor.PredatorComponent.TailFullness < 1);
        }
        return true;
    }
}
