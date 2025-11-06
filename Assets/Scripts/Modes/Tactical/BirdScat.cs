using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class BirdScat : MiscDiscard// Credits to Tatltuae for sprites
{
    [OdinSerialize]
    internal ScatInfo scatInfo;

    public BirdScat(Vec2i location, int sortOrder, ScatInfo scatInfo, MiscDiscardType type = 0, int spriteNum = 0, int color = 0, string description = "") : base(location, type, spriteNum, sortOrder, color, description)
    {
        this.scatInfo = scatInfo;
        this.description = scatInfo.GetDescription();
    }

    override public void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f));

        var birbScatBack = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        birbScatBack.sortingOrder = sortOrder;

        var birbScatFront = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        birbScatFront.sortingOrder = sortOrder + 1;

        Vector3 birbscatSpriteScalingGloble = new Vector3(1.3f, 1.3f);

        if (scatInfo.preySize <= 15)
        {
            int rndNum = Random.Range(0, 2);//used for the 3 tones of each column on the sprite sheet
            int rndNum2 = (Random.Range(0, 1) * 3);//random for the 2 smallest sizes of the front sprites on the sprite sheet
            birbScatFront.sprite = State.GameManager.SpriteDictionary.BirdScat[9 + rndNum2 + rndNum];
            birbScatFront.transform.localScale = birbscatSpriteScalingGloble;
            if (scatInfo.preySize < 6)
            {
                birbScatBack.sprite = State.GameManager.SpriteDictionary.BirdScat[0 + rndNum];
                birbScatBack.transform.localScale = birbscatSpriteScalingGloble;
            }
            else
            {
                birbScatBack.sprite = State.GameManager.SpriteDictionary.BirdScat[3 + rndNum];
                birbScatBack.transform.localScale = birbscatSpriteScalingGloble;
            }
        }
        else
        {
            int rndNum = Random.Range(0, 2);//used for the 3 tones of each column on the sprite sheet
            int baseSize = scatInfo.preySize - 16; // min = 0
            float xy = 1f + baseSize / (100.0f + baseSize);
            birbscatSpriteScalingGloble = new Vector3(xy, xy);
            birbScatBack.sprite = State.GameManager.SpriteDictionary.BirdScat[6 + rndNum];
            birbScatBack.transform.localScale = birbscatSpriteScalingGloble;
            if (scatInfo.preySize < 19)
            {
                birbScatFront.sprite = State.GameManager.SpriteDictionary.BirdScat[18 + rndNum];
                birbScatFront.transform.localScale = birbscatSpriteScalingGloble;
            }
            else
            {
                birbScatFront.sprite = State.GameManager.SpriteDictionary.BirdScat[15 + rndNum];
                birbScatFront.transform.localScale = birbscatSpriteScalingGloble;
            }
        }
    }
}
