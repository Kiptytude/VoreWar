using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    Action YesAction;
    Action NoAction;
    public Button Yes;
    public Button No;
    public Text Text;

    public void SetData(Action action, string yesText, string noText, string mainText, Action noAction = null)
    {
        YesAction = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        NoAction = noAction;
    }

    public void YesClicked()
    {
        YesAction?.Invoke();
        Destroy(gameObject);
    }

    public void NoClicked()
    {
        NoAction?.Invoke();
        Destroy(gameObject);
    }

}
