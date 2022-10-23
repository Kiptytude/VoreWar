using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnReportPanel : MonoBehaviour
{
    public Transform Folder;
    public GameObject Prefab;

    internal void Generate(List<StrategicReport> reports)
    {
        foreach (Transform child in Folder)
        {
            Destroy(child.gameObject);
        }
        gameObject.SetActive(true);
        foreach (var report in reports)
        {
            var obj = Instantiate(Prefab, Folder).GetComponent<ClickableText>();
            obj.Text.text = report.Text;
            obj.EventTrigger.triggers.Add(new UnityEngine.EventSystems.EventTrigger.Entry());
            obj.EventTrigger.triggers[0].callback.AddListener((s) => State.GameManager.CenterCameraOnTile(report.Position.x, report.Position.y));
        }
    }
}
