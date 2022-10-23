
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Modes.Strategic
{
    class StrategicTerrainGenerator
    {
        WorldGenerator.MapGenArgs GenArgs;

        int xSize = Config.StrategicWorldSizeX;
        int ySize = Config.StrategicWorldSizeY;
        float humidity_plus = 0.0f;
        float mountain_threshold = 0.7f;

        //this is used to sharpen mountains and flatten valleys. 
        //Ideally the curve should be 0y in 0x and 1y in 1x, and below 0.5 for most of its length
        AnimationCurve height_multiplier = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.2f, .2f), new Keyframe(.8f, .5f), new Keyframe(1, 1) });       

        float he_zoom = 10f;
        float he_factor = 2f; //1.8 to 4 look good
        Vector2 he_seed;

        float hu_zoom = 10f;
        float hu_factor = 1.8f; //1.8 to 10 look good
        Vector2 hu_seed;

        float tmp_zoom = 10f;
        float tmp_factor = 1.8f;
        Vector2 tmp_seed;

        float[,] he_array;
        float[,] hu_array;
        float[,] te_array;

        public StrategicTerrainGenerator(WorldGenerator.MapGenArgs genArgs)
        {
            GenArgs = genArgs;
        }

        internal StrategicTileType[,] GenerateTerrain()
        {
            he_seed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));
            hu_seed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));
            tmp_seed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));

            StrategicTileType[,] tiles = new StrategicTileType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            MakeArrays();
            for (int x = 0; x < Config.StrategicWorldSizeX; x++)
            {
                for (int y = 0; y < Config.StrategicWorldSizeY; y++)
                {
                    tiles[x, y] = GetTerrain(x, y);
                }

            }
            return tiles;
        }

        StrategicTileType GetTerrain(int x, int y)
        {            
            float height = he_array[x, y];
            float humidity = hu_array[x, y];
            float temperature = te_array[x, y];
            temperature += GenArgs.Temperature;
            if (GenArgs.Poles)
            {
                float position = (float)y / Config.StrategicWorldSizeY;
                if (position < .4f)
                {
                    temperature = (temperature + .5f) / 2;
                    temperature += (.4f - position) / 2;
                }
                else if (position > .6f)
                {
                    temperature = (temperature + .5f) / 2;
                    temperature += (.6f - position) / 1.3333f;
                }
                else
                    temperature = (temperature + .5f) / 2;
            }
            if (height < GenArgs.WaterPct && temperature < .3f)
                return StrategicTileType.ice;
            if (height < GenArgs.WaterPct)
                return StrategicTileType.water;
            if (temperature > .6f && height < .55f - (GenArgs.Hilliness / 5))
                return StrategicTileType.desert;
            if (temperature > .6f && height > .55f - (GenArgs.Hilliness / 5) + Random.Range(0f, 1 - mountain_threshold))
                return StrategicTileType.brokenCliffs;
            if (temperature > .6f)
                return StrategicTileType.sandHills;
            if (temperature < .3f && height < .55f - (GenArgs.Hilliness / 5) && humidity < (.5f + GenArgs.ForestPct / 4) && humidity > (.5f - GenArgs.ForestPct / 4))
                return StrategicTileType.snowTrees;
            if (temperature < .3f && height < .55f - (GenArgs.Hilliness / 5))
                return StrategicTileType.snow;         
            if (temperature < .3f && height > .55f - (GenArgs.Hilliness / 5) + Random.Range(0f, 1 - mountain_threshold))
                return StrategicTileType.snowMountain; 
            if (temperature < .3f)
                return StrategicTileType.snowHills;
            if (height > .55f - (GenArgs.Hilliness / 5) + Random.Range(0f, 1 - mountain_threshold))
                return StrategicTileType.mountain;
            if (height > .55f - (GenArgs.Hilliness / 5))
                return StrategicTileType.hills;
            if (humidity > .75f - GenArgs.Swampiness / 4)
                return StrategicTileType.swamp;
            if (temperature < .55f && temperature > .4f && humidity < (.5f + GenArgs.ForestPct / 4) && humidity > (.5f - GenArgs.ForestPct / 4))
                return StrategicTileType.forest;
            else
                return StrategicTileType.grass;
        }



        //calculate the value of an element of the array based on noise and location
        float FractalNoise(int i, int j, float zoom, float factor, Vector2 seed)
        {
            i = i + Mathf.RoundToInt(seed.x * zoom);
            j = j + Mathf.RoundToInt(seed.y * zoom);
            //fractal behavior occurs here. Everything else is parameter fine-tuning
            return 0
            + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
            + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
            + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;
        }

        //float FractalNoiseRidges(int i, int j, float zoom, float factor, Vector2 seed)
        //{
        //    i = i + Mathf.RoundToInt(seed.x * zoom);
        //    j = j + Mathf.RoundToInt(seed.y * zoom);
        //    //fractal behavior occurs here. Everything else is parameter fine-tuning
        //    float sum = 0
        //    + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;

        //    return 1 - Mathf.Abs(sum - 0.5f) * 2f;
        //}

        //float FractalNoiseMounds(int i, int j, float zoom, float factor, Vector2 seed)
        //{
        //    i = i + Mathf.RoundToInt(seed.x * zoom);
        //    j = j + Mathf.RoundToInt(seed.y * zoom);
        //    //fractal behavior occurs here. Everything else is parameter fine-tuning
        //    float sum = 0
        //    + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;

        //    return Mathf.Abs(sum - 0.5f) * 2f;
        //}


        //makes and calculates the values of the 3 arrays that are used to determine the type of tile
        void MakeArrays()
        {
            he_array = new float[xSize, ySize];
            hu_array = new float[xSize, ySize];
            te_array = new float[xSize, ySize];

            RecalculateArray();
        }

        void RecalculateArray()
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    he_array[i, j] = height_multiplier.Evaluate(FractalNoise(i, j, he_zoom, he_factor, he_seed));
                    hu_array[i, j] = FractalNoise(i, j, hu_zoom, hu_factor, hu_seed) + humidity_plus;
                    te_array[i, j] = FractalNoise(i, j, tmp_zoom, tmp_factor, tmp_seed);
                }
            }
        }

    }
}
