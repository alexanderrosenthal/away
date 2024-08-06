using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarManager : StationManager
{
    [SerializeField] private float strength = 1f;
    [SerializeField] private float preStrokeSeconds = 0.7f;
    [SerializeField] private float strokeSeconds = 2f;
    [SerializeField] private bool usingOar = false;
    [SerializeField] private Rigidbody2D boatRb;
    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private GameObject forcePoint;
    [SerializeField] private Animator rowingAnimator;
    [SerializeField] private GameObject oarSprite;
    [SerializeField] private AudioSource splashAudio;


    // TODO rowing doesn't stop if person falls off
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        oarSprite.SetActive(stationUsed);
        if(!stationUsed) return;
        if (input.y == 0) return;
        if (usingOar) return;
        usingOar = true;

        if (boatMovement.boatState != BoatState.AtTarget)
        {
            StartCoroutine(RudderStroke());
        }        
    }

    private IEnumerator RudderStroke()
    {
        splashAudio.Play();
        if (input.y > 0)
        {
            rowingAnimator.SetTrigger("startRowingForward");
            // Debug.Log("Adding rudder force");
            yield return new WaitForSeconds(preStrokeSeconds);
            boatRb.AddForceAtPosition(boatRb.transform.up * strength, forcePoint.transform.position);
        }
        else
        {
            rowingAnimator.SetTrigger("startRowingBackward");
            yield return new WaitForSeconds(preStrokeSeconds);
            boatRb.AddForceAtPosition(-boatRb.transform.up * strength, forcePoint.transform.position);
        }
        yield return new WaitForSeconds(strokeSeconds);
        usingOar = false;
    }
}