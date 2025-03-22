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
    UIManager uIManager;
    private Transform crowsNestUI;
    private int playerSortingOrder;

    public override void Start()
    {
        base.Start();

        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override void Update()
    {
        base.Update();

        if (upInCrowNest == false && onStation == true)
        {
            camZoom.ToggleZoom();

            HandleCrowsNestUI();

            HandleOrderInLayer(4);
            upInCrowNest = true;
        }
        else if (upInCrowNest == true && onStation == false)
        {
            camZoom.ToggleZoom();

            HandleCrowsNestUI();

            HandleOrderInLayer(-4);
            upInCrowNest = false;
        }
    }

    private void HandleCrowsNestUI()
    {
        //Spawn CrowNestUI
        //Stellt sicher, dass keine doppelte UI entsteht, wenn man schnell in und aus der Station geht.
        if (!uIManager.FindUI("CrowsNestUI(Clone)"))
        {
            uIManager.SpawnUIPrefab(2);
        }

        crowsNestUI = uIManager.FindUI("CrowsNestUI(Clone)");

        crowsNestUI.gameObject.GetComponent<CrowsNestUI>().ToggleCrowNestUI();
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