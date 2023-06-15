using System.Collections.Generic;
using UnityEngine;

class Earthworms : BlankSlate
{
    RaceFrameList frameListHeadIdle = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .5f, .5f, 1.5f, .5f, .5f });

    enum Position
    {
        Underground,
        Aboveground
    }
    Position position;
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Earthworms;

    public Earthworms()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        clothingColors = 0;
        GentleAnimation = true;
        WeightGainDisabled = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EarthwormSkin);

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, WhiteColored); // rocks
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // belly support
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // belly cover
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // tail
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (!actor.HasAttackedThisCombat)
            position = Position.Underground;
        else
            position = Position.Aboveground;
        base.RunFirst(actor);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false) };  // Index 0.
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Belly, 0, -48 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (!actor.Targetable) return Sprites[4];

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[1];
                return Sprites[0];
            case Position.Aboveground:
                return Sprites[4];
        }
        return base.BodySprite(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[8];

        if (actor.IsAttacking || actor.IsEating || position == Position.Underground)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.IsEating || actor.IsAttacking)
            {
                if (position == Position.Underground)
                    return Sprites[16];
                return Sprites[11];
            }
            else
            {
                if (position == Position.Underground)
                    return null;
                return Sprites[8];
            }
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListHeadIdle.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListHeadIdle.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return Sprites[8 + frameListHeadIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return Sprites[8];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Rocks
    {
        if (!actor.Targetable) return null;

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[3];
                return Sprites[2];
            case Position.Aboveground:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Belly support
    {
        if (!actor.Targetable) return null;

        if (position == Position.Aboveground)
            return Sprites[6];
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Belly cover
    {
        if (!actor.Targetable) return Sprites[7];

        if (position == Position.Aboveground && actor.HasBelly == false)
            return Sprites[7];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail
    {
        if (!actor.Targetable) return Sprites[5];

        if (position == Position.Aboveground)
            return Sprites[5];
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[12];

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[17];
                return null;
            case Position.Aboveground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[15];
                return Sprites[12 + frameListHeadIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            default:
                return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (position == Position.Aboveground)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                return Sprites[43];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (actor.GetStomachSize(21, .76f) == 21)
                    return Sprites[42];
                else if (actor.GetStomachSize(21, .84f) == 21)
                    return Sprites[41];
                else if (actor.GetStomachSize(21, .92f) == 21)
                    return Sprites[40];
            }
            return Sprites[18 + actor.GetStomachSize(21)];
        }
        return null;
    }

}
