using MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MapVillagePopulator
{
    readonly StrategicTileType[,] tiles;

    public MapVillagePopulator(StrategicTileType[,] tiles)
    {
        this.tiles = tiles;
    }

    internal void PopulateVillages(Map map, ref Village[] villages)
    {
        List<Village> newVillages = new List<Village>();
        Dictionary<int, int> nameIndex = new Dictionary<int, int>();

        for (int i = 0; i < map.storedVillages.Length; i++)
        {
            if (map.storedVillages[i].Race == Race.Vagrants)
            {
                Race race = Race.Vagrants;
                if (nameIndex.ContainsKey((int)race) == false)
                    nameIndex[(int)race] = 0;
                Village vill = new Village(State.NameGen.GetTownName(race, nameIndex[(int)race]), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, false);
                vill.SubtractPopulation(999999);
                newVillages.Add(vill);
                nameIndex[(int)race] = nameIndex[(int)race] + 1;
                continue;
            }
            if (Config.VillagesPerEmpire.Length < (int)map.storedVillages[i].Race)
                continue;
            if (Config.VillagesPerEmpire[(int)map.storedVillages[i].Race] == 0)
                continue;
            
            if (map.storedVillages[i].Capital == true)
            {
                Race race = map.storedVillages[i].Race;
                newVillages.Add(new Village(State.NameGen.GetTownName(race, 0), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, true));
            }
            else
            {
                Race race = map.storedVillages[i].Race;
                if (nameIndex.ContainsKey((int)race) == false)
                    nameIndex[(int)race] = 0;
                newVillages.Add(new Village(State.NameGen.GetTownName(race, nameIndex[(int)race] + 1), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, false));
                nameIndex[(int)race] = nameIndex[(int)race] + 1;
            }

        }
        villages = newVillages.ToArray();
    }

    internal void PopulateMercenaryHouses(Map map, ref MercenaryHouse[] houses)
    {
        if (map.mercLocations == null)
        {
            houses = new MercenaryHouse[0];
            return;
        }
        List<MercenaryHouse> newHouses = new List<MercenaryHouse>();
        

        for (int i = 0; i < map.mercLocations.Length; i++)
        {
            newHouses.Add(new MercenaryHouse(map.mercLocations[i]));
        }
        houses = newHouses.ToArray();
    }

    internal void PopulateClaimables(Map map, ref ClaimableBuilding[] claimables)
    {
        if (map.claimables == null)
        {
            claimables = new ClaimableBuilding[0];
            return;
        }
        List<ClaimableBuilding> newClaimables = new List<ClaimableBuilding>();


        for (int i = 0; i < map.claimables.Length; i++)
        {
            if (map.claimables[i].Type == ClaimableType.GoldMine)
                newClaimables.Add(new GoldMine(map.claimables[i].Position));
        }
        claimables = newClaimables.ToArray();
    }

    int FarmSquares(Vec2i pos)
    {
        int t = 0;
        for (int i = pos.x - 1; i < pos.x + 2; i++)
        {
            for (int j = pos.y - 1; j < pos.y + 2; j++)
            {
                if (!(i == pos.x && pos.y == j))
                {
                    if (tiles[i, j] == StrategicTileType.field || tiles[i, j] == StrategicTileType.fieldDesert || tiles[i, j] == StrategicTileType.fieldSnow)
                    {
                        t++;
                    }
                }
            }
        }
        return t;
    }
}
