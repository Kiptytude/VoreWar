using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StrategicTileDictionary : MonoBehaviour
{
    internal Tile[] WaterFloat;
    internal Tile[] GrassFloat;
    internal Tile[] IceOverSnow;
    internal Tile[] DeepWaterOverWater;

    internal Tile[] Objects;

    public Sprite[] WaterFloatSprites;
    public Sprite[] GrassFloatSprites;

    public Sprite[] IceOverSnowSprites;
    public Sprite[] DeepWaterOverWaterSprites;
    public Sprite[] ObjectSprites;

    void Start()
    {

        IceOverSnow = CreateTiles(IceOverSnowSprites);
        DeepWaterOverWater = CreateTiles(DeepWaterOverWaterSprites);
        WaterFloat = CreateTiles(WaterFloatSprites);
        GrassFloat = CreateTiles(GrassFloatSprites);
        Objects = CreateTiles(ObjectSprites);



        Tile[] CreateTiles(Sprite[] sprites)
        {
            Tile[] temptiles = new Tile[sprites.Count()];
            for (int i = 0; i < sprites.Count(); i++)
            {
                temptiles[i] = ScriptableObject.CreateInstance<Tile>();
                temptiles[i].sprite = sprites[i];
            }
            return temptiles;
        }
    }
}

