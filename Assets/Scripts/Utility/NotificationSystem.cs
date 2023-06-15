static class NotificationSystem
{
    internal static void VillageOwnerChanged(Village village, int previousSide, int newSide)
    {
        string previousRace = State.World.GetEmpireOfSide(previousSide)?.Name ?? "Unknown Race";
        string newRace = State.World.GetEmpireOfSide(newSide)?.Name ?? "Unknown Race";
        State.GameManager.StrategyMode.ShowNotification($"{village.Name} has been taken from the {previousRace} and claimed by the {newRace}", 3);
    }

    internal static void ShowNotification(string message, float time = 3)
    {
        State.GameManager.StrategyMode.ShowNotification(message, time);
    }
}

