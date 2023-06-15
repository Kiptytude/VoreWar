static class StrategicConnectedChecker
{
    internal static bool AreAllConnected(Village[] villages, Army[] armies, bool broadcast = true)
    {
        if (armies == null)
            armies = new Army[0];
        int[,] tileZone = new int[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        int nextZone = 1;
        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                if (TileCheck(x, y) == false)
                    continue;

                if (x > 0 && y > 0 && tileZone[x - 1, y - 1] != 0)
                    tileZone[x, y] = tileZone[x - 1, y - 1];
                else if (y > 0 && tileZone[x, y - 1] != 0)
                    tileZone[x, y] = tileZone[x, y - 1];
                else if (x < Config.StrategicWorldSizeX - 1 && y > 0 && tileZone[x + 1, y - 1] != 0)
                    tileZone[x, y] = tileZone[x + 1, y - 1];
                else if (x > 0 && tileZone[x - 1, y] != 0)
                    tileZone[x, y] = tileZone[x - 1, y];
                else
                {
                    tileZone[x, y] = nextZone;
                    nextZone++;
                }
            }
        }

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                if (TileCheck(x, y) == false)
                    continue;
                int neighborZone = GetNeighborZone(tileZone, x, y);
                if (neighborZone != 0)
                {
                    for (int i = 0; i < Config.StrategicWorldSizeX; i++)
                    {
                        for (int j = 0; j < Config.StrategicWorldSizeY; j++)
                        {
                            if (tileZone[i, j] == neighborZone)
                                tileZone[i, j] = tileZone[x, y];
                        }
                    }
                }
            }
        }
        int zone = 0;
        string lastVillageUsed = "";
        foreach (Village village in villages)
        {
            if (zone == 0)
            {
                zone = tileZone[village.Position.x, village.Position.y];
                lastVillageUsed = $"{village.Race} village at x:{village.Position.x} y: {village.Position.y}";
            }
            else if (zone != tileZone[village.Position.x, village.Position.y])
            {
                if (broadcast)
                {

                    State.GameManager.CreateMessageBox($"Some villages cannot be reached from other villages\nCouldn't find path between\n{lastVillageUsed} and {village.Race} village at x:{village.Position.x} y: {village.Position.y}");
                }

                return false;
            }
        }

        foreach (Army army in armies)
        {
            if (zone == 0)
                zone = tileZone[army.Position.x, army.Position.y];
            else if (zone != tileZone[army.Position.x, army.Position.y])
            {
                if (broadcast) State.GameManager.CreateMessageBox("Some armies cannot reach villages");
                return false;
            }
        }

        return true;
    }

    static bool TileCheck(int x, int y)
    {
        if (State.GameManager.CurrentScene == State.GameManager.MapEditor)
            return State.GameManager.MapEditor.CanWalkInto(x, y);
        return StrategicTileInfo.CanWalkInto(x, y);
    }

    static int GetNeighborZone(int[,] tileZone, int x, int y)
    {
        if (y > 0 && tileZone[x, y - 1] != 0 && tileZone[x, y - 1] != tileZone[x, y])
            return tileZone[x, y - 1];
        if (y < Config.StrategicWorldSizeY - 1 && tileZone[x, y + 1] != 0 && tileZone[x, y + 1] != tileZone[x, y])
            return tileZone[x, y + 1];
        if (x < Config.StrategicWorldSizeX - 1 && tileZone[x + 1, y] != 0 && tileZone[x + 1, y] != tileZone[x, y])
            return tileZone[x + 1, y];
        if (x > 0 && tileZone[x - 1, y] != 0 && tileZone[x - 1, y] != tileZone[x, y])
            return tileZone[x - 1, y];
        if (x < Config.StrategicWorldSizeX - 1 && y < Config.StrategicWorldSizeY - 1 && tileZone[x + 1, y + 1] != 0 && tileZone[x + 1, y + 1] != tileZone[x, y])
            return tileZone[x + 1, y + 1];
        if (x < Config.StrategicWorldSizeX - 1 && y > 0 && tileZone[x + 1, y - 1] != 0 && tileZone[x + 1, y - 1] != tileZone[x, y])
            return tileZone[x + 1, y - 1];
        if (y < Config.StrategicWorldSizeY - 1 && x > 0 && tileZone[x - 1, y + 1] != 0 && tileZone[x - 1, y + 1] != tileZone[x, y])
            return tileZone[x - 1, y + 1];
        if (x > 0 && y > 0 && tileZone[x - 1, y - 1] != 0 && tileZone[x - 1, y - 1] != tileZone[x, y])
            return tileZone[x - 1, y - 1];

        return 0;
    }

}
