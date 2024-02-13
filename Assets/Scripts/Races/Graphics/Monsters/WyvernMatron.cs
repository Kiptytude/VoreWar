using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WyvernMatron : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[6] { 2, 1, 0, 5, 4, 3 }, new float[6] { 0.55f, 0.55f, 0.55f, 0.55f, 0.55f, 0.55f });
    RaceFrameList frameListWings = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { 0.35f, 0.65f });
    RaceFrameList frameListHair = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 3, 2, 1 }, new float[8] { 0.85f, 0.35f, 0.45f, 0.55f, 0.95f, 0.55f, 0.45f, 0.35f});

    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    public WyvernMatron()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.WyvernMatron);
        EyeColors = ColorMap.EyeColorCount;
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Hermaphrodite };

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Body
        Eyes = new SpriteExtraInfo(4, EyesSprite, (s) => ColorMap.GetEyeColor(s.Unit.EyeColor)); // Eyes
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Tail
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Wing Left
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Wing Right
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Head
        Balls = new SpriteExtraInfo(2, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Two globes.
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Sheath
        BodyAccent5 = new SpriteExtraInfo(4, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Legs
        Dick = new SpriteExtraInfo(8, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Dick, CV
        Hair = new SpriteExtraInfo(5, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Hair
        Belly = new SpriteExtraInfo(9, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.WyvernMatron, s.Unit.SkinColor)); // Belly

        EyeTypes = 2; // Eye types
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, 0, -80 * .625f);
        AddOffset(BodyAccent2, -80 * .625f, 0); // Left wing offset
        AddOffset(BodyAccent3, 80 * .625f, 0); // Right wing offset
        AddOffset(Belly, 0, -80 * .625f);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Tail controller. Index 0.
            new AnimationController.FrameList(1, State.Rand.Next(0, 65) * .01f, true), // Wing controller. Index 1.
        new AnimationController.FrameList(2, 0, false)}; // Hair controller. Index 2.
}

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (!actor.Targetable) return State.GameManager.SpriteDictionary.WyvernMatron[3];

        return State.GameManager.SpriteDictionary.WyvernMatron[3 + frameListWings.frames[actor.AnimationController.frameLists[1].currentFrame]];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.WyvernMatron[9 + actor.Unit.EyeType]; // One of two eye options.

    protected override Sprite HeadSprite(Actor_Unit actor) // Head.
    {
        if (actor.IsAttacking || actor.IsOralVoring) return State.GameManager.SpriteDictionary.WyvernMatron[1];
        if (actor.HasBelly || actor.GetBallSize(30) != 0) return State.GameManager.SpriteDictionary.WyvernMatron[2];
        return State.GameManager.SpriteDictionary.WyvernMatron[0];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Legs.
    {
        int size = actor.GetBallSize(30);

        if (size >= 15) return State.GameManager.SpriteDictionary.WyvernMatron[18];
        if (size >= 10) return State.GameManager.SpriteDictionary.WyvernMatron[17];
        return State.GameManager.SpriteDictionary.WyvernMatron[16];
    }

    protected override Sprite HairSprite(Actor_Unit actor) // Hair.
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.WyvernMatron[11];

        if (actor.AnimationController.frameLists[2].currentlyActive)
        {
            if (actor.AnimationController.frameLists[2].currentTime >= frameListHair.times[actor.AnimationController.frameLists[2].currentFrame])
            {
                actor.AnimationController.frameLists[2].currentFrame++;
                actor.AnimationController.frameLists[2].currentTime = 0f;

                if (actor.AnimationController.frameLists[2].currentFrame >= frameListHair.frames.Length)
                {
                    actor.AnimationController.frameLists[2].currentlyActive = false;
                    actor.AnimationController.frameLists[2].currentFrame = 0;
                    actor.AnimationController.frameLists[2].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.WyvernMatron[11 + frameListHair.frames[actor.AnimationController.frameLists[2].currentFrame]];
        }

        if (State.Rand.Next(100) == 0)
        {
            actor.AnimationController.frameLists[2].currentlyActive = true;
        }

        return State.GameManager.SpriteDictionary.WyvernMatron[11];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail.
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.WyvernMatron[23];

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.WyvernMatron[22 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(250) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return State.GameManager.SpriteDictionary.WyvernMatron[23];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Wing, controls animation
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.WyvernMatron[5];

        if (actor.AnimationController.frameLists[1].currentTime >= frameListWings.times[actor.AnimationController.frameLists[1].currentFrame])
        {
            actor.AnimationController.frameLists[1].currentFrame++;
            actor.AnimationController.frameLists[1].currentTime = 0f;

            if (actor.AnimationController.frameLists[1].currentFrame >= frameListWings.frames.Length)
            {
                actor.AnimationController.frameLists[1].currentFrame = 0;
                actor.AnimationController.frameLists[1].currentTime = 0f;
            }
        }

        return State.GameManager.SpriteDictionary.WyvernMatron[5 + frameListWings.frames[actor.AnimationController.frameLists[1].currentFrame]];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right Wing
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.WyvernMatron[7];

        return State.GameManager.SpriteDictionary.WyvernMatron[7 + frameListWings.frames[actor.AnimationController.frameLists[1].currentFrame]];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Sheath
    {
        return State.GameManager.SpriteDictionary.WyvernMatron[19];
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick + CV
    {
        if (actor.IsCockVoring) return State.GameManager.SpriteDictionary.WyvernMatron[21];
        if (actor.IsErect()) return State.GameManager.SpriteDictionary.WyvernMatron[20];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (actor.GetBallSize(30) == 0 && Config.HideCocks == false) return State.GameManager.SpriteDictionary.WyvernMatron[28];

        int size = actor.GetBallSize(30);

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[53];
        }

        else if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[52];
        }

        else if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[51];
        }

        else if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[50];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[49];
        }

        else if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[48];
        }

        if (size > 19) size = 19;

        return State.GameManager.SpriteDictionary.WyvernMatron[28 + size];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(42);

        if (size == 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[88];
        }

        if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[87];
        }

        if (size >= 39 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[86];
        }

        if (size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[85];
        }

        if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[84];
        }

        if (size >= 33 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[83];
        }

        if (size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[82];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[81];
        }

        if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WyvernMatron[80];
        }

        if (size > 25) size = 25;

        return State.GameManager.SpriteDictionary.WyvernMatron[54 + size];
    }
}
