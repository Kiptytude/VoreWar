using UnityEngine;
using System;
using System.Linq;

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
        AllowedMainClothingTypes.Add(new Kimono());
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
    
    class Kimono : MainClothing
    {
        public Kimono()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Kimono[23];
            blocksBreasts = true;
            coversBreasts = false;
            blocksDick = false;
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(12, null, null);//base
            clothing2 = new SpriteExtraInfo(13, null, null);//belt
            clothing3 = new SpriteExtraInfo(15, null, null);//arms
            clothing4 = new SpriteExtraInfo(15, null, null);//pants
            clothing5 = new SpriteExtraInfo(18, null, null);//bust
            Type = 60030;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if ((Races.Youko.oversize) || (actor.GetStomachSize(31, 0.7f) > 1))
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[22];
                blocksBreasts = false;
                blocksDick = false;
                clothing2.GetSprite = null;
                clothing4.GetSprite = null;
                clothing5.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                {
                    if (actor.Unit.BreastSize < 3)
                    {
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[19];
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[18];
                        clothing5.GetSprite = null;
                    }
                    else if (actor.Unit.BreastSize < 5)
                    {
                        coversBreasts = true;
                        blocksBreasts = false;
                        clothing1.GetSprite = null;
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[18];
                        clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[20];
                    }
                    else if (actor.Unit.BreastSize < 8)
                    {
                        coversBreasts = true;
                        blocksBreasts = false;
                        clothing1.GetSprite = null;
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[18];
                        clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[21];
                    }
                    else
                    {
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[22];
                        blocksBreasts = false;
                        clothing2.GetSprite = null;
                        clothing5.GetSprite = null;
                    }
                }
                else
                {
                    blocksBreasts = true;
                    breastSprite = null;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[19];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[17];
                    clothing5.GetSprite = null;
                }

                if (!(actor.Unit.HasDick) || !(actor.GetBallSize(28, .8f) > 12))
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[16];
                    blocksDick = true;

                }
                else
                {
                    clothing4.GetSprite = null;
                }
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[3 + 4 * actor.Unit.BodySize];
                else clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[0 + 4 * actor.Unit.BodySize];
            }
            else if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[2 + 4 * actor.Unit.BodySize];
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[3 + 4 * actor.Unit.BodySize];
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Kimono[1 + 4 * actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

}
