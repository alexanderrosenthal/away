using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNest : StationManager
{
    private bool lastUsed;
    [SerializeField] private CameraZoom camZoom;

    public override void Start()
    {
        base.Start();
        lastUsed = stationUsed;
    }

    public override void Update()
    {
        base.Update();
        if (lastUsed != stationUsed)
        {
            Debug.Log("Attempt toggle");
            camZoom.ToggleZoom();
            lastUsed = stationUsed;
        }
    }
}
