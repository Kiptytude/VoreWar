using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UniformLoaderWindow : MonoBehaviour
{
    public Transform ActorFolder;

    public GameObject UnitDisplay;

    public Slider UniformFraction;

    internal bool EnteredFromUnitEditor = false;

    Race ActiveRace;

    public void Open(bool inUnitEditor)
    {
        EnteredFromUnitEditor = inUnitEditor;
        PopulateGrid();
        gameObject.SetActive(true);
        Unit tempUnit;
        if (EnteredFromUnitEditor)
            tempUnit = State.GameManager.UnitEditor.UnitEditor.Unit;
        else
            tempUnit = State.GameManager.Recruit_Mode.Customizer.Unit;
        ActiveRace = tempUnit.Race;
        UniformFraction.value = UniformDataStorer.GetUniformOdds(ActiveRace);
    }


    public void PopulateGrid()
    {
        List<UniformData> customs;
        if (EnteredFromUnitEditor)
            customs = UniformDataStorer.GetCompatibleCustomizations(State.GameManager.UnitEditor.UnitEditor.Unit);
        else
            customs = UniformDataStorer.GetCompatibleCustomizations(State.GameManager.Recruit_Mode.Customizer.Unit);
        int children = ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.transform.GetChild(i).gameObject);
        }
        if (customs == null)
            return;
        foreach (UniformData uniformData in customs)
        {
            GameObject obj = Instantiate(UnitDisplay, ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Unit tempUnit;
            if (EnteredFromUnitEditor)
                tempUnit = State.GameManager.UnitEditor.UnitEditor.Unit.Clone();
            else
                tempUnit = State.GameManager.Recruit_Mode.Customizer.Unit.Clone();
            
            uniformData.CopyToUnit(tempUnit);
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), tempUnit);
            sprite.UpdateSprites(actor);
            sprite.Name.text = uniformData.Name;
            var ucd = obj.GetComponent<UnitCustomizerDisplayPanel>();
            ucd.DeleteButton.onClick.AddListener(() => UniformDataStorer.Remove(uniformData));
            ucd.DeleteButton.onClick.AddListener(() => PopulateGrid());
            if (EnteredFromUnitEditor)
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(uniformData, State.GameManager.UnitEditor.UnitEditor.Unit));
            else
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(uniformData, State.GameManager.Recruit_Mode.Customizer.Unit));
        }

        if (EnteredFromUnitEditor)
            customs = UniformDataStorer.GetIncompatibleCustomizations(State.GameManager.UnitEditor.UnitEditor.Unit);
        else
            customs = UniformDataStorer.GetIncompatibleCustomizations(State.GameManager.Recruit_Mode.Customizer.Unit);
        foreach (UniformData uniformData in customs)
        {
            GameObject obj = Instantiate(UnitDisplay, ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Unit tempUnit;
            if (EnteredFromUnitEditor)
                tempUnit = State.GameManager.UnitEditor.UnitEditor.Unit;
            else
                tempUnit = State.GameManager.Recruit_Mode.Customizer.Unit;
            string rejectMessage;
            if (tempUnit.Type == UnitType.Leader && uniformData.Type != UnitType.Leader)
                rejectMessage = "Not for leaders";
            else if (tempUnit.Type != UnitType.Leader && uniformData.Type == UnitType.Leader)
                rejectMessage = "Only For Leaders";
            else if (tempUnit.GetGender() == Gender.Male)
                rejectMessage = "Only for Females/Herms";
            else if (tempUnit.GetGender() != Gender.Male)
                rejectMessage = "Only for Males";
            else
                rejectMessage = "Unspecified Error";
            tempUnit = new Unit((int)uniformData.Race, uniformData.Race, 0, false, uniformData.Type);           
            uniformData.CopyToUnit(tempUnit);
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), tempUnit);
            sprite.UpdateSprites(actor);
            sprite.Name.text = uniformData.Name;
            var ucd = obj.GetComponent<UnitCustomizerDisplayPanel>();
            ucd.DeleteButton.onClick.AddListener(() => UniformDataStorer.Remove(uniformData));
            ucd.DeleteButton.onClick.AddListener(() => PopulateGrid());
            ucd.CopyFromButton.interactable = false;


            ucd.CopyFromButton.GetComponentInChildren<Text>().text = rejectMessage;
            if (EnteredFromUnitEditor)
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(uniformData, State.GameManager.UnitEditor.UnitEditor.Unit));
            else
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(uniformData, State.GameManager.Recruit_Mode.Customizer.Unit));
        }
    }

    void CopyToUnit(UniformData data, Unit unit)
    {
        data.CopyToUnit(unit);
        if (EnteredFromUnitEditor)
        {
            State.GameManager.UnitEditor.UnitEditor.RefreshView();
            State.GameManager.UnitEditor.UnitEditor.RefreshGenderSelector();

        }
        else
        {
            State.GameManager.Recruit_Mode.Customizer.RefreshView();
            State.GameManager.Recruit_Mode.Customizer.RefreshGenderSelector();
        }

        CloseThis();
    }



    public void CloseThis()
    {
        if (UniformDataStorer.GetUniformOdds(ActiveRace) != UniformFraction.value)
        {
            UniformDataStorer.SetUniformOdds(ActiveRace, UniformFraction.value);
        }
        
        
        int children = ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
        
    }
}

