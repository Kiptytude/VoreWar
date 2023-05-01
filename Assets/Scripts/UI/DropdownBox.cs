using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceDropdownBox : MonoBehaviour
{
    Action<Race> YesAction;
    Action NoAction;
    public Button Yes;
    public Button No;
    public TMP_Dropdown Dropdown;

    public void SetData(Action<Race> action, string yesText, string noText, string mainText, Action noAction = null)
    {
        YesAction = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        NoAction = noAction;
    }

    public void AddRace(Race race, int quantity, bool set)
    {
        Dropdown.options.Add(new TMP_Dropdown.OptionData($"{race} - {quantity}"));
        if (set)
        {
            Dropdown.value = Dropdown.options.Count;
        }
        Dropdown.RefreshShownValue();
    }

    public void YesClicked()
    {
        var strings = Dropdown.captionText.text.Split(' ');
        if (Enum.TryParse(strings[0], out Race race))
        {
            YesAction?.Invoke(race);
        }
        else
            Debug.LogWarning("Couldn't separate the race correctly!");

        Destroy(gameObject);
    }

    public void NoClicked()
    {
        NoAction?.Invoke();
        Destroy(gameObject);
    }

}