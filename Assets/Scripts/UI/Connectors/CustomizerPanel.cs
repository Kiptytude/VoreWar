using UnityEngine;
using System.Collections;
using TMPro;


public class CustomizerPanel : MonoBehaviour
{
    public UIUnitSprite DisplayedSprite;
    public GameObject ButtonPanel;
    public GameObject ButtonPrefab;
    public CustomizationLoaderWindow CustomizationLoader;
    public TMP_Dropdown Gender;
    public TMP_InputField Nominative;
    public TMP_InputField Accusative;
    public TMP_InputField PronominalPossessive;
    public TMP_InputField PredicativePossessive;
    public TMP_InputField Reflexive;
    public TMP_Dropdown Quantification;

    public virtual void ChangeGender()
    {
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangeGender();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangeGender();
    }

    public virtual void ChangePronouns()
    {
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangePronouns();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangePronouns();
    }

    public virtual void ChangeName(Actor_Unit actor)
    {
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangeGender();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangeGender();
    }
}
