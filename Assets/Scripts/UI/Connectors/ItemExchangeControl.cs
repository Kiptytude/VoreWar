using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ItemExchangeControl : MonoBehaviour
{

    internal ItemType type;
    public Button TransferToRightButton;
    public Button TransferToLeftButton;
    public TextMeshProUGUI LeftText;
    public TextMeshProUGUI RightText;
    public TextMeshProUGUI ItemText;

    public void UpdateValues(int left, int right)
    {
        LeftText.text = left.ToString();
        RightText.text = right.ToString();
        ItemText.text = type.ToString();
        TransferToRightButton.interactable = left > 0;
        TransferToLeftButton.interactable = right > 0;
    }

    public void TransferRight()
    {
        State.GameManager.StrategyMode.ExchangerUI.TransferItemToRight(type);
    }

    public void TransferLeft()
    {
        State.GameManager.StrategyMode.ExchangerUI.TransferItemToLeft(type);
    }
    
}
