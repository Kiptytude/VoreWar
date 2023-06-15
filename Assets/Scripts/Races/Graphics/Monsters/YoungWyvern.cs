using System.Collections.Generic;
using UnityEngine;

class YoungWyvern : BlankSlate
{
    const float StomachGainDivisor = 1.2f; //Higher is faster, should be balanced with stomach size to max out at 80-100 capacity
    public YoungWyvern()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        GentleAnimation = true;
        SkinColors = ColorMap.SlimeColorCount;
        AccessoryColors = ColorMap.SlimeColorCount;
        Body = new SpriteExtraInfo(3, BodySprite, (s) => ColorMap.GetSlimeColor(s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, (s) => ColorMap.GetSlimeColor(s.Unit.AccessoryColor));
        Mouth = new SpriteExtraInfo(10, MouthSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, (s) => ColorMap.GetSlimeColor(s.Unit.AccessoryColor));
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
        {
            if (actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[1];
            if (actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[5];
            if (actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[8];
            if (actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[11];
            if (actor.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[13];
            return State.GameManager.SpriteDictionary.YoungWyvern[15];
        }
        else
        {
            if (actor.HasBelly == false)
                return State.GameManager.SpriteDictionary.YoungWyvern[0];
            if (actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[2];
            if (actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[4];
            if (actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[7];
            if (actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[10];
            if (actor.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
                return State.GameManager.SpriteDictionary.YoungWyvern[12];
            return State.GameManager.SpriteDictionary.YoungWyvern[14];
        }


    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
        {
            BodyAccent.GetColor = (s) => ColorMap.GetSlimeColor(s.Unit.AccessoryColor);
            BodyAccent.layer = 5;
            return State.GameManager.SpriteDictionary.YoungWyvern[3];
        }
        BodyAccent.GetColor = (s) => ColorMap.GetSlimeColor(s.Unit.SkinColor);
        BodyAccent.layer = 0;
        if (actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
            return State.GameManager.SpriteDictionary.YoungWyvern[29];
        if (actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
            return State.GameManager.SpriteDictionary.YoungWyvern[6];
        if (actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
            return State.GameManager.SpriteDictionary.YoungWyvern[9];
        if (actor.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor)
            return State.GameManager.SpriteDictionary.YoungWyvern[16];
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor) => actor.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor && ((actor.IsEating || actor.IsAttacking) == false) ? State.GameManager.SpriteDictionary.YoungWyvern[17] : null;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        Belly.layer = 8;
        if (actor.PredatorComponent.VisibleFullness < 0.2 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[18];
        }
        if (actor.PredatorComponent.VisibleFullness < 0.5 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[19];
        }
        if (actor.PredatorComponent.VisibleFullness < 1.2 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[20];
        }
        if (actor.PredatorComponent.VisibleFullness < 1.75 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[21];
        }
        if (actor.PredatorComponent.VisibleFullness < 2.5 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[22];
        }
        if (actor.PredatorComponent.VisibleFullness < 3 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[23];
        }
        if (actor.PredatorComponent.VisibleFullness < 3.5 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[24];
        }
        Belly.layer = 2;
        if (actor.PredatorComponent.VisibleFullness > 5 / StomachGainDivisor && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true))
            return State.GameManager.SpriteDictionary.YoungWyvern[31];
        if (actor.PredatorComponent.VisibleFullness > 5 / StomachGainDivisor && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false))
            return State.GameManager.SpriteDictionary.YoungWyvern[30];
        if (actor.PredatorComponent.VisibleFullness < 4 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[25];
        }
        if (actor.PredatorComponent.VisibleFullness < 4.5 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[26];
        }
        if (actor.PredatorComponent.VisibleFullness < 5 / StomachGainDivisor)
        {
            return State.GameManager.SpriteDictionary.YoungWyvern[27];
        }

        return State.GameManager.SpriteDictionary.YoungWyvern[28];

    }


}
