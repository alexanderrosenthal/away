using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RudderManager : StationManager
{
    [Header("RudderManager:")]

    [SerializeField] private GameObject rudderSprite;
    [SerializeField] private Transform PlayerPlacementUpdater;
    public float rudderAngle = 0f;
    [SerializeField] private float maxRudderAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;

    [HideInInspector] public float rudderForce;
    // Update is called once per frame

    public override void Update()
    {
        base.Update();

        if (stationUsed)
        {
            UseRudder(); 
            MovePlayer();
        }
    }

    private void UseRudder()
    {        
        float wantedAngle = input.x;
        rudderAngle = MoveAndClamp(rudderAngle, wantedAngle, rotationSpeed, 
            -maxRudderAngle, maxRudderAngle);
            
        rudderSprite.transform.localRotation = Quaternion.AngleAxis(rudderAngle, Vector3.back);
    }
    private void MovePlayer()
    {
            playerThatEntered.transform.position = PlayerPlacementUpdater.position;
            playerThatEntered.transform.rotation = PlayerPlacementUpdater.rotation;
    }

    public float RudderPercentage()
    {
        return rudderAngle / maxRudderAngle;
    }
}
