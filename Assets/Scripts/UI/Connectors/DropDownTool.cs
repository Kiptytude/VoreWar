using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.Connectors
{
    public class DropDownTool : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.activeSelf)
            {
                if (Input.anyKeyDown)
                {                    
                    string key = "4";
                    for (KeyCode i = KeyCode.A; i <= KeyCode.Z; i++)
                    {
                        if (Input.GetKeyDown(i))
                        {
                            key = i.ToString();
                        }
                    }
                    if (key == "4")
                        return;
                    for (int i = 1; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text.StartsWith(key))
                        {
                            var trans = transform.GetComponent<RectTransform>();
                            var newpos = trans.anchoredPosition;
                            newpos.y = transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y * (i - 1);
                            trans.anchoredPosition = newpos;
                            //trans.position.y = 40 * i;
                            //transform.SetPositionAndRotation(new Vector3(0, 40 * i), new Quaternion());
                            break;
                        }
                    }
                    
                }
            }
        }
    }
}