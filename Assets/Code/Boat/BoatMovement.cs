using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Assign These")]
    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private SailManager sailManager;
    [SerializeField] private SailAnimation sailAnimation;
    [SerializeField] private RudderManager rudderManager;
    [SerializeField] private NewWindManager windManager;

    [Header("Sail Behavior")] 
    [SerializeField] private float sailSize;

    [SerializeField] private float cosinePowerFactor = 2;

    // [SerializeField] private AnimationCurve sailForceCurve; TODO find out if this is possible
    [Header("Keel Behavior")]
    [SerializeField] private float keelDrag;

    [Header("Debug Information")] 
    [SerializeField] private Vector2 sailNormal;
    [SerializeField] private Vector2 windDirection;
    [SerializeField] private float forceFactor;
    [SerializeField] private Vector2 sailForce;
    [SerializeField] private Vector2 keelForce;
    [SerializeField] private float rudderForce;
    [SerializeField] private float torque;

    public bool boatStopped;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (boatStopped) return;
        PropelBoat();
        RotateBoat();
    }

    private Vector2 SailForce()
    {
        windDirection = windManager.GetDirection();
        sailNormal = sailManager.GetNormal();
        Vector2 boatDirection = transform.up;
        print($"Wind Angle: {windDirection}, Sail Angle: {sailNormal}");
        forceFactor = Mathf.Cos(Vector2.Angle(windDirection, sailNormal) * Mathf.Deg2Rad);
        forceFactor = Mathf.Clamp(forceFactor, 0, 1);
        sailAnimation.SetSprite(forceFactor);
        forceFactor = Mathf.Pow(forceFactor, cosinePowerFactor);
        return sailSize * forceFactor * boatDirection;
    }

    private void PropelBoat()
    {
        // Debug.Log($"SailForce: {Vector2.up * sailForce}");
        // sailForce = transform.up * sailManager.sailForce;
        sailForce = SailForce();
        // Debug.DrawLine(transform.position, transform.position + (Vector3)sailForce / 100);
        
        // keel Force, so that the boat doesn't drift so much
        keelForce = KeelForce();
        myRb.AddForce(keelForce);
        myRb.AddForce(sailForce);
    }

    private void RotateBoat()
    {
        // Debug.Log($"Velocity: {myRb.velocity}");

        // Debug.Log($"Boat forward: {transform.up}");
        // Debug.Log(rudderManager.rudderAngle);
        rudderForce = rudderManager.rudderForce;
        torque = Vector2.Dot(transform.up, myRb.velocity) * rudderForce;
        // Debug.Log($"Torque: {torque}");


        myRb.AddTorque(torque);
    }

    private Vector2 KeelForce()
    {
        return Vector2.left * (keelDrag * Vector2.Dot(myRb.velocity, transform.right));
    }

    public void StopBoat()
    {
        Debug.Log("Boat just got stopped");
        boatStopped = true;
        myRb.velocity = Vector2.zero;
    }
}
