using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Camera cam;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = this.GetComponent<Rigidbody2D>();
        Move();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing.");
        }
        if (cam == null)
        {
            Debug.LogError("Main camera is missing.");
        }
    }
    
    private void Move()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (cam.transform.position - this.transform.position).normalized;
            rb.velocity = direction * speed;
            
            // Rotate the object to face the direction of movement
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            Debug.LogWarning("Object does not have a Rigidbody2D component.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Out of Camera bounds. Destroying object.");
            Destroy(this.gameObject);
        }

    }
}
