using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GullMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float randomPos = 2f;
    [SerializeField] private float deathTime = 10f;
    private Camera cam;
    private Rigidbody2D rb;
    

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
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

        StartCoroutine(killGull());
    }
    IEnumerator killGull()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(this.gameObject);
    }
    
    private void Move()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 randomCamPosition = cam.transform.position + new Vector3(UnityEngine.Random.Range(-randomPos, randomPos), UnityEngine.Random.Range(-randomPos, randomPos), 0);
            Vector2 direction = (randomCamPosition - this.transform.position).normalized;
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
    
}
