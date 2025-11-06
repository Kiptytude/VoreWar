using System.Collections.Generic;



namespace TacticalDecorations
{
    enum PathType
    {
        BlockAll,
        Tree
    }

    enum TacDecType
    {
        GrassBush,
        GrassFlowers1,
        GrassSmallRock1,
        GrassMediumRock,
        GrassHugeRock,
        GrassTree1,
        GrassTree2,
        GrassFlowers2,
        GrassFlowers3,
        GrassSmallRock2,
        GrassBirchTree1,
        GrassBirchTree2,


        DesertBones1 = 100,
        DesertBones2,
        DesertBones3,
        DesertBones4,
        DesertOffsetPillar,
        DesertRocks1,
        DesertRocks2,
        DesertRocks3,
        DesertRocks4,
        DesertRocks5,
        DesertRocks6,
        DesertRocks7,
        DesertRocks8,
        DesertCactus1,
        DesertCactus2,
        DesertCactus3,
        DesertCactus4,
        DesertCactus5,

        SnowTree1,
        SnowTree2,
        SnowRock1,
        SnowMound1,
        SnowBear,
        SnowHolidayTree1,
        SnowHolidayTree2,
        SnowCandyCane1,
        SnowCandyCane2,
        SnowPresent,
        SnowGoblin,
        SnowWreath,
        SnowHolidayRock1,
        SnowHolidayRock2,
        SnowFootprints,

        VolcanicBones1,
        VolcanicBones2,
        VolcanicBones3,
        VolcanicBones4,
        VolcanicOffsetPillar,
        VolcanicRocks1,
        VolcanicRocks2,
        VolcanicRocks3,
        VolcanicRocks4,
        VolcanicRocks5,
        VolcanicRocks6,
        VolcanicRocks7,
        VolcanicRocks8,
        VolcanicMagmaRock1,
        VolcanicMagmaRock2,
        VolcanicMagmaRock3,
        VolcanicMagmaRock4,
        VolcanicMagmaRock5,

        BeachPlainSand,
        BeachSmallRock1,
        BeachMediumRock1,
        BeachBigRock1,
        BeachGrass,
        BeachBones,
        BeachFishBones,
        BeachStarfish,
        BeachPalm,

        SwampPad1,
        SwampPad2,
        SwampPad3,
        SwampCatTail,
        SwampGrass,
        SwampTreeBig,
        SwampTree,
    }

    class TacticalDecoration
    {
        internal int Width;
        /// <summary>
        /// The height of blocked tiles
        /// </summary>
        internal int Height;
        /// <summary>
        /// Graphical tiles.  Height, then width
        /// </summary>
        internal int[,] Tile;

        internal PathType PathType;

        public TacticalDecoration(int width, int height, int[,] tile, PathType pathType = PathType.BlockAll)
        {
            Width = width;
            Height = height;
            Tile = tile;
            PathType = pathType;
        }
    }

