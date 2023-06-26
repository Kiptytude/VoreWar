using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

class FogSystemTactical
{
    internal bool[,] FoggedTile;
    internal Tilemap FogOfWar;
    internal TileBase FogTile;
     
    public FogSystemTactical(Tilemap fogOfWar, TileBase fogTile)
    {
        FogTile = fogTile;
        FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        FogOfWar = fogOfWar;
    }

    internal void UpdateFog(List<Actor_Unit> all, int defenderSide, bool turn, bool AIAttacker, bool AIDefender,int currentturn)
    {
        //FogOfWar.ClearAllTiles();
        if (FoggedTile.GetUpperBound(0) + 1 != Config.TacticalSizeX || FoggedTile.GetUpperBound(1) + 1 != Config.TacticalSizeY)
            FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        //StrategicTileType[,] tiles = State.World.Tiles;
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j] == false)
                {
                    FoggedTile[i, j] = true;
                }               
            }
        }
        
        //Set all as unseen
        foreach(Actor_Unit unit in all)
        {
            unit.InSight = true;
            int unitSightRange = Config.World.DefualtSightRange + unit.Unit.TraitBoosts.SightRangeBoost;
            if (currentturn < Config.World.RevealTurn) //Keeps all units revealed after the set ammount of turns have passed or if in turbo mode.
            {
                if (unit.Unit.GetStatusEffect(StatusEffectType.Illuminated) == null || unit.PredatorComponent.PreyCount <= 0)
                {
                    unit.InSight = false;
                }
                else
                {
                    unit.InSight = true; // Set unit to seen if they have the Illuminated effect, have prey, or if the battle is going on for too long
                }
                if (Config.DayNightCosmetic == true)
                {
                    unit.InSight = true;
                }
            }
            if ((AIAttacker && AIDefender) || Config.DayNightCosmetic == true)
            {
                ClearWithinSTilesOf(unit.Position, unitSightRange); // Shows all units to player for AI only battles
            }

            if (unit.Targetable)
            {
                ScanEnemies(unit.Position, unitSightRange, unit.Unit.Side, all);
            }

            if(unit.Unit.Side == defenderSide && unit.Targetable == true && turn != true && (State.GameManager.TacticalMode.IsPlayerTurn || !AIDefender))
            {
                ClearWithinSTilesOf(unit.Position, unitSightRange);
            }
            if (unit.Unit.Side != defenderSide && unit.Targetable == true && turn == true && (State.GameManager.TacticalMode.IsPlayerTurn || !AIAttacker))
            {
                ClearWithinSTilesOf(unit.Position, unitSightRange);
            }
            
        }

        // Removes fog from tile if it's not suppose to be there.
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j])
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), FogTile);
                else
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), null);
            }
        }

        if (all != null)
        {
            for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
                {
                    if (all != null)
                    {
                        foreach (Actor_Unit unit in all)
                        {
                            if (FoggedTile[unit.Position.x, unit.Position.y] && unit.PredatorComponent.PreyCount == 0 && unit.Unit.GetStatusEffect(StatusEffectType.Illuminated) == null)
                            {
                                unit.UnitSprite.gameObject.SetActive(false);
                                unit.UnitSprite.FlexibleSquare.gameObject.SetActive(false);
                                unit.Hidden = true;
                            }
                            else if (unit.Targetable == true)
                            {
                                unit.UnitSprite.gameObject.SetActive(true);
                                unit.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
                                unit.Hidden = false;
                            }
                        }
                    }               
                }
            }
        }
    }

    void ClearWithinSTilesOf(Vec2i pos, int sight = 1)
    {
        for (int x = pos.x - sight; x <= pos.x + sight; x++)
        {
            for (int y = pos.y - sight; y <= pos.y + sight; y++)
            {
                if (x < 0 || y < 0 || x > FoggedTile.GetUpperBound(0) || y > FoggedTile.GetUpperBound(1))
                    continue;
                FoggedTile[x, y] = false;
            }
        }
    }

    void ScanEnemies(Vec2i pos, int sight = 1, int side = 0, List<Actor_Unit> units = null)
    {
        for (int x = pos.x - sight; x <= pos.x + sight; x++)
        {
            for (int y = pos.y - sight; y <= pos.y + sight; y++)
            {
                foreach (Actor_Unit actor in units)
                {
                    if (actor == null)
                        continue;
                    if (actor.Position.x == x && actor.Position.y == y && actor.Unit.Side != side)
                    {
                        actor.InSight = true;
                    }                       
                }
            }
        }
    }
}



