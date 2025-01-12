using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGulls : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private bool spawning = true;

    private Camera cam;

    private void OnEnable()
    {
        cam = Camera.main;
        // Debug.Log("Camera is: " + cam);
        StartCoroutine(SpawnGull());
    }

    private IEnumerator SpawnGull()
    {
        while (spawning)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnObject()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, transform);
    }

    Vector2 GetRandomSpawnPosition()
    {
        // Get the camera's viewport boundaries in world coordinates
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Randomly select a position on the screen edges
        float x, y;

        if (Random.value < 0.5f)
        {
            x = Random.Range(screenBottomLeft.x, screenTopRight.x);
            y = Random.value < 0.5f ? screenBottomLeft.y - spawnDistance : screenTopRight.y + spawnDistance;
        }
        else
        {
            x = Random.value < 0.5f ? screenBottomLeft.x - spawnDistance : screenTopRight.x + spawnDistance;
            y = Random.Range(screenBottomLeft.y, screenTopRight.y);
        }

        return new Vector2(x, y);
    }
}