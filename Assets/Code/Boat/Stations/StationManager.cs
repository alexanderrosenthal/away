using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StationManager : MonoBehaviour
{
    [Header("StationManager:")]
    public PlayerController playerAController;
    public PlayerController playerBController;

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

    // Neue Variablen für das Input System
    private PlayerControls playerControls;
    private InputAction aAction;
    private InputAction bAction;
    private InputAction aHorizontalAction;
    private InputAction aVerticalAction;
    private InputAction bHorizontalAction;
    private InputAction bVerticalAction;

    public virtual void Awake()
    {
        particleEffect = GetComponentInChildren<ParticleSystem>().gameObject;

        // Initialisiere die Input Actions
        playerControls = new PlayerControls();
        aAction = playerControls.Station.AAction;
        bAction = playerControls.Station.BAction;
        aHorizontalAction = playerControls.Station.AHorizontal;
        aVerticalAction = playerControls.Station.AVertical;
        bHorizontalAction = playerControls.Station.BHorizontal;
        bVerticalAction = playerControls.Station.BVertical;
    }

    public virtual void OnEnable()
    {
        // Aktiviere die Input Actions
        playerControls.Enable();
        aAction.performed += OnAActionPerformed;
        bAction.performed += OnBActionPerformed;
    }

    public virtual void OnDisable()
    {
        // Deaktiviere die Input Actions
        aAction.performed -= OnAActionPerformed;
        bAction.performed -= OnBActionPerformed;
        playerControls.Disable();
    }

    // Neue Callback-Methoden für die Input Actions
    private void OnAActionPerformed(InputAction.CallbackContext context)
    {
        if (GameManager.isGamePaused) return;

        if (playerAInRange && !onStation)
        {
            playerType = 'A';
            playerController = playerAController;
            JoinStation(playerController);
        }
        else if (playerAInRange && playerAController.onStation)
        {
            playerController = playerAController;
            if (!lockedInAnimation)
            {
                LeaveStation(playerController);
            }
        }
    }

    private void OnBActionPerformed(InputAction.CallbackContext context)
    {
        if (GameManager.isGamePaused) return;

        if (playerBInRange && !onStation)
        {
            playerType = 'B';
            playerController = playerBController;
            JoinStation(playerController);
        }
        else if (playerBInRange && playerBController.onStation)
        {
            playerController = playerBController;
            LeaveStation(playerController);
        }
    }

    public virtual void Update()
    {
        if (GameManager.isGamePaused) return;

        // nur GetInput if station is in use
        if (!onStation)
        {
            particleEffect.SetActive(true);
            return;
        }
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
        PlayerController exitingPlayerController = other.GetComponent<PlayerController>();
        char exitingPlayerType = exitingPlayerController.playerType;
        playerAInRange = playerAInRange && exitingPlayerType != 'A';
        playerBInRange = playerBInRange && exitingPlayerType != 'B';
        exitingPlayerController.onStation = false;
        if (exitingPlayerType == playerType) onStation = false;
    }

    public Vector2 GetInput()
    {
        if (playerType != 'A' && playerType != 'B') return Vector2.zero;

        if (playerType == 'A')
        {
            return new Vector2(
                aHorizontalAction.ReadValue<float>(),
                aVerticalAction.ReadValue<float>()
            );
        }
        else // playerType == 'B'
        {
            return new Vector2(
                bHorizontalAction.ReadValue<float>(),
                bVerticalAction.ReadValue<float>()
            );
        }
    }
}