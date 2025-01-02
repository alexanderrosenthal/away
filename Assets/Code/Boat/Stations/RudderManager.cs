using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RudderManager : StationManager
{
    [Header("RudderManager:")]

    [SerializeField] private GameObject rudderSprite;
    public float rudderAngle = 0f;
    [SerializeField] private float maxRudderAngle = 50f;
    [SerializeField] private float rotationSpeed = 100f;

    // Update is called once per frame

    public override void Update()
    {
        base.Update();
        if (!onStation) return;

        if (input.x > 0 || input.x < 0)
        {
            playerController.usingStation = true;

            UseRudder();
        }
        else if (input.x == 0)
        {
            playerController.usingStation = false;
        }
    }

    private void UseRudder()
    {
        float wantedAngle = input.x;
        rudderAngle = MoveAndClamp(rudderAngle, wantedAngle, rotationSpeed,
            -maxRudderAngle, maxRudderAngle);

        if (rudderAngle < 0)
        {
            playerController.transform.GetChild(0).GetComponent<PlayerAnimationManager>().ChangeAnimation("PlayerRudderLeft");
        }
        else if (rudderAngle > 0)
        {
            playerController.transform.GetChild(0).GetComponent<PlayerAnimationManager>().ChangeAnimation("PlayerRudderRight");
        }

        rudderSprite.transform.localRotation = Quaternion.AngleAxis(rudderAngle, Vector3.back);
    }

    public float RudderPercentage()
    {
        return rudderAngle / maxRudderAngle;
    }
}