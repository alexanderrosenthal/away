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
            hitObj = other.gameObject;
            hasKicked = true;
            direction = hitObj.transform.position - transform.position;
            direction.Normalize();
            gullCollisionAudio.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(KickPlayerTimer());
        }
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
