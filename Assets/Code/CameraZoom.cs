using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float targetSize = 5f; 
    public float lerpDuration = 2f;
    private bool zoomOut = false;
    
    private Camera mainCamera; 
    private float startSize; 
    private float startTime; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void ToggleZoom()
    {
        if (zoomOut)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
        zoomOut = !zoomOut;
    }

    private void ZoomOut()
    {
        // Initialize startSize and startTime
        startSize = mainCamera.orthographicSize;
        startTime = Time.time;
        zoomOut = true;

        // Start the lerp coroutine
        StartCoroutine(LerpCameraSize());
    }
    
    private void ZoomIn()
    {
        // Initialize startSize and startTime
        startSize = mainCamera.orthographicSize;
        startTime = Time.time;
        zoomOut = false;

        // Start the lerp coroutine
        StartCoroutine(LerpCameraSize());
    }
    

    IEnumerator LerpCameraSize()
    {
        while (Time.time - startTime < lerpDuration)
        {
            float normalizedTime = (Time.time - startTime) / lerpDuration;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, normalizedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure final value is exact
        mainCamera.orthographicSize = targetSize;
    }
}

