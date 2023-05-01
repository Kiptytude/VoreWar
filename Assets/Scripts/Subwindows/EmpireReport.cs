using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EmpireReport : MonoBehaviour
{
    EmpireReportItem[] Reports;

    public GameObject ReportItemPrefab;
    public Transform ReportFolder;

    public DiplomacyScreen DiplomacyScreen;

    bool pausedState = false;

    const int GoblinNum = Config.NumberOfRaces;
    const int FirstMonster = Config.NumberOfRaces + 1;

    public void Open()
    {
        pausedState = State.GameManager.StrategyMode.Paused;
        State.GameManager.StrategyMode.Paused = true;
        gameObject.SetActive(true);
        if (Reports == null)
        {
            Reports = new EmpireReportItem[Config.NumberOfRaces + World.MonsterCount + 1];
            for (int i = 0; i < Reports.Length; i++)
            {
                Reports[i] = Instantiate(ReportItemPrefab, ReportFolder).GetComponent<EmpireReportItem>();
            }
        }
        for (int i = 0; i < Config.NumberOfRaces; i++)
        {
            int side = i;
            Reports[i].Contact.onClick.RemoveAllListeners();
            Reports[i].Contact.gameObject.SetActive(State.World.ActingEmpire.Side != i);
            Reports[i].Contact.onClick.AddListener(() => DiplomacyScreen.Open(State.World.ActingEmpire, State.World.GetEmpireOfSide(side)));
        }
        if (Config.GoblinCaravans)
        {
            int side = (int)Race.Goblins;
            Reports[GoblinNum].Contact.onClick.RemoveAllListeners();
            Reports[GoblinNum].Contact.onClick.AddListener(() => DiplomacyScreen.Open(State.World.ActingEmpire, State.World.GetEmpireOfSide(side)));
        }


        Refresh();

    }

    public void Refresh()
    {
        for (int i = 0; i < Reports.Length; i++)
        {
            Reports[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < State.World.MainEmpires.Count; i++)
        {
            Empire empire = State.World.MainEmpires[i];
            Reports[i].gameObject.SetActive(!empire.KnockedOut);
            if (empire.KnockedOut)
                continue;

            Reports[i].EmpireStatus.text = $"{empire.Name}  Villages: {State.World.Villages.Where(s => s.Side == empire.Side).Count()}  Mines: {State.World.Claimables.Where(s => s.Owner == empire).Count()} Armies : {empire.Armies.Count()} ";
            if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
            {
                Reports[i].EmpireStatus.text += $"Units: {empire.GetAllUnits().Count} Gold: {empire.Gold}  Income: {empire.Income}";
            }

        }
        if (Config.GoblinCaravans)
        {
            Empire empire = State.World.GetEmpireOfRace(Race.Goblins);
            Reports[GoblinNum].gameObject.SetActive(true);
            if (empire != null)
            {
                Reports[GoblinNum].EmpireStatus.text = $"{empire.Name}  Armies : {empire.Armies.Count()} ";
                if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
                {
                    Reports[GoblinNum].EmpireStatus.text += $"Units: {empire.GetAllUnits().Count}";
                }
            }
        }


        int currentIndex = GoblinNum;
        foreach (Empire empire in State.World.MonsterEmpires)
        {
            currentIndex += 1;
            if (empire.Race == Race.Goblins)
                continue;
            SpawnerInfo spawner = Config.SpawnerInfo(empire.Race);
            if (spawner == null)
                continue;
            Reports[currentIndex].gameObject.SetActive(spawner.Enabled);
            Reports[currentIndex].Contact.gameObject.SetActive(false);

            Reports[currentIndex].EmpireStatus.text = $"{empire.Name}  Villages: {State.World.Villages.Where(s => s.Side == empire.Side).Count()} Armies : {empire.Armies.Count()} ";
            if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
            {
                Reports[currentIndex].EmpireStatus.text += $"Units: {empire.GetAllUnits().Count}";
            }

        }
    }

    public void CreateDiplomacyReport()
    {
        StringBuilder sb = new StringBuilder();
        List<string> Allies = new List<string>();
        List<string> Neutral = new List<string>();
        List<string> Enemies = new List<string>();
        List<Empire> list = State.World.MainEmpires.Where(s => s.KnockedOut == false).ToList();
        //list.Append(State.World.GetEmpireOfRace(Race.Goblins));

        foreach (Empire emp in list)
        {
            Allies.Clear();
            Neutral.Clear();
            Enemies.Clear();

            foreach (Empire emp2 in list)
            {
                if (emp == emp2)
                    continue;
                if (emp.Side >= 700 || emp2.Side >= 700)
                    continue;
                var relation = RelationsManager.GetRelation(emp.Side, emp2.Side);
                switch (relation.Type)
                {
                    case RelationState.Neutral:
                        Neutral.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Allied:
                        Allies.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Enemies:
                        Enemies.Add(emp2.Name.ToString());
                        break;
                }
            }
            string allies = Allies.Count() > 0 ? $"Allies:<color=blue> {string.Join(", ", Allies)}</color>" : "";
            string neutrals = Neutral.Count() > 0 ? $"Neutral: {string.Join(", ", Neutral)}" : "";
            string enemies;
            if (Enemies.Count > 6)
                enemies = "Enemies: <color=red>all others</color>";
            else
                enemies = Enemies.Count() > 0 ? $"Enemies: <color=red>{string.Join(", ", Enemies)}</color>" : "";

            sb.AppendLine($"{emp.Name} - {allies} {neutrals} {enemies} ");
        }
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
    }

    public void Close()
    {
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = pausedState;
    }
}
