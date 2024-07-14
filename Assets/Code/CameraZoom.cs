using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomOutSize = 20f; 
    public float lerpDuration = 2f;
    private bool zoomOut = false;
    
    private Camera mainCamera; 
    private float zoomInSize; 
    private float startTime; 
    
    private float startSize;
    private float endSize;

    void Start()
    {
        mainCamera = Camera.main;
        zoomInSize = mainCamera.orthographicSize;
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
        startTime = Time.time;
        startSize = zoomInSize;
        endSize = zoomOutSize;
        zoomOut = true;

        // Start the lerp coroutine
        StartCoroutine(LerpCameraSize());
    }
    
    private void ZoomIn()
    {
        // Initialize startSize and startTime
        startTime = Time.time;
        startSize = zoomOutSize;
        endSize = zoomInSize;
        zoomOut = false;

        // Start the lerp coroutine
        StartCoroutine(LerpCameraSize());
    }
    

    IEnumerator LerpCameraSize()
    {
        while (Time.time - startTime < lerpDuration)
        {
            float normalizedTime = (Time.time - startTime) / lerpDuration;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, normalizedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure final value is exact
        mainCamera.orthographicSize = endSize;
    }
}

