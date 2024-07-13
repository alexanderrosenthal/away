using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI windText;
    [SerializeField] private Image windRose;
    public float windDirection;
    public float interval = 5f;
    public float minWindDirection = -50f;
    public float maxWindDirection = 50f;
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
            UpdateUIText(windDirection);
            UpdateWindRose(windDirection);
            yield return new WaitForSeconds(interval);
        }
    }
    
    float CalculateWindDirection()
    {
        // Calculate wind direction
        windDirection = Random.Range(minWindDirection, maxWindDirection);
        return windDirection;
    }

    private void UpdateUIText(float windDirection)
    {
        string text = "Wind Direction: " + windDirection;
        windText.text = text;
    }
    
    private void UpdateWindRose(float windDirection)
    {
        // Rotate the wind rose to show the wind direction
        windRose.transform.rotation = Quaternion.AngleAxis((windDirection), Vector3.back);
    }
}
