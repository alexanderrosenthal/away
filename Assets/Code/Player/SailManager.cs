using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailManager : StationManager
{
    // [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject sailSprite;
    [SerializeField] private float sailAngle = 0f;
    [SerializeField] private float maxSailAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject WindManager;
    private float windDirection;
    

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
        windDirection = WindManager.GetComponent<WindManager>().windDirection;
        Debug.Log("Wind Direction: " + windDirection + ", Sail Angle: " + sailAngle);
    }
    
}
