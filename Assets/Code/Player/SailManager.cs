using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SailManager : StationManager
{
    // [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject sailSprite;
    [SerializeField] private float sailAngle = 0f;
    [SerializeField] private float maxSailAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private WindManager windManager;
    private float windDirection;
    public float boatSpeed = 0f;
    

    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            UseSail();
        }
    }
    
    
    public void UseSail()
    {
        float wantedAngle = input.x;
        sailAngle = MoveAndClamp(sailAngle, wantedAngle, rotationSpeed, 
            -maxSailAngle, maxSailAngle);
        sailSprite.transform.rotation = Quaternion.AngleAxis(sailAngle, Vector3.back);
        // sail.transform.Rotate(Vector3.forward * direction.x * rotationSpeed * Time.deltaTime);
        windDirection = windManager.windDirection;
        calculateSpeed();
        Debug.Log("Wind Direction: " + windDirection + ", Sail Angle: " + sailAngle);
    }

    private void calculateSpeed()
    {
        // Convert angles from degrees to radians
        double windDirectionInRadians = windDirection * (Math.PI / 180);
        double sailAngleInRadians = sailAngle * (Math.PI / 180);

        // Calculate boat speed
        float cosValue = (float)Math.Cos(windDirectionInRadians - sailAngleInRadians);
        boatSpeed = cosValue * cosValue * cosValue;
        if (boatSpeed < 0)
        {
            boatSpeed = 0;
        }
        Debug.Log("Boat Speed: " + boatSpeed);
    }
    
}
