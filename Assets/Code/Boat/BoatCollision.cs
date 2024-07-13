using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    private Collider2D boatCollider;
    [SerializeField] private float knockBackForce = 10f;
    [SerializeField] private Rigidbody2D boatRigidbody;

    // Knock back
    public float knockBackDistance = 1f; // The distance to move for knockback
    public float knockBackDuration = 0.2f; // Duration of the knockback movement

    public bool isKnockingBack = false;
    private Vector2 knockBackTarget;
    private float knockBackStartTime;
    
    // Start is called before the first frame update
    void Start()
    {
        boatCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isKnockingBack)
        {
            float elapsed = Time.time - knockBackStartTime;
            if (elapsed < knockBackDuration)
            {
                Vector2 newPosition = Vector2.Lerp(boatRigidbody.position, knockBackTarget, elapsed / knockBackDuration);
                boatRigidbody.MovePosition(newPosition);
            }
            else
            {
                boatRigidbody.MovePosition(knockBackTarget);
                isKnockingBack = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Boat collided with obstacle");
            ReduceHealth();
            KnockBack();
            // TODO: No Collision for x seconds
        }
    }

    private void ReduceHealth()
    {
        HealthManager.ModifyHealth(-1);
    }

    private void KnockBack()
    {
        // This part uses the boat's rigidbody to apply a force in the opposite direction of the collision
        // Not working
        //Vector2 knockBackDirection = (boatRigidbody.position - (Vector2)transform.position).normalized;
        //boatRigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        
        // isKnockingBack = true;
        // knockBackStartTime = Time.time;
        // Vector2 direction = (boatRigidbody.position - (Vector2)transform.position).normalized;
        // knockBackTarget = boatRigidbody.position + direction * knockBackDistance;
        //Debug.Log("Knockback applied");
        
        isKnockingBack = true;
        knockBackStartTime = Time.time;

        // Calculate knockback direction relative to the boat's position and the collision point
        Vector2 direction = (boatRigidbody.position - (Vector2)transform.position).normalized;
        knockBackTarget = boatRigidbody.position + direction * knockBackDistance;

        // Move the boat to the knockBackTarget position over knockBackDuration seconds
        StartCoroutine(MoveWithLerp(knockBackTarget, knockBackDuration));

        Debug.Log("Knockback applied with direction: " + direction);
    }

    private IEnumerator MoveWithLerp(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = boatRigidbody.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            boatRigidbody.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boatRigidbody.MovePosition(targetPosition); // Ensure it ends exactly at the target position
        isKnockingBack = false;
    }
}
