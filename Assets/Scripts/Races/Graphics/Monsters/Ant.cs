using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Ant : BlankSlate
{
    public Ant()
    {
        CanBeGender = new List<Gender>() { Gender.None };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Ant);
        GentleAnimation = true;
        Head = new SpriteExtraInfo(1, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, s.Unit.SkinColor)); // Head
        Belly = new SpriteExtraInfo(0, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, s.Unit.SkinColor)); // Belly
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.PredatorComponent == null)
            return State.GameManager.SpriteDictionary.Ant[2];

        int size = actor.GetStomachSize(16, 0.75f);

        if (size == 16 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
        {
                return State.GameManager.SpriteDictionary.Ant[19];
        }

        if (size == 16 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
        {
            return State.GameManager.SpriteDictionary.Ant[18];
        }

        if (size > 15) size = 15;

        return State.GameManager.SpriteDictionary.Ant[2 + actor.GetStomachSize(15)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring || actor.IsAttacking) return State.GameManager.SpriteDictionary.Ant[1];

        return State.GameManager.SpriteDictionary.Ant[0];
    }
}
