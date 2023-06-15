using OdinSerializer;
using System.Text;



public class StrategicStats
{
    class RaceStats
    {
        [OdinSerialize]
        public string EmpireName;
        [OdinSerialize]
        public int BattlesWon;
        [OdinSerialize]
        public int BattlesLost;
        [OdinSerialize]
        public int ArmiesLost;
        [OdinSerialize]
        public int LeaderResurrections;
        [OdinSerialize]
        public int TotalGoldCollected;
        [OdinSerialize]
        public int TotalGoldSpent;
        [OdinSerialize]
        public int GoldSpentOnEquipment;
        [OdinSerialize]
        public int GoldSpentOnBuildings;
        [OdinSerialize]
        public int GoldSpentOnTraining;
        [OdinSerialize]
        public int GoldSpentOnMaintainingArmies;
        [OdinSerialize]
        public int SoldiersRecruited;
        [OdinSerialize]
        public int SoldiersLost;

        public RaceStats(string empireName)
        {
            EmpireName = empireName;
            TotalGoldCollected = Config.StartingGold;
        }

    }
    [OdinSerialize]
    RaceStats[] EmpireStats;

    public StrategicStats()
    {
        int empires = State.World.MainEmpires.Count;
        EmpireStats = new RaceStats[empires];
        for (int i = 0; i < empires; i++)
        {
            EmpireStats[i] = new RaceStats(((Race)i).ToString());
        }
    }

    public void ExpandToIncludeNewRaces()
    {
        var empireStats = new RaceStats[State.World.MainEmpires.Count];
        for (int i = 0; i < State.World.MainEmpires.Count; i++)
        {
            if (EmpireStats.Length > i)
                empireStats[i] = EmpireStats[i];
            else
                empireStats[i] = new RaceStats(((Race)i).ToString());
        }
        EmpireStats = empireStats;
    }

    public string Summary()
    {
        StringBuilder sb = new StringBuilder();
        foreach (RaceStats race in EmpireStats)
        {
            if (race.TotalGoldCollected == Config.StartingGold || (race.BattlesLost == 0 && race.BattlesWon == 0))
                continue;
            sb.AppendLine($"Empire of {race.EmpireName}");
            sb.AppendLine($"Battles Won: {race.BattlesWon}");
            sb.AppendLine($"Battles Lost: {race.BattlesLost}");
            sb.AppendLine($"Armies Lost: {race.ArmiesLost}");
            if (Config.FactionLeaders)
                sb.AppendLine($"Times Leader Resurrected: {race.LeaderResurrections}");
            sb.AppendLine($"Gold Collected: {race.TotalGoldCollected}");
            sb.AppendLine($"Gold Spent: {race.TotalGoldSpent}");
            sb.AppendLine($"Gold Spent on Army Equipment: {race.GoldSpentOnEquipment}");
            sb.AppendLine($"Gold Spent on Buildings: {race.GoldSpentOnBuildings}");
            sb.AppendLine($"Gold Spent on Army Training: {race.GoldSpentOnTraining}");
            sb.AppendLine($"Gold Spent on Army Maintenance: {race.GoldSpentOnMaintainingArmies}");
            sb.AppendLine($"Soldiers Recruited: {race.SoldiersRecruited}");
            sb.AppendLine($"Soldiers Lost: {race.SoldiersLost}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void BattleResolution(int winner, int loser)
    {
        if (winner >= State.World.MainEmpires.Count || loser >= State.World.MainEmpires.Count)
            return;
        EmpireStats[winner].BattlesWon++;
        EmpireStats[loser].BattlesLost++;
    }

    public void LostArmy(int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].ArmiesLost++;
    }

    public void ResurrectedLeader(int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].LeaderResurrections++;
    }

    public void CollectedGold(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].TotalGoldCollected += amount;
    }

    public void SpentGold(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].TotalGoldSpent += amount;
    }

    public void SpentGoldOnArmyEquipment(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].GoldSpentOnEquipment += amount;
    }

    public void SpentGoldOnBuildings(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].GoldSpentOnBuildings += amount;
    }

    public void SpentGoldOnArmyTraining(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].GoldSpentOnTraining += amount;
    }

    public void SpentGoldOnArmyMaintenance(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].TotalGoldSpent += amount;
        EmpireStats[side].GoldSpentOnMaintainingArmies += amount;
    }

    public void SoldiersRecruited(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].SoldiersRecruited += amount;
    }

    public void SoldiersLost(int amount, int side)
    {
        if (side >= State.World.MainEmpires.Count)
            return;
        EmpireStats[side].SoldiersLost += amount;
    }




}
