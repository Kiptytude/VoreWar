using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveNamePrompt : MonoBehaviour {

    public InputField Name;

    public Button Save;   

    public void TerminateSelf()
    {
        Destroy(gameObject);
    }

}
