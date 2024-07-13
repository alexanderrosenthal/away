using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    // public float boatSpeed = 10f;
    // try to work with forces here.
    public float sailForce = 10f;
    [SerializeField] private Rigidbody2D myRb;
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
        myRb.AddForce(Vector2.up * sailForce);
    }

    private void RotateBoat()
    {
        Debug.Log($"Velocity: {myRb.velocity}");
        Debug.Log($"Boat forward: {transform.forward}");
        float torque = Vector2.Dot(transform.forward, myRb.velocity);
        myRb.AddTorque(torque);
    }
    
}
