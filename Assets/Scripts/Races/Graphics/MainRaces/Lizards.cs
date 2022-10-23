using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Lizards : DefaultRaceData
{
    public Lizards()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        EyeTypes = 5;
        HairStyles = 6;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        MouthTypes = 1;
        BodySizes = 0;


        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Head = null;
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.HairColor)); //Horns / Skin
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor)); //Torso
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, WhiteColored); //Claws
        BodyAccent3 = null;
        BodyAccent4 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = new SpriteExtraInfo(0, BackWeaponSprite, WhiteColored);
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));


        AllowedClothingHatTypes = new List<ClothingAccessory>() //Crown
        {
            RaceSpecificClothing.LizardLeaderCrown,
            RaceSpecificClothing.LizardBoneCrown,
            RaceSpecificClothing.LizardLeatherCrown,
            RaceSpecificClothing.LizardClothCrown,
        };

        AvoidedMainClothingTypes = 3;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardLight);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BeltTop,
            ClothingTypes.Leotard,
            RaceSpecificClothing.LizardBlackTop,
            RaceSpecificClothing.LizardBikiniTop,
            RaceSpecificClothing.LizardStrapTop,            
            RaceSpecificClothing.LizardBoneTop,
            RaceSpecificClothing.LizardLeatherTop,
            RaceSpecificClothing.LizardClothTop,
            RaceSpecificClothing.LizardPeasant,
            ClothingTypes.Rags,
            RaceSpecificClothing.LizardLeaderTop,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
            ClothingTypes.Shorts,
            RaceSpecificClothing.LizardLeaderSkirt,
            RaceSpecificClothing.LizardBoneLoins,
            RaceSpecificClothing.LizardLeatherLoins,
            RaceSpecificClothing.LizardClothLoins,
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
            RaceSpecificClothing.LizardLeaderLegguards,
            RaceSpecificClothing.LizardBoneLegguards,
            RaceSpecificClothing.LizardLeatherLegguards,
            RaceSpecificClothing.LizardClothShorts,
        };
        ExtraMainClothing2Types = new List<MainClothing>()
        {
            RaceSpecificClothing.LizardLeaderArmbands,
            RaceSpecificClothing.LizardBoneArmbands1,
            RaceSpecificClothing.LizardBoneArmbands2,
            RaceSpecificClothing.LizardBoneArmbands3,
            RaceSpecificClothing.LizardLeatherArmbands1,
            RaceSpecificClothing.LizardLeatherArmbands2,
            RaceSpecificClothing.LizardLeatherArmbands3,
            RaceSpecificClothing.LizardClothArmbands1,
            RaceSpecificClothing.LizardClothArmbands2,
            RaceSpecificClothing.LizardClothArmbands3,
        };

        AvoidedMouthTypes = 0;
        AvoidedEyeTypes = 0;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingHatType = 1 + AllowedClothingHatTypes.IndexOf(RaceSpecificClothing.LizardLeaderCrown);
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.LizardLeaderTop);
            unit.ClothingType2 = 1 + AllowedWaistTypes.IndexOf(RaceSpecificClothing.LizardLeaderSkirt);
            unit.ClothingExtraType1 = 1 + ExtraMainClothing1Types.IndexOf(RaceSpecificClothing.LizardLeaderLegguards);
            unit.ClothingExtraType2 = 1 + ExtraMainClothing2Types.IndexOf(RaceSpecificClothing.LizardLeaderArmbands);
        }

        if (unit.ClothingType == 10) //If in prison garb
        {
            unit.ClothingHatType = 0;
            unit.ClothingExtraType1 = 0;
            unit.ClothingExtraType2 = 0;
        }
    }

    internal override int BreastSizes => Config.AllowHugeBreasts ? 8 : 5;

    //protected override Color BodyColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);
    //protected override Color BodyAccessoryColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);

    protected override Sprite BodySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lizards[actor.GetSimpleBodySprite()];
    //protected override Sprite HairSprite(Actor_Unit actor) => State.GameManager.OldSpriteDictionary.LizardHorns[actor.Unit.HairStyle];
    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lizards[7 + actor.Unit.HairStyle];
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lizards[5 + (actor.IsEating ? 1 : 0)];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lizards[3 + (actor.IsAttacking ? 1 : 0)];
    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lizards[13 + actor.Unit.EyeType];

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        
        if (actor.Unit.HasBreasts == false)
            return null;
        if (Config.LizardsHaveNoBreasts) //Moved so that it doesn't affect males
        {
            actor.Unit.SetDefaultBreastSize(0); //So that clothing works right
            return null;
        }
        if (actor.SquishedBreasts && actor.Unit.BreastSize >= 3 && actor.Unit.BreastSize <= 6)
            return State.GameManager.SpriteDictionary.Lizards[26 + actor.Unit.BreastSize - 3];
        return State.GameManager.SpriteDictionary.Lizards[18 + actor.Unit.BreastSize];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -28 * .625f);
                return State.GameManager.SpriteDictionary.Lizards[55];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -28 * .625f);
                return State.GameManager.SpriteDictionary.Lizards[54];
            }


            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return State.GameManager.SpriteDictionary.Lizards[30 + actor.GetStomachSize()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.GetWeaponSprite() == 7)
                return null;
            return State.GameManager.SpriteDictionary.Lizards[46 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }

}
