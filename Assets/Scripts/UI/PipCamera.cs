using UnityEngine;
using System.Collections;

public class PipCamera : MonoBehaviour
{

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    internal void SetLocation(int x, int y, int zoom)
    {
        cam.transform.position = new Vector3(x, y, cam.transform.position.z);
        cam.orthographicSize = zoom;
    }

    internal void SetLocation(Vector3 position, int zoom)
    {
        cam.transform.position = new Vector3(position.x, position.y, cam.transform.position.z);
        cam.orthographicSize = zoom;
    }



}
