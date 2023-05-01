using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{

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
