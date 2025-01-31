using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GullCollision : MonoBehaviour
{
    [SerializeField] private bool hasKicked;
    [SerializeField] private float speed;
    [SerializeField] private float effectSeconds;
    [SerializeField] private float secondsTillGullRemove;
    [SerializeField] private AudioSource gullCollisionAudio;
    private GameObject hitObj;
    private Vector2 direction;
    private GullMovement gullMovement;
    private Animator animator;

    private void Start()
    {
        gullMovement = GetComponent<GullMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hasKicked) KickObject();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Triggered by " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            hitObj = other.gameObject;
            hasKicked = true;
            direction = hitObj.transform.position - transform.position;
            direction.Normalize();

            gullCollisionAudio.Play();
            // Den Animator abrufen (nehmen wir an, er befindet sich am Kind-Objekt)
            animator = transform.GetChild(0).GetComponent<Animator>();
            // Die Explosion-Animation starten
            animator.Play("Explosion");

            // Coroutine starten, um auf das Ende der Animation zu warten
            StartCoroutine(WaitForAnimationEnd());

            StartCoroutine(KickPlayerTimer());
        }
    }

    private IEnumerator WaitForAnimationEnd()
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

    void KickObject()
    {
        hitObj.transform.Translate(direction * (speed * Time.deltaTime));
    }

    IEnumerator KickPlayerTimer()
    {
        yield return new WaitForSeconds(effectSeconds);
        hasKicked = false;
        yield return new WaitForSeconds(secondsTillGullRemove);
        gullMovement.killGull();
    }
}