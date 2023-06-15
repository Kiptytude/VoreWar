using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationLoaderWindow : MonoBehaviour
{
    public Transform ActorFolder;

    public GameObject UnitDisplay;

    public Toggle ShowAll;
    public Toggle CopyName;

    internal bool EnteredFromUnitEditor = false;

    public void Open(bool inUnitEditor)
    {
        EnteredFromUnitEditor = inUnitEditor;
        PopulateGrid();
        gameObject.SetActive(true);

    }

    public void PopulateGrid()
    {
        List<CustomizerData> customs;
        if (EnteredFromUnitEditor)
            customs = CustomizationDataStorer.GetCompatibleCustomizations(State.GameManager.UnitEditor.UnitEditor.Unit.Race, State.GameManager.UnitEditor.UnitEditor.Unit.Type, ShowAll.isOn);
        else
            customs = CustomizationDataStorer.GetCompatibleCustomizations(State.GameManager.Recruit_Mode.Customizer.Unit.Race, State.GameManager.Recruit_Mode.Customizer.Unit.Type, ShowAll.isOn);
        int children = ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.transform.GetChild(i).gameObject);
        }
        foreach (CustomizerData customizerData in customs)
        {
            GameObject obj = Instantiate(UnitDisplay, ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Unit tempUnit = new Unit(1, customizerData.Race, 0, false);
            if (customizerData.Type == UnitType.Leader)
                tempUnit.Type = UnitType.Leader;
            customizerData.CopyToUnit(tempUnit, true);
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), tempUnit);
            sprite.UpdateSprites(actor);
            sprite.Name.text = customizerData.Name;
            var ucd = obj.GetComponent<UnitCustomizerDisplayPanel>();
            ucd.DeleteButton.onClick.AddListener(() => CustomizationDataStorer.Remove(customizerData));
            ucd.DeleteButton.onClick.AddListener(() => PopulateGrid());
            if (EnteredFromUnitEditor)
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(customizerData, State.GameManager.UnitEditor.UnitEditor.Unit));
            else
                ucd.CopyFromButton.onClick.AddListener(() => CopyToUnit(customizerData, State.GameManager.Recruit_Mode.Customizer.Unit));
        }

    }

    void CopyToUnit(CustomizerData data, Unit unit)
    {
        data.CopyToUnit(unit, CopyName.isOn);
        if (EnteredFromUnitEditor)
        {
            State.GameManager.UnitEditor.UnitEditor.RefreshGenderSelector();
            State.GameManager.UnitEditor.UnitEditor.Unit.ReloadTraits();
            State.GameManager.UnitEditor.UnitEditor.Unit.InitializeTraits();
            State.GameManager.UnitEditor.UnitEditor.RefreshView();

        }
        else
        {
            State.GameManager.Recruit_Mode.Customizer.RefreshGenderSelector();
            State.GameManager.Recruit_Mode.Customizer.Unit.ReloadTraits();
            State.GameManager.Recruit_Mode.Customizer.Unit.InitializeTraits();
            State.GameManager.Recruit_Mode.Customizer.RefreshView();
        }

        CloseThis();
    }



    public void CloseThis()
    {
        int children = ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}

