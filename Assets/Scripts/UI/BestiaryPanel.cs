using UnityEngine;
using System.Collections;

public class BestiaryPanel : MonoBehaviour
{
    public GameObject Foreward;
    public GameObject Compy;
    public GameObject Shark;
    public GameObject Harvester;


    void ClearAll()
    {
        Foreward.SetActive(false);
        Compy.SetActive(false);
        Shark.SetActive(false);
        Harvester.SetActive(false);
    }

    public void ShowForeward()
    {
        ClearAll();
        Foreward.SetActive(true);
    }

    public void ShowCompy()
    {
        ClearAll();
        Compy.SetActive(true);
    }

    public void ShowHarvester()
    {
        ClearAll();
        Harvester.SetActive(true);
    }


    public void ShowShark()
    {
        ClearAll();
        Shark.SetActive(true);
    }
}
