using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DRACO : BlankSlate
{
    public DRACO()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);		
        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored);		
        Belly = new SpriteExtraInfo(4, null, WhiteColored);
        clothingColors = 0;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "DRACO";
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.DRACO[3];
        return State.GameManager.SpriteDictionary.DRACO[2];
    }

    protected override Sprite BodySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.DRACO[0];
	
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.DRACO[1];	
	
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        {
            if (actor.PredatorComponent.VisibleFullness > 3)
                return State.GameManager.SpriteDictionary.DRACO[10];
        }

        return actor.HasBelly ? State.GameManager.SpriteDictionary.DRACO[5 + actor.GetStomachSize(4)] : null;
    }
}

