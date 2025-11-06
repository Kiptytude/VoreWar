using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitTagModifierItem : MonoBehaviour
{
    public Button duplicateButton;
    public Button DeleteButton;
    public TMP_Dropdown tagEffect;
    public TMP_Dropdown tagCase;
    public TMP_Dropdown tagTarget;
    public TMP_Dropdown targetValue;
    public InputField modifierValue;
    public Slider targetSlider;

    public void RefreshSliderText()
    {
        targetSlider.GetComponentInChildren<Text>().text = $"{targetSlider.value}";
    }
}