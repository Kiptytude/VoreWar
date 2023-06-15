using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputBox : MonoBehaviour
{
    bool stringMode = false;
    Action<int> YesAction;
    Action<string> YesActionString;
    Action NoAction;
    public TMP_InputField InputField;
    public Button Yes;
    public Button No;
    public Text Text;

    public void SetData(Action<int> action, string yesText, string noText, string mainText, int characterLimit, Action noAction = null)
    {
        State.GameManager.ActiveInput = true;
        stringMode = false;
        YesAction = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        NoAction = noAction;
        InputField.characterLimit = characterLimit;
        InputField.ActivateInputField();
    }

    public void SetData(Action<string> action, string yesText, string noText, string mainText, int characterLimit, Action noAction = null)
    {
        State.GameManager.ActiveInput = true;
        stringMode = true;
        InputField.contentType = TMP_InputField.ContentType.Standard;
        YesActionString = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        NoAction = noAction;
        InputField.characterLimit = characterLimit;
        InputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu") && NoAction == null)
            NoClicked();
    }

    public void ActivateTypeMethod(Func<string, string> getYesValue)
    {
        InputField.onValueChanged.AddListener((s) => Yes.GetComponentInChildren<Text>().text = getYesValue(InputField.text));
    }

    public void YesClicked()
    {
        if (stringMode == false && int.TryParse(InputField.text, out int result))
        {
            YesAction?.Invoke(result);
        }
        else if (stringMode)
        {
            YesActionString?.Invoke(InputField.text);
        }
        else
        {
            State.GameManager.CreateMessageBox("Invalid value");
        }
        State.GameManager.ActiveInput = false;
        Destroy(gameObject);
    }

    public void NoClicked()
    {
        NoAction?.Invoke();
        State.GameManager.ActiveInput = false;
        Destroy(gameObject);
    }

}
