using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FileLoaderUI : MonoBehaviour
{
    public GameObject ButtonType;
    public Transform Folder;

    internal void CreateMapLoadButton(string file)
    {
        var button = Instantiate(ButtonType, Folder);
        button.GetComponentInChildren<Text>().text = $"Open File : {Path.GetFileName(file)}";
        button.GetComponent<Button>().onClick.AddListener(() => State.GameManager.MapEditor.LoadMap(file));
        button.GetComponent<Button>().onClick.AddListener(() => TerminateSelf());
    }

    internal void CreateMapStrategicIntegrateButton(string file)
    {
        var button = Instantiate(ButtonType, Folder);
        button.GetComponentInChildren<Text>().text = $"Use Map : {Path.GetFileName(file)}";
        button.GetComponent<Button>().onClick.AddListener(() => State.GameManager.Start_Mode.CreateStrategicGame.PickMap(file));
        button.GetComponent<Button>().onClick.AddListener(() => TerminateSelf());
    }

    internal void CreateGrabContentSettingsButton(string file)
    {
        var button = Instantiate(ButtonType, Folder);
        button.GetComponentInChildren<Text>().text = $"Use Save Game : {Path.GetFileName(file)}";
        button.GetComponent<Button>().onClick.AddListener(() => State.GameManager.Start_Mode.CreateStrategicGame.PickSaveForContentData(file));
        button.GetComponent<Button>().onClick.AddListener(() => TerminateSelf());
    }

    public void TerminateSelf()
    {
        Destroy(gameObject);
    }
}

