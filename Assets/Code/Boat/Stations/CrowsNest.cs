using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNest : StationManager
{
    [Header("CrowsNest:")]
    private bool lastUsed;
    [SerializeField] private CameraZoom camZoom;

    public override void Start()
    {
        base.Start();
        lastUsed = onStation;
    }
    public override void Update()
    {
        base.Update();
        if (lastUsed != onStation)
        {
            Debug.Log("Attempt toggle");
            camZoom.ToggleZoom();

            //Anpassen des Sprte Render "Order in Layer"
            playerThatEntered.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;

            lastUsed = onStation;
        }
        else if (lastUsed = onStation)
        {
            playerThatEntered.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
    }
}
