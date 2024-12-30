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
        if (input.y == 0) return;
        if (playerController.usingStation) return;
        playerController.usingStation = true;

        UseRudder();
    }

    private void UseRudder()
    {
        float wantedAngle = input.x;
        rudderAngle = MoveAndClamp(rudderAngle, wantedAngle, rotationSpeed,
            -maxRudderAngle, maxRudderAngle);

        rudderSprite.transform.localRotation = Quaternion.AngleAxis(rudderAngle, Vector3.back);

        playerController.usingStation = false;
    }

    public float RudderPercentage()
    {
        return rudderAngle / maxRudderAngle;
    }
}
