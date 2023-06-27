public class Spawn : Unit
{

    public Spawn(int side, Race race, int startingXP) : base(side, race, startingXP, true, UnitType.Spawn)
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);
        Health = Health/2;

        Stats[(int)Stat.Strength] = 5 + raceStats.Strength.Minimum;
        Stats[(int)Stat.Dexterity] = 5 + raceStats.Dexterity.Minimum;
        Stats[(int)Stat.Endurance] = 6 + raceStats.Endurance.Minimum;
        Stats[(int)Stat.Mind] = 6 + raceStats.Mind.Minimum;
        Stats[(int)Stat.Will] = 6 + raceStats.Will.Minimum;
        Stats[(int)Stat.Agility] = 9 + raceStats.Agility.Minimum;
        Stats[(int)Stat.Voracity] = 8 + raceStats.Voracity.Minimum;
        Stats[(int)Stat.Stomach] = 4 + raceStats.Stomach.Minimum;

        Health = MaxHealth;
        if (race == Race.Lizards)
            Races.GetRace(Race.Lizards).RandomCustom(this);
        ExpMultiplier = 2;
        Type = UnitType.Spawn;
        if (race == Race.Slimes)
        {
            if (Config.HermFraction >= 0.05)
            {
                DickSize = 0;
            }
            else
            {
                DickSize = -1;
            }
            ClothingType = 1;
            SetDefaultBreastSize(1);
            HairStyle = 1;
            ClothingColor = 0;
            ClothingColor2 = 0;
            ClothingColor3 = 0;
            ReloadTraits();
            InitializeTraits();
        }
        var list = CustomizationDataStorer.GetCompatibleCustomizations(race, UnitType.Spawn, false);
        if (list != null && list.Count > 0)
        {
            CustomizationDataStorer.ExternalCopyToUnit(list[State.Rand.Next(list.Count)], this);
        }
    }

    public override int GetStatTotal()
    {
        return base.GetStatTotal();
    }
}

