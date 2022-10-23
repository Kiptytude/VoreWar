using System;
using UnityEngine;

class SpriteExtraInfo
{
    //internal Sprite sprite;
    //internal Color Color;
    //internal ColorPalette Palette;
    internal int layer;
    internal Func<Actor_Unit, Sprite> GetSprite;
    internal Func<Actor_Unit, Color> GetColor;
    internal Func<Actor_Unit, ColorSwapPalette> GetPalette;
    internal float XOffset;
    internal float YOffset;

    public SpriteExtraInfo(int layer, Func<Actor_Unit, Sprite> getSprite, Func<Actor_Unit, Color> getColor, Func<Actor_Unit, ColorSwapPalette> getPalette = null)
    {
        this.layer = layer;
        GetSprite = getSprite;
        GetColor = getColor;
        GetPalette = getPalette;
        XOffset = 0;
        YOffset = 0;
    }

}
