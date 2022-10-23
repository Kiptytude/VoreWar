using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCheatsPanel : MonoBehaviour
{
    public TMP_Dropdown ArmyPicker;
    public TMP_Dropdown TraitPicker;

    public Button SwapArmyButton;
    public Button AddTraitButton;
    public Button RemoveTraitButton;

    public Button RefreshButton;

    bool init = false;

    Army Army;

    internal void Setup(Army army)
    {
        Army = army;
        if (init == false)
        {
            foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
            {
                return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
            }))
            {
                TraitPicker.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
            }
            TraitPicker.RefreshShownValue();
            SwapArmyButton.onClick.AddListener(MoveToAnotherEmpire);
            AddTraitButton.onClick.AddListener(AddTrait);
            RemoveTraitButton.onClick.AddListener(RemoveTrait);
            RefreshButton.onClick.AddListener(Refresh);
            init = true;
        }


        ArmyPicker.ClearOptions();

        foreach (Empire empire in State.World.MainEmpires.Where(s => s.KnockedOut == false))
        {
            ArmyPicker.options.Add(new TMP_Dropdown.OptionData(empire.Name));
        }
        ArmyPicker.RefreshShownValue();
    }

    void Refresh()
    {
        Army.Refresh();
    }

    void MoveToAnotherEmpire()
    {
        if (StrategicUtilities.GetVillageAt(Army.Position) != null)
        {
            State.GameManager.CreateMessageBox("Can't switch sides in villages, it generates bugs.");
            return;
        }
        if (State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text).Any() == false)
        {
            State.GameManager.CreateMessageBox("Invalid Empire, try repicking from the dropdown.");
            return;
        }
        if (Army.Units.Where( s => s.Type == UnitType.Leader).Any())
        {
            State.GameManager.CreateMessageBox("That army had a leader in it, unexpected behavior may occur when the leader dies.");
        }        
        State.GameManager.Recruit_Mode.ButtonCallback(86);
        State.GameManager.SwitchToStrategyMode();
        foreach (Empire empire in State.World.AllActiveEmpires)
        {
            if (empire.Armies.Contains(Army))
                empire.Armies.Remove(Army);
        }
        foreach (Empire empire in State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text))
        {
            Army newArmy = new Army(empire, Army.Position, empire.Side);
            newArmy.Units = Army.Units;
            empire.Armies.Add(newArmy);
            break;
        }
    }

    void AddTrait()
    {
        if (Enum.TryParse(TraitPicker.captionText.text, out Traits trait))
        {
            foreach (var unit in Army.Units)
            {
                unit.AddPermanentTrait(trait);
            }
        }        
    }

    void RemoveTrait()
    {
        if (Enum.TryParse(TraitPicker.captionText.text, out Traits trait))
        {
            foreach (var unit in Army.Units)
            {
                unit.RemoveTrait(trait);
            }
        }
    }

}
