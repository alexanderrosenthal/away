using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    private Collider2D boatCollider;
    [SerializeField] private float knockBackForce = 10f;
    [SerializeField] private Rigidbody2D boatRigidbody;

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
        Vector2 knockBackDirection = (boatRigidbody.position - (Vector2)transform.position).normalized;
        boatRigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback applied");


    }
}
