using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    private Collider2D boatCollider;

    // Start is called before the first frame update
    void Start()
    {
        boatCollider = GetComponent<Collider2D>();
        Debug.Log("Boat Collision script attached to " + gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Boat collided with obstacle");
            // TODO: Get knocked back
            // TODO: No Collision for x seconds
            // TODO: Reduce health

        }
    }
}
