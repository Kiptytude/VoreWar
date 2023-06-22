using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    public Toggle UnitEditorDisplayed;
    public Toggle CompleteTooltips;
    public Toggle ViewHostileArmies;
    public Toggle AddUnitButton;
    public Toggle PopulationCheat;

    public TMP_Dropdown EmpireDropdown;

    public GameObject StrategicBlocker;
    public GameObject TacticalBlocker;

    public TMP_InputField FirstRaceRelation;
    public TMP_InputField SecondRaceRelation;

    public TMP_Dropdown EmpireReplaced;
    public TMP_Dropdown ReplacementRace;

    public TMP_Dropdown FirstRaceDropdown;
    public TMP_Dropdown SecondRaceDropdown;

    Relationship Relation;
    Relationship CounterRelation;


    public void Open()
    {
        GetValues();
        StrategicBlocker.gameObject.SetActive(State.GameManager.CurrentScene != State.GameManager.StrategyMode);
        TacticalBlocker.gameObject.SetActive(State.GameManager.CurrentScene != State.GameManager.TacticalMode);
        FirstRaceDropdown.ClearOptions();
        SecondRaceDropdown.ClearOptions();
        EmpireReplaced.ClearOptions();
        ReplacementRace.ClearOptions();
        if (State.World?.MainEmpires != null)
        {
            foreach (Empire empire in State.World.MainEmpires)
            {
                if (empire.Side >= 700)
                    continue;
                FirstRaceDropdown.options.Add(new TMP_Dropdown.OptionData(empire.Name));
                SecondRaceDropdown.options.Add(new TMP_Dropdown.OptionData(empire.Name));
                EmpireReplaced.options.Add(new TMP_Dropdown.OptionData(empire.Name));
            }

            if (State.World.MonsterEmpires != null)
            {
                foreach (Empire empire in State.World.MonsterEmpires)
                {
                    if (empire.Side >= 700)
                        continue;
                    EmpireReplaced.options.Add(new TMP_Dropdown.OptionData(empire.Name));
                }
            }


            if (Config.GoblinCaravans)
            {
                FirstRaceDropdown.options.Add(new TMP_Dropdown.OptionData("Goblins"));
                SecondRaceDropdown.options.Add(new TMP_Dropdown.OptionData("Goblins"));
            }
            if (State.World.ActingEmpire != null)
            {
                EmpireDropdown.value = State.World.MainEmpires.IndexOf(State.World.ActingEmpire);
                EmpireDropdown.captionText.text = State.World.ActingEmpire.Name;
            }
            FirstRaceDropdown.RefreshShownValue();
            SecondRaceDropdown.RefreshShownValue();
            EmpireReplaced.RefreshShownValue();
            ReplacementRace.RefreshShownValue();

            foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
            {
                ReplacementRace.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }
        }


    }

    public void CloseAndSave()
    {
        SetNewValues();
        gameObject.SetActive(false);
    }

    void GetValues()
    {
        UnitEditorDisplayed.isOn = PlayerPrefs.GetInt("UnitEditorDisplayed", 0) == 1;
        CompleteTooltips.isOn = PlayerPrefs.GetInt("CompleteTooltips", 0) == 1;
        ViewHostileArmies.isOn = PlayerPrefs.GetInt("ViewHostileArmies", 0) == 1;
        AddUnitButton.isOn = PlayerPrefs.GetInt("AddUnitButton", 0) == 1;
        PopulationCheat.isOn = PlayerPrefs.GetInt("PopulationCheat", 0) == 1;
        EmpireDropdown.ClearOptions();
        if (State.World.MainEmpires != null)
        {
            foreach (Empire empire in State.World.MainEmpires.Where(s => s.Side < 100))
            {
                EmpireDropdown.options.Add(new TMP_Dropdown.OptionData(empire.Name));
            }
            if (State.World.ActingEmpire != null)
            {
                EmpireDropdown.value = State.World.MainEmpires.IndexOf(State.World.ActingEmpire);
                EmpireDropdown.captionText.text = State.World.ActingEmpire.Name;
            }

        }

        UpdateDisplayedRelation();

        LoadFromStored();
    }

    public void CheckReplaceRace()
    {
        var box = State.GameManager.CreateDialogBox();
        if (State.World.MainEmpires.Where(s => s.Name == EmpireReplaced.captionText.text).FirstOrDefault() != null)
            box.SetData(ReplaceRace, "Replace", "Cancel", $"Are you sure you want to replace all villages belonging to the empire {EmpireReplaced.captionText.text} with {ReplacementRace.captionText.text}? " +
            $"This doesn't change the race of any existing units except the leader to avoid potential issues.");
        else
            box.SetData(ReplaceRace, "Replace", "Cancel", $"Are you sure you want to replace this monster race {EmpireReplaced.captionText.text} with {ReplacementRace.captionText.text}? " +
            $"This doesn't change the race of any existing units, and the monsters will still use the same spawners, settings, and strategic graphics.");
    }

    void ReplaceRace()
    {
        Empire emp = State.World.MainEmpires.Where(s => s.Name == EmpireReplaced.captionText.text).FirstOrDefault();
        if (emp == null)
        {
            Empire monsterEmp = State.World.MonsterEmpires.Where(s => s.Name == EmpireReplaced.captionText.text).FirstOrDefault();
            if (Enum.TryParse(ReplacementRace.captionText.text, out Race monRace))
            {
                monsterEmp.ReplacedRace = monRace;
            }

            return;
        }

        if (Enum.TryParse(ReplacementRace.captionText.text, out Race race))
        {
            foreach (Village village in State.World.Villages.Where(s => s.Side == emp.Side))
            {
                village.Race = race;
                village.OriginalRace = race;
                village.VillagePopulation.ConvertToSingleRace();
                if (Config.MultiRaceVillages)
                    village.VillagePopulation.ConvertToMultiRace();
            }
            emp.Name = race.ToString();
            emp.ReplacedRace = race;
            if (emp.Leader != null)
            {
                emp.Leader.Race = race;
                emp.Leader.RandomizeNameAndGender(race, Races.GetRace(emp.Leader));
            }
        }


    }

    void SetNewValues()
    {
        PlayerPrefs.SetInt("UnitEditorDisplayed", UnitEditorDisplayed.isOn ? 1 : 0);
        PlayerPrefs.SetInt("CompleteTooltips", CompleteTooltips.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ViewHostileArmies", ViewHostileArmies.isOn ? 1 : 0);
        PlayerPrefs.SetInt("AddUnitButton", AddUnitButton.isOn ? 1 : 0);
        PlayerPrefs.SetInt("PopulationCheat", PopulationCheat.isOn ? 1 : 0);
        LoadFromStored();
        PlayerPrefs.Save();
    }

    public void LoadFromStored()
    {
        Config.CheatUnitEditorEnabled = PlayerPrefs.GetInt("UnitEditorDisplayed", 0) == 1;
        Config.CheatExtraStrategicInfo = PlayerPrefs.GetInt("CompleteTooltips", 0) == 1;
        Config.CheatViewHostileArmies = PlayerPrefs.GetInt("ViewHostileArmies", 0) == 1;
        Config.CheatAddUnitButton = PlayerPrefs.GetInt("AddUnitButton", 0) == 1;
        Config.CheatPopulation = PlayerPrefs.GetInt("PopulationCheat", 0) == 1;
    }



    public void TacticalAttackerWin()
    {
        //State.GameManager.TacticalMode.SurrenderAll(false); //Done this way so that it isn't based on active side
        State.GameManager.TacticalMode.KillAll(false);
        State.GameManager.TacticalMode.ProcessSkip(false, false);
        CloseAndSave();
        State.GameManager.Menu.CloseMenu();
    }

    public void TacticalDefenderWin()
    {
        //State.GameManager.TacticalMode.SurrenderAll(true);
        State.GameManager.TacticalMode.KillAll(true);
        State.GameManager.TacticalMode.ProcessSkip(false, false);
        CloseAndSave();
        State.GameManager.Menu.CloseMenu();
    }

    public void TacticalUnsurrenderAllAttackers()
    {
        if (TacticalUtilities.Units == null)
            return;
        foreach (Actor_Unit actor in TacticalUtilities.Units)
        {
            if (State.GameManager.TacticalMode.IsDefender(actor) == false)
                actor.Surrendered = false;
        }
    }

    public void TacticalUnsurrenderAllDefenders()
    {
        if (TacticalUtilities.Units == null)
            return;
        foreach (Actor_Unit actor in TacticalUtilities.Units)
        {
            if (State.GameManager.TacticalMode.IsDefender(actor))
                actor.Surrendered = false;
        }
    }

    public void TacticalTakeControlOfAttacker()
    {
        State.GameManager.TacticalMode.DisableAttackerAI();
    }

    public void TacticalTakeControlOfDefender()
    {
        State.GameManager.TacticalMode.DisableDefenderAI();
    }

    public InputField MoneyAmount;
    public void StrategicAddMoney()
    {
        if (int.TryParse(MoneyAmount.text, out int money))
            State.World.MainEmpires[EmpireDropdown.value].AddGold(money);
        else
            State.GameManager.CreateMessageBox("Invalid amount");
    }

    public void DegradeRelations()
    {
        if (State.World?.MainEmpires == null)
        {
            State.GameManager.CreateMessageBox("Couldn't get empires, something is wrong with the world state");
            return;
        }
        foreach (Empire firstEmpire in State.World.MainEmpires)
        {
            if (firstEmpire.Side >= 700)
                continue;
            foreach (Empire secondEmpire in State.World.MainEmpires)
            {
                if (secondEmpire.Side >= 700)
                    continue;
                if (firstEmpire == secondEmpire)
                    continue;
                RelationsManager.MakeLike(firstEmpire, secondEmpire); //To standardize relations
                RelationsManager.MakeHate(firstEmpire, secondEmpire);
            }

        }
    }

    public void UpdateDisplayedRelation()
    {
        if (State.World.MainEmpires == null)
            return;
        if (State.World.MainEmpires.Count < FirstRaceDropdown.value || State.World.MainEmpires.Count < SecondRaceDropdown.value)
        {
            Relation = null;
            CounterRelation = null;
            FirstRaceRelation.text = (Math.Round(Relation.Attitude, 5)*100).ToString();
            SecondRaceRelation.text = (Math.Round(CounterRelation.Attitude, 5)*100).ToString();
            return;
        }

        int firstSide;
        if (FirstRaceDropdown.value < State.World.MainEmpires.Count - 2)
            firstSide = State.World.MainEmpires[FirstRaceDropdown.value].Side;
        else
            firstSide = State.World.MonsterEmpires[7].Side;
        int secondSide;
        if (SecondRaceDropdown.value < State.World.MainEmpires.Count - 2)
            secondSide = State.World.MainEmpires[SecondRaceDropdown.value].Side;
        else
            secondSide = State.World.MonsterEmpires[7].Side;
        if (firstSide != secondSide)
        {
            Relation = RelationsManager.GetRelation(firstSide, secondSide);
            CounterRelation = RelationsManager.GetRelation(secondSide, firstSide);
            FirstRaceRelation.text = (Math.Round(Relation.Attitude, 5) * 100).ToString();
            SecondRaceRelation.text = (Math.Round(CounterRelation.Attitude, 5) * 100).ToString();
        }
        else
        {
            Relation = null;
            CounterRelation = null;
            FirstRaceRelation.text = "";
            SecondRaceRelation.text = "";
        }
    }

    public void UpdateRelations()
    {
        if (Relation != null && CounterRelation != null)
        {
            if (int.TryParse(FirstRaceRelation.text, out int first))
                Relation.Attitude = first/100;
            if (int.TryParse(SecondRaceRelation.text, out int second))
                CounterRelation.Attitude = second/100;
        }
    }


    //internal void ChangeToolTip(int value)
    //{
    //    TooltipText.text = DefaultTooltips.Tooltip(value);
    //}


}