    static class TacticalDecorationList
    {
        internal static Dictionary<TacDecType, TacticalDecoration> DecDict = new Dictionary<TacDecType, TacticalDecoration>()
        {
            [TacDecType.GrassBush] = new TacticalDecoration(0, 0, new int[,] { { 101 } }),
            [TacDecType.GrassFlowers1] = new TacticalDecoration(0, 0, new int[,] { { 102 } }),
            [TacDecType.GrassMediumRock] = new TacticalDecoration(1, 1, new int[,] { { 103 } }),
            [TacDecType.GrassSmallRock1] = new TacticalDecoration(0, 0, new int[,] { { 104 } }),
            [TacDecType.GrassTree1] = new TacticalDecoration(1, 1, new int[,] { { 106, 105 } }, PathType.Tree),
            [TacDecType.GrassTree2] = new TacticalDecoration(1, 1, new int[,] { { 112, 111 } }, PathType.Tree),
            [TacDecType.GrassHugeRock] = new TacticalDecoration(2, 1, new int[,] { { 108, 107 }, { 110, 109 } }),
            [TacDecType.GrassFlowers2] = new TacticalDecoration(0, 0, new int[,] { { 113 } }),
            [TacDecType.GrassSmallRock2] = new TacticalDecoration(0, 0, new int[,] { { 114 } }),
            [TacDecType.GrassFlowers3] = new TacticalDecoration(0, 0, new int[,] { { 115 } }),
            [TacDecType.GrassBirchTree1] = new TacticalDecoration(1, 1, new int[,] { { 117, 116 } }, PathType.Tree),
            [TacDecType.GrassBirchTree2] = new TacticalDecoration(1, 1, new int[,] { { 119, 118 } }, PathType.Tree),

            [TacDecType.DesertBones1] = new TacticalDecoration(2, 1, new int[,] { { 202 }, { 203 } }),
            [TacDecType.DesertBones2] = new TacticalDecoration(1, 1, new int[,] { { 204 } }),
            [TacDecType.DesertBones3] = new TacticalDecoration(1, 1, new int[,] { { 205 } }),
            [TacDecType.DesertBones4] = new TacticalDecoration(1, 1, new int[,] { { 206 } }),
            [TacDecType.DesertRocks1] = new TacticalDecoration(1, 1, new int[,] { { 217 } }),
            [TacDecType.DesertRocks2] = new TacticalDecoration(1, 1, new int[,] { { 219, 218 } }),
            [TacDecType.DesertRocks3] = new TacticalDecoration(1, 1, new int[,] { { 220 } }),
            [TacDecType.DesertRocks4] = new TacticalDecoration(1, 1, new int[,] { { 221 } }),
            [TacDecType.DesertRocks5] = new TacticalDecoration(1, 1, new int[,] { { 222 } }),
            [TacDecType.DesertRocks6] = new TacticalDecoration(1, 1, new int[,] { { 223 } }),
            [TacDecType.DesertRocks7] = new TacticalDecoration(1, 1, new int[,] { { 224 } }),
            [TacDecType.DesertRocks8] = new TacticalDecoration(1, 1, new int[,] { { 225 } }),
            [TacDecType.DesertCactus1] = new TacticalDecoration(1, 1, new int[,] { { 226 } }, PathType.Tree),
            [TacDecType.DesertCactus2] = new TacticalDecoration(1, 1, new int[,] { { 227 } }, PathType.Tree),
            [TacDecType.DesertCactus3] = new TacticalDecoration(1, 1, new int[,] { { 228 } }, PathType.Tree),
            [TacDecType.DesertCactus4] = new TacticalDecoration(1, 1, new int[,] { { 229 } }, PathType.Tree),
            [TacDecType.DesertCactus5] = new TacticalDecoration(1, 2, new int[,] { { 232, 231 } }, PathType.Tree),


            [TacDecType.SnowTree1] = new TacticalDecoration(1, 1, new int[,] { { 402, 401 } }, PathType.Tree),
            [TacDecType.SnowTree2] = new TacticalDecoration(1, 1, new int[,] { { 404, 403 } }, PathType.Tree),
            [TacDecType.SnowRock1] = new TacticalDecoration(1, 1, new int[,] { { 405 } }),
            [TacDecType.SnowMound1] = new TacticalDecoration(0, 0, new int[,] { { 406 } }),
            [TacDecType.SnowBear] = new TacticalDecoration(1, 1, new int[,] { { 407 } }),

            [TacDecType.SnowHolidayTree1] = new TacticalDecoration(1, 1, new int[,] { { 409, 408 } }, PathType.Tree),
            [TacDecType.SnowHolidayTree2] = new TacticalDecoration(1, 1, new int[,] { { 411, 410 } }, PathType.Tree),
            [TacDecType.SnowCandyCane1] = new TacticalDecoration(1, 1, new int[,] { { 412 } }),
            [TacDecType.SnowCandyCane2] = new TacticalDecoration(1, 1, new int[,] { { 413 } }),
            [TacDecType.SnowPresent] = new TacticalDecoration(1, 1, new int[,] { { 414 } }),
            [TacDecType.SnowGoblin] = new TacticalDecoration(1, 1, new int[,] { { 415 } }),
            [TacDecType.SnowWreath] = new TacticalDecoration(1, 1, new int[,] { { 416 } }),
            [TacDecType.SnowHolidayRock1] = new TacticalDecoration(1, 1, new int[,] { { 417 } }),
            [TacDecType.SnowHolidayRock2] = new TacticalDecoration(1, 1, new int[,] { { 418 } }),
            [TacDecType.SnowFootprints] = new TacticalDecoration(0, 0, new int[,] { { 419 } }),


            [TacDecType.VolcanicBones1] = new TacticalDecoration(2, 1, new int[,] { { 502 }, { 503 } }),
            [TacDecType.VolcanicBones2] = new TacticalDecoration(1, 1, new int[,] { { 504 } }),
            [TacDecType.VolcanicBones3] = new TacticalDecoration(1, 1, new int[,] { { 505 } }),
            [TacDecType.VolcanicBones4] = new TacticalDecoration(1, 1, new int[,] { { 506 } }),
            [TacDecType.VolcanicRocks1] = new TacticalDecoration(1, 1, new int[,] { { 517 } }),
            [TacDecType.VolcanicRocks2] = new TacticalDecoration(1, 1, new int[,] { { 519, 518 } }),
            [TacDecType.VolcanicRocks3] = new TacticalDecoration(1, 1, new int[,] { { 520 } }),
            [TacDecType.VolcanicRocks4] = new TacticalDecoration(1, 1, new int[,] { { 521 } }),
            [TacDecType.VolcanicRocks5] = new TacticalDecoration(1, 1, new int[,] { { 522 } }),
            [TacDecType.VolcanicRocks6] = new TacticalDecoration(1, 1, new int[,] { { 523 } }),
            [TacDecType.VolcanicRocks7] = new TacticalDecoration(1, 1, new int[,] { { 524 } }),
            [TacDecType.VolcanicRocks8] = new TacticalDecoration(1, 1, new int[,] { { 525 } }),
            [TacDecType.VolcanicMagmaRock1] = new TacticalDecoration(1, 1, new int[,] { { 526 } }, PathType.Tree),
            [TacDecType.VolcanicMagmaRock2] = new TacticalDecoration(1, 1, new int[,] { { 527 } }, PathType.Tree),
            [TacDecType.VolcanicMagmaRock3] = new TacticalDecoration(1, 1, new int[,] { { 528 } }, PathType.Tree),
            [TacDecType.VolcanicMagmaRock4] = new TacticalDecoration(1, 1, new int[,] { { 529 } }, PathType.Tree),
            [TacDecType.VolcanicMagmaRock5] = new TacticalDecoration(1, 2, new int[,] { { 532, 531 } }, PathType.Tree),

            [TacDecType.BeachSmallRock1] = new TacticalDecoration(1, 1, new int[,] { { 601 } }, PathType.Tree),
            [TacDecType.BeachBigRock1] = new TacticalDecoration(2, 1, new int[,] { { 602 }, { 603 } }, PathType.Tree),
            [TacDecType.BeachGrass] = new TacticalDecoration(0, 0, new int[,] { { 604 } }),
            [TacDecType.BeachMediumRock1] = new TacticalDecoration(1, 1, new int[,] { { 605 }}, PathType.Tree),
            [TacDecType.BeachBones] = new TacticalDecoration(2, 2, new int[,] { { 608, 606 }, { 609, 607 } }, PathType.Tree),
            [TacDecType.BeachFishBones] = new TacticalDecoration(0, 0, new int[,] { {610} }),
            [TacDecType.BeachStarfish] = new TacticalDecoration(0, 0, new int[,] { {611} }),
            [TacDecType.BeachPalm] = new TacticalDecoration(1, 1, new int[,] { {613, 612 } }),

            [TacDecType.SwampPad1] = new TacticalDecoration(0, 0, new int[,] { {701 } }),
            [TacDecType.SwampPad2] = new TacticalDecoration(0, 0, new int[,] { {702} }),
            [TacDecType.SwampPad3] = new TacticalDecoration(0, 0, new int[,] { { 703 }, {704} }),
            [TacDecType.SwampCatTail] = new TacticalDecoration(0, 0, new int[,] { {705} }),
            [TacDecType.SwampGrass] = new TacticalDecoration(0, 0, new int[,] { {706 } }),
            [TacDecType.SwampTreeBig] = new TacticalDecoration(1, 1, new int[,] { { 708, 707}, { 710, 709 } }, PathType.Tree),
            [TacDecType.SwampTree] = new TacticalDecoration(1, 1, new int[,] { {712, 711 } }, PathType.Tree),
        };



