using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBox : MonoBehaviour
{
    Action AAction;
    Action BAction;
    Action CAction;
    Action DAction;
    Action EAction;
    public Button A;
    public Button B;
    public Button C;
    public Button D;
    public Button E;
    public Text Text;

    public void SetData(string mainText, string aText, Action aAction, string bText, Action bAction, string cText = null, Action cAction = null, string dText = null, Action dAction = null, string eText = null, Action eAction = null)
    {
        AAction = aAction;
        A.GetComponentInChildren<Text>().text = aText;
        BAction = bAction;
        B.GetComponentInChildren<Text>().text = bText;
        CAction = cAction;
        if (cText != null)
            C.GetComponentInChildren<Text>().text = cText;
        else
            C.gameObject.SetActive(false);
        DAction = dAction;
        if (dText != null)
            D.GetComponentInChildren<Text>().text = dText;
        else
            D.gameObject.SetActive(false);
        EAction = eAction;
        if (eText != null)
            E.GetComponentInChildren<Text>().text = eText;
        else
            E.gameObject.SetActive(false);
        Text.text = mainText;
    }

    public void AClicked()
    {
        AAction?.Invoke();
        Destroy(gameObject);
    }

    public void BClicked()
    {
        BAction?.Invoke();
        Destroy(gameObject);
    }

    public void CClicked()
    {
        CAction?.Invoke();
        Destroy (gameObject);
    }

    public void DClicked()
    {
        DAction?.Invoke();
        Destroy(gameObject);
    }

    public void EClicked()
    {
        EAction?.Invoke();
        Destroy(gameObject);
    }
}
