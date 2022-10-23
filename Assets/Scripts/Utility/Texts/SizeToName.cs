using System;

static class SizeToName
{
    static string[] labels;

    static SizeToName()
    {
        labels = new string[]
        {
"Anemic",
"Feeble",
"Fragile",
"Frail",
"Shaky",
"Insubstantial",
"Weak",
"Delicate",
"Scouts",
"Fair",
"Respectable",
"Raiders",
"Middling",
"Warband",
"Resilient",
"Tough",
"Strong",
"Tempered",
"Potent",
"Powerful",
"Principal",
"Professional",
"Veteran",
"Distinguished",
"Renowned",
"Great",
"Famed",
"Unprecedented",
"Honored",
"Fabled",
"Enduring",
"Venerable",
"Immortal",
"Eternal",
"Legendary",
"Historic",
"Revered",
"Worshiped",
"Demonic",
"Prophetic",
"Hallowed",
"Divine",
"Almighty",
"Infinite",
"Godlike",
        };
    }

    public static string ForTroops(int size)
    {
        if (size <= 0)
            return "none";
        else if (size <= 4)
            return "scouts (1-4)";
        else if (size <= 8)
            return "squad (5-8)";
        else if (size <= 14)
            return "division (9-14)";
        else if (size <= 21)
            return "regiment (15-21)";
        else if (size <= 28)
            return "army (22-28)";
        else
            return "horde (28+)";
    }

    public static string ForArmyStrength(double strength)
    {
        const int startingBase = 60;
        const float multiplier = 1.3f;
        int log = 1 + (int)(Math.Log(strength / startingBase) / Math.Log(multiplier));
        if (log < 0)
            log = 0;
        if (log < labels.Length)
            return $"{labels[log]} ({log})";
        return $"Indescribable ({log})";
    }
}