        internal static TacDecType[] Bones = new TacDecType[] { TacDecType.DesertBones1, TacDecType.DesertBones2, TacDecType.DesertBones3, TacDecType.DesertBones4 };
        internal static TacDecType[] Rocks = new TacDecType[] { TacDecType.DesertRocks1, TacDecType.DesertRocks2, TacDecType.DesertRocks3, TacDecType.DesertRocks4, TacDecType.DesertRocks5, TacDecType.DesertRocks6, TacDecType.DesertRocks7, TacDecType.DesertRocks8 };
        internal static TacDecType[] Cactus = new TacDecType[] { TacDecType.DesertCactus1, TacDecType.DesertCactus2, TacDecType.DesertCactus3, TacDecType.DesertCactus4, TacDecType.DesertCactus5 };


        internal static TacDecType[] GrassEnvironment = new TacDecType[] { TacDecType.GrassBush, TacDecType.GrassFlowers1, TacDecType.GrassFlowers2, TacDecType.GrassFlowers3, TacDecType.GrassSmallRock1, TacDecType.GrassSmallRock2, TacDecType.GrassTree1, TacDecType.GrassTree2, TacDecType.GrassMediumRock, TacDecType.GrassHugeRock, TacDecType.GrassBirchTree1, TacDecType.GrassBirchTree2 };
        internal static TacDecType[] GrassPureTrees = new TacDecType[] { TacDecType.GrassTree1, TacDecType.GrassTree2, TacDecType.GrassBirchTree1, TacDecType.GrassBirchTree2 };

