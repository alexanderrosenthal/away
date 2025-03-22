using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNestUI : MonoBehaviour
{
    public RectTransform targetTransform;
    public Vector2 totalStartPosition;
    public Vector2 totalEndPosition;
    private Vector2 currentEndPos;
    private bool zoomOut = false;
    private float startTime;
    private float calLerpDuration;
    private Coroutine currentCoroutine;

    void Start()
    {
        if (targetTransform == null)
            targetTransform = GetComponent<RectTransform>();

        targetTransform.anchoredPosition = totalStartPosition;
    }

    public void ToggleCrowNestUI()
    {
        //Stoppen des vorherigen Zoooms 
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Initialize startSize and startTime
        Vector2 localStartPos = targetTransform.anchoredPosition;
        startTime = Time.time;

        if (zoomOut)
        {
            ZoomIn(localStartPos);
        }
        else
        {
            ZoomOut(localStartPos);
        }
    }

    private void ZoomOut(Vector2 localStartSize)
    {
        currentEndPos = totalEndPosition;
        int direction = 1;

        // Start the lerp coroutine
        currentCoroutine = StartCoroutine(LerpCrowNestUI(localStartSize, direction));
        zoomOut = true;
    }

    private void ZoomIn(Vector2 localStartSize)
    {
        currentEndPos = totalStartPosition;
        int direction = -1;

        // Start the lerp coroutine
        currentCoroutine = StartCoroutine(LerpCrowNestUI(localStartSize, direction));
        zoomOut = false;
    }

    IEnumerator LerpCrowNestUI(Vector2 localStartSize, int direction)
    {
        calLerpDuration = GameObject.Find("CrowsNestTrigger").GetComponent<CameraZoom>().calLerpDuration;

        while (Time.time - startTime < calLerpDuration)
        {
            float normalizedTime = (Time.time - startTime) / calLerpDuration;

            targetTransform.anchoredPosition = Vector2.Lerp(localStartSize, currentEndPos, normalizedTime);

            yield return null; // Wait for the next frame
        }

        // Ensure final value is exact
        targetTransform.anchoredPosition = currentEndPos;

        if (targetTransform.anchoredPosition == totalStartPosition)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().KillUI("CrowsNestUI(Clone)");
        }
    }
}