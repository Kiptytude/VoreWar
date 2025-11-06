using LegacyAI;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EmpireSettings : MonoBehaviour
{
    public GameObject SetEmpireTraitsPrefab;
    public Transform folder;
    EditEmpireTraitsUI[] Empires;

    public void Open()
    {
        if (Empires != null)
        {
            foreach (var item in Empires)
            {
                Destroy(item.gameObject);
            }
        }
        Empires = new EditEmpireTraitsUI[State.World.MainEmpires.Count];
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i] = Instantiate(SetEmpireTraitsPrefab, folder).GetComponent<EditEmpireTraitsUI>();
            Empires[i].Name.text = State.World.MainEmpires[i].Name.ToString();
            Empires[i].EmpTraits.text = RaceEditorPanel.TraitListToText(State.World.MainEmpires[i].EmpTraits).ToString();
            if (State.World.MainEmpires[i].KnockedOut || State.World.MainEmpires[i].Side > 600)
                Empires[i].gameObject.SetActive(false);
        }
        RelationsManager.ResetMonsterRelations();
    }

    public static Color GetLighterColor(Color color)
    {
        color.r /= .6f;
        color.g /= .6f;
        color.b /= .6f;
        return color;
    }

    public void ExitAndSave()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (State.World.MainEmpires[i].Side > 500)
                continue;
            State.World.MainEmpires[i].EmpTraits = RaceEditorPanel.TextToTraitList(Empires[i].EmpTraits.text);
            State.World.MainEmpires[i].Name = Empires[i].Name.text;
        }
        if (Config.Diplomacy == false)
            RelationsManager.ResetRelationTypes();
        State.World.RefreshTurnOrder();
        State.World.UpdateBanditLimits();
        State.GameManager.StrategyMode?.CheckIfOnlyAIPlayers();
        gameObject.SetActive(false);
        foreach (Unit unit in StrategicUtilities.GetAllUnits())
            {
                unit.ReloadTraits(); //Add those traits right away!
            }
    }

    public void ExitWithoutSaving()
    {
        gameObject.SetActive(false);
    }

}
