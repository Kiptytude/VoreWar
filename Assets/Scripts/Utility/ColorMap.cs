using System.Collections.Generic;
using UnityEngine;

public static class ColorMap
{

    enum color
    {
        White,
        Tone1,
        Tone2,
        Tone3,
        Tone4,
        Tone5,
        Brown,
        Red,
        Pink,
        Yellow,
        Golden,
        Orange,
        DarkGray,
        OffWhite,
        DarkBlue,
        DarkerBlue,
        DarkGreen,
        DarkerGreen,
        DarkPurple,
        DarkerPurple,
        Blue,
        LightBlue,
        LightGreen,
        LightPurple,
        RedOrange,
    }

    static Color[] Colors;

    static Color[] HairColors;
    static Color[] SkinColors;
    static Color[] BodyAccesoryColors;
    static Color[] LizardColors;
    static Color[] SlimeColors;

    static Color[] EarthyColors;

    static Color[] EyeColors;
    static Color[] ClothingColors;

    static Color[] ImpSkinColors;
    static Color[] ImpSecondaryColors;
    static Color[] ImpHairColors;
    static Color[] ImpScleraColors;

    static Color[] VagrantColors;

    static Color[] SharkColors;
    static Color[] SharkBellyColors;
    static Color[] PastelColors;

    static Color[] WyvernColors;
    static Color[] WyvernBellyColors;

    static Color[] SchiwardezColors;

    static Color[] ExoticColors;

    static Color[] DratopyrMainColors;
    static Color[] DratopyrWingColors;
    static Color[] DratopyrFleshColors;
    static Color[] DratopyrEyeColors;

