using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoatState
{
    Sail,
    Collision,
    AtTarget
}

public class BoatMovement : MonoBehaviour
{
    public BoatState boatState = BoatState.Sail;
    [Header("Assign These")]
    [SerializeField] private WindManager windManager;

    [Header("Connections")]
    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private SailManager sailManager;
    [SerializeField] private SailAnimation sailAnimation;
    [SerializeField] private RudderManager rudderManager;

    [Header("Sail Behavior")] 
    [SerializeField] private float sailSize;
    [SerializeField] private float cosinePowerFactor = 2;

    [Header("Rudder Behavior")] 
    [SerializeField] private float rudderSize;
    
    
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
    // public bool boatStopped;


    private void FixedUpdate()
    {
        if (boatState != BoatState.Sail) return;
        PropelBoat();
        RotateBoat();
    }

    private void PropelBoat()
    {   
        sailForce = SailForce();
        keelForce = KeelForce();
        
        myRb.AddForce(keelForce);
        myRb.AddForce(sailForce);

        var pos = transform.position;
        Debug.DrawLine(pos, pos + (Vector3)sailForce / 100, Color.white);
        Debug.DrawLine(pos, pos + (Vector3)keelForce / 100, Color.blue);
    }

    private void RotateBoat()
    {
        rudderForce = rudderManager.RudderPercentage() * rudderSize;
        torque = Vector2.Dot(transform.up, myRb.velocity) * rudderForce;
        myRb.AddTorque(torque);
    }

    private Vector2 SailForce()
    {
        if (!sailManager.sailUp) return new Vector2();
        windDirection = windManager.GetDirection();
        sailNormal = sailManager.GetNormal();
        Vector2 boatDirection = transform.up;
        //print($"Wind Angle: {windDirection}, Sail Angle: {sailNormal}");
        forceFactor = Mathf.Cos(Vector2.Angle(windDirection, sailNormal) * Mathf.Deg2Rad);
        forceFactor = Mathf.Clamp(forceFactor, 0, 1);
        sailAnimation.SetSprite(forceFactor);
        forceFactor = Mathf.Pow(forceFactor, cosinePowerFactor);
        return sailSize * forceFactor * boatDirection;
    }

    private Vector2 KeelForce()
    {
        return Vector2.left * (keelDrag * Vector2.Dot(myRb.velocity, transform.right));
    }
    
    public void StopBoat()
    {
        Debug.Log("Boat just got stopped");
        boatState = BoatState.AtTarget;
        myRb.velocity = Vector2.zero;
    }
}
