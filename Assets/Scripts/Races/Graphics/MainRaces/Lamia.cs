using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Lamia : DefaultRaceData
{
    readonly float xOffset = -1.875f; //3 pixels * 5/8
    readonly float yOffset = 3.75f;
    bool Selicia = false;
    public Lamia()
    {
        EyeTypes = 3;
        BodySizes = 4;
        SpecialAccessoryCount = 2;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImpDark);
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(3, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.ExtraColor1));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, s.Unit.ExtraColor2));
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = new SpriteExtraInfo(0, BackWeaponSprite, WhiteColored);
        BodySize = new SpriteExtraInfo(4, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));


        ClothingShift = new Vector3(xOffset, yOffset, 0);
        AvoidedMainClothingTypes = 2;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniTop,
            ClothingTypes.BeltTop,
            ClothingTypes.StrapTop,
            ClothingTypes.Leotard,
            ClothingTypes.BlackTop,
            ClothingTypes.Rags,
            RaceSpecificClothing.Toga,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
        };

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.PredatorComponent == null)
            Selicia = false;
        else
            Selicia = (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)
                 || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb)
                 || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2))
                 && (actor.GetCombinedStomachSize() == 15);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Body, xOffset, yOffset);
        AddOffset(Head, xOffset, yOffset);
        AddOffset(Mouth, xOffset, yOffset);
        AddOffset(Hair, xOffset, yOffset);
        AddOffset(Hair2, xOffset, yOffset);
        AddOffset(BodyAccent3, xOffset, yOffset);
        AddOffset(BodyAccent4, xOffset, yOffset);
        AddOffset(BodyAccent5, xOffset, yOffset);
        AddOffset(Weapon, xOffset + 1.25f, yOffset + 1.25f);
        AddOffset(BackWeapon, xOffset, yOffset);
        if (Selicia == false)
            AddOffset(Belly, xOffset, yOffset);
        AddOffset(Breasts, xOffset, yOffset);
        AddOffset(Dick, xOffset, yOffset + 2.5f);
        AddOffset(Balls, xOffset, yOffset + 2.5f);
        AddOffset(Eyes, 0, -1 * .625f);
        if (actor.Unit.GetGender() != Gender.Male)
            AddOffset(SecondaryAccessory, 0, -1 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Scylla[24 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (actor.Unit.HasBreasts ? 0 : 8)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int eatingOffset = actor.IsEating ? 1 : 0;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
        return State.GameManager.SpriteDictionary.Lamia[18 + eatingOffset + genderOffset];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lamia[0];

    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lamia[5 + actor.Unit.EyeType];

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[16];
        return State.GameManager.SpriteDictionary.Lamia[1];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[17];

        int bonusCap = 0;
        if (actor.PredatorComponent != null && actor.PredatorComponent.TailFullness > 0)
            bonusCap = 1 + actor.GetTailSize(2);
        if (Config.LamiaUseTailAsSecondBelly && actor.PredatorComponent != null)
            return State.GameManager.SpriteDictionary.Lamia[Math.Min(bonusCap + (actor.PredatorComponent?.Stomach2ndFullness > 0 ? (11 + actor.GetStomach2Size(2)) : 10), 13)];
        
        return State.GameManager.SpriteDictionary.Lamia[Math.Min(10 + actor.Unit.BodySize + bonusCap, 13)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        switch (actor.Unit.SpecialAccessoryType)
        {
            default:
            case 0:
                int eatingOffset = actor.IsEating ? 1 : 0;
                int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
                return State.GameManager.SpriteDictionary.Lamia[22 + eatingOffset + genderOffset];
            case 1:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        int eatingOffset = actor.IsEating ? 1 : 0;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
        return State.GameManager.SpriteDictionary.Lamia[26 + eatingOffset + genderOffset];
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return State.GameManager.SpriteDictionary.Lamia[9];
        return State.GameManager.SpriteDictionary.Lamia[8];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[15];
        if (!Config.LamiaUseTailAsSecondBelly)
        {
            if (actor.HasBelly)
            {
                belly.SetActive(true);

                if (actor.PredatorComponent.CombinedStomachFullness > 4)
                {
                    float extraCap = actor.PredatorComponent.CombinedStomachFullness - 4;
                    float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                    float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                    belly.transform.localScale = new Vector3(xScale, yScale, 1);
                }
                else
                    belly.transform.localScale = new Vector3(1, 1, 1);
                return State.GameManager.SpriteDictionary.Bellies[actor.GetCombinedStomachSize()];
            }
            else
            {
                return null;
            }
        }
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return State.GameManager.SpriteDictionary.Bellies[actor.GetStomachSize()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
       
        if (actor.IsErect())
        {
            if (actor.HasBelly == false)
            {
                Dick.layer = 18;
                return State.GameManager.SpriteDictionary.ErectDicks[actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (Selicia)
        {
            return State.GameManager.SpriteDictionary.Lamia[14];
        }
        int bonusCap = 0;
        if (actor.PredatorComponent != null && actor.PredatorComponent.TailFullness > 0)
            bonusCap = 1 + actor.GetTailSize(2);
    
        if (Config.LamiaUseTailAsSecondBelly && actor.PredatorComponent != null)
        {
            if (actor.PredatorComponent.Stomach2ndFullness > 0 || actor.PredatorComponent.TailFullness > 0)
                return State.GameManager.SpriteDictionary.Lamia[Math.Min(2 + actor.GetStomach2Size(2) + bonusCap, 4)];
            return State.GameManager.SpriteDictionary.Lamia[1];
        }
        else
        {
            int effectiveSize = Math.Min(actor.Unit.BodySize + bonusCap, 3);
            if (effectiveSize == 0)
                return null;
            else
                return State.GameManager.SpriteDictionary.Lamia[1 + effectiveSize];
        }

    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        var sprite = base.BallsSprite(actor);
        return sprite;
    }
}

