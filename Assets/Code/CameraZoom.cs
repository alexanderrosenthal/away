using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomOutSize = 20f;
    public float lerpDuration = 2f;
    private float calLerpDuration;
    public bool zoomOut = false;

    private Camera mainCamera;
    private float zoomInSize;
    private float startTime;
    private float endSize;

    private Animator cameraAnimator;

    private Coroutine currentCoroutine;

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

        //Stoppen des vorherigen Zoooms 
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Initialize startSize and startTime
        float localStartSize = mainCamera.orthographicSize;
        startTime = Time.time;

        if (zoomOut)
        {
            ZoomIn(localStartSize);
        }
        else
        {
            ZoomOut(localStartSize);
        }

    }

    private void ZoomOut(float localStartSize)
    {
        Debug.Log("Zooming out");
        endSize = zoomOutSize;
        int direction = 1;        

        // Start the lerp coroutine
        currentCoroutine = StartCoroutine(LerpCameraSize(localStartSize, direction));
        zoomOut = true;
    }

    private void ZoomIn(float localStartSize)
    {
        Debug.Log("Zooming in");
        endSize = zoomInSize;
        int direction = -1;

        // Start the lerp coroutine
        currentCoroutine = StartCoroutine(LerpCameraSize(localStartSize, direction));
        zoomOut = false;
    }

    IEnumerator LerpCameraSize(float localStartSize, int direction)
    {

        //lerpDuration;
        float totalStrecke = zoomOutSize - zoomInSize;
        float Teilstrecke = (endSize - mainCamera.orthographicSize) * direction;
        calLerpDuration = lerpDuration * Teilstrecke / totalStrecke;

        // Debug.Log("Lerping camera size while loop, startSize: " + localStartSize + ", " +
        //           " endSize: " + endSize + ", " + "time: " + Time.time + ", " + "startTime: " +
        //           startTime + ", " + "calLerpDuration: " + calLerpDuration + ", " + "normalizedTime: " +
        //           (Time.time - startTime) / calLerpDuration);
        while (Time.time - startTime < calLerpDuration)
        {
            float normalizedTime = (Time.time - startTime) / calLerpDuration;
            //mainCamera.orthographicSize = endSize;
            mainCamera.orthographicSize = Mathf.Lerp(localStartSize, endSize, normalizedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure final value is exact
        mainCamera.orthographicSize = endSize;
    }
}

