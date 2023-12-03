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

        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored); // Body
        Belly = new SpriteExtraInfo(2, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); // Leg
        Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored); // Face
        Hair = new SpriteExtraInfo(5, HeadSprite, WhiteColored); // Attack Frame
        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        //  if (actor.IsAttacking)
        //      return Sprites[2];
        //   if (actor.IsOralVoring)
        //       return Sprites[1];

        //Debug.Log("BodySize: " + actor.Unit.BodySize + " StomachSize:" + actor.GetStomachSize(3,2));

        //Sprites , Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if(actor.HasBelly == true)
                return Sprites[1 + actor.GetStomachSize(2,2)];

            return Sprites[0];  
        }

        //Sprites , Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.HasBelly == true)
                return Sprites[5];
            return Sprites[4];
        }

        
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) // Face
    {
        return null;
        if (actor.IsOralVoring)
            return null;
        return Sprites[4+actor.Unit.EyeType];
    }

   // protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[9]; // Leg

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        return null;
        if (actor.HasBelly == false)
            return null;
        return Sprites[1 + actor.GetStomachSize(4)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Attack Antenna
    {
      //  if (actor.IsAttacking)
      //      return Sprites[3];
        return null;
    }

}
