using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowsNest : StationManager
{
    [Header("CrowsNest:")]
    private bool lastUsed;
    [SerializeField] private CameraZoom camZoom;
    SpriteRenderer spriteRenderer;
    private int playerSortingOrder;

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

            //Anpassen des Sprite Render "Order in Layer";
            spriteRenderer = playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
            playerSortingOrder = spriteRenderer.sortingOrder;
            if (playerSortingOrder == spriteRenderer.sortingOrder)
            {
                spriteRenderer.sortingOrder -= 3;
            }

            lastUsed = onStation;
        }
        else if (lastUsed = onStation)
        {
            spriteRenderer.sortingOrder += 3;
        }
    }
}
