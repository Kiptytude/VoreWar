using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryHousePanel : MonoBehaviour
{
    public Transform Folder;
    public GameObject HireableObject;

    public Text RemainingGold;
    public Text ArmySize;


    public void Close()
    {
        gameObject.SetActive(false);
    }
}

