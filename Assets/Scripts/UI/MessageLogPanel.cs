using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MessageLogPanel : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Button IncreaseSize;
    public Button DecreaseSize;

    public Button Restore;

    public Toggle ShowOdds;
    public Toggle ShowHealing;
    public Toggle ShowSpells;
    public Toggle ShowMisses;
    public Toggle ShowInformational;
    public Toggle ShowWeaponCombat;
    public Toggle ShowFluff;

    RectTransform rect;

    int size = 1;

    bool initialized = false;

    public void SetBase()
    {
        initialized = false;
        TacticalUtilities.Log.ShowWeaponCombat = PlayerPrefs.GetInt("LogShowWeaponCombat", 1) == 1;
        TacticalUtilities.Log.ShowOdds = PlayerPrefs.GetInt("LogShowOdds", 0) == 1;
        TacticalUtilities.Log.ShowHealing = PlayerPrefs.GetInt("LogShowHealing", 1) == 1;
        TacticalUtilities.Log.ShowSpells = PlayerPrefs.GetInt("LogShowSpells", 1) == 1;
        TacticalUtilities.Log.ShowMisses = PlayerPrefs.GetInt("LogShowMisses", 1) == 1;
        TacticalUtilities.Log.ShowInformational = PlayerPrefs.GetInt("LogShowInformational", 1) == 1;
        TacticalUtilities.Log.ShowPureFluff = PlayerPrefs.GetInt("LogShowPureFluff", 1) == 1;
        ShowWeaponCombat.isOn = TacticalUtilities.Log.ShowWeaponCombat;
        ShowOdds.isOn = TacticalUtilities.Log.ShowOdds;
        ShowHealing.isOn = TacticalUtilities.Log.ShowHealing;
        ShowSpells.isOn = TacticalUtilities.Log.ShowSpells;
        ShowMisses.isOn = TacticalUtilities.Log.ShowMisses;
        ShowInformational.isOn = TacticalUtilities.Log.ShowInformational;
        ShowFluff.isOn = TacticalUtilities.Log.ShowPureFluff;
        initialized = true;
    }

    public void Refresh()
    {
        if (initialized == false)
            return;
        TacticalUtilities.Log.ShowWeaponCombat = ShowWeaponCombat.isOn;
        TacticalUtilities.Log.ShowOdds = ShowOdds.isOn;
        TacticalUtilities.Log.ShowHealing = ShowHealing.isOn;
        TacticalUtilities.Log.ShowSpells = ShowSpells.isOn;
        TacticalUtilities.Log.ShowMisses = ShowMisses.isOn;
        TacticalUtilities.Log.ShowInformational = ShowInformational.isOn;
        TacticalUtilities.Log.ShowPureFluff = ShowFluff.isOn;
        PlayerPrefs.SetInt("LogShowWeaponCombat", ShowWeaponCombat.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowOdds", ShowOdds.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowHealing", ShowHealing.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowSpells", ShowSpells.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowMisses", ShowMisses.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowInformational", ShowInformational.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowPureFluff", ShowFluff.isOn ? 1 : 0);
        PlayerPrefs.Save();
        TacticalUtilities.Log.RefreshListing();
    }


    public void Expand()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        if (size == 0)
        {
            Restore.gameObject.SetActive(false);
            gameObject.SetActive(true);
            //rect.sizeDelta = new Vector2(1920, 160);
            //rect.anchoredPosition = new Vector3(0, 118, 0);            
            size = 1;
        }
        else if (size == 1)
        {
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(.5f, .5f);
            rect.sizeDelta = new Vector2(1920, Screen.height);
            transform.position = Vector3.zero;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            size = 2;
            IncreaseSize.interactable = false;
            ShowHealing.gameObject.SetActive(true);
            ShowMisses.gameObject.SetActive(true);
            ShowInformational.gameObject.SetActive(true);
            ShowOdds.gameObject.SetActive(true);
            ShowWeaponCombat.gameObject.SetActive(true);
            ShowFluff.gameObject.SetActive(true);
            ShowSpells.gameObject.SetActive(true);
        }
    }

    public void Shrink()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        if (size == 2)
        {
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = Vector2.zero;
            rect.sizeDelta = new Vector2(1920, 160);
            rect.anchoredPosition = new Vector3(0, 98, 0);
            size = 1;
            IncreaseSize.interactable = true;
            ShowHealing.gameObject.SetActive(false);
            ShowMisses.gameObject.SetActive(false);
            ShowInformational.gameObject.SetActive(false);
            ShowOdds.gameObject.SetActive(false);
            ShowWeaponCombat.gameObject.SetActive(false);
            ShowFluff.gameObject.SetActive(false);
            ShowSpells.gameObject.SetActive(false);
        }
        else if (size == 1)
        {
            gameObject.SetActive(false);
            size = 0;
            Restore.gameObject.SetActive(true);
        }
    }

}