    static ColorMap()
    {
        Colors = new Color[25];
        Colors[(int)color.White] = new Color(1.0f, 1.0f, 1.0f);
        Colors[(int)color.Tone1] = new Color(1f, .8f, .643f);
        Colors[(int)color.Tone2] = new Color(1f, .757f, .565f);
        Colors[(int)color.Tone3] = new Color(.914f, .725f, .584f);
        Colors[(int)color.Tone4] = new Color(.875f, .694f, .459f);
        Colors[(int)color.Tone5] = new Color(.804f, .631f, .518f);
        Colors[(int)color.Brown] = new Color(.5f, .4f, .25f);
        Colors[(int)color.Red] = new Color(.7f, .1f, 0);
        Colors[(int)color.Pink] = new Color(.9f, .4f, .4f);
        Colors[(int)color.Yellow] = new Color(.9f, .9f, .4f);
        Colors[(int)color.Golden] = new Color(.5f, .5f, .1f);
        Colors[(int)color.Orange] = new Color(.9f, .6f, .0f);
        Colors[(int)color.DarkGray] = new Color(.2f, .2f, .2f);
        Colors[(int)color.OffWhite] = new Color(.9f, .9f, .9f);
        Colors[(int)color.DarkBlue] = new Color(.3f, .3f, .85f);
        Colors[(int)color.DarkerBlue] = new Color(.15f, .15f, .75f);
        Colors[(int)color.DarkGreen] = new Color(0, .45f, 0);
        Colors[(int)color.DarkerGreen] = new Color(0, .3f, 0);
        Colors[(int)color.DarkPurple] = new Color(.4f, 0.1f, .8f);
        Colors[(int)color.DarkerPurple] = new Color(.325f, 0.5f, .65f);
        Colors[(int)color.Blue] = new Color(.3f, .3f, .8f);
        Colors[(int)color.LightBlue] = new Color(.6f, .6f, .9f);
        Colors[(int)color.LightGreen] = new Color(.6f, .9f, .6f);
        Colors[(int)color.LightPurple] = new Color(.7f, .4f, .9f);
        Colors[(int)color.RedOrange] = new Color(.92f, .45f, .03f);

        HairColors = new Color[]
        {
            Colors[(int)color.Brown],
            Colors[(int)color.Pink],
            Colors[(int)color.Red],
            Colors[(int)color.Yellow],
            Colors[(int)color.Golden],
            Colors[(int)color.Orange],
            Colors[(int)color.DarkGray],
            Colors[(int)color.OffWhite],
            Colors[(int)color.RedOrange],
        };

        BodyAccesoryColors = new Color[]
        {
            Colors[(int)color.Brown],
            Colors[(int)color.Pink],
            Colors[(int)color.Red],
            Colors[(int)color.Yellow],
            Colors[(int)color.Golden],
            Colors[(int)color.Orange],
            Colors[(int)color.DarkGray],
            Colors[(int)color.OffWhite],
            Colors[(int)color.RedOrange],
        };

        SkinColors = new Color[]
         {
            Colors[(int)color.Tone1],
            Colors[(int)color.Tone2],
            Colors[(int)color.Tone3],
            Colors[(int)color.Tone4],
            Colors[(int)color.Tone5],
         };

        LizardColors = new Color[]
        {
            Colors[(int)color.DarkBlue],
            Colors[(int)color.DarkerBlue],
            Colors[(int)color.DarkGreen],
            Colors[(int)color.DarkerGreen],
            Colors[(int)color.DarkPurple],
            Colors[(int)color.DarkerPurple],
            Colors[(int)color.Brown],
            Colors[(int)color.Golden],
        };

        EyeColors = new Color[]
        {
            Colors[(int)color.Brown],
            Colors[(int)color.Pink],
            Colors[(int)color.Red],
            Colors[(int)color.Yellow],
            Colors[(int)color.Golden],
            Colors[(int)color.Orange],
            Colors[(int)color.Blue],
            Colors[(int)color.LightBlue],
            Colors[(int)color.LightGreen],
            Colors[(int)color.DarkGreen],
            Colors[(int)color.LightPurple],
            new Color(.22f, .95f, 1),
        };


        SlimeColors = new Color[]
        {
            Colors[(int)color.Blue],
            Colors[(int)color.LightBlue],
            Colors[(int)color.DarkGreen],
            Colors[(int)color.LightGreen],
            Colors[(int)color.LightPurple],
            Colors[(int)color.Pink],
            Colors[(int)color.Red],
            Colors[(int)color.Orange],
            Colors[(int)color.DarkGray],
            new Color(.96f, .94f, .92f),
        };


        EarthyColors = new Color[]
        {
            new Color(.29f, .21f, .15f),
            new Color(.50f, .42f, .34f),
            new Color(.66f, .63f, .53f),
            new Color(.73f, .61f, .38f),
            new Color(.73f, .70f, .47f),
        };

        ClothingColors = new Color[]
        {
            Colors[(int)color.Blue],
            Colors[(int)color.LightGreen],
            Colors[(int)color.DarkGreen],
            Colors[(int)color.LightBlue],
            Colors[(int)color.DarkBlue],
            Colors[(int)color.LightPurple],
            Colors[(int)color.DarkPurple],
            Colors[(int)color.Brown],
            Colors[(int)color.Pink],
            Colors[(int)color.Red],
            Colors[(int)color.Yellow],
            Colors[(int)color.Golden],
            Colors[(int)color.Orange],
            Colors[(int)color.DarkGray],
            Colors[(int)color.OffWhite],
        };

        ImpSkinColors = new Color[]
        {
            new Color(.75f, .85f, .85f),
            new Color(.85f, .77f, .75f),
            new Color(.86f, .92f, .75f),
            new Color(1f, .91f, 1f),
            Colors[(int)color.OffWhite],
            new Color(.86f, .96f, 1f),
            new Color(1f, 1f, .79f),
        };

        ImpSecondaryColors = new Color[]
{
            new Color(.22f, .14f, .26f),
            new Color(.26f, .14f, .38f),
            new Color(.29f, 0, .04f),
            new Color(.09f, .17f, .2f),
            new Color(.43f, 0, .22f),
            new Color(.19f, .19f, .19f),
};

        ImpHairColors = new Color[]
        {
            new Color(.32f, .32f, .32f),
            new Color(.91f, .39f, .1f),
            new Color(1f, .98f, .61f),
            new Color(.89f, 0, .48f),
            new Color(1f, .98f, .61f),
            Colors[(int)color.White],
            new Color(.61f, .25f, .06f),
            new Color(0, .69f, .2f),
        };

        ImpScleraColors = new Color[]
        {
            Color.white,
            new Color(1f, .98f, .61f),
            new Color(.9f, .9f, .5f),
            new Color(.9f, .8f, .6f),
            new Color(1f, .66f, .61f),
            Colors[(int)color.Red],
        };


        VagrantColors = new Color[]
        {
            new Color(1f, .66f, .66f),
            new Color(.85f, .66f, 1f),
            new Color(.66f, .73f, 1f),
            new Color(.66f, 1f, .85f),
            new Color(.85f, 1f, .66f),
            new Color(1, .80f, .66f),
        };

        SharkColors = new Color[]
       {
            new Color(.3f, .8f, 1f),
            new Color(.7f, .9f, 1f),
            new Color(.5f, .8f, 1f),
            new Color(.8f, .7f, .3f),
            new Color(.8f, .3f, .3f),
            new Color(.1f, .3f, .5f),
            new Color(.4f, .4f, .4f),
            new Color(.2f, .7f, .5f),
            new Color(1f, 1f, 1f)
       };

        SharkBellyColors = new Color[]
        {
            new Color(1f, 1f, 1f),
            new Color(.9f, .9f, .9f),
            new Color(.8f, .8f, .8f),
            new Color(.6f, .6f, .6f)
        };

        PastelColors = new Color[]
        {
            new Color(.93f, 1f, .965f),
            new Color(.92f, 1f, 1f),
            new Color(.92f, .965f, 1f),
            new Color(.84f, .84f, .975f),
            new Color(1f, .88f, .93f),
            new Color(1f, .84f, .8f),
            new Color(1f, .975f, .75f),
            new Color(.88f, 1f, .92f),
            new Color(1f, .95f, .92f),
            new Color(.95f, .55f, .95f),
        };


        WyvernColors = new Color[]
          {
            new Color(.8f, .4f, .1f), // Flame
            new Color(.7f, .05f, 0f), // Crimson
            new Color(.2f, .2f, .8f), // Blue
            new Color(.5f, .6f, 1f), // Sky
            new Color(.25f, .25f, .3f), // Black
            new Color(.1f, .4f, .1f), // Deep Green
            new Color(.5f, .2f, .9f), // Purple
            new Color(1f, 1f, 0f), // Yellow
            new Color(.5f, .8f, .5f), // Pale Green
            new Color(.8f, .5f, .5f), // Rose Red
            new Color(.92f, .95f, .96f), // Dust
          };

        WyvernBellyColors = new Color[] // Lighter versions of the main wyvern colours.
       {
            new Color(1f, .6f, .2f), // Flame
            new Color(1f, .15f, .05f), // Crimson
            new Color(.3f, .3f, 1f), // Blue
            new Color(.6f, .75f, 1f), // Sky
            new Color(.6f, .6f, .7f), // Black
            new Color(.2f, .6f, .2f), // Deep Green
            new Color(.6f, .3f, 1f), // Purple
            new Color(.6f, 1f, .6f), // Pale Green
            new Color(1f, .6f, .6f), // Rose Red
            new Color(.94f, .96f, .98f), // Dust
       };

        ExoticColors = new Color[]
       {
            new Color(.6f, 0f, 1f),
            new Color(.5f, .9f, 0f),
            new Color(1f, .9f, 0f),
            new Color(1f, 0f, 0f),
            new Color(0f, .8f, .7f),
            new Color(.5f, .9f, 0f),
            new Color(.2f, 0f, .8f),
            new Color(1f, .5f, 0f)
       };

        SchiwardezColors = new Color[]
        {
            new Color(.8f, .5f, 0f),
            new Color(.5f, .5f, .5f),
            new Color(.2f, .2f, .25f),
            new Color(0f, 0f, .8f),
            new Color(0f, .7f, .7f),
            new Color(.8f, 0f, 0f)
        };

        DratopyrMainColors = new Color[]
        {
            new Color(.4f, .4f, 1f),
            new Color(.4f, 1f, .4f),
            new Color(1f, .4f, .4f),
            new Color(1f, 1f, 1f),
            new Color(.8f, .8f, .8f),
            new Color(.2f, .2f, .9f),
            new Color(.2f, .9f, .2f),
            new Color(.9f, .2f, .2f),
            new Color(.6f, 0f, 1f),
            new Color(.4f, 0f, .9f)
        };

        DratopyrWingColors = new Color[]
        {
            new Color(.2f, .2f, .8f),
            new Color(.2f, .8f, .2f),
            new Color(.8f, .2f, .2f),
            new Color(.1f, .1f, .7f),
            new Color(.1f, .7f, .1f),
            new Color(.7f, .1f, .1f),
            new Color(.4f, 0f, .8f),
            new Color(.2f, 0f, .7f)
        };

        DratopyrFleshColors = new Color[]
        {
            new Color(.3f, .3f, 1f),
            new Color(1f, .3f, .3f),
            new Color(.6f, 0f, 1f),
            new Color(1f, .9f, 0f)
        };

        DratopyrEyeColors = new Color[]
        {
            new Color(1f, 1f, 1f),
            new Color(.6f, 0f, 0f),
            new Color(0f, 0f, 0f)
        };

    }

