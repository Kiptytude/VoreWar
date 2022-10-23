using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Humans : DefaultRaceData
{
    public Humans()
    {
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, BodyColor);
        BodyAccent = null;
        BodyAccent2 = null;
        BodyAccent3 = null;
        AccessoryColors = 1;
        BaseBody = true;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bodies[9];   
}
