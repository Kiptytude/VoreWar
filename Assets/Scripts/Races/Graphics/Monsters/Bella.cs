using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Bella : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Bella;

    public Bella()
    {
        FixedGender = true;
        CanBeGender = new List<Gender>() { Gender.Female};
        SkinColors = 1;
        EyeTypes = 1;
        GentleAnimation = false;
        BodySizes = 2;
        /*
        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored); // Body
        Belly = new SpriteExtraInfo(2, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); // Leg
        Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored); // Face
        Hair = new SpriteExtraInfo(5, HeadSprite, WhiteColored); // Attack Frame
        */

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); // Body
        Head = new SpriteExtraInfo(3, HeadSprite, WhiteColored);
        Belly = new SpriteExtraInfo(4, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored); // Arm + staff
        //Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored); // Face
        //Hair = new SpriteExtraInfo(5, HeadSprite, WhiteColored); // Attack Frame
        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        Debug.Log("BodySize: " + actor.Unit.BodySize + " StomachSize:" + actor.GetStomachSize(3,2));

        //Sprites , Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if(actor.HasBelly == true)
                return Sprites[1 + actor.GetStomachSize(3,1)]; // 4 bellies total

            return Sprites[0];  
        }

        //Sprites , Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.HasBelly == true)
                return Sprites[6 + actor.GetStomachSize(1, 1)];
            return Sprites[5];
        }

        
        return null;
    }


   // protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[9]; // Leg

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) //Belly and Boob
    {
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsOralVoring == true)
            {
                if (actor.HasBelly == false)
                    return Sprites[13];
                return Sprites[14];
            }

            return Sprites[12];
        }

        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsOralVoring == true) {
                if (actor.HasBelly == false)
                    return Sprites[15];
                return Sprites[16];
            }

            return Sprites[15];
        }

        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Arm and staff
    {
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsAttacking == true)
                return Sprites[9];
            return Sprites[8];
        }

        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsAttacking == true)
                return Sprites[11];
            return Sprites[10];
        }

        return null;
    }

}
