using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class DiaperDiscard : MiscDiscard
{
    [OdinSerialize]
    internal ScatInfo scatInfo;

    public DiaperDiscard(Vec2i location, int sortOrder, ScatInfo scatInfo, MiscDiscardType type = 0, int spriteNum = 0, int color = 0, string description = "") : base(location, type, spriteNum, sortOrder, color, description)
    {
        this.scatInfo = scatInfo;
        this.description = scatInfo.GetDescription();
    }

    override public void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f));

        var diaperMain = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        diaperMain.sortingOrder = sortOrder;

        Vector3 diaperSpriteScalingGloble = new Vector3(1.8f, 1.8f);

        if (scatInfo.preySize < 9)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.CleanDisposalS.Length);
            diaperMain.sprite = State.GameManager.SpriteDictionary.CleanDisposalS[rndNum];
            diaperMain.transform.localScale = diaperSpriteScalingGloble;
        }
        else if (scatInfo.preySize > 15)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.CleanDisposalL.Length);
            diaperMain.sprite = State.GameManager.SpriteDictionary.CleanDisposalL[rndNum];
            int baseSize = scatInfo.preySize - 16; // min = 0
            float xy = 1f + baseSize / (100.0f + baseSize);
            diaperSpriteScalingGloble = new Vector3(xy, xy);
            diaperMain.transform.localScale = diaperSpriteScalingGloble;
        }
        else
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.CleanDisposalM.Length);
            diaperMain.sprite = State.GameManager.SpriteDictionary.CleanDisposalM[rndNum];
            diaperMain.transform.localScale = diaperSpriteScalingGloble;
        }
    }
}
