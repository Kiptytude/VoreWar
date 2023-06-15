using UnityEngine;

class Foxes : DefaultRaceData
{
    public Foxes()
    {
        FurCapable = true;
        BaseBody = true;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bodies[11];
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.BodyParts[2];
}
