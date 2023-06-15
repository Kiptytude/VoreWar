using System.Collections.Generic;
using UnityEngine;

class Harvester : BlankSlate
{
    RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
    RaceFrameList frameListArms = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });
    RaceFrameList frameListDick = new RaceFrameList(new int[6] { 0, 1, 0, 1, 0, 1 }, new float[6] { .2f, .2f, .2f, .2f, .3f, .4f });
    RaceFrameList frameListTongue = new RaceFrameList(new int[13] { 0, 1, 2, 3, 4, 2, 3, 4, 2, 3, 4, 1, 0 }, new float[13] { .2f, .2f, .2f, .3f, .2f, .3f, .2f, .2f, .2f, .2f, .3f, .3f, .3f });

    public Harvester()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Harvester);
        CanBeGender = new List<Gender>() { Gender.Male };
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Tail
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Body
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Head
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Arms
        Dick = new SpriteExtraInfo(4, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Dick
        Belly = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Belly
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, s.Unit.SkinColor)); // Tongue
    }

    internal override int DickSizes => 1;
    internal override int BreastSizes => 1;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Eye controller. Index 0.
            new AnimationController.FrameList(0, 0, false), // Arm controller. Index 1.
            new AnimationController.FrameList(0, 0, false),  // Dick controller. Index 2.
            new AnimationController.FrameList(0, 0, false)}; // Tongue controller. Index 3.
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail
    {
        return State.GameManager.SpriteDictionary.Harvester[1];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return State.GameManager.SpriteDictionary.Harvester[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.Harvester[2];

        if (actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return State.GameManager.SpriteDictionary.Harvester[5];
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListEyes.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListEyes.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.Harvester[2 + frameListEyes.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(400) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return State.GameManager.SpriteDictionary.Harvester[2];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Arms
    {
        if (!actor.Targetable) return State.GameManager.SpriteDictionary.Harvester[6];

        if (actor.IsAttacking)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return State.GameManager.SpriteDictionary.Harvester[9];
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListArms.times[actor.AnimationController.frameLists[1].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListArms.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.Harvester[6 + frameListArms.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return State.GameManager.SpriteDictionary.Harvester[6];
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        if (!actor.IsErect())
        {
            return null;
        }

        if (!actor.Targetable) return null;

        if (actor.AnimationController.frameLists[2].currentlyActive)
        {
            if (actor.AnimationController.frameLists[2].currentTime >= frameListDick.times[actor.AnimationController.frameLists[2].currentFrame])
            {
                actor.AnimationController.frameLists[2].currentFrame++;
                actor.AnimationController.frameLists[2].currentTime = 0f;

                if (actor.AnimationController.frameLists[2].currentFrame >= frameListDick.frames.Length)
                {
                    actor.AnimationController.frameLists[2].currentlyActive = false;
                    actor.AnimationController.frameLists[2].currentFrame = 0;
                    actor.AnimationController.frameLists[2].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.Harvester[10 + frameListDick.frames[actor.AnimationController.frameLists[2].currentFrame]];
        }

        if (State.Rand.Next(300) == 0)
        {
            actor.AnimationController.frameLists[2].currentlyActive = true;
            actor.AnimationController.frameLists[2].currentFrame = 0;
            actor.AnimationController.frameLists[2].currentTime = 0f;
        }

        if (actor.IsErect()) return State.GameManager.SpriteDictionary.Harvester[10];

        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(26);

        if (size >= 26 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.Harvester[38];
        }

        if (size >= 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Harvester[37];
        }

        if (size >= 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Harvester[36];
        }

        if (size >= 20 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Harvester[35];
        }

        if (size > 18) size = 18;

        return State.GameManager.SpriteDictionary.Harvester[17 + size];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tongue
    {
        if (!actor.Targetable) return null;

        if (actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[3].currentlyActive = false;
            actor.AnimationController.frameLists[3].currentFrame = 0;
            actor.AnimationController.frameLists[3].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[3].currentlyActive)
        {
            if (actor.AnimationController.frameLists[3].currentTime >= frameListTongue.times[actor.AnimationController.frameLists[3].currentFrame])
            {
                actor.AnimationController.frameLists[3].currentFrame++;
                actor.AnimationController.frameLists[3].currentTime = 0f;

                if (actor.AnimationController.frameLists[3].currentFrame >= frameListTongue.frames.Length)
                {
                    actor.AnimationController.frameLists[3].currentlyActive = false;
                    actor.AnimationController.frameLists[3].currentFrame = 0;
                    actor.AnimationController.frameLists[3].currentTime = 0f;
                }
            }

            return State.GameManager.SpriteDictionary.Harvester[12 + frameListTongue.frames[actor.AnimationController.frameLists[3].currentFrame]];
        }

        if (State.Rand.Next(500) == 0 && actor.HasBelly)
        {
            actor.AnimationController.frameLists[3].currentlyActive = true;
        }

        return null;
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(0, SkinColors);
        unit.DickSize = 0;
        unit.SetDefaultBreastSize(0);
    }
}

