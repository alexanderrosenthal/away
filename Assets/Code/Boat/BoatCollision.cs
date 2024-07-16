using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    [SerializeField] private Collider2D boatCollider;
    [SerializeField] private int collisionCooldown = 0;
    private bool cooldownActive = false; 
    private bool soundIsPlaying = false;
    private AudioSource collisionAudio;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private float flashDuration;
    [SerializeField] private int numFlashes;
    [SerializeField] private SpriteRenderer sprite1;
    [SerializeField] private SpriteRenderer sprite2;
    

    // Start is called before the first frame update
    void Start()
    {
        // boatCollider = GetComponent<Collider2D>();
        collisionAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Boat collided with obstacle");

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
        //Handling Sound
        if (soundIsPlaying == false)
        {
            collisionAudio.Play();
            soundIsPlaying = true;
        }
        StartCoroutine(waitForSound());
    }

    IEnumerator waitForSound()
        {
            //Wait Until Sound has finished playing
            while (collisionAudio.isPlaying)
            {
                yield return null;
            }

            //Audio has finished playing, set soundisplaying true
            soundIsPlaying = false; 
        }

    private void DamageHandling()
    {
        //Handling Damage
        if (cooldownActive == false)
        {
            Debug.Log("damage dealt");
            HealthManager.ModifyHealth(-1);        

            StartCoroutine(FlashBoat());

            cooldownActive = true;
        }
        
        StartCoroutine(CollisionCooldown());
    }

    private IEnumerator FlashBoat()
    {
        int temp = 0;
        boatCollider.enabled = false;
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

        boatCollider.enabled = true;
    }
    
    IEnumerator CollisionCooldown()
    {
        Debug.Log("Cooldown counting");
        //wait für Cooldown (in sec) before next damage is possible
        yield return new WaitForSeconds(collisionCooldown);

        //Cooldown done - reactivate possibility of Damage
        cooldownActive = false; 

    }
}
