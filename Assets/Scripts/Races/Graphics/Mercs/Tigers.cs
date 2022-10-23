using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Tigers : DefaultRaceData
{
    public Tigers()
    {
        FurCapable = true;
        BodyAccent5 = new SpriteExtraInfo(6, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BaseBody = true;

        AllowedMainClothingTypes.Insert(0, RaceSpecificClothing.TigerSpecial);      
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.ClothingType != 0)
            unit.ClothingType = 1;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bodies[14];
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.BodyParts[4];
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        int thinOffset = actor.Unit.BodySize < 2 ? 8 : 0;
        return (Config.FurryHandsAndFeet || actor.Unit.Furry) ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[6 + thinOffset + (actor.IsAttacking ? 1 : 0)] : null;
    }
}
