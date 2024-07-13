using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    private Collider2D boatCollider;
    private int collisionCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        boatCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Boat collided with obstacle");
            ReduceHealth();
            
            // TODO: No Collision for x seconds
        }
    }

    private void ReduceHealth()
    {
        HealthManager.ModifyHealth(-1);
    }
}
