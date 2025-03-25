using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNestUI : MonoBehaviour
{
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private Vector2 totalStartPosition;
    [SerializeField] private Vector2 totalEndPosition;
    private Vector2 currentEndPos;
    private bool zoomOut = false;
    private float startTime;
    private float calLerpDuration;
    private Coroutine currentCoroutine;

    [Header("Map Movement:")]
    [SerializeField] private RectTransform mapTransform;
    [SerializeField] private float MapMovementSpeed;
    [SerializeField] private float minY; // Unteres Limit
    [SerializeField] private float maxY;  // Oberes Limit

    void Start()
    {
        if (targetTransform == null)
            targetTransform = GetComponent<RectTransform>();

        targetTransform.anchoredPosition = totalStartPosition;
    }

    //MOVE UI MAP 
    public void MoveUIMap(Vector2 input)
    {
        Vector3 currentPosition = mapTransform.localPosition;

        // Bewegungsrichtung basierend auf der Rotation des Objekts
        Vector3 moveDirection = mapTransform.up * input.y * (MapMovementSpeed * Time.deltaTime);

        // Neue Position berechnen
        Vector3 newPosition = currentPosition + moveDirection;

        // Begrenzung auf y-Koordinate anwenden
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Position setzen
        mapTransform.localPosition = newPosition;
    }

    //UI HANDLING
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