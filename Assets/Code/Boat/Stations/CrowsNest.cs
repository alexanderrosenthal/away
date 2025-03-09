using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNest : StationManager
{
    [Header("CrowsNest:")]
    private bool upInCrowNest;
    [SerializeField] private CameraZoom camZoom;
    SpriteRenderer spriteRenderer;
    private int playerSortingOrder;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (upInCrowNest == false && onStation == true)
        {
            camZoom.ToggleZoom();
            HandleOrderInLayer(4);
            upInCrowNest = true;
        }
        else if (upInCrowNest == true && onStation == false)
        {
            camZoom.ToggleZoom();
            HandleOrderInLayer(-4);
            upInCrowNest = false;
        }
    }

    private void HandleOrderInLayer(int sortInt)
    {
        //Anpassen des Sprite Render "Order in Layer";
        spriteRenderer = playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerSortingOrder = spriteRenderer.sortingOrder;

        if (playerSortingOrder == spriteRenderer.sortingOrder)
        {
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder + sortInt;
        }
    }
}