using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI windText;
    [SerializeField] private Image windRose;
    public float windDirection;
    [SerializeField] private float interval = 0.05f;
    public float minWindDirection = -50f;
    public float maxWindDirection = 50f;
    [SerializeField] private float minChangeSpeed = -4f;
    [SerializeField] private float maxChangeSpeed = 4f;

    void Start()
    {
        // Start the coroutine to update wind direction every 'interval' seconds
        StartCoroutine(UpdateWindDirection());
    }
    
    IEnumerator UpdateWindDirection()
    {
        while (true)
        {
            CalculateWindDirection();
            UpdateWindRose();
            yield return new WaitForSeconds(interval);
        }
    }
    
    private void CalculateWindDirection()
    {
        // Calculate wind direction
        windDirection += Random.Range(minChangeSpeed, maxChangeSpeed);
        windDirection = Mathf.Clamp(windDirection, minWindDirection, maxWindDirection);
    }

    private void UpdateUIText()
    {
        string text = "Wind Direction: " + windDirection;
    }
    
    private void UpdateWindRose()
    {
        // Rotate the wind rose to show the wind direction
        windRose.transform.rotation = Quaternion.AngleAxis(windDirection, Vector3.back);
    }
}
