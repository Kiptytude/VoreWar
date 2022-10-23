using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CustomizerButton : MonoBehaviour
{
    public Text Label;
    public string defaultText;
    public Button Increase;
    public Button Decrease;

    internal void SetData(string text, Action<int> action)
    {
        Label.text = text;
        defaultText = text;
        Increase.onClick.AddListener(() => action(1));
        Decrease.onClick.AddListener(() => action(-1));
    }
}
