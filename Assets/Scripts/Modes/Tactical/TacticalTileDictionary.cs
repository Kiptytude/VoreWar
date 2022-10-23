using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Linq;

public class TacticalTileDictionary : MonoBehaviour
{
    internal Tile[] TileTypes;
    internal Tile[] DesertTileTypes;
    internal Tile[] RocksOverTar;
    internal Tile[] RocksOverSand;
    internal Tile[] GrassOverWater;
    internal Tile[] GrassEnviroment;
    internal Tile[] SnowEnviroment;
    internal Tile[] Paths;


    public Sprite[] TileSprites;
    public Sprite[] DesertTileSprites;
    public Sprite[] DesertTileSprites2;
    public Sprite[] RocksOverSandSprites;
    public Sprite[] RocksOverSandSprites2;
    public Sprite[] RocksOverTarSprites;
    public Sprite[] GrassOverWaterSprites;
    public Sprite[] GrassEnviromentSprites;
    public Sprite[] SnowEnviromentSprites;
    public Sprite[] SnowEnviromentSprites2;
    public Sprite[] PathSprites;

    void Start()
    {
        if (PlayerPrefs.GetInt("DesaturatedTiles", 0) == 1)
        {
            DesertTileTypes = CreateTiles(DesertTileSprites2);
            RocksOverSand = CreateTiles(RocksOverSandSprites2);
            SnowEnviroment = CreateTiles(SnowEnviromentSprites2);
        }
        else
        {
            DesertTileTypes = CreateTiles(DesertTileSprites);
            RocksOverSand = CreateTiles(RocksOverSandSprites);
            SnowEnviroment = CreateTiles(SnowEnviromentSprites);
        }
        TileTypes = CreateTiles(TileSprites);
        RocksOverTar = CreateTiles(RocksOverTarSprites);
        GrassOverWater = CreateTiles(GrassOverWaterSprites);
        GrassEnviroment = CreateTiles(GrassEnviromentSprites);
        Paths = CreateTiles(PathSprites);


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
