using UnityEngine;
using System.Collections;

public abstract class SceneBase : MonoBehaviour
{
    public GameObject UI;
    abstract public void ReceiveInput();
    abstract public void CleanUp();


}