    public static Color GetColor(Color[] type, int index)
    {
        if (index < type.Length)
            return type[index];
        return type[0];
    }

    public static Color GetHairColor(int i) => GetColor(HairColors, i);
    public static Color GetEyeColor(int i) => GetColor(EyeColors, i);
    public static Color GetSkinColor(int i) => GetColor(SkinColors, i);
    public static Color GetBodyAccesoryColor(int i) => GetColor(BodyAccesoryColors, i);
    public static Color GetLizardColor(int i) => GetColor(LizardColors, i);
    public static Color GetSlimeColor(int i) => GetColor(SlimeColors, i);
    public static Color GetEarthyColor(int i) => GetColor(EarthyColors, i);
    public static Color GetImpSkinColor(int i) => GetColor(ImpSkinColors, i);
    public static Color GetImpSecondaryColor(int i) => GetColor(ImpSecondaryColors, i);
    public static Color GetImpHairColor(int i) => GetColor(ImpHairColors, i);
    public static Color GetImpScleraColor(int i) => GetColor(ImpScleraColors, i);
    public static Color GetClothingColor(int i) => GetColor(ClothingColors, i);
    public static Color GetVagrantColor(int i) => GetColor(VagrantColors, i);
    public static Color GetSharkColor(int i) => GetColor(SharkColors, i);
    public static Color GetSharkBellyColor(int i) => GetColor(SharkBellyColors, i);
    public static Color GetPastelColor(int i) => GetColor(PastelColors, i);
    public static Color GetWyvernColor(int i) => GetColor(WyvernColors, i);
    public static Color GetWyvernBellyColor(int i) => GetColor(WyvernBellyColors, i);
    public static Color GetExoticColor(int i) => GetColor(ExoticColors, i);
    public static Color GetSchiwardezColor(int i) => GetColor(SchiwardezColors, i);

