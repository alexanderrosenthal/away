using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraZoomOut : MonoBehaviour
{
    public Camera cam;
    
    void Start()
    {
    }

    public void ZoomOut()
    {
        Debug.Log("Zooom out from button");
        cam.orthographicSize = 20f;
    }
}
