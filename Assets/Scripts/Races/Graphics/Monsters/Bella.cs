using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Bella : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Bella;
    readonly Sprite[] Body1 = State.GameManager.SpriteDictionary.BellaBody1;
    readonly Sprite[] Head1 = State.GameManager.SpriteDictionary.BellaHead1;
    readonly Sprite[] Arm1 = State.GameManager.SpriteDictionary.BellaArm1;
    readonly Sprite[] Body2 = State.GameManager.SpriteDictionary.BellaBody2;
    readonly Sprite[] Head2 = State.GameManager.SpriteDictionary.BellaHead2;
    readonly Sprite[] Arm2 = State.GameManager.SpriteDictionary.BellaArm2;

    public Bella()
    {
        FixedGender = true;
        CanBeGender = new List<Gender>() { Gender.Female};
        SkinColors = 1;
        EyeTypes = 1;
        GentleAnimation = false;
        BodySizes = 2;

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); // body, vore bellies, boobs
        Head = new SpriteExtraInfo(3, HeadSprite, WhiteColored);
        //Belly = new SpriteExtraInfo(4, null, WhiteColored); 
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored); // Arm + staff

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        //Debug.Log("BodySize: " + actor.Unit.BodySize + " StomachSize:" + actor.GetStomachSize(3,2));

        //Sprites , Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if(actor.HasBelly == true)
                return Body1[1 + actor.GetStomachSize(2,1)]; // 4 bellies total

            return Body1[0];  
        }

        //Sprites , Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.HasBelly == true)
                return Body2[1 + actor.GetStomachSize(2, 1)];
            return Body2[0];
        }

        
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) //Belly and Boob
    {
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        //Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsOralVoring == true)
            {
                if (actor.HasBelly == false) //If vore sucess
                    return Head1[1];
                return Head1[2]; // if vore fail
            }

            return Head1[0];
        }

        //Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsOralVoring == true)
            {
                if (actor.HasBelly == false) //If vore sucess
                    return Head2[1];
                return Head2[2]; // if vore fail
            }

            return Head2[0];
        }

        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Arm and staff
    {
        //Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsAttacking == true)
                return Arm1[1];
            return Arm1[0];
        }

        //Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsAttacking == true)
                return Arm2[1];
            return Arm2[0];
        }

        return null;
    }

}
