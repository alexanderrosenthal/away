using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI windText;
    [SerializeField] private Image windRose;
    [SerializeField] private Transform windEffects;

    //WindDirection from 0 - 360 degrees as max value
    public float windDirection = 0;

    //How often the winddirection is changing
    [SerializeField] private float interval = 1f;

    //Max values for winddirection to design levels
    public float minWindDirection = -90f;
    public float maxWindDirection = 90f;

    //Steps of change in both direction, when changing
    [SerializeField] private float minChangeSpeed = -5f;
    [SerializeField] private float maxChangeSpeed = 5f;

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
            UpdateWindeffects();
            yield return new WaitForSeconds(interval);
        }
    }

    private void CalculateWindDirection()
    {
        windDirection += Random.Range(minChangeSpeed, maxChangeSpeed);;
        windDirection = Mathf.Clamp(windDirection, minWindDirection, maxWindDirection);
    }

    private void UpdateWindRose()
    {
        // Rotate the wind rose to show the wind direction
        windRose.transform.rotation = Quaternion.AngleAxis(windDirection, Vector3.back);
    }

    private void UpdateWindeffects()
    {
        // Rotate the wind particlesystem to show the wind direction
        windEffects.rotation = Quaternion.AngleAxis(windDirection, Vector3.back);
    }
}
