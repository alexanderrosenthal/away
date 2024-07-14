using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    // public float boatSpeed = 10f;
    // try to work with forces here.
    [SerializeField] private Rigidbody2D myRb;

    [SerializeField] private SailManager sailManager;
    [SerializeField] private RudderManager rudderManager;
    [SerializeField] private float keelDrag;

    [Header("Debug Information")] 
    [SerializeField] private float sailForce;
    [SerializeField] private Vector2 keelForce;
    [SerializeField] private float rudderForce;
    [SerializeField] private float torque;

    [SerializeField] private bool boatStopped;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!boatStopped)
        {
            PropelBoat();
            RotateBoat();
        }
    }

    private void PropelBoat()
    {
        // Debug.Log($"SailForce: {Vector2.up * sailForce}");
        sailForce = sailManager.sailForce;
        
        // keel Force, so that the boat doesn't drift so much
        keelForce = KeelForce();
        myRb.AddForce(keelForce);
        myRb.AddForce(transform.up * sailForce);
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
