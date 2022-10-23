using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBox : MonoBehaviour {

    public TextMeshProUGUI InfoText;

    public void Update()
    {
        if (!gameObject.activeSelf)
            return;
        if (Input.GetButtonDown("Cancel"))
            DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
