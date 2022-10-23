using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Cats : DefaultRaceData
{
    public Cats()
    {
        FurCapable = true;
        BaseBody = true;
        AllowedMainClothingTypes.Add(RaceSpecificClothing.CatLeader);
        AvoidedMainClothingTypes++;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.CatLeader);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bodies[8];
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.BodyParts[0];
}
