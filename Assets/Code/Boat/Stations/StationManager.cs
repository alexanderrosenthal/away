using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StationManager : MonoBehaviour
{
    [Header("StationManager:")]

    [Header("Debug Only")]
    [HideInInspector] public Vector2 input;
    [HideInInspector] public char playerType;
    [HideInInspector] public PlayerController playerController;
    public GameObject playerThatEntered;
    public bool playerAInRange;
    public bool playerBInRange;
    public bool onStation;
    public int stationPosition;
    private GameObject particleEffect;

    [Header("Player Placement Korrektur")]
    public bool lockedInAnimation = false;
    private GameObject currentStation;


    public virtual void Awake()
    {
        particleEffect = GetComponentInChildren<ParticleSystem>().gameObject;
    }

    public virtual void Update()
    {
        if (GameManager.isGamePaused) return;
        // LEAVE STATION
        if (onStation && playerController != null)
        {
            if (playerController.ConsumeInteract())
            {
                LeaveStation(playerController);
            }
            return;
        }
        // JOIN STATION
        if (!onStation && playerThatEntered != null)
        {
            var pc = playerThatEntered.GetComponent<PlayerController>();

            if (pc != null && pc.ConsumeInteract() && !onStation)
            {
                JoinStation(pc);
            }
        }

        // NO PLAYER ON STATION
        if (!onStation)
        {
            particleEffect.SetActive(true);
            return;
        }

        // PLAYER USING STATION
        particleEffect.SetActive(false);

        if (playerController != null)
        {
            input = playerController.GetInputVector();
        }
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

            if (!placementFound)
            {
                Debug.Log("No PlayerPlacement on " + currentStation);
            }
        }
        else
        {
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerController.lookingAngle = 0;
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
        playerThatEntered = null;
        PlayerController exitingPlayerController = other.GetComponent<PlayerController>();
        char exitingPlayerType = exitingPlayerController.playerType;
        playerAInRange = playerAInRange && exitingPlayerType != 'A';
        playerBInRange = playerBInRange && exitingPlayerType != 'B';
        exitingPlayerController.onStation = false;
        if (exitingPlayerType == playerType) onStation = false;
    }
}