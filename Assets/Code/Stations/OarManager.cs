using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarManager : StationManager
{
    [SerializeField] private float strength = 1f;
    [SerializeField] private float strokeSeconds = 2f;
    [SerializeField] private bool usingOar = false;
    [SerializeField] private Rigidbody2D boatRb;
    [SerializeField] private GameObject forcePoint;
    

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (input.y == 0) return;
        if (usingOar) return;
        usingOar = true;
        if (input.y > 0)
        {
            StartCoroutine(ForwardStroke());
        }
        else
        {
            
            StartCoroutine(BackwardStroke());
        }

    }

    IEnumerator ForwardStroke()
    {
        Debug.Log("Adding rudder force");
        boatRb.AddForceAtPosition(boatRb.transform.up * strength, forcePoint.transform.position);
        yield return new WaitForSeconds(strokeSeconds);
        usingOar = false;
    }

    IEnumerator BackwardStroke()
    {
        boatRb.AddForceAtPosition(-boatRb.transform.up * strength, forcePoint.transform.position);
        yield return new WaitForSeconds(strokeSeconds);
        usingOar = false;
    }
}