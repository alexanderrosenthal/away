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

    [SerializeField] private Transform LeftKnoppRobController;

    private float windDirection;
    [FormerlySerializedAs("sailSet")][FormerlySerializedAs("sailsSet")][SerializeField] private bool sailDown;

    public override void Update()
    {
        base.Update();
        if (onStation)
        {
            //HandleSailSet();
            HandleAngleOfSail();

            //Handle Player-Animation while Interaction
            if (input.x > 0 || input.x < 0)
            {
                playerController.usingStation = true;
                playerController.transform.GetChild(0).GetComponent<PlayerAnimationManager>().ChangeAnimation("SailHandle");
            }
            else if (input.x == 0)
            {
                playerController.usingStation = false;
            }
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

        //Connect to Rope
        LeftKnoppRobController.GetComponent<RobeController>().points[2] = playerThatEntered.transform.GetChild(0);
    }


    public override void LeaveStation(PlayerController playerController)
    {
        base.LeaveStation(playerController);

        sailUp = false;
        sailAnimation.LowerSail();

        //Disconnect to Rope
        LeftKnoppRobController.GetComponent<RobeController>().points[2] = LeftKnoppRobController;
    }
}
