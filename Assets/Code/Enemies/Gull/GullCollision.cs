using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GullCollision : MonoBehaviour
{
    [SerializeField] private float collissionEnergy;
    [SerializeField] private float durationOfHit;
    [SerializeField] private float secondsTillGullRemove;
    [SerializeField] private AudioSource gullCollisionAudio;
    private Transform hitObj;
    private GullMovement gullMovement;
    private Animator animator;

    private void Start()
    {
        gullMovement = GetComponent<GullMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Stoppt Möwe
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //Effect on Player
            hitObj = other.transform;
            hitObj.GetComponent<PlayerCollision>().HandleHit(transform.GetComponent<Collider2D>(), collissionEnergy, durationOfHit);

            HandleGullBehavior();
        }
    }

    private void HandleGullBehavior()
    {
        gullCollisionAudio.Play();
        // Den Animator abrufen (nehmen wir an, er befindet sich am Kind-Objekt)
        animator = transform.GetChild(0).GetComponent<Animator>();
        // Die Explosion-Animation starten
        animator.Play("Explosion");

        // Coroutine starten, um auf das Ende der Animation zu warten
        StartCoroutine(WaitForExplosionEnd());

        StartCoroutine(KickPlayerTimer());
    }

    private IEnumerator WaitForExplosionEnd()
    {
        //Einen Frame warten, damit die explosion auch wirklich gestartet ist.
        yield return null;

        // Hier holen wir uns die aktuelle Animation
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;

        // Warten, bis die Dauer der Animation vergangen ist
        yield return new WaitForSeconds(clip.length);

        // Deaktivieren des GameObjects nach dem Ende der Animation
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator KickPlayerTimer()
    {
        yield return new WaitForSeconds(secondsTillGullRemove);
        gullMovement.killGull();
    }
}