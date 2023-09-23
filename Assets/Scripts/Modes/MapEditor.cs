﻿using MapObjects;
using OdinSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace MapObjects
{
    internal enum ClaimableType
    {
        GoldMine = 0,
    }
    class MapVillage
    {
        public MapVillage(bool capital, Race race, Vec2i position)
        {
            Capital = capital;
            Race = race;
            Position = position;
        }

        [OdinSerialize]
        public bool Capital { get; set; }
        [OdinSerialize]
        public Race Race { get; private set; }
        [OdinSerialize]
        public Vec2i Position { get; set; }
    }

    struct MapClaimable
    {
        public MapClaimable(ClaimableType type, Vec2i position) : this()
        {
            Type = type;
            Position = position;
        }

        [OdinSerialize]
        public ClaimableType Type { get; private set; }
        [OdinSerialize]
        public Vec2i Position { get; set; }
    }

    class Map
    {
        [OdinSerialize]
        internal StrategicTileType[,] Tiles;
        [OdinSerialize]
        internal StrategicDoodadType[,] Doodads;
        [OdinSerialize]
        internal MapVillage[] storedVillages;
        [OdinSerialize]
        internal Vec2i[] mercLocations;
        [OdinSerialize]
        internal MapClaimable[] claimables;

        static public Map Get(string filename)
        {
            Map map = null;
            if (!File.Exists(filename))
            {
                State.GameManager.CreateMessageBox("Couldn't find the saved file");
                return null;
            }
            try
            {
                byte[] bytes = File.ReadAllBytes(filename);
                map = SerializationUtility.DeserializeValue<Map>(bytes, DataFormat.Binary);
                return map;
            }
            catch
            {
                State.GameManager.CreateMessageBox($"Failed to load map {filename}");
                return null;
            }

        }
    }

    class UndoMapAction
    {
        List<Action> Actions;

        public UndoMapAction()
        {
            Actions = new List<Action>();
        }

        public void Add(Action action)
        {
            Actions.Add(action);
        }

        public void Undo()
        {
            for (int i = Actions.Count - 1; i >= 0; i--)
            {
                Actions[i].Invoke();
            }
            State.GameManager.MapEditor.RecreateObjects();

        }
    }

}


public class MapEditor : SceneBase
{
    bool EditingActiveMap;

    internal bool ActiveAnything => ActiveDoodad || ActiveTile || ActiveVillage || ActiveSpecial;

    internal bool ActiveDoodad = false;
    StrategicDoodadType currentDoodadType = StrategicDoodadType.none;

    internal bool ActiveTile = false;
    StrategicTileType currentTileType = StrategicTileType.grass;

    internal bool ActiveVillage = false;
    Race villageRace;

    internal bool ActiveSpecial = false;
    SpecialType activeSpecialType;

    List<UndoMapAction> UndoActions = new List<UndoMapAction>();
    UndoMapAction LastActionBuilder;

    public Tilemap[] TilemapLayers;

    public GameObject SelectionBackground;

    public Button ExitMapEditor;
    public Button LoadMapButton;
    public Button ResizeButton;

    public Toggle SimpleDisplay;

    public MapResizePanel ResizeUI;

    public Button UndoButton;

    Tile[] TileTypes;
    TileBase[] DoodadTypes;

    public TileBase[] SpawnerTypes;
    public Sprite[] Sprites;
    public Sprite[] VillageSprites;
    GameObject[] SpriteCategories;

    public Transform VillageFolder;
    public Transform ArmyFolder;

    public TMP_Dropdown BrushType;

    public Text Tooltip;

    StrategicTileType[,] tiles;
    StrategicDoodadType[,] doodads;

    public enum SpecialType
    {
        MercenaryHouse,
        GoldMine,
    }

    public void CloseEditor()
    {
        if (EditingActiveMap)
        {
            StrategyPathfinder.Initialized = false;
            foreach (Army army in StrategicUtilities.GetAllArmies().ToList())
            {
                if (CanWalkInto(army.Position.x, army.Position.y) == false)
                {
                    if (doodads == null || (State.World.Doodads[army.Position.x, army.Position.y] < StrategicDoodadType.bridgeVertical ||
                        State.World.Doodads[army.Position.x, army.Position.y] > StrategicDoodadType.virtualBridgeIntersection))
                        State.World.GetEmpireOfSide(army.Side).Armies.Remove(army);
                }
            }
            State.World.Tiles = tiles;
            State.World.Doodads = doodads;
            //if (StrategicConnectedChecker.AreAllConnected(State.World.Villages, StrategicUtilities.GetAllArmies()) == false)
            //    return;

            foreach (Village village in State.World.Villages)
            {
                UpdateVillagePopulation(village);
            }
            CleanUp();
            foreach (Army army in StrategicUtilities.GetAllArmies())
                army.GetTileHealRate(); //Because villages may be gone or have indexes changed
            State.GameManager.StrategyMode.RedrawVillages();
            State.GameManager.StrategyMode.FogSystem = null;
            State.GameManager.SwitchToStrategyMode();
            State.GameManager.StrategyMode.CheckForRevivedPlayerFromMapEditor();
            State.GameManager.StrategyMode.RedrawTiles();
            State.GameManager.StrategyMode.RebuildSpawners();

        }
        else
        {
            CleanUp();
            State.GameManager.SwitchToMainMenu();
        }

    }

    internal void Initialize(bool editingActiveMap)
    {
        if (State.World.Tiles == null)
            State.World.Tiles = new StrategicTileType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        if (State.World.Claimables == null)
            State.World.Claimables = new ClaimableBuilding[0];
        if (State.World.Doodads == null)
            State.World.Doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        CatchUpEmpires();
        tiles = State.World.Tiles;
        doodads = State.World.Doodads;

        TileTypes = State.GameManager.StrategyMode.TileTypes;
        DoodadTypes = State.GameManager.StrategyMode.DoodadTypes;
        Sprites = State.GameManager.StrategyMode.Sprites;
        VillageSprites = State.GameManager.StrategyMode.VillageSprites;
        SpriteCategories = State.GameManager.StrategyMode.SpriteCategories;
        EditingActiveMap = editingActiveMap;
        LoadMapButton.gameObject.SetActive(editingActiveMap == false);
        if (editingActiveMap)
        {
            State.GameManager.CreateMessageBox("Note that you are editing the currently played map, and changes take effect immediately, so you may wish to back out and save your game if you haven't already");
            ExitMapEditor.GetComponentInChildren<Text>().text = "Return to strategic";
        }
        else
        {
            ExitMapEditor.GetComponentInChildren<Text>().text = "Exit to Main Menu";
        }
        RecreateObjects();
    }

