using OdinSerializer;
using System;

namespace TacticalBuildings
{
    abstract class TacticalBuilding
    {
        [OdinSerialize]
        internal Vec2 LowerLeftPosition;
        /// <summary>
        /// The width of blocked tiles
        /// </summary>
        [OdinSerialize]
        internal int Width;
        /// <summary>
        /// The height of blocked tiles
        /// </summary>
        [OdinSerialize]
        internal int Height;
        /// <summary>
        /// Graphical tiles.  Height, then width
        /// </summary>
        [OdinSerialize]
        internal int[,] Tile;

        [OdinSerialize]
        internal int[,] FrontColoredTile;

        public TacticalBuilding(Vec2 lowerLeftPosition)
        {
            LowerLeftPosition = lowerLeftPosition;
            FrontColoredTile = new int[0, 0];
        }
    }

    class LogCabin : TacticalBuilding
    {
        public LogCabin(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[3, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[8 + State.Rand.Next(3)]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[11 + State.Rand.Next(3)])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[2 + State.Rand.Next(3)]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[5 + State.Rand.Next(3)])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[0]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[1])
            }
            };
        }
    }

    class Log1x2 : TacticalBuilding
    {
        public Log1x2(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[2 + State.Rand.Next(3)]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[5 + State.Rand.Next(3)])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[0]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[1])
            },
            };
        }
    }

    class Log1x1 : TacticalBuilding
    {
        public Log1x1(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 1]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[9]),
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[8]),
            },
            };
        }
    }

    class Well : TacticalBuilding
    {
        public Well(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[10]),
            },
            };
        }
    }

    class HarpyNest : TacticalBuilding
    {
        public HarpyNest(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[2]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[3])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[0]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[1])
            },
            };
        }
    }

    class HarpyNestCanopy : TacticalBuilding
    {
        public HarpyNestCanopy(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[2]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[3])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[4]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[5])
            },
            };
            FrontColoredTile = new int[2, 2]
            {
            {
                -1, -1
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[6]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[7])
            },
            };
        }
    }

    class CatHouse : TacticalBuilding
    {
        public CatHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[2]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[3])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[0]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[1])
            },
            };
        }
    }

    class StoneHouse : TacticalBuilding
    {
        public StoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[6]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[7])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[4]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[5])
            },
            };
        }
    }

    class LamiaTemple : TacticalBuilding
    {
        public LamiaTemple(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[2]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[3])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[0]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[1])
            },
            };
        }
    }

    class CobbleStoneHouse : TacticalBuilding
    {
        public CobbleStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[10]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[11])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[8]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[9])
            },
            };
        }
    }
    class YellowCobbleStoneHouse : TacticalBuilding
    {
        public YellowCobbleStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[12]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[13])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[8]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[9])
            },
            };
        }
    }

    class Barrels : TacticalBuilding
    {
        public Barrels(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[11]),
            },
            };
        }
    }

    class LogPile : TacticalBuilding
    {
        public LogPile(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[12]),
            },
            };
        }
    }

    class FancyStoneHouse : TacticalBuilding
    {
        public FancyStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[16]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[17])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[14]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[15])
            },
            };
        }
    }

    class FoxStoneHouse : TacticalBuilding
    {
        public FoxStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[20]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[21])
            },
            {
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[18]),
                Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[19])
            },
            };
        }
    }


}

