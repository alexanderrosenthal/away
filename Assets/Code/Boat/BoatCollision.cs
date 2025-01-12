using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// TODO CLEANUP
public class BoatCollision : MonoBehaviour
{
    [SerializeField] private BoatMovement boatMovement;
    // [SerializeField] private Collider2D boatCollider;
    [SerializeField] private Rigidbody2D boatRigidbody;
    private AudioSource collisionAudio;
    [Header("Knockback")] 
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackSpeed;
    


    [Header("Invulnerability Frame")]
    [SerializeField] private int collisionCooldown;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private float flashDuration;
    private int numFlashes;
    [SerializeField] private SpriteRenderer sprite1;
    [SerializeField] private SpriteRenderer sprite2;
    
    private bool cooldownActive;
    // private bool soundIsPlaying;
    
    private void OnEnable()
    {
        // boatCollider = GetComponent<Collider2D>();
        collisionAudio = GetComponent<AudioSource>();
        numFlashes = Mathf.CeilToInt(collisionCooldown / flashDuration / 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (GameManager.isGamePaused) return;  
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Boat collided with obstacle");
            StartCoroutine(ApplyKnockback(collision));
            SoundHandling();
            DamageHandling();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Boat collided with finish");
        }
    }

    private void SoundHandling()
    {
        collisionAudio.Play();
        /*
        StartCoroutine(PlayDamageSound());
        //Handling Sound
        if (soundIsPlaying == false)
        {
            collisionAudio.Play();
            soundIsPlaying = true;
        }
        StartCoroutine(WaitForSound());
        */
    }

    /*
    IEnumerator WaitForSound()
        {
            //Wait Until Sound has finished playing
            while (collisionAudio.isPlaying)
            {
                yield return null;
            }

            //Audio has finished playing, set soundisplaying true
            soundIsPlaying = false; 
        }
    */

    private IEnumerator PlayDamageSound()
    {
        while (collisionAudio.isPlaying)
        {
            yield return null;
        }
        collisionAudio.Play();
    }

    private void DamageHandling()
    {
        //Handling Damage
        if (cooldownActive == false)
        {
            Debug.Log("damage dealt");
            HealthManager.ModifyHealth(-1);

            cooldownActive = true;
            StartCoroutine(FlashBoat());
        }
        
        // StartCoroutine(CollisionCooldown());
    }

    private IEnumerator FlashBoat()
    {
        int temp = 0;
        // boatCollider.enabled = false;
        while (temp < numFlashes)
        {
            sprite1.color = flashColor;
            sprite2.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sprite1.color = regularColor;
            sprite2.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }

        cooldownActive = false;

        // boatCollider.enabled = true;
    }
    
    private IEnumerator CollisionCooldown()
    {
        Debug.Log("Cooldown counting");
        //wait für Cooldown (in sec) before next damage is possible
        yield return new WaitForSeconds(collisionCooldown);

        //Cooldown done - reactivate possibility of Damage
        cooldownActive = false; 

    }

    private IEnumerator ApplyKnockback(Collision2D collision)
    {
        boatMovement.boatState = BoatState.Collision;
        Vector3 collisionPoint = collision.GetContact(0).point;
        boatRigidbody.velocity = knockbackSpeed * (transform.position - collisionPoint).normalized;
        yield return new WaitForSeconds(knockbackTime);
        boatMovement.boatState = BoatState.Sail;
    }

}
