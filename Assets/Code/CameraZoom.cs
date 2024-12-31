using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomOutSize = 20f;
    public float lerpDuration = 2f;
    public bool zoomOut = false;

    private Camera mainCamera;
    private float zoomInSize;
    private float startTime;

    private float startSize;
    private float endSize;

    private Animator cameraAnimator;

    void Start()
    {
        mainCamera = Camera.main;
        zoomInSize = mainCamera.orthographicSize;

        cameraAnimator = mainCamera.GetComponent<Animator>();

    }

    public void ToggleZoom()
    {
        if (cameraAnimator != null)
        {
            Debug.Log("Disabling camera animator");
            cameraAnimator.enabled = false;
        }
        Debug.Log("Toggling zoom");
        if (zoomOut)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        zoomOut = true;
        Debug.Log("Zooming out");
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
        zoomOut = false;
        Debug.Log("Zooming in");
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
        Debug.Log("Lerping camera size while loop, startSize: " + startSize + ", " +
                  " endSize: " + endSize + ", " + "time: " + Time.time + ", " + "startTime: " +
                  startTime + ", " + "lerpDuration: " + lerpDuration + ", " + "normalizedTime: " +
                  (Time.time - startTime) / lerpDuration);
        while (Time.time - startTime < lerpDuration)
        {
            float normalizedTime = (Time.time - startTime) / lerpDuration;
            //mainCamera.orthographicSize = endSize;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, normalizedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure final value is exact
        mainCamera.orthographicSize = endSize;
    }
}