    public static Color GetDratopyrMainColor(int i) => GetColor(DratopyrMainColors, i);
    public static Color GetDratopyrWingColor(int i) => GetColor(DratopyrWingColors, i);
    public static Color GetDratopyrFleshColor(int i) => GetColor(DratopyrFleshColors, i);
    public static Color GetDratopyrEyeColor(int i) => GetColor(DratopyrEyeColors, i);
  

    public static int HairColorCount => HairColors.Length;
    public static int EyeColorCount => EyeColors.Length;
    public static int SkinColorCount => SkinColors.Length;
    public static int BodyAccesoryColorCount => BodyAccesoryColors.Length;
    public static int LizardColorCount => LizardColors.Length;
    public static int SlimeColorCount => SlimeColors.Length;
    public static int EarthyColorCount => EarthyColors.Length;
    public static int ImpSkinColorCount => ImpSkinColors.Length;
    public static int ImpSecondaryColorCount => ImpSecondaryColors.Length;
    public static int ImpHairColorCount => ImpHairColors.Length;
    public static int ImpScleraColorCount => ImpScleraColors.Length;
    public static int ClothingColorCount => ClothingColors.Length;
    public static int VagrantColorCount => VagrantColors.Length;
    public static int SharkColorCount => SharkColors.Length;
    public static int SharkBellyColorCount => SharkBellyColors.Length;
    public static int PastelColorCount => PastelColors.Length;
    public static int WyvernColorCount => WyvernColors.Length;
    public static int WyvernBellyColorCount => WyvernBellyColors.Length;
    public static int ExoticColorCount => ExoticColors.Length;
    public static int SchiwardezColorCount => SchiwardezColors.Length;

    public static int DratopyrMainColorCount => DratopyrMainColors.Length;
    public static int DratopyrWingColorCount => DratopyrWingColors.Length;
    public static int DratopyrFleshColorCount => DratopyrFleshColors.Length;
    public static int DratopyrEyeColorCount => DratopyrEyeColors.Length;

}