        internal static TacDecType[] SnowEnvironment = new TacDecType[] { TacDecType.SnowTree1, TacDecType.SnowTree2, TacDecType.SnowRock1, TacDecType.SnowMound1, TacDecType.SnowBear };
        internal static TacDecType[] HolidaySnowEnvironment = new TacDecType[] { TacDecType.SnowTree1, TacDecType.SnowTree2, TacDecType.SnowRock1, TacDecType.SnowMound1, TacDecType.SnowBear, TacDecType.SnowHolidayTree1, TacDecType.SnowHolidayTree2, TacDecType.SnowCandyCane1, TacDecType.SnowCandyCane2, TacDecType.SnowPresent, TacDecType.SnowGoblin, TacDecType.SnowWreath, TacDecType.SnowHolidayRock1, TacDecType.SnowHolidayRock2, TacDecType.SnowFootprints };

        internal static TacDecType[] CharredBones = new TacDecType[] { TacDecType.VolcanicBones1, TacDecType.VolcanicBones2, TacDecType.VolcanicBones3, TacDecType.VolcanicBones4 };
        internal static TacDecType[] VolcanicRocks = new TacDecType[] { TacDecType.VolcanicRocks1, TacDecType.VolcanicRocks2, TacDecType.VolcanicRocks3, TacDecType.VolcanicRocks4, TacDecType.VolcanicRocks5, TacDecType.VolcanicRocks6, TacDecType.VolcanicRocks7, TacDecType.VolcanicRocks8 };
        internal static TacDecType[] VolcanicMagmaRocks = new TacDecType[] { TacDecType.VolcanicMagmaRock1, TacDecType.VolcanicMagmaRock2, TacDecType.VolcanicMagmaRock3, TacDecType.VolcanicMagmaRock4, TacDecType.VolcanicMagmaRock5 };

        internal static TacDecType[] BeachEnvironment = new TacDecType[] { TacDecType.BeachSmallRock1, TacDecType.BeachMediumRock1, TacDecType.BeachBigRock1, TacDecType.BeachGrass, TacDecType.BeachBones, TacDecType.BeachFishBones, TacDecType.BeachStarfish, TacDecType.BeachPalm};
        
        internal static TacDecType[] SwampEnvironment = new TacDecType[] {TacDecType.SwampGrass, TacDecType.SwampTreeBig, TacDecType.SwampTree };
        internal static TacDecType[] SwampWaterEnvironment = new TacDecType[] { TacDecType.SwampPad1, TacDecType.SwampPad2, TacDecType.SwampPad3, TacDecType.SwampCatTail,};
    }
}



