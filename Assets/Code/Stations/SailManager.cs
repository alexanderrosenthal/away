using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class SailManager : StationManager
{
    // [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject sailSprite;
    [SerializeField] private Sprite Sprite;
    [SerializeField] public List<Sprite> spriteList;
    [SerializeField] private float sailAngle = 0f;
    [SerializeField] private float maxSailAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private WindManager windManager;

    private float windDirection;
    [SerializeField] private float sailSize = 100;
    [HideInInspector] public float sailForce = 0f;

    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            UseSail();
        }
        else
        {
            sailForce = 0;
        }
    }
    
    public void UseSail()
    {
        float wantedAngle = input.x;
        sailAngle = MoveAndClamp(sailAngle, wantedAngle, rotationSpeed, 
            -maxSailAngle, maxSailAngle);
        sailSprite.transform.localRotation = Quaternion.AngleAxis(sailAngle, Vector3.back);
        
        windDirection = windManager.windDirection;
        calculateSpeed();
        AnimateSail();
        // Debug.Log("Wind Direction: " + windDirection + ", Sail Angle: " + sailAngle);
    }

    private void calculateSpeed()
    {
        // Convert angles from degrees to radians
        double windDirectionInRadians = windDirection * Mathf.Deg2Rad;
        double sailAngleInRadians = sailAngle * Mathf.Deg2Rad;

        // Calculate boat speed
        float cosValue = (float)Math.Cos(windDirectionInRadians - sailAngleInRadians);
        sailForce = math.pow(cosValue, 3f) * sailSize;
        if (sailForce < 0)
        {
            sailForce = 0;
        }
    }

    private void AnimateSail()
    {
        float corSailForce = sailForce / 10;

        Debug.Log("corSailForce: " + corSailForce);

        if (corSailForce < 25)
        {
            Debug.Log("setSail: 0");
            Sprite = spriteList[0];
        }
        else if(corSailForce >= 25 && corSailForce < 50)
        {
            Debug.Log("setSail: 1");
            Sprite = spriteList[1];
        }        
        else if(corSailForce >= 50 && corSailForce < 75)
        {
            Debug.Log("setSail: 2");
            Sprite = spriteList[2];
        }
        else
        {
            Debug.Log("setSail: 3");
            Sprite = spriteList[3];
        }

    }    
}
