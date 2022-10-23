using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UniformSaver : MonoBehaviour
{
    public InputField Text;

    public Toggle IncludeHat;
    public Toggle IncludeClothingAccesory;

    bool OpenedFromEditor;

    public void Open(bool openedFromEditor)
    {
        OpenedFromEditor = openedFromEditor;
        Unit unit;
        if (OpenedFromEditor)
            unit = State.GameManager.UnitEditor.UnitEditor.Unit;
        else
            unit = State.GameManager.Recruit_Mode.Customizer.Unit;

        Text.text = unit.Name;
        var raceData = Races.GetRace(unit.Race);
        if (raceData.AllowedClothingHatTypes.Count > 0)
        {            
            IncludeHat.interactable = true;
        }
        else
        {
            IncludeHat.isOn = false;
            IncludeHat.interactable = false;
        } 
        
        if (raceData.AllowedClothingAccessoryTypes.Count > 0)
        {            
            IncludeClothingAccesory.interactable = true;
        }
        else
        {
            IncludeClothingAccesory.isOn = false;
            IncludeClothingAccesory.interactable = false;
        }
        gameObject.SetActive(true);
    }

    public void Save()
    {
        UniformData uniform = new UniformData();
        if (OpenedFromEditor)
            uniform.CopyFromUnit(State.GameManager.UnitEditor.UnitEditor.Unit, IncludeHat.isOn, IncludeClothingAccesory.isOn);
        else
            uniform.CopyFromUnit(State.GameManager.Recruit_Mode.Customizer.Unit, IncludeHat.isOn, IncludeClothingAccesory.isOn);
        uniform.Name = Text.text;
        UniformDataStorer.Add(uniform);
    }


}
