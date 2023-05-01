using OdinSerializer;
using UnityEngine;

enum MiscDiscardType
{
    Scat,
    Bones,
    Cum,
}

class MiscDiscard
{
    [OdinSerialize]
    internal Vec2i location;
    [OdinSerialize]
    internal MiscDiscardType type;
    [OdinSerialize]
    internal int spriteNum;
    [OdinSerialize]
    internal int sortOrder;
    [OdinSerialize]
    internal string description;
    [OdinSerialize]
    internal int color;

    public MiscDiscard(Vec2i location, MiscDiscardType type, int spriteNum, int sortOrder, int color, string description = "")
    {
        this.location = location;
        this.type = type;
        this.spriteNum = spriteNum;
        this.sortOrder = sortOrder;
        this.description = description;
        this.color = color;
    }

    virtual public void GenerateSpritePrefab(Transform folder)
    {
        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f)), new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite.sortingOrder = sortOrder;
        switch (type)
        {
            case MiscDiscardType.Scat:
                sprite.sprite = State.GameManager.SpriteDictionary.Scat[spriteNum];
                if (color != -1)
                {
                    sprite.color = ColorPaletteMap.GetSlimeBaseColor(color);
                }
                break;
            case MiscDiscardType.Bones:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                if (color != -1)
                {
                    if (spriteNum == (int)BoneTypes.CrypterBonePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CrypterWeapon, color).colorSwapMaterial;
                    else if (spriteNum == (int)BoneTypes.SlimePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, color).colorSwapMaterial;
                }
                break;
            case MiscDiscardType.Cum:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                sprite.sortingOrder = int.MinValue;
                if (color == 0) sprite.color = new Color(.51f, .89f, .98f);
                break;


        }
    }
}
