using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RudderManager : StationManager
{
    [SerializeField] private GameObject rudderSprite;
    [SerializeField] private float rudderAngle = 0f;
    [SerializeField] private float maxRudderAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    // Update is called once per frame


    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            UseStation();
        }
    }
    
    
    public void UseStation()
    {
        float wantedAngle = input.x;
        rudderAngle = MoveAndClamp(rudderAngle, wantedAngle, rotationSpeed, 
            -maxRudderAngle, maxRudderAngle);
        rudderSprite.transform.rotation = Quaternion.AngleAxis(rudderAngle, Vector3.back);
        // sail.transform.Rotate(Vector3.forward * direction.x * rotationSpeed * Time.deltaTime);
    }
}
