using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class SailManager : StationManager
{
    [Header("Needed Objects")]
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject sailSprite;
    // [SerializeField] private SpriteRenderer spriteRenderer;
    // [SerializeField] private List<Sprite> spriteList;
    private float shipAngle = 0f;
    private float localSailAngleDegrees;
    [SerializeField] private float maxSailAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private WindManager windManager;

    private float windDirection;
    [SerializeField] private float sailSize = 100;
    [HideInInspector] public float sailForce;

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
        HandleAngleOfSail();

        //Identify angle of ship
        // shipAngle = boat.transform.eulerAngles.z;

        // windDirection = windManager.windDirection;

        // CalculateForce();

        // AnimateSail();  // I made a new script for this
        // Debug.Log("Wind Direction: " + windDirection + ", Sail Angle: " + sailAngle);
    }

    private void HandleAngleOfSail()
    {
        float wantedAngle = input.x;
        localSailAngleDegrees = MoveAndClamp(localSailAngleDegrees, wantedAngle, rotationSpeed,
            -maxSailAngle, maxSailAngle);

        sailSprite.transform.localRotation = Quaternion.AngleAxis(localSailAngleDegrees, Vector3.back);
    }
    
    
    /*
    private void CalculateForce()
    {

        // Convert angles from degrees to radians
        double windDirectionInRadians = windDirection * Mathf.Deg2Rad;
        double shipAngleInRadians = shipAngle * Mathf.Deg2Rad;
        double sailAngleInRadians = localSailAngleDegrees * Mathf.Deg2Rad;

        // Combine sailAngleInRadians with angleAngleInRadians for the angle of both in relation to the level
        double combinedAngleInRadian = shipAngleInRadians + sailAngleInRadians;

        // Calculate sail force
        float cosValue = (float)Math.Cos(windDirectionInRadians - combinedAngleInRadian);
        // sailForce = math.pow(cosValue, 3f) * sailSize;
        if (sailForce < 0)
        {
            sailForce = 0;
        }
    }
    */
    
    /*
    private void AnimateSail()
    {
        //corrected sail value for direct handling of spritelist

        float corSailForce = sailForce / 100;

        int roundedvalue = Mathf.RoundToInt(corSailForce);

        spriteRenderer.sprite = spriteList[(int)roundedvalue];
    }
    */
    public Vector2 GetNormal()
    {
        return sailSprite.transform.up;
    }
}
