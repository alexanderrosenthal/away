using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class StationManager : MonoBehaviour
{
    [Header("StationManager:")]
    public PlayerController playerAController;
    public PlayerController playerBController;
    [Header("Debug Only")]
    [HideInInspector] public Vector2 input;
    [HideInInspector] public char playerType;
    [HideInInspector] public PlayerController playerController;
    // [HideInInspector] 
    public GameObject playerThatEntered;
    // [HideInInspector] 
    public bool playerAInRange;
    // [HideInInspector] 
    public bool playerBInRange;
    // [HideInInspector] 
    public bool onStation;
    //nur relevant bei verschiedene Varianten z.B. Oar left & right (Für Animation)
    public int stationPosition;
    private GameObject particleEffect;

    [Header("Player Placement Korrektur")]
    public bool lockedInAnimation = false;
    private GameObject currentStation;

    public virtual void Start()
    {
        particleEffect = GetComponentInChildren<ParticleSystem>().gameObject;
    }

    public virtual void Update()
    {
        // TODO: probleme wenn Spieler ohne Action zu drücken von der Station fliegt.
        // check if A Action is pressed
        if (Input.GetButtonDown("A Action"))
        {
            // Check if A is in range and the station is not used yet
            if (playerAInRange & !onStation)
            {
                playerType = 'A';
                playerController = playerAController;
                JoinStation(playerController);
            }
            else if (playerAInRange & playerAController.onStation)
            {
                playerController = playerAController;
                if (!lockedInAnimation)
                {
                    LeaveStation(playerController);
                }
            }
        }

        if (Input.GetButtonDown("B Action"))
        {
            if (playerBInRange & !onStation)
            {
                playerType = 'B';
                playerController = playerBController;
                JoinStation(playerController);
            }
            else if (playerBInRange & playerBController.onStation)
            {
                playerController = playerBController;
                LeaveStation(playerController);
            }
        }

        // only GetInput if station is in use
        if (!onStation) return;
        particleEffect.SetActive(false);
        input = GetInput();
    }

    public virtual void JoinStation(PlayerController playerController)
    {
        currentStation = transform.parent.gameObject;
        playerController.currentStation = currentStation;

        onStation = true;
        playerController.onStation = true;

        PlacePlayerInStation();

        Debug.Log(playerController.name + " joins " + currentStation);
    }

    public virtual void LeaveStation(PlayerController playerController)
    {
        if (playerController.usingStation)
        {
            return;
        }
        playerController.currentStation = null;

        onStation = false;
        playerController.onStation = false;

        playerType = 'X';

        PlacePlayerInStation();

        Debug.Log(playerController.name + " leaves " + currentStation);
    }


    public void PlacePlayerInStation()
    {
        GameObject playerSprite = playerController.playerSprite;

        if (playerController.onStation)
        {
            bool placementFound = false;

            foreach (Transform child in currentStation.transform)
            {
                if (child.name == "PlayerPlacement")
                {
                    playerSprite.transform.position = child.position;
                    playerSprite.transform.rotation = child.rotation;

                    placementFound = true;

                    return;
                }
            }

            if (placementFound == false)
            {
                Debug.Log("No PlayerPlacement on " + currentStation);
            }
        }
        else
        {
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public float MoveAndClamp(float value, float direction, float speed, float clampLow, float clampHigh)
    {
        value += direction * speed * Time.deltaTime;
        value = Mathf.Clamp(value, clampLow, clampHigh);
        return value;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerThatEntered = other.gameObject;
        playerController = playerThatEntered.GetComponent<PlayerController>();
        char enteredPlayerType = playerController.playerType;
        playerAInRange = playerAInRange || enteredPlayerType == 'A';
        playerBInRange = playerBInRange || enteredPlayerType == 'B';
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerController exitingPlayerController = other.GetComponent<PlayerController>();
        char exitingPlayerType = exitingPlayerController.playerType;
        playerAInRange = playerAInRange && exitingPlayerType != 'A';
        playerBInRange = playerBInRange && exitingPlayerType != 'B';
        exitingPlayerController.onStation = false;
        if (exitingPlayerType == playerType) onStation = false;
    }

    public Vector2 GetInput()
    {
        if (playerType != 'A' & playerType != 'B') return new Vector2();
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"),
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
}