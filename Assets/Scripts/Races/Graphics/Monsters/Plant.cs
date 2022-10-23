using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Plant : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Plant;
    public Plant()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored);
        Belly = new SpriteExtraInfo(3, null, WhiteColored);
        EyeTypes = 2;
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[4];

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int headType = actor.Unit.EyeType * 2;
        if (actor.IsAttacking || actor.IsEating)
            headType++;
        return Sprites[headType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return Sprites[5];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && actor.GetStomachSize(15, 1) == 15)
            return State.GameManager.SpriteDictionary.Plant[15];			
        return State.GameManager.SpriteDictionary.Plant[6 + actor.GetStomachSize(8)];
    }
}
