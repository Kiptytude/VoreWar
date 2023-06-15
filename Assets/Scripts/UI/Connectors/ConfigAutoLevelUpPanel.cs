using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigAutoLevelUpPanel : MonoBehaviour
{
    public TMP_Dropdown Dropdown;
    public TMP_Dropdown CustomDropdown;
    public Slider[] Sliders;
    public Toggle AutoSpend;
    internal bool Custom;
    bool deniedChanges = false;
    Unit Unit;

    internal void Open(Unit unit)
    {
        if (Dropdown.options.Count == 0)
        {
            foreach (AIClass type in (AIClass[])Enum.GetValues(typeof(AIClass)))
            {
                Dropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }
        }

        CustomDropdown.ClearOptions();
        {
            CustomDropdown.captionText.text = "Saved Custom Configs";
            CustomDropdown.options.Add(new TMP_Dropdown.OptionData("None"));
            foreach (string str in CustomAutoLevel.GetAllNames())
            {
                CustomDropdown.options.Add(new TMP_Dropdown.OptionData(str));
            }
        }
        CustomDropdown.value = 0;
        Unit = unit;
        Dropdown.value = (int)unit.AIClass;
        Sliders[(int)Stat.Voracity].gameObject.SetActive(unit.Predator);
        Sliders[(int)Stat.Stomach].gameObject.SetActive(unit.Predator);
        Sliders[(int)Stat.Leadership].gameObject.SetActive(unit.Type == UnitType.Leader);
        AutoSpend.isOn = unit.AutoLeveling;
        ResetSliders();
    }



    public void ResetSliders()
    {
        if ((AIClass)Dropdown.value == AIClass.Custom)
        {
            Custom = true;
            if (Unit.StatWeights != null)
            {
                for (int i = 0; i < (int)Stat.None; i++)
                {
                    Sliders[i].value = Unit.StatWeights.Weight[i];
                }
            }
            return;
        }
        else
        {
            Unit.StatWeights = null;
        }
        deniedChanges = true;
        Custom = false;
        Unit.AIClass = (AIClass)Dropdown.value;
        float[] weight = new float[(int)Stat.None];

        switch (Unit.AIClass)
        {
            case AIClass.Default:
                if ((Unit.BestSuitedForRanged() && Unit.FixedGear == false) || Unit.GetBestRanged() != null)
                {
                    weight[(int)Stat.Strength] = 0;
                    weight[(int)Stat.Dexterity] = 3;
                    weight[(int)Stat.Voracity] = 2;
                    weight[(int)Stat.Agility] = 2;
                    weight[(int)Stat.Will] = 1.25f;
                    weight[(int)Stat.Mind] = 0f;
                    weight[(int)Stat.Endurance] = 1.5f;
                    weight[(int)Stat.Stomach] = 2;
                    weight[(int)Stat.Leadership] = 0;
                }
                else
                {
                    weight[(int)Stat.Strength] = 3;
                    weight[(int)Stat.Dexterity] = 0;
                    weight[(int)Stat.Voracity] = 3;
                    weight[(int)Stat.Agility] = 3;
                    weight[(int)Stat.Will] = 2;
                    weight[(int)Stat.Mind] = 0;
                    weight[(int)Stat.Endurance] = 2;
                    weight[(int)Stat.Stomach] = 2;
                    weight[(int)Stat.Leadership] = 0;
                }
                break;
            case AIClass.Melee:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 2;
                weight[(int)Stat.Agility] = 3;
                weight[(int)Stat.Will] = 2;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.5f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MeleeVore:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 3;
                weight[(int)Stat.Agility] = 3;
                weight[(int)Stat.Will] = 2;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 2.5f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.Ranged:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 1.25f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 1.25f;
                weight[(int)Stat.Mind] = 0f;
                weight[(int)Stat.Endurance] = 1.5f;
                weight[(int)Stat.Stomach] = .75f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.RangedVore:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 2.25f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 1.25f;
                weight[(int)Stat.Mind] = 0f;
                weight[(int)Stat.Endurance] = 1.5f;
                weight[(int)Stat.Stomach] = 2;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.PureVore:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 4;
                weight[(int)Stat.Agility] = 1;
                weight[(int)Stat.Will] = 1;
                weight[(int)Stat.Mind] = 0;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 4;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MagicMelee:
                weight[(int)Stat.Strength] = 3;
                weight[(int)Stat.Dexterity] = 0;
                weight[(int)Stat.Voracity] = 2;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 2.25f;
                weight[(int)Stat.Mind] = 2.75f;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.25f;
                weight[(int)Stat.Leadership] = 0;
                break;
            case AIClass.MagicRanged:
                weight[(int)Stat.Strength] = 0;
                weight[(int)Stat.Dexterity] = 3;
                weight[(int)Stat.Voracity] = 1.5f;
                weight[(int)Stat.Agility] = 2;
                weight[(int)Stat.Will] = 2.25f;
                weight[(int)Stat.Mind] = 2.75f;
                weight[(int)Stat.Endurance] = 2;
                weight[(int)Stat.Stomach] = 1.25f;
                weight[(int)Stat.Leadership] = 0;
                break;
        }

        if (Unit.Type == UnitType.Leader)
        {
            weight[(int)Stat.Leadership] = 4;
        }

        for (int i = 0; i < Sliders.Length; i++)
        {
            Sliders[i].value = weight[i];
        }

        deniedChanges = false;
    }

    public void PickCustomClass()
    {
        if (CustomDropdown.value != 0)
        {
            var obj = CustomAutoLevel.GetByName(CustomDropdown.captionText.text);
            if (obj != null)
            {
                for (int i = 0; i < Sliders.Length; i++)
                {
                    Sliders[i].value = obj.Weights.Weight[i];
                }
                Dropdown.value = (int)AIClass.Custom;
            }

        }
    }

    public void SaveAsClass()
    {
        var obj = Instantiate(State.GameManager.InputBoxPrefab).GetComponent<InputBox>();
        var tempObj = new StoredClassWeight();
        tempObj.Weights = new StatWeights();
        tempObj.Weights.Weight = new float[(int)Stat.None];
        for (int i = 0; i < Sliders.Length; i++)
        {
            tempObj.Weights.Weight[i] = Sliders[i].value;
        }
        obj.SetData((string s) => { tempObj.Name = s.ToString(); CustomAutoLevel.Add(tempObj); }, "Save", "Cancel", "Enter a name to save this configuration as", 16);
    }

    public void DeleteCustomClass()
    {
        var obj = CustomAutoLevel.GetByName(CustomDropdown.captionText.text);
        if (obj != null)
        {
            CustomAutoLevel.Remove(obj);
        }

    }

    public void UpdateSliderText()
    {

        for (int i = 0; i < Sliders.Length; i++)
        {
            Sliders[i].GetComponentInChildren<Text>().text = $"{(Stat)i} - {Math.Round(Sliders[i].value, 2)} ";
        }
        if (deniedChanges == false)
        {
            Custom = true;
            Unit.AIClass = AIClass.Custom;
            Dropdown.value = (int)Unit.AIClass;
        }
    }

    public void DisplayHelp()
    {
        State.GameManager.CreateMessageBox("Will try to level up to maintain these ratios between stats, since stats avaiable are random it won't maintain it exactly, but with 3 strength and 1 will, it would try to keep the strength stat at 3 times the will stat, etc.");
    }



}
