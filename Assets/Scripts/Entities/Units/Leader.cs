public class Leader : Unit
{

    public Leader(int side, Race race, int startingXP) : base(side, race, startingXP, true, UnitType.Leader)
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);

        Stats[(int)Stat.Strength] = 5 + raceStats.Strength.Minimum + raceStats.Strength.Roll;
        Stats[(int)Stat.Dexterity] = 5 + raceStats.Dexterity.Minimum + raceStats.Dexterity.Roll;
        Stats[(int)Stat.Endurance] = 6 + raceStats.Endurance.Minimum + raceStats.Endurance.Roll;
        Stats[(int)Stat.Mind] = 6 + raceStats.Mind.Minimum + raceStats.Mind.Roll;
        Stats[(int)Stat.Will] = 6 + raceStats.Will.Minimum + raceStats.Will.Roll;
        Stats[(int)Stat.Agility] = 9 + raceStats.Agility.Minimum + raceStats.Agility.Roll;
        Stats[(int)Stat.Voracity] = 8 + raceStats.Voracity.Minimum + raceStats.Voracity.Roll;
        Stats[(int)Stat.Stomach] = 4 + raceStats.Stomach.Minimum + raceStats.Stomach.Roll;
        Stats[(int)Stat.Leadership] = 10;

        Health = MaxHealth;
        if (race == Race.Lizards)
            Races.GetRace(Race.Lizards).RandomCustom(this);
        if (Config.LetterBeforeLeaderNames != "")
            Name = Config.LetterBeforeLeaderNames + Name.ToLower();
        ExpMultiplier = 2;
        Type = UnitType.Leader;
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
        if (Config.LeadersUseCustomizations)
        {
            var list = CustomizationDataStorer.GetCompatibleCustomizations(race, UnitType.Leader, false);
            if (list != null && list.Count > 0)
            {
                CustomizationDataStorer.ExternalCopyToUnit(list[State.Rand.Next(list.Count)], this);
            }
        }
    }
    public static Stat[] GetLevelUpPossibilities()
    {
        Stat[] ret;
        if (Config.LeadersAutoGainLeadership)
            ret = new Stat[(int)Stat.Leadership];
        else
            ret = new Stat[(int)Stat.None];
        for (int i = 0; i < ret.Length; i++)
        {

            ret[i] = (Stat)i;
        }
        return ret;
    }

    public override int GetStatTotal()
    {
        return base.GetStatTotal() + GetStat(Stat.Leadership);
    }
}

