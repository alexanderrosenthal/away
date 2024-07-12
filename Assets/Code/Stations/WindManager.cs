using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI windText;
    public float windDirection;
    public float interval = 5f;
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
            yield return new WaitForSeconds(interval);
        }
    }
    
    float CalculateWindDirection()
    {
        // Calculate wind direction
        windDirection = Random.Range(0 , 180);
        return windDirection;
    }

    private void UpdateUIText(float windDirection)
    {
        string text = "Wind Direction: " + windDirection;
        windText.text = text;
    }
}
