using System.Collections.Generic;
using UnityEngine;

class Smudger : BlankSlate
{
    public Smudger()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite };

        EyeTypes = 4;
        SpecialAccessoryCount = 6; // head shapes
        BodyAccentTypes1 = 10; // body patterns - body pattern 0 is supposed to be for no pattern at all, patterns 1-9 use sprites

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SmudgerSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SmudgerSkin); // for body patterns
        ExtraColors1 =  ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SmudgerSkin); // for genitals and mouth
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);

        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Body
        BodyAccessory = new SpriteExtraInfo(1, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.AccessoryColor)); // Body Patterns
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Slit
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.ExtraColor1)); // Slit Inside
        Balls = new SpriteExtraInfo(4, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.ExtraColor1)); // Balls
        Dick = new SpriteExtraInfo(5, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.ExtraColor1)); // Penis
        Belly = new SpriteExtraInfo(6, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Belly
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Arms
        SecondaryAccessory = new SpriteExtraInfo(8, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.AccessoryColor)); // Arm patterns
        // Layer 9 alternative for Belly
        BreastShadow = new SpriteExtraInfo(10, BreastsShadowSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Male Chest
        Breasts = new SpriteExtraInfo(11, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Breasts
        // Layer 12 alternative for Dick
        Hair = new SpriteExtraInfo(13, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Head Frills (Head Shape)
        Head = new SpriteExtraInfo(14, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.SkinColor)); // Head
        Hair2 = new SpriteExtraInfo(15, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.AccessoryColor)); // Head Patterns
        Mouth = new SpriteExtraInfo(16, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SmudgerSkin, s.Unit.ExtraColor1)); // Mouth
        Eyes = new SpriteExtraInfo(17, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));; // Eyes
        SecondaryEyes = new SpriteExtraInfo(18, EyesSecondarySprite, WhiteColored); // Pupils
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 5;

    internal override void SetBaseOffsets(Actor_Unit actor) // Offsets
    {
        AddOffset(Balls, 0, -53 * .625f);
        AddOffset(Belly, 0, -44 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        return State.GameManager.SpriteDictionary.Smudger[0];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Body Patterns
    {
        if (actor.Unit.BodyAccentType1 == 0) return null;

        return State.GameManager.SpriteDictionary.Smudger[22 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Slit Outline
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return State.GameManager.SpriteDictionary.Smudger[81];
            return State.GameManager.SpriteDictionary.Smudger[79];
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return State.GameManager.SpriteDictionary.Smudger[80];
            return State.GameManager.SpriteDictionary.Smudger[79];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Slit Insides
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return State.GameManager.SpriteDictionary.Smudger[83];
            return null;
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return State.GameManager.SpriteDictionary.Smudger[82];
            return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (actor.GetBallSize(24) == 0 && Config.HideCocks == false) return null;

        int size = actor.GetBallSize(40);

        if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[143];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[142];
        }

        else if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[141];
        }

        else if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[140];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[139];
        }

        else if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[138];
        }

        if (size > 29) size = 29;

        return State.GameManager.SpriteDictionary.Smudger[108 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < 0.75f)
            {
                Dick.layer = 12;
                if (actor.IsCockVoring) 
                {
                    return State.GameManager.SpriteDictionary.Smudger[90 + actor.Unit.DickSize];              
                }
                else
                {
                    return State.GameManager.SpriteDictionary.Smudger[84 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 5;
                if (actor.IsCockVoring)
                {
                    return State.GameManager.SpriteDictionary.Smudger[102 + actor.Unit.DickSize];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.Smudger[96 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 5;
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(40);

        if (size >= 15) Belly.layer = 9;
        else Belly.layer = 6;

        if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[179];
        }

        if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[178];
        }

        if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[177];
        }

        if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[176];
        }

        if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[175];
        }

        if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[174];
        }

        if (size > 29) size = 29;
        return State.GameManager.SpriteDictionary.Smudger[144 + size];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Arms
    {
        if (actor.IsEating) return State.GameManager.SpriteDictionary.Smudger[8];
        if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Smudger[7];
        return State.GameManager.SpriteDictionary.Smudger[6];
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Arm Patterns
    {
        if (actor.Unit.BodyAccentType1 == 0) return null;
        
        if (actor.IsEating) return State.GameManager.SpriteDictionary.Smudger[58 + actor.Unit.BodyAccentType1];
        if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Smudger[49 + actor.Unit.BodyAccentType1];
        return State.GameManager.SpriteDictionary.Smudger[40 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor) // Male Chest
    {
        if (actor.Unit.BreastSize > 0) return null;

        int size = actor.GetStomachSize(40);

        if (size == 0) return State.GameManager.SpriteDictionary.Smudger[68];
        if (size <= 9) return State.GameManager.SpriteDictionary.Smudger[69];
        if (size <= 17) return State.GameManager.SpriteDictionary.Smudger[70];
        if (size <= 23) return State.GameManager.SpriteDictionary.Smudger[71];
        if (size <= 29) return State.GameManager.SpriteDictionary.Smudger[72];
        if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia) ?? false))
        {
            return State.GameManager.SpriteDictionary.Smudger[73];
        }

        return null;
    }

    protected override Sprite BreastsSprite(Actor_Unit actor) // Breasts
    {
        if (actor.Unit.HasBreasts == false)
            return null;

        return State.GameManager.SpriteDictionary.Smudger[74 + actor.Unit.BreastSize];
    }

    protected override Sprite HairSprite(Actor_Unit actor) // Head Frills - Head Shape
    {
        return State.GameManager.SpriteDictionary.Smudger[17 + actor.Unit.SpecialAccessoryType];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsEating) return State.GameManager.SpriteDictionary.Smudger[3];
        if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Smudger[2];
        return State.GameManager.SpriteDictionary.Smudger[1];
    }

    protected override Sprite HairSprite2(Actor_Unit actor) // Head Patterns
    {
        if (actor.Unit.BodyAccentType1 == 0) return null;

        return State.GameManager.SpriteDictionary.Smudger[31 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite MouthSprite(Actor_Unit actor) // Mouth
    {
        if (actor.IsEating) return State.GameManager.SpriteDictionary.Smudger[5];
        if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Smudger[4];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
       return State.GameManager.SpriteDictionary.Smudger[9 + actor.Unit.EyeType];
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
       return State.GameManager.SpriteDictionary.Smudger[13 + actor.Unit.EyeType];
    }
}