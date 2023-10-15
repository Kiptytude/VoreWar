using System.Collections.Generic;
using UnityEngine;

class Abakhanskya : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Abakhanskya;

    public Abakhanskya()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        BodySizes = 8;

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); //Right Breast / Shoulders
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored); //Its her head, man...
        Breasts = new SpriteExtraInfo(7, BreastsSprite, WhiteColored); //Boob 1
        SecondaryBreasts = new SpriteExtraInfo(1, SecondaryBreastsSprite, WhiteColored); //Boob Deux
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, WhiteColored); //Belly placeholder
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored); //Left Arm
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored); //Right Arm
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored); // Tail
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // Legs
        BodyAccent5 = new SpriteExtraInfo(1, BodyAccentSprite5, WhiteColored); // Shadow
        Belly = new SpriteExtraInfo(5, null, WhiteColored); // You know, the reason we're all here

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Abakhanskya";
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize >= 5)
            return Sprites[22];
        else 
            return Sprites[21];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[13];
        else
            return Sprites[18];
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize >= 7)
            return Sprites[1];
        else
            return Sprites[0];
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize >= 7)
            return Sprites[3];
        else
            return Sprites[2];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Left Arm
    {
        return Sprites[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) //Right Arm
    {
        return Sprites[19 + (actor.IsAttacking ? 1 : 0)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail
    {
        if (actor.Unit.BodySize >= 3)
            return Sprites[26];
        else
            return Sprites[25];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Leggies
    {
        if (actor.Unit.BodySize >= 5)
            return Sprites[24];
        else
            return Sprites[23];
    }

    //protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Shadow
    //{
        //int size = actor.GetStomachSize(21);
        //if (size >= 5)
    //}

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // O R B
    {
        int size = actor.GetStomachSize(21);
        if (actor.HasBelly == false)
            {
            //belly.SetActive(false);
            return Sprites[5];
            }
        else if (actor.HasBelly == true)
            {
            //belly.SetActive(true);
            return Sprites[5 + actor.GetStomachSize(7)];
            }
        else return null;

    }

}