    void CatchUpEmpires()
    {
        bool changed = false;
        for (int i = 0; i < Config.NumberOfRaces; i++)
        {
            if (State.World.MainEmpires.Where(s => s.Side == i).Any() == false)
            {
                changed = true;
                State.World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(i, CreateStrategicGame.ColorFromIndex(i), Color.white, 0, StrategyAIType.Basic, TacticalAIType.Full, i, 16, 16)));
                State.World.AllActiveEmpires.Add(State.World.MainEmpires.Last());
            }
        }
        if (changed)
        {
            State.World.MainEmpires = State.World.MainEmpires.OrderBy(s => s.Side).ToList();
            Config.World.VillagesPerEmpire = new int[Config.NumberOfRaces];
            State.World.Stats.ExpandToIncludeNewRaces();
            State.World.RefreshTurnOrder();
        }



    }

    void UpdateVillagePopulation(Village village)
    {
        int farmSquares = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                if (tiles[village.Position.x + x, village.Position.y + y] == StrategicTileType.field || tiles[village.Position.x + x, village.Position.y + y] == StrategicTileType.fieldDesert || tiles[village.Position.x + x, village.Position.y + y] == StrategicTileType.fieldSnow)
                    farmSquares++;
            }
        }
        village.UpdateFarms(farmSquares);
    }

    internal void RecreateObjects()
    {
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
        RedrawTiles();
        RedrawVillages();
        if (EditingActiveMap)
            RedrawArmies();
    }

    internal void SetTileType(StrategicTileType type, Transform location)
    {
        currentTileType = type;
        ActiveTile = true;
        ActiveSpecial = false;
        ActiveVillage = false;
        ActiveDoodad = false;
        SelectionBackground.SetActive(true);
        SelectionBackground.transform.position = location.position;
    }

    internal void SetTileTooltip(StrategicTileType type)
    {
        Tooltip.gameObject.SetActive(true);
        Tooltip.text = $"Place {type} tile\n";
        if (StrategicTileInfo.CanWalkInto(type) == false)
            Tooltip.text += "Impassible to Walking";
        else
            Tooltip.text += $"Movement Cost: {StrategicTileInfo.WalkCost(type)}";
    }

    internal void SetMiscType(SpecialType type, Transform location)
    {
        ActiveVillage = false;
        ActiveTile = false;
        ActiveSpecial = true;
        ActiveDoodad = false;
        activeSpecialType = type;
        SelectionBackground.SetActive(true);
        SelectionBackground.transform.position = location.position;
    }
    internal void SetMiscTooltip(SpecialType type)
    {
        Tooltip.gameObject.SetActive(true);
        switch (type)
        {
            case SpecialType.MercenaryHouse:
                Tooltip.text = $"Place Mercenary House";
                break;
            case SpecialType.GoldMine:
                Tooltip.text = $"Place Gold Mine";
                break;
        }


    }


    internal void SetVillageType(Race race, Transform location)
    {
        villageRace = race;
        ActiveVillage = true;
        ActiveTile = false;
        ActiveSpecial = false;
        ActiveDoodad = false;
        SelectionBackground.SetActive(true);
        SelectionBackground.transform.position = location.position;
    }

    internal void SetVillageTooltip(Race race)
    {
        Tooltip.gameObject.SetActive(true);
        if (race == Race.Vagrants)
            Tooltip.text = $"Place abandoned Village";
        else
            Tooltip.text = $"Place {race} Village";
    }

    internal void SetDoodadType(StrategicDoodadType type, Transform location)
    {
        currentDoodadType = type;
        ActiveTile = false;
        ActiveSpecial = false;
        ActiveVillage = false;
        ActiveDoodad = true;
        SelectionBackground.SetActive(true);
        SelectionBackground.transform.position = location.position;
    }

    internal void SetDoodadTooltip(StrategicDoodadType type)
    {
        Tooltip.gameObject.SetActive(true);
        switch (type)
        {
            case StrategicDoodadType.bridgeVertical:
                Tooltip.text = $"Place vertical bridge tile\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.bridgeHorizontal:
                Tooltip.text = $"Place horizontal bridge tile\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.bridgeIntersection:
                Tooltip.text = $"Place bridge intersection tile\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.virtualBridgeVertical:
                Tooltip.text = $"Place sea path tile (An alternate bridge)\nAlso looks better than bridges for things such as wide bridges or diagonal bridges\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.virtualBridgeHorizontal:
                Tooltip.text = $"Place sea path tile (An alternate bridge)\nAlso looks better than bridges for things such as wide bridges or diagonal bridges\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.virtualBridgeIntersection:
                Tooltip.text = $"Place sea path tile (An alternate bridge)\nAlso looks better than bridges for things such as wide bridges or diagonal bridges\nMakes walkable and lowers movement cost to 1";
                break;
            case StrategicDoodadType.SpawnerVagrant:
                Tooltip.text = $"Place a monster spawn location for Vagrants, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerSerpents:
                Tooltip.text = $"Place a monster spawn location for Serpents, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerWyvern:
                Tooltip.text = $"Place a monster spawn location for Wyvern, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerCompy:
                Tooltip.text = $"Place a monster spawn location for Compy, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerSharks:
                Tooltip.text = $"Place a monster spawn location for Sharks, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFeralWolves:
                Tooltip.text = $"Place a monster spawn location for Feral Wolves, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerCake:
                Tooltip.text = $"Place a monster spawn location for Cakes, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerHarvester:
                Tooltip.text = $"Place a monster spawn location for Harvesters, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerVoilin:
                Tooltip.text = $"Place a monster spawn location for Voilin, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerBats:
                Tooltip.text = $"Place a monster spawn location for Bats, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFrogs:
                Tooltip.text = $"Place a monster spawn location for Frogs, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerDragon:
                Tooltip.text = $"Place a monster spawn location for Dragons, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerDragonfly:
                Tooltip.text = $"Place a monster spawn location for Dragonflies, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerTwistedVines:
                Tooltip.text = $"Place a monster spawn location for Twisted Vines, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFairy:
                Tooltip.text = $"Place a monster spawn location for Fairies, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerAnts:
                Tooltip.text = $"Place a monster spawn location for Ants, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerGryphon:
                Tooltip.text = $"Place a monster spawn location for Gryphons, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerSlugs:
                Tooltip.text = $"Place a monster spawn location for Slugs, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerSalamanders:
                Tooltip.text = $"Place a monster spawn location for Salamanders, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerMantis:
                Tooltip.text = $"Place a monster spawn location for Mantis, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerEasternDragon:
                Tooltip.text = $"Place a monster spawn location for Eastern Dragons, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerCatfish:
                Tooltip.text = $"Place a monster spawn location for Catfish, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerGazelle:
                Tooltip.text = $"Place a monster spawn location for Gazelle, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerEarthworm:
                Tooltip.text = $"Place a monster spawn location for Earthworms, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFeralLizards:
                Tooltip.text = $"Place a monster spawn location for Feral lizards, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerMonitor:
                Tooltip.text = $"Place a monster spawn location for Monitors, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerSchiwardez:
                Tooltip.text = $"Place a monster spawn location for Schiwardez, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerTerrorbird:
                Tooltip.text = $"Place a monster spawn location for Terrorbirds, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerDratopyr:
                Tooltip.text = $"Place a monster spawn location for Dratopyr, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFeralLions:
                Tooltip.text = $"Place a monster spawn location for FeralLions, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
			case StrategicDoodadType.SpawnerGoodra:
                Tooltip.text = $"Place a monster spawn location for Goodra, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            case StrategicDoodadType.SpawnerFeralHorses:
                Tooltip.text = $"Place a monster spawn location for FeralHorses, they have to spawn within 2 tiles of a spawner if at least one exists";
                break;
            default:
                Tooltip.text = $"Place {type} tile\n";
                break;
        }
    }

    internal void SetBlankTooltip()
    {
        Tooltip.text = "";
    }

    public void RedrawArmies()
    {
        ClearArmies();
        foreach (Empire empire in State.World.AllActiveEmpires)
        {
            foreach (Army army in empire.Armies)
            {

                if (army.Side < 30)
                {
                    if (army.BannerStyle > (int)BannerTypes.VoreWar && CustomBannerTest.Sprites[army.BannerStyle - 23] != null)
                    {
                        army.Sprite = Instantiate(SpriteCategories[1], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<SpriteRenderer>();
                        army.Sprite.sprite = CustomBannerTest.Sprites[army.BannerStyle - 23];
                    }
                    else
                    {
                        army.Banner = Instantiate(SpriteCategories[3], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<MultiStageBanner>();
                        army.Banner.Refresh(army, false);
                    }

                }
                else
                {
                    int tileType = empire.BannerType;
                    if (army.Units.Contains(empire.Leader)) tileType += 4;
                    army.Sprite = Instantiate(SpriteCategories[1], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<SpriteRenderer>();
                    army.Sprite.sprite = Sprites[tileType];
                    army.Sprite.color = empire.UnityColor;
                }


            }
        }
    }

    private void ClearArmies()
    {
        var previousTiles = GameObject.FindGameObjectsWithTag("Army");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    public void RedrawTiles()
    {
        for (int i = 0; i < 5; i++)
        {
            TilemapLayers[i].ClearAllTiles();
        }
        if (SimpleDisplay.isOn)
        {
            for (int i = 0; i <= tiles.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= tiles.GetUpperBound(1); j++)
                {
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(tiles[i, j], i, j)]);

                    var type = StrategicTileInfo.GetObjectTileType(this.tiles[i, j], i, j);
                    if (type != -1)
                        TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.Objects[type]);

                }
            }
        }
        else
        {
            DrawTiles(0, tiles.GetUpperBound(0), 0, tiles.GetUpperBound(1));
        }

        if (doodads != null)
        {
            for (int i = 0; i <= tiles.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= tiles.GetUpperBound(1); j++)
                {
                    if (doodads[i, j] > 0)
                    {
                        if (doodads[i, j] < StrategicDoodadType.SpawnerVagrant)
                        {
                            TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), DoodadTypes[-1 + (int)doodads[i, j]]);
                        }
                        else
                        {
                            TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), SpawnerTypes[0]);
                            TilemapLayers[4].SetTile(new Vector3Int(i, j, 0), SpawnerTypes[-1000 + (int)doodads[i, j]]);
                        }
                    }

                }
            }
        }
    }

    void ClearTiles(int minX, int maxX, int minY, int maxY)
    {
        if (minX < 0) minX = 0;
        if (minY < 0) minY = 0;
        if (maxX > tiles.GetUpperBound(0)) maxX = tiles.GetUpperBound(0);
        if (maxY > tiles.GetUpperBound(1)) maxY = tiles.GetUpperBound(1);
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    TilemapLayers[k].SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }

    }

    void DrawTiles(int minX, int maxX, int minY, int maxY)
    {
        if (minX < 0) minX = 0;
        if (minY < 0) minY = 0;
        if (maxX > this.tiles.GetUpperBound(0)) maxX = this.tiles.GetUpperBound(0);
        if (maxY > this.tiles.GetUpperBound(1)) maxY = this.tiles.GetUpperBound(1);

        StrategicTileLogic logic = new StrategicTileLogic();
        StrategicTileType[,] tiles = logic.ApplyLogic(this.tiles, out var overTiles, out var underTiles);
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (overTiles[i, j] >= (StrategicTileType)2000)
                {
                    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.WaterFloat[(int)overTiles[i, j] - 2000]);
                }
                else if (overTiles[i, j] != 0)
                {
                    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(overTiles[i, j], i, j)]);
                }
                else
                {
                    var type = StrategicTileInfo.GetObjectTileType(this.tiles[i, j], i, j);
                    if (type != -1)
                        TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.Objects[type]);

                }
                if (tiles[i, j] >= (StrategicTileType)2100 && underTiles[i, j] >= (StrategicTileType)2200)
                {
                    TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.IceOverSnow[(int)underTiles[i, j] - 2200]);
                    //TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i, j]]);

                }
                else if (tiles[i, j] >= (StrategicTileType)2100)
                {
                    TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), State.GameManager.StrategyMode.TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    if (underTiles[i, j] != (StrategicTileType)99)
                    {
                        TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i, j]]);
                    }
                    else
                    {
                        switch (this.tiles[i, j])
                        {
                            case StrategicTileType.field:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.grass]);
                                break;
                            case StrategicTileType.fieldDesert:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.desert]);
                                break;
                            case StrategicTileType.fieldSnow:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.snow]);
                                break;
                            default:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(this.tiles[i, j], i, j)]);
                                break;

                        }

                    }

                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceOverSnow[(int)tiles[i, j] - 2100]);
                }
                //else if (tiles[i, j] >= (StrategicTileType)2000)
                //{
                //    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterFloat[(int)tiles[i, j] - 2000]);
                //    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2000]);
                //    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i,j]]);
                //}
                else
                {
                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(tiles[i, j], i, j)]);
                }

                if (doodads != null && doodads[i, j] > 0)
                {
                    if (doodads[i, j] < StrategicDoodadType.SpawnerVagrant)
                    {
                        TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), DoodadTypes[-1 + (int)doodads[i, j]]);
                    }
                    else
                    {
                        TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), SpawnerTypes[0]);
                        TilemapLayers[4].SetTile(new Vector3Int(i, j, 0), SpawnerTypes[-1000 + (int)doodads[i, j]]);
                    }
                }
            }
        }
    }

    public void RedrawVillages()
    {
        ClearVillages();
        Village[] villages = State.World.Villages;
        int highestVillageSprite = VillageSprites.Count() - 1;
        for (int i = 0; i < villages.Length; i++)
        {
            if (EditingActiveMap)
            {
                GameObject vill = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                vill.GetComponent<SpriteRenderer>().sprite = VillageSprites[villages[i].GetImageNum(highestVillageSprite)];
                vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
                int villageColorSprite = villages[i].GetColoredImageNum(highestVillageSprite);
                GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                villColored.GetComponent<SpriteRenderer>().sprite = VillageSprites[villageColorSprite];
                villColored.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnityColor;
                if (villageColorSprite == 0)
                    villColored.GetComponent<SpriteRenderer>().color = Color.clear;
                GameObject villShield = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                villShield.GetComponent<SpriteRenderer>().sprite = Sprites[11];
                villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
                villShield.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnitySecondaryColor;

                GameObject villShieldInner = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                villShieldInner.GetComponent<SpriteRenderer>().sprite = Sprites[10];
                villShieldInner.GetComponent<SpriteRenderer>().sortingOrder = 2;
                villShieldInner.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnityColor;
            }
            else
            {
                GameObject vill = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                vill.GetComponent<SpriteRenderer>().sprite = VillageSprites[villages[i].GetImageNum(highestVillageSprite)];
                vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
                int villageColorSprite = villages[i].GetColoredImageNum(highestVillageSprite);
                GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                villColored.GetComponent<SpriteRenderer>().sprite = VillageSprites[villageColorSprite];
                villColored.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side)?.UnityColor ?? Color.white;
                if (villageColorSprite == 0)
                    villColored.GetComponent<SpriteRenderer>().color = Color.clear;
                GameObject villShield = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
                villShield.GetComponent<SpriteRenderer>().sprite = Sprites[10];
                villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
                villShield.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side)?.UnityColor ?? Color.white;
            }

        }
        foreach (var mercHouse in State.World.MercenaryHouses)
        {
            GameObject merc = Instantiate(SpriteCategories[2], new Vector3(mercHouse.Position.x, mercHouse.Position.y), new Quaternion(), VillageFolder);
            merc.GetComponent<SpriteRenderer>().sprite = Sprites[14];
            merc.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        foreach (var claimable in State.World.Claimables)
        {
            int spr = 0;
            if (claimable is GoldMine)
                spr = 12;
            GameObject vill = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = Sprites[spr];
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            villColored.GetComponent<SpriteRenderer>().sprite = Sprites[spr + 2];
            villColored.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            GameObject villShield = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            villShield.GetComponent<SpriteRenderer>().sprite = Sprites[10];
            villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShield.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
        }
    }

    private void ClearVillages()
    {
        var previousTiles = GameObject.FindGameObjectsWithTag("Village");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    public override void CleanUp()
    {
        ClearArmies();
        ClearVillages();
        UndoActions.Clear();
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
    }

    void ProcessRightClick(int x, int y)
    {
        var tileObjs = FindObjectsOfType<MapEditorTile>();
        var tile = tileObjs.Where(s => s.type == tiles[x, y]).FirstOrDefault();
        SetTileType(tile.type, tile.transform);
    }

    void ProcessClick(int x, int y, bool held = false)
    {
        Vec2i clickLocation = new Vec2i(x, y);
        if (held == false)
        {
            LastActionBuilder = new UndoMapAction();
            if (UndoActions.Count > 15)
                UndoActions.RemoveAt(0);
            UndoActions.Add(LastActionBuilder);
        }

        if (ActiveTile)
        {
            if (BrushType.value == 0)
            {
                var lastTile = tiles[x, y];
                LastActionBuilder.Add(() => tiles[x, y] = lastTile);
                tiles[x, y] = currentTileType;
                DestroyVillagesAtTile(clickLocation);
                if (doodads != null)
                {
                    var lastDoodad = doodads[x, y];
                    LastActionBuilder.Add(() => doodads[x, y] = lastDoodad);
                    doodads[x, y] = 0;
                }

                if (SimpleDisplay.isOn)
                {
                    TilemapLayers[0].SetTile(new Vector3Int(x, y, 0), TileTypes[StrategicTileInfo.GetTileType(tiles[x, y], x, y)]);
                    var type = StrategicTileInfo.GetObjectTileType(this.tiles[x, y], x, y);
                    if (type != -1)
                        TilemapLayers[2].SetTile(new Vector3Int(x, y, 0), State.GameManager.StrategyMode.TileDictionary.Objects[type]);
                    else
                        TilemapLayers[2].SetTile(new Vector3Int(x, y, 0), null);
                }
                else
                {
                    ClearTiles(x - 2, x + 2, y - 2, y + 2);
                    DrawTiles(x - 2, x + 2, y - 2, y + 2);
                }

            }
            else if (BrushType.value <= 4)
            {
                int radius = BrushType.value;
                for (int xAdjust = -radius; xAdjust <= radius; xAdjust++)
                {
                    for (int yAdjust = -radius; yAdjust <= radius; yAdjust++)
                    {
                        if (x + xAdjust >= tiles.GetLength(0) || x + xAdjust < 0)
                            continue;
                        if (y + yAdjust >= tiles.GetLength(1) || y + yAdjust < 0)
                            continue;
                        var lastTile = tiles[x + xAdjust, y + yAdjust];
                        int lastX = x + xAdjust;
                        int lastY = y + yAdjust;
                        LastActionBuilder.Add(() => tiles[lastX, lastY] = lastTile);
                        tiles[x + xAdjust, y + yAdjust] = currentTileType;
                        DestroyVillagesAtTile(new Vec2i(x + xAdjust, y + yAdjust));
                    }
                }
                RedrawTiles();
            }
            else if (BrushType.value == 5)
            {
                Fill(x, y);
                RedrawTiles();
            }
            else if (BrushType.value == 6)
            {
                ReplaceAll(x, y);
                RedrawTiles();
            }

        }
        else if (ActiveVillage)
        {
            if (StrategicUtilities.GetVillageAt(clickLocation) == null && StrategicUtilities.GetMercenaryHouseAt(clickLocation) == null)
            {
                if (x >= tiles.GetLength(0) - 1 || x < 1)
                    return;
                if (y >= tiles.GetLength(1) - 1 || y < 1)
                    return;
                if (CanWalkInto(x, y) == false)
                {
                    var lastTile = tiles[x, y];
                    LastActionBuilder.Add(() => tiles[x, y] = lastTile);
                    tiles[x, y] = StrategicTileType.grass;
                }

                bool activeRace = false;
                foreach (Empire empire in State.World.MainEmpires)
                {
                    if (empire.Race == villageRace)
                        activeRace = true;
                }
                if (villageRace == Race.Vagrants)
                    activeRace = true;
                if (activeRace == false)
                    return;
                Village newVillage;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (j != 0 || i != 0)
                        {
                            var lastTile = tiles[x + i, y + j];
                            int lastX = x + i;
                            int lastY = y + j;
                            LastActionBuilder.Add(() => tiles[lastX, lastY] = lastTile);
                            tiles[x + i, y + j] = StrategicTileType.field;
                        }
                        //DestroyVillagesAtTile(new Vec2i(x + i, y + j));

                    }
                }

                var curVillages = State.World.Villages.Where(s => s.Side == (int)villageRace);
                if (curVillages.Where(s => s.Capital || s.Race == Race.Vagrants).Any() == false)
                {
                    newVillage = new Village(State.NameGen.GetTownName(villageRace, 0), clickLocation, 8, villageRace, true);
                }
                else
                {
                    bool blocked = true;
                    int nameIndex = 1;
                    for (int i = 1; i < 200; i++)
                    {
                        blocked = false;
                        foreach (Village village in curVillages)
                        {
                            if (village.Name == State.NameGen.GetTownName(villageRace, i))
                                blocked = true;
                        }
                        if (blocked == false)
                        {
                            nameIndex = i;
                            break;
                        }
                    }
                    newVillage = new Village(State.NameGen.GetTownName(villageRace, nameIndex), clickLocation, 8, villageRace, false);
                }
                LastActionBuilder.Add(() => DestroyVillagesAtTile(new Vec2i(x, y)));
                var villages = State.World.Villages.ToList();
                villages.Add(newVillage);
                if (newVillage.Race == Race.Vagrants)
                    newVillage.SubtractPopulation(99999);
                State.World.Villages = villages.ToArray();
                RefreshVillageCounts();
                RedrawVillages();
                RedrawTiles();
            }
            else if (EditingActiveMap)
            {
                Village vill = StrategicUtilities.GetVillageAt(clickLocation);
                if (vill != null)
                {
                    if (vill.Side != State.World.GetEmpireOfRace(villageRace).Side)
                    {
                        var lastSide = State.World.GetEmpireOfRace(villageRace).Side;
                        LastActionBuilder.Add(() => vill.ChangeOwner(State.World.GetEmpireOfRace(villageRace).Side));
                        vill.ChangeOwner(State.World.GetEmpireOfRace(villageRace).Side);
                    }
                    else if (vill.Race == villageRace)
                    {
                        var lastPop = vill.GetTotalPop();
                        LastActionBuilder.Add(() => { vill.SubtractPopulation(99999); vill.AddPopulation(lastPop); });
                        vill.AddPopulation(99999999);
                    }
                }
            }
            else
            {
                Village vill = StrategicUtilities.GetVillageAt(clickLocation);
                Village newVillage;
                if (vill != null)
                {
                    var curVillages = State.World.Villages.Where(s => s.Side == (int)villageRace);
                    if (curVillages.Where(s => s.Capital).Any() == false)
                    {
                        newVillage = new Village(State.NameGen.GetTownName(villageRace, 0), clickLocation, 8, villageRace, true);
                    }
                    else
                    {
                        bool blocked = true;
                        int nameIndex = 1;
                        for (int i = 1; i < 200; i++)
                        {
                            blocked = false;
                            foreach (Village village in curVillages)
                            {
                                if (village.Name == State.NameGen.GetTownName(villageRace, i))
                                    blocked = true;
                            }
                            if (blocked == false)
                            {
                                nameIndex = i;
                                break;
                            }
                        }
                        newVillage = new Village(State.NameGen.GetTownName(villageRace, nameIndex), clickLocation, 8, villageRace, false);
                    }
                    LastActionBuilder.Add(() =>
                    {
                        var tempVillages = State.World.Villages.ToList();
                        tempVillages.Remove(newVillage);
                        tempVillages.Add(vill);
                        State.World.Villages = tempVillages.ToArray();
                        RefreshVillageCounts();
                    });
                    var villages = State.World.Villages.ToList();
                    villages.Remove(vill);
                    villages.Add(newVillage);
                    State.World.Villages = villages.ToArray();
                    RefreshVillageCounts();
                    RedrawVillages();
                    RedrawTiles();
                }
            }
        }
        else if (ActiveSpecial && StrategicUtilities.GetVillageAt(clickLocation) == null && StrategicUtilities.GetMercenaryHouseAt(clickLocation) == null)
        {
            if (CanWalkInto(x, y) == false)
            {
                var lastTile = tiles[x, y];
                LastActionBuilder.Add(() => tiles[x, y] = lastTile);
                tiles[x, y] = StrategicTileType.grass;
            }

            DestroyVillagesAtTile(clickLocation);
            switch (activeSpecialType)
            {
                case SpecialType.MercenaryHouse:
                    MercenaryHouse newHouse = new MercenaryHouse(clickLocation);
                    var houses = State.World.MercenaryHouses.ToList();
                    houses.Add(newHouse);
                    State.World.MercenaryHouses = houses.ToArray();
                    LastActionBuilder.Add(() => DestroyVillagesAtTile(new Vec2i(x, y)));
                    break;
                case SpecialType.GoldMine:
                    GoldMine goldMine = new GoldMine(clickLocation);
                    var claimables = State.World.Claimables.ToList();
                    claimables.Add(goldMine);
                    State.World.Claimables = claimables.ToArray();
                    LastActionBuilder.Add(() => DestroyVillagesAtTile(new Vec2i(x, y)));
                    break;
            }
            RedrawTiles();
            RedrawVillages();
        }
        else if (ActiveDoodad && StrategicUtilities.GetVillageAt(clickLocation) == null && StrategicUtilities.GetMercenaryHouseAt(clickLocation) == null)
        {
            if (doodads == null)
                doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];

            if (doodads != null)
            {
                var lastDoodad = doodads[x, y];
                LastActionBuilder.Add(() => doodads[x, y] = lastDoodad);
            }

            doodads[x, y] = currentDoodadType;
            RedrawTiles();
        }

    }

    private void Fill(int startX, int startY)
    {
        StrategicTileType fillOverType = tiles[startX, startY];
        Vec2 q = new Vec2(startX, startY);
        int h = tiles.GetLength(1);
        int w = tiles.GetLength(0);

        List<Vec2> visited = new List<Vec2>();

        Stack<Vec2> stack = new Stack<Vec2>();
        stack.Push(q);
        while (stack.Count > 0)
        {
            Vec2 p = stack.Pop();
            int x = p.x;
            int y = p.y;
            if (y < 0 || y > h - 1 || x < 0 || x > w - 1)
                continue;
            if (visited.Contains(p))
            {
                continue;
            }
            if (tiles[x, y] != fillOverType)
                continue;
            visited.Add(p);
            var lastTile = tiles[x, y];
            LastActionBuilder.Add(() => tiles[x, y] = lastTile);
            tiles[x, y] = currentTileType;
            stack.Push(new Vec2(x + 1, y));
            stack.Push(new Vec2(x - 1, y));
            stack.Push(new Vec2(x, y + 1));
            stack.Push(new Vec2(x, y - 1));
        }
    }

    private void ReplaceAll(int targetX, int targetY)
    {
        StrategicTileType oldType = tiles[targetX, targetY];
        StrategicTileType newType = currentTileType;
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] == oldType)
                {
                    int lastX = x;
                    int lastY = y;
                    var lastTile = tiles[x, y];
                    LastActionBuilder.Add(() => tiles[lastX, lastY] = lastTile);
                    tiles[x, y] = newType;
                }
            }
        }
    }

    private void DestroyVillagesAtTile(Vec2i clickLocation)
    {
        Village villageAtTile = StrategicUtilities.GetVillageAt(clickLocation);
        if (villageAtTile != null)
        {
            LastActionBuilder.Add(() =>
            {
                var tempVillages = State.World.Villages.ToList();
                tempVillages.Add(villageAtTile);
                State.World.Villages = tempVillages.ToArray();
                RefreshVillageCounts();
                RedrawVillages();
            });
            var villages = State.World.Villages.ToList();
            villages.Remove(villageAtTile);
            State.World.Villages = villages.ToArray();
            RefreshVillageCounts();
            RedrawVillages();
        }
        MercenaryHouse mercHouseAtTile = StrategicUtilities.GetMercenaryHouseAt(clickLocation);
        if (mercHouseAtTile != null)
        {
            LastActionBuilder.Add(() =>
            {
                var tempHouses = State.World.MercenaryHouses.ToList();
                tempHouses.Add(mercHouseAtTile);
                State.World.MercenaryHouses = tempHouses.ToArray();
                RedrawVillages();
            });
            var houses = State.World.MercenaryHouses.ToList();
            houses.Remove(mercHouseAtTile);
            State.World.MercenaryHouses = houses.ToArray();
            RedrawVillages();
        }
        ClaimableBuilding claimableAtTile = StrategicUtilities.GetClaimableAt(clickLocation);
        if (claimableAtTile != null)
        {
            LastActionBuilder.Add(() =>
            {
                var tempClaimables = State.World.Claimables.ToList();
                tempClaimables.Add(claimableAtTile);
                State.World.Claimables = tempClaimables.ToArray();
                RedrawVillages();
            });
            var claimables = State.World.Claimables.ToList();
            claimables.Remove(claimableAtTile);
            State.World.Claimables = claimables.ToArray();
            RedrawVillages();
        }
    }


    public void LoadMapPicker()
    {
        var ui = Instantiate(State.GameManager.LoadPicker).GetComponent<FileLoaderUI>();
        new SimpleFileLoader(State.MapDirectory, "map", ui, true, SimpleFileLoader.LoaderType.MapEditor);
    }

    public void LoadMap(string filename)
    {

        Map map = Map.Get(filename);
        if (map == null)
            return;

        UndoActions.Clear();

        tiles = map.Tiles;
        doodads = map.Doodads;
        List<Village> newVillages = new List<Village>();
        for (int i = 0; i < map.storedVillages.Length; i++)
        {
            newVillages.Add(new Village("None", map.storedVillages[i].Position, 8, map.storedVillages[i].Race, map.storedVillages[i].Capital));
            if (newVillages.Last().Race == Race.Vagrants)
                newVillages.Last().SubtractPopulation(99999);
        }
        State.World.Villages = newVillages.ToArray();
        if (map.mercLocations != null)
        {
            List<MercenaryHouse> houses = new List<MercenaryHouse>();
            foreach (var merc in map.mercLocations)
            {
                houses.Add(new MercenaryHouse(merc));
            }
            if (houses.Count > 0)
                State.World.MercenaryHouses = houses.ToArray();
            else
                State.World.MercenaryHouses = new MercenaryHouse[0];
        }
        else
        {
            State.World.MercenaryHouses = new MercenaryHouse[0];
        }
        if (map.claimables != null)
        {
            List<ClaimableBuilding> claimables = new List<ClaimableBuilding>();
            foreach (var claimable in map.claimables)
            {
                if (claimable.Type == ClaimableType.GoldMine)
                    claimables.Add(new GoldMine(claimable.Position));
            }
            if (claimables.Count > 0)
                State.World.Claimables = claimables.ToArray();
        }
        else
        {
            State.World.Claimables = new ClaimableBuilding[0];
        }

        Config.World.StrategicWorldSizeX = tiles.GetLength(0);
        Config.World.StrategicWorldSizeY = tiles.GetLength(1);

        if (doodads == null)
            doodads = new StrategicDoodadType[tiles.GetLength(0), tiles.GetLength(1)];

        RecreateObjects();
    }


    public void SaveMap(string filename)
    {
        State.World.Tiles = tiles;
        State.World.Doodads = doodads;
        //if (StrategicConnectedChecker.AreAllConnected(State.World.Villages, StrategicUtilities.GetAllArmies()) == false)
        //{
        //    return;
        //}

        List<MapVillage> storedVillages = new List<MapVillage>();
        foreach (Village village in State.World.Villages)
        {
            storedVillages.Add(new MapVillage(village.Capital, village.Race, village.Position));
        }
        List<Vec2i> storedMercLocations = new List<Vec2i>();
        foreach (MercenaryHouse mercHouse in State.World.MercenaryHouses)
        {
            storedMercLocations.Add(mercHouse.Position);
        }
        List<MapClaimable> storedClaimables = new List<MapClaimable>();
        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable is GoldMine)
                storedClaimables.Add(new MapClaimable(ClaimableType.GoldMine, claimable.Position));
        }
        Map map = new Map
        {
            Tiles = tiles,
            Doodads = doodads,
            storedVillages = storedVillages.ToArray(),
            mercLocations = storedMercLocations.ToArray(),
            claimables = storedClaimables.ToArray(),
        };

        byte[] bytes = SerializationUtility.SerializeValue(map, DataFormat.Binary);
        File.WriteAllBytes(filename, bytes);

    }

    public void OpenResizePanel()
    {
        ResizeUI.gameObject.SetActive(true);
        ResizeUI.NewSizeX.text = tiles.GetLength(0).ToString();
        ResizeUI.NewSizeY.text = tiles.GetLength(1).ToString();
    }

    public void Resize()
    {

        int x;
        int y;
        try
        {
            x = Convert.ToInt32(ResizeUI.NewSizeX.text);
            y = Convert.ToInt32(ResizeUI.NewSizeY.text);
        }
        catch
        {
            State.GameManager.CreateMessageBox("Invalid value for one of the values");
            return;
        }
        if (x < 16 || y < 16)
        {
            State.GameManager.CreateMessageBox("Can't have a dimension less than 16");
            return;
        }
        UndoActions.Clear();
        int oldX = tiles.GetLength(0);
        int oldY = tiles.GetLength(1);




        StrategicTileType[,] newTiles = new StrategicTileType[x, y];
        StrategicDoodadType[,] newDoodads = new StrategicDoodadType[x, y];
        int diffX = 0;
        int diffY = 0;
        if (ResizeUI.AddRemoveX.value == 0)
            diffX = x - oldX;
        if (ResizeUI.AddRemoveY.value == 1)
            diffY = y - oldY;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (i < tiles.GetLength(0) + diffX && j < tiles.GetLength(1) + diffY && i - diffX >= 0 && i - diffX <= oldX - 1 && j - diffY >= 0 && j - diffY <= oldY - 1)
                    newTiles[i, j] = tiles[i - diffX, j - diffY];
                else
                    newTiles[i, j] = StrategicTileType.water;
            }
        }
        if (doodads != null)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i < tiles.GetLength(0) + diffX && j < tiles.GetLength(1) + diffY && i - diffX >= 0 && i - diffX <= oldX - 1 && j - diffY >= 0 && j - diffY <= oldY - 1)
                        newDoodads[i, j] = doodads[i - diffX, j - diffY];
                    else
                        newDoodads[i, j] = StrategicDoodadType.none;
                }
            }
        }



        List<Village> newVillages = new List<Village>();
        foreach (Village village in State.World.Villages.ToList())
        {
            village.Position = new Vec2i(village.Position.x + diffX, village.Position.y + diffY); //done for double checking a fix
            if (village.Position.x < x - 1 && village.Position.x > 0 && village.Position.y < y - 1 && village.Position.y > 0)
                newVillages.Add(village);
        }
        State.World.Villages = newVillages.ToArray();

        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            army.Position.x += diffX;
            army.Position.y += diffY;
        }


        foreach (Army army in StrategicUtilities.GetAllArmies().ToList())
        {
            if (army.Position.x < 0 || army.Position.y < 0 || army.Position.x >= x - 1 || army.Position.y >= y - 1)
            {
                Empire emp = State.World.GetEmpireOfSide(army.Side);
                emp.Armies.Remove(army);
            }
        }

        List<MercenaryHouse> newMercs = new List<MercenaryHouse>();
        foreach (MercenaryHouse merc in State.World.MercenaryHouses.ToList())
        {
            merc.Position.x += diffX;
            merc.Position.y += diffY;
            if (merc.Position.x < x - 1 && merc.Position.x > 0 && merc.Position.y < y - 1 && merc.Position.y > 0)
                newMercs.Add(merc);
        }
        State.World.MercenaryHouses = newMercs.ToArray();

        List<ClaimableBuilding> newClaims = new List<ClaimableBuilding>();
        foreach (ClaimableBuilding claim in State.World.Claimables.ToList())
        {
            claim.Position.x += diffX;
            claim.Position.y += diffY;
            if (claim.Position.x < x - 1 && claim.Position.x > 0 && claim.Position.y < y - 1 && claim.Position.y > 0)
                newClaims.Add(claim);
        }
        State.World.Claimables = newClaims.ToArray();


        tiles = newTiles;
        doodads = newDoodads;
        Config.World.StrategicWorldSizeX = x;
        Config.World.StrategicWorldSizeY = y;
        RecreateObjects();

        ResizeUI.gameObject.SetActive(false);

    }

    /// <summary>
    /// Local Version for the map editor that doesn't use the world tiles/doodads.  
    /// </summary>
    internal bool CanWalkInto(int x, int y)
    {
        if (StrategicTileInfo.CanWalkInto(tiles[x, y]) == true)
            return true;
        if (doodads != null && doodads[x, y] >= StrategicDoodadType.bridgeVertical && doodads[x, y] <= StrategicDoodadType.virtualBridgeIntersection)
            return true;
        return false;
    }

    public void SaveMapPrompt()
    {
        //if (StrategicConnectedChecker.AreAllConnected(State.World.Villages, StrategicUtilities.GetAllArmies()) == false)
        //{
        //    return;
        //}
        var ui = Instantiate(State.GameManager.SavePrompt).GetComponent<SaveNamePrompt>();
        ui.Save.onClick.AddListener(() => TrySave($"{State.MapDirectory}{ui.Name.text}.map"));
    }

    void TrySave(string name)
    {
        if (File.Exists(name))
        {
            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(() => SaveMap(name), "Overwrite", "Cancel", "Map with that name already exists, overwrite it?");
        }
        else
        {
            SaveMap(name);
        }
    }

    private static void RefreshVillageCounts()
    {
        if (Config.VillagesPerEmpire.Length != State.World.MainEmpires.Count)
            Config.World.VillagesPerEmpire = new int[State.World.MainEmpires.Count];
        for (int i = 0; i < State.World.MainEmpires.Count; i++)
        {
            Config.VillagesPerEmpire[i] = 0;
        }
        foreach (Village vill in State.World.Villages)
        {
            if (vill.Race < Race.Succubi)
                Config.VillagesPerEmpire[(int)vill.Race]++;
        }
    }

    void UpdateTooltips(int ClickX, int ClickY)
    {

        Village villageAtCursor = StrategicUtilities.GetVillageAt(new Vec2i(ClickX, ClickY));
        if (villageAtCursor == null)
        {
            MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(new Vec2i(ClickX, ClickY));
            if (house != null)
            {
                Tooltip.text = "Mercenary House";
            }
            else
            {
                Tooltip.gameObject.SetActive(false);
            }
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            Tooltip.gameObject.SetActive(true);
            if (EditingActiveMap)
            {
                sb.AppendLine($"Village: {villageAtCursor.Name}");
                if (villageAtCursor.Capital)
                    sb.AppendLine($"Capital City ({villageAtCursor.OriginalRace})");
                if (villageAtCursor.Race != Race.Vagrants || villageAtCursor.GetTotalPop() != 0)
                {
                    sb.AppendLine($"Owner: {(Race)villageAtCursor.Side}");
                    sb.AppendLine($"Race: {villageAtCursor.Race}");
                }
                else
                {
                    sb.AppendLine($"Abandoned Village");
                }
                sb.AppendLine($"Village Location - X: {villageAtCursor.Position.x} Y: {villageAtCursor.Position.y}");

            }
            else
            {
                if (villageAtCursor.Race != Race.Vagrants || villageAtCursor.GetTotalPop() != 0)
                {
                    if (villageAtCursor.Capital)
                        sb.AppendLine($"Capital City ({villageAtCursor.OriginalRace})");
                    sb.AppendLine($"Race: {villageAtCursor.Race}");
                }
                else
                {
                    sb.AppendLine($"Abandoned Village");
                }

            }
            Tooltip.text = sb.ToString();

        }

    }


    public void AttemptUndo()
    {
        if (UndoActions.Any())
        {
            UndoActions[UndoActions.Count - 1].Undo();
            UndoActions.RemoveAt(UndoActions.Count - 1);
        }
    }

    public override void ReceiveInput()
    {
        UndoButton.interactable = UndoActions.Any();

        if (EventSystem.current.IsPointerOverGameObject() == false) //Makes sure mouse isn't over a UI element
        {

            Vector2 currentMousePos = State.GameManager.Camera.ScreenToWorldPoint(Input.mousePosition);

            int x = (int)(currentMousePos.x + 0.5f);
            int y = (int)(currentMousePos.y + 0.5f);
            if (x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY)
            {
                UpdateTooltips(x, y);
                if (Input.GetMouseButtonDown(0))
                    ProcessClick(x, y);
                else if (Input.GetMouseButton(0) && (ActiveTile || ActiveDoodad))
                    ProcessClick(x, y, true);
                if (Input.GetMouseButtonDown(1))
                    ProcessRightClick(x, y);
            }

        }
        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            if (UndoButton.interactable)
                UndoButton.onClick.Invoke();
        }


        if (Input.GetButtonDown("Cancel"))
        {
            ActiveTile = false;
            ActiveVillage = false;
            ActiveSpecial = false;
            ActiveDoodad = false;
            SelectionBackground.SetActive(false);
        }

    }

}

