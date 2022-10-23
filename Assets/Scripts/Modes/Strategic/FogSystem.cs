using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

class FogSystem
{
    internal bool[,] FoggedTile;
    internal Tilemap FogOfWar;
    internal TileBase FogTile;

    public FogSystem(Tilemap fogOfWar, TileBase fogTile)
    {
        FogTile = fogTile;
        FoggedTile = new bool[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        FogOfWar = fogOfWar;
    }

    internal void UpdateFog(Empire playerEmpire, Village[] villages, Army[] armies, List<GameObject> currentVillageTiles)
    {
        FogOfWar.ClearAllTiles();
        if (State.World.Relations == null)
            return;
        if (State.World.AllActiveEmpires == null)
            return;
        if (playerEmpire == null)
            return;
        if (FoggedTile.GetUpperBound(0) + 1 != Config.StrategicWorldSizeX || FoggedTile.GetUpperBound(1) + 1 != Config.StrategicWorldSizeY)
            FoggedTile = new bool[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        //StrategicTileType[,] tiles = State.World.Tiles;
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                FoggedTile[i, j] = true;
            }
        }

        foreach (Village village in villages)
        {
            if (village.Empire.IsAlly(playerEmpire) && village.GetTotalPop() > 0)
            {
                ClearWithin2TilesOf(village.Position);
            }
        }
        foreach (Army army in armies)
        {
            if (army.Empire.IsAlly(playerEmpire))
            {
                ClearWithin2TilesOf(army.Position);
            }
        }

        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j])
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), FogTile);
            }
        }

        foreach (Army army in StrategicUtilities.GetAllHostileArmies(playerEmpire))
        {
            var spr = army.Banner?.GetComponent<MultiStageBanner>();
            if (spr != null)
                spr.gameObject.SetActive(!FoggedTile[army.Position.x, army.Position.y]);
            var spr2 = army.Sprite;
            if (spr2 != null) spr2.enabled = !FoggedTile[army.Position.x, army.Position.y];

        }

        if (currentVillageTiles != null)
        {
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (FoggedTile[State.World.Villages[i].Position.x, State.World.Villages[i].Position.y])
                {
                    currentVillageTiles[3 * i].GetComponent<SpriteRenderer>().enabled = false;
                    currentVillageTiles[3 * i + 1].GetComponent<SpriteRenderer>().enabled = false;
                    currentVillageTiles[3 * i + 2].GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    currentVillageTiles[3 * i].GetComponent<SpriteRenderer>().enabled = true;
                    currentVillageTiles[3 * i + 1].GetComponent<SpriteRenderer>().enabled = true;
                    currentVillageTiles[3 * i + 2].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

    }

    void ClearWithin2TilesOf(Vec2i pos)
    {
        for (int x = pos.x - 2; x <= pos.x + 2; x++)
        {
            for (int y = pos.y - 2; y <= pos.y + 2; y++)
            {
                if (x < 0 || y < 0 || x > FoggedTile.GetUpperBound(0) || y > FoggedTile.GetUpperBound(1))
                    continue;
                FoggedTile[x, y] = false;
            }
        }
    }
}
