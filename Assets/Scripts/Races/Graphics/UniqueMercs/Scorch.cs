using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Scorch : BlankSlate
{
    public Scorch()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
		GentleAnimation = true;
        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Scorch";
    }

    protected override Sprite BodySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Scorch[actor.IsAttacking || actor.IsOralVoring ? 1 : 0];

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Scorch[2];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        {
            if (actor.PredatorComponent.VisibleFullness > 4)
                return State.GameManager.SpriteDictionary.Scorch[7];
		}
		
        return actor.HasBelly ? State.GameManager.SpriteDictionary.Scorch[3 + actor.GetStomachSize(3)] : null;
    }
}

