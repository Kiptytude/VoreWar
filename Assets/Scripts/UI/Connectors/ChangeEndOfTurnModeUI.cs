using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChangeEndOfTurnModeUI : MonoBehaviour
{

    public TMP_Dropdown dropdown;

    public Toggle display;


    public void Open()
    {
        gameObject.SetActive(true);
        dropdown.value = PlayerPrefs.GetInt("AutoAdvance", 1);
        display.isOn = PlayerPrefs.GetInt("DisplayEndOfTurnText", 0) == 1;
        dropdown.RefreshShownValue();
    }

    public void CloseAndApply()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("AutoAdvance", dropdown.value);
        Config.AutoAdvance = (Config.AutoAdvanceType)PlayerPrefs.GetInt("AutoAdvance", 1);
        
        PlayerPrefs.SetInt("DisplayEndOfTurnText", display.isOn ? 1 : 0);
        Config.DisplayEndOfTurnText = PlayerPrefs.GetInt("DisplayEndOfTurnText", 0) == 1;
        State.GameManager.TacticalMode.UpdateEndTurnButtonText();       
    }

    public void Close()
    {
        gameObject.SetActive(false);

    }


    
}
