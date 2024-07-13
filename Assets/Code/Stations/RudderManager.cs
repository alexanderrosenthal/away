using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RudderManager : StationManager
{
    // [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject rudderSprite;
    public float rudderAngle = 0f;
    [SerializeField] private float maxRudderAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    // Update is called once per frame

    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            UseRudder();
        }
    }
    
    public void UseRudder()
    {
        float wantedAngle = input.x;
        rudderAngle = MoveAndClamp(rudderAngle, wantedAngle, rotationSpeed, 
            -maxRudderAngle, maxRudderAngle);
        rudderSprite.transform.localRotation = Quaternion.AngleAxis(rudderAngle, Vector3.back);
        // sail.transform.Rotate(Vector3.forward * direction.x * rotationSpeed * Time.deltaTime);
    }
}
