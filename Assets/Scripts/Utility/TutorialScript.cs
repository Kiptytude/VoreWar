using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TutorialScript
{

    List<Actor_Unit> tacticalUnits;

    string message;
    bool warned = false;
    internal int step = -1;

    internal void InitializeTactical(List<Actor_Unit> actors)
    {
        tacticalUnits = actors;
        if (tacticalUnits.Count != 5)
        {
            State.GameManager.CreateMessageBox("Seems as though the tutorial save was replaced with a different save, exiting tutorial mode to avoid exceptions");
            State.TutorialMode = false;
            return;
        }
        Config.World.ClothedFraction = 1;
        var allUnits = StrategicUtilities.GetAllUnits();
        foreach (Unit unit in allUnits)
        {
            var race = Races.GetRace(unit);
            race.RandomCustom(unit);
        }
        State.GameManager.TacticalMode.AttackerName = "Cats";
        State.GameManager.TacticalMode.DefenderName = "Imps";

        foreach (Actor_Unit actor in actors)
        {
            actor.Unit.ClearAllTraits();
            actor.PredatorComponent = new PredatorComponent(actor, actor.Unit);
        }



        UpdateStep();
        tacticalUnits[0].Movement = 9999;
        tacticalUnits[1].Movement = 0;
        tacticalUnits[2].Movement = 0;
        for (int i = 0; i < tacticalUnits.Count(); i++)
        {
            tacticalUnits[i].AnimationController = new AnimationController();
        }
        tacticalUnits[0].Unit.SetDefaultBreastSize(2);
        tacticalUnits[0].Unit.DickSize = -1;
        State.GameManager.TacticalMode.TacticalStats.SetInitialUnits(3, 1, 0, 0, 9);
        State.World.MainEmpires[(int)Race.Imps].MaxGarrisonSize = 4;
        State.World.MainEmpires[(int)Race.Cats].Armies[0].SetEmpire(State.World.MainEmpires[(int)Race.Cats]);
        State.World.MainEmpires[(int)Race.Cats].AddGold(1000);
        State.World.MainEmpires[(int)Race.Imps].Armies[0].SetEmpire(State.World.MainEmpires[(int)Race.Imps]);
        State.World.Villages[0].UpdateNetBoosts();
        State.World.Villages[1].UpdateNetBoosts();
        State.World.Villages[0].AddPopulation(60);
        State.World.Villages[1].AddPopulation(60);
        State.World.Villages[1].TutorialWeapons();
        State.World.MonsterEmpires = new MonsterEmpire[1];
        State.World.MonsterEmpires[0] = new MonsterEmpire(new Empire.ConstructionArgs(100, Color.white, Color.white, 9, StrategyAIType.Monster, TacticalAIType.Full, 999, 8, 0));
    }

    internal void CheckStatus()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            step--;
            UpdateStep();
        }

        try
        {
            switch (step)
            {
                case 0:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }

                    if (tacticalUnits[0].Position.GetNumberOfMovesDistance(tacticalUnits[3].Position) == 1)
                        UpdateStep();
                    break;
                case 1:
                    if (State.GameManager.TacticalMode.ActionMode == 2 || State.GameManager.TacticalMode.ActionMode == 3)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }
                    if (tacticalUnits[0].Movement == 0)
                        UpdateStep();
                    break;
                case 2:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }
                    if (tacticalUnits[1].Position.GetNumberOfMovesDistance(tacticalUnits[3].Position) > 1 && tacticalUnits[1].Position.GetNumberOfMovesDistance(tacticalUnits[3].Position) < 6)
                        UpdateStep();
                    break;
                case 3:
                    if (State.GameManager.TacticalMode.ActionMode == 1 || State.GameManager.TacticalMode.ActionMode == 3)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }
                    if (tacticalUnits[1].Movement == 0)
                        UpdateStep();
                    break;
                case 4:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }
                    if (tacticalUnits[2].Position.GetNumberOfMovesDistance(tacticalUnits[3].Position) == 1)
                        UpdateStep();
                    break;
                case 5:
                    if (State.GameManager.TacticalMode.ActionMode == 1 || State.GameManager.TacticalMode.ActionMode == 2)
                        State.GameManager.TacticalMode.ActionMode = 0;
                    if (tacticalUnits.Count < 3)
                    {
                        step = 5;
                        UpdateStep();
                    }
                    if (tacticalUnits[2].Movement == 0)
                        UpdateStep();
                    break;
                case 6:
                    if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
                        UpdateStep();
                    break;
                case 7:
                    if (State.GameManager.CurrentScene == State.GameManager.Recruit_Mode)
                        UpdateStep();
                    break;
                case 8:
                    if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
                        UpdateStep();
                    break;
                case 9:
                    if (State.World.MainEmpires[0].Armies[0].InVillageIndex > -1)
                        UpdateStep();
                    break;
                case 10:
                    if (State.GameManager.CurrentScene == State.GameManager.Recruit_Mode)
                        UpdateStep();
                    break;
                case 11:
                    if (State.World.MainEmpires[0].Armies[0].Units.Count() >= 10)
                        UpdateStep();
                    break;
                case 12:
                    if (State.World.MainEmpires[0].Armies[0].Units.Count > 0 && State.World.MainEmpires[0].Armies[0].Units.Where(s => s.GetBestMelee() != State.World.ItemRepository.Claws || s.GetBestRanged() != null).Count() == State.World.MainEmpires[0].Armies[0].Units.Count())
                        UpdateStep();
                    break;
                case 13:
                    if (State.World.Villages[0].Weapons.Count >= 4)
                        UpdateStep();
                    break;
            }
        }
        catch
        {
            if (warned == false)
            {
                warned = true;
                State.GameManager.CreateMessageBox("Something has gone wrong, let the developer know that there was an error in step " + step + " of the tutorial and what you were doing that may have caused it, if known");
            }            
        }
    }





    void UpdateStep()
    {
        step++;
        switch (step)
        {
            case 0:
                message = "Looks like we're under attack, press N to select a unit, then right click next to the enemy unit (with the red border around it) (You can press H to repeat the current message at any point during the tutorial)";
                break;
            case 1:
                message = "Now press 1 or click the melee button, then left click on the enemy unit to attack";
                break;
            case 2:
                message = "Good, now press N to select the next unit (it is a ranged unit), and move it to anywhere within 5 tiles of the enemy unit, but not directly bordering it";
                tacticalUnits[1].Movement = 9999;
                break;
            case 3:
                message = "Now press 2 or click the ranged button, then left click on the enemy unit to attack";
                break;
            case 4:
                message = "Now that we've softened it up, it's time to eat it. Press N to select the next unit, and move it right next to the enemy unit.  An alternate way of moving is to press M, then left click, which shows you the movement path";
                tacticalUnits[2].Movement = 9999;
                tacticalUnits[2].Unit.ModifyStat(Stat.Voracity, 400);
                tacticalUnits[3].SubtractHealth(tacticalUnits[3].Unit.Health - 1);
                break;
            case 5:
                message = "Now press 3 or click the vore button, then left click on the enemy unit to eat it";
                break;
            case 6:
                message = "Now, depending on your settings, press end turn and the battle should finish automatically";
                break;
            case 7:
                message = "One of your units has a level up, so select your army and click on the army info button at the bottom";
                var allUnits = StrategicUtilities.GetAllUnits();
                foreach (Unit unit in allUnits)
                {
                    unit.ReloadTraits();
                    unit.InitializeTraits();
                }
                State.World.MainEmpires[0].Armies[0].RemainingMP = 999;
                break;
            case 8:
                message = "Select the unit with the orange text, and either level up manually, or let it auto-level your unit with the auto-level button.  Then, when you're ready, exit back to the strategy screen";
                break;
            case 9:
                message = "Move your army to the friendly city, by either pressing m and left clicking on the city, or just right clicking on the city";
                break;
            case 10:
                message = "Now left click on your army again, or click the army info button at the bottom to enter the city view";
                break;
            case 11:
                message = "We need some more units to take on this imp threat, so click recruit unit until you have at least 10 units";
                break;
            case 12:
                message = "Now, you can either hit the shop manually button, or use the auto-buy button to quickly outfit your troops.  Buy weapons for all of your units now";
                break;
            case 13:
                message = "It is a good idea to buy some weapons to enable the villagers to fight back if this village gets attacked.   Click the buy weapons button near the top left and buy at least 4 weapons.";
                break;
            case 14:
                message = "Return to the world view, then right click your army on the enemy village, and battle will start, you can either fight that battle, or quit, because this is basically the end of the tutorial";
                break;
        }
        State.GameManager.CreateMessageBox(message);
    }
}
