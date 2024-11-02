using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarManager : StationManager
{
    [Header("OarManager:")]
    [SerializeField] private float strength = 1f;
    [SerializeField] private float preStrokeSeconds = 0.7f;
    [SerializeField] private float strokeSeconds = 2f;
    [SerializeField] private bool usingOar = false;
    [SerializeField] private Rigidbody2D boatRb;
    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private GameObject forcePoint;
    [SerializeField] private AudioSource splashAudio;

    // TODO rowing doesn't stop if person falls off
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (!stationUsed) return;
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
            playerAnimator.SetBool("IsRowing", true);
            lockedInAnimation = true;
            
            yield return new WaitForSeconds(preStrokeSeconds);
            boatRb.AddForceAtPosition(boatRb.transform.up * strength, forcePoint.transform.position);
        }
        else
        {
            playerAnimator.SetBool("IsRowingBack", true);
            lockedInAnimation = true;

            yield return new WaitForSeconds(preStrokeSeconds);
            boatRb.AddForceAtPosition(-boatRb.transform.up * strength, forcePoint.transform.position);
        }

        yield return new WaitForSeconds(strokeSeconds);

        playerAnimator.SetBool("IsRowing", false);
        playerAnimator.SetBool("IsRowingBack", false);
        lockedInAnimation = false;

        usingOar = false;  
    }
}