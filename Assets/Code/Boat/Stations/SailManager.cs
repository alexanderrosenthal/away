using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SailManager : StationManager
{    
    [Header("SailManager:")]
    [Header("Needed Objects")]
    [SerializeField] private GameObject sailSprite;
    [SerializeField] private SailAnimation sailAnimation;
    private float localSailAngleDegrees;
    [SerializeField] private float maxSailAngle;
    [SerializeField] private float rotationSpeed;
    public bool sailUp;

    private float windDirection;
    [FormerlySerializedAs("sailSet")] [FormerlySerializedAs("sailsSet")] [SerializeField] private bool sailDown;

    public override void Update()
    {
        base.Update();
        if (stationUsed)
        {
            //HandleSailSet();
            HandleAngleOfSail();
        }
        else
        {

        }
    }

    public Vector2 GetNormal()
    {
        return sailSprite.transform.up;
    }
    private void HandleAngleOfSail()
    {
        float wantedAngle = input.x;
        localSailAngleDegrees = MoveAndClamp(localSailAngleDegrees, wantedAngle, rotationSpeed,
            -maxSailAngle, maxSailAngle);

        sailSprite.transform.localRotation = Quaternion.AngleAxis(localSailAngleDegrees, Vector3.back);
    }

    public override void JoinStation(PlayerController playerController)
    {
            base.JoinStation(playerController);

            sailUp = true;
            sailAnimation.RaiseSail();
    }

    
    public override void LeaveStation(PlayerController playerController)
    {
            base.LeaveStation(playerController);

            sailUp = false;
            sailAnimation.LowerSail();
        
    }

    // private void HandleSailSet()
    // {
    //     if (input.y < 0)
    //     {
    //         sailUp = true;
    //     }

    //     if (input.y > 0)
    //     {
    //         sailUp = false;
    //         sailAnimation.RaiseSail();
    //     }
    // }    
    
}
