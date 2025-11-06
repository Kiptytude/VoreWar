using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TacticalTileDictionary : MonoBehaviour
{
    internal Tile[] TileTypes;
    internal Tile[] DesertTileTypes;
    internal Tile[] VolcanicTileTypes;
    internal Tile[] RocksOverTar;
    internal Tile[] RocksOverSand;
    internal Tile[] GrassOverWater;
    internal Tile[] GrassEnviroment;
    internal Tile[] SnowEnviroment;
    internal Tile[] VolcanicOverLava;
    internal Tile[] VolcanicOverGravel;
    internal Tile[] BeachOverOcean;
    internal Tile[] BeachEnviroment;
    internal Tile[] SwampOverBog;
    internal Tile[] SwampEnviroment;
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
    public Sprite[] VolcanicTileSprites;
    public Sprite[] VolcanicOverLavaSprites;
    public Sprite[] VolcanicOverGravelSptites;
    public Sprite[] BeachOverOceanSprites;
    public Sprite[] BeachEnviromentSprites;
    public Sprite[] SwampOverWaterSprites;
    public Sprite[] SwampEnviromentSprites;
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
        VolcanicTileTypes = CreateTiles(VolcanicTileSprites);
        RocksOverTar = CreateTiles(RocksOverTarSprites);
        VolcanicOverLava = CreateTiles(VolcanicOverLavaSprites);
        VolcanicOverGravel = CreateTiles(VolcanicOverGravelSptites);
        GrassOverWater = CreateTiles(GrassOverWaterSprites);
        GrassEnviroment = CreateTiles(GrassEnviromentSprites);
        BeachOverOcean = CreateTiles(BeachOverOceanSprites);
        BeachEnviroment = CreateTiles(BeachEnviromentSprites);
        SwampOverBog = CreateTiles(SwampOverWaterSprites);
        SwampEnviroment = CreateTiles(SwampEnviromentSprites);
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
