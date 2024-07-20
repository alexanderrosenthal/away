using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class SailManager : StationManager
{
    // [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject sailSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public List<Sprite> spriteList;
    private float shipAngle = 0f;
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
        handleAngleOfSail();
        
        //Identify angle of ship
        shipAngle = boat.transform.eulerAngles.z;

        windDirection = windManager.windDirection;

        calculateSpeed();
        
        AnimateSail();
        // Debug.Log("Wind Direction: " + windDirection + ", Sail Angle: " + sailAngle);
    }

    private void handleAngleOfSail()
    {
        float wantedAngle = input.x;
        sailAngle = MoveAndClamp(sailAngle, wantedAngle, rotationSpeed,
            -maxSailAngle, maxSailAngle);

        sailSprite.transform.localRotation = Quaternion.AngleAxis(sailAngle, Vector3.back);
    }

    private void calculateSpeed()
    {

        Debug.Log("shipAngle: " + shipAngle);
        Debug.Log("sailAngle: " + sailAngle);

        // Convert angles from degrees to radians
        double windDirectionInRadians = windDirection * Mathf.Deg2Rad;
        double shipAngleInRadians = shipAngle * Mathf.Deg2Rad;
        double sailAngleInRadians = sailAngle * Mathf.Deg2Rad;

        // Combine sailAngleInRadians with angleAngleInRadians for the angle of both in relation to the level
        double combinedAngleInRadian = shipAngleInRadians + sailAngleInRadians;

        
        Debug.Log("combinedAngleInRadian: " + combinedAngleInRadian);

        // Calculate boat speed
        float cosValue = (float)Math.Cos(windDirectionInRadians - combinedAngleInRadian);
        sailForce = math.pow(cosValue, 3f) * sailSize;
        if (sailForce < 0)
        {
            sailForce = 0;
        }
    }

    private void AnimateSail()
    {
        //corrected sail value for direct handling of spritelist
        float corSailForce = sailForce / 100;
        int roundedvalue = Mathf.RoundToInt(corSailForce);

        spriteRenderer.sprite = spriteList[(int)roundedvalue];        
    }    
}
