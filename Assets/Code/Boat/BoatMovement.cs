using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    // public float boatSpeed = 10f;
    // try to work with forces here.
    public float sailForce = 10f;
    [SerializeField] private Rigidbody2D myRb;

    [SerializeField] private SailManager sailManager;
    [SerializeField] private RudderManager rudderManager;
    [SerializeField] private float keelDrag;
    [SerializeField] private Vector2 keelForce;

    public float rudderAngle;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        PropelBoat();
        RotateBoat();
    }

    private void PropelBoat()
    {
        Debug.Log($"SailForce: {Vector2.up * sailForce}");
        
        
        // keel Force, so that the boat doesn't drift so much
        keelForce = KeelForce();
        myRb.AddForce(keelForce);
        myRb.AddForce(Vector2.up * sailForce);
    }

    private void RotateBoat()
    {
        // Debug.Log($"Velocity: {myRb.velocity}");

        // Debug.Log($"Boat forward: {transform.up}");
        // Debug.Log(rudderManager.rudderAngle);
        float torque = Vector2.Dot(transform.up, myRb.velocity) * rudderManager.rudderAngle;
        // Debug.Log($"Torque: {torque}");


        myRb.AddTorque(torque);
    }

    private Vector2 KeelForce()
    {
        return Vector2.left * (keelDrag * Vector2.Dot(myRb.velocity, transform.right));
    }
    
}
