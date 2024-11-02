using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class SailManager : StationManager
{    
    [Header("SailManager:")]
    [Header("Needed Objects")]
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject sailSprite;
    [SerializeField] private SailAnimation sailAnimation;
    private float localSailAngleDegrees;
    [SerializeField] private float maxSailAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private WindManager windManager;

    private float windDirection;
    [FormerlySerializedAs("sailSet")] [FormerlySerializedAs("sailsSet")] [SerializeField] private bool sailDown;

    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            HandleSailSet();
            HandleAngleOfSail();
        }
        else
        {

        }
    }

    private void HandleAngleOfSail()
    {
        float wantedAngle = input.x;
        localSailAngleDegrees = MoveAndClamp(localSailAngleDegrees, wantedAngle, rotationSpeed,
            -maxSailAngle, maxSailAngle);

        sailSprite.transform.localRotation = Quaternion.AngleAxis(localSailAngleDegrees, Vector3.back);
    }

    private void HandleSailSet()
    {
        if (input.y < 0)
        {
            sailDown = true;
        }

        if (input.y > 0)
        {
            sailDown = false;
            sailAnimation.RaiseSail();
        }
    }    
    
    public Vector2 GetNormal()
    {
        return sailSprite.transform.up;
    }

    public bool SailDown()
    {
        return sailDown;
    }
}
