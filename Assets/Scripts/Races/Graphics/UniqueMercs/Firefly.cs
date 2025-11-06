using System.Collections.Generic;
using UnityEngine;

class Firefly : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Firefly;
    internal Firefly()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        SpecialAccessoryCount = 2;
        BodyAccentTypes2 = 2;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FireflyColor);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FireflyColor);
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(3, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(7, HeadSprite, WhiteColored);
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, WhiteColored);//Pilot Knife
        BodyAccent2 =null;
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FireflyColor, s.Unit.AccessoryColor));// Shoulder Pauldron
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, WhiteColored);// Shoulder Pauldron
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FireflyColor, s.Unit.SkinColor));// Shoulder Pauldron
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = null;
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(10, WeaponSprite, WhiteColored);//HND15 Pistol
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = null;
        Balls = null;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Firefly";
        unit.SpecialAccessoryType = 1;
        unit.AccessoryColor = 17;
        unit.SkinColor = 17;
        unit.BodyAccentType2 = 1;
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsRangeAttacking)
            return Sprites [2 + (3 * actor.Unit.SpecialAccessoryType)];
        else if (actor.IsMeleeAttacking)
            return Sprites [3 + (3 * actor.Unit.SpecialAccessoryType)];
        else
            return Sprites [1 + (3 * actor.Unit.SpecialAccessoryType)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites [0];
        else
            return Sprites [0];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => null;

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
    if (actor.Unit.BodyAccentType2 == 1 && !actor.IsMeleeAttacking)
        return Sprites [8];
    return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
    if (actor.Unit.BodyAccentType2 == 1)
        return Sprites [actor.IsMeleeAttacking ? 9 : 7];
    return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
    if (actor.Unit.BodyAccentType2 == 1 && !actor.IsMeleeAttacking)
        return Sprites [16];
    return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)//HND15 Pistol
    {
        if (actor.Surrendered == true)
            return null;
        else if (actor.IsRangeAttacking)
            return Sprites [12];
        else
            return Sprites [10];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)//Pilot Knife
    {
        if (actor.Surrendered == true)
            return null;
        else if (actor.IsMeleeAttacking)
            return Sprites [15];
        else
            return Sprites [14];
    }


    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

