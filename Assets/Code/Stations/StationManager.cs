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
    public bool stationUsed;
    //nur relevant bei verschiedene Varianten z.B. Oar left & right (Für Animation)
    public int stationPosition;
    private GameObject particleEffect;

    [Header("Player Placement Korrektur")]
    public bool changeAlsoSprite;

    public Animator playerAnimator;
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
            if (playerAInRange & !stationUsed)
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
            if (playerBInRange & !stationUsed)
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
        if (!stationUsed) return;
        particleEffect.SetActive(false);
        input = GetInput();
    }

    private void JoinStation(PlayerController playerController)
    {
        currentStation = transform.parent.gameObject;
        playerController.currentStation = currentStation;

        stationUsed = true;
        playerController.onStation = true;

        //HandleIdleAnimaton();

        PlacePlayerInStation(changeAlsoSprite);

        Debug.Log(playerController.name + " joins " + currentStation);
    }

    private void LeaveStation(PlayerController playerController)
    {
        playerController.currentStation = null;

        stationUsed = false;
        playerController.onStation = false;

        //HandleIdleAnimaton();

        playerType = 'X';

        PlacePlayerInStation(changeAlsoSprite);

        Debug.Log(playerController.name + " leaves " + currentStation);
    }

    // private void HandleIdleAnimaton()
    // {
    //     //Handle Animator
    //     playerAnimator = playerThatEntered.transform.GetChild(0).gameObject.GetComponent<Animator>();
    //     string idleStation = "is" + gameObject.transform.parent.name + "Idle";
    //     playerAnimator.SetBool(idleStation, stationUsed);

    //     //Handle Position wenn vorhanden
    //     if (stationPosition != 0)
    //     {
    //         playerAnimator.SetInteger("stationPosition", stationPosition);
    //     }
    // }

    public void PlacePlayerInStation(bool changeAlsoSprite)
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

                    ChangeAlsoSprite(changeAlsoSprite, playerSprite);

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
            ChangeAlsoSprite(changeAlsoSprite, playerSprite);
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ChangeAlsoSprite(bool changeAlsoSprite, GameObject playerSprite)
    {
        //Falls für die Animation eine größere/andere Sprite genutzt wird, kann hier die Position korrigiert werden.
        if (changeAlsoSprite)
        {
            if (playerController.onStation)
            {
                playerSprite.transform.position = playerSprite.transform.position;
            }
            else
            {
                playerSprite.transform.localPosition = Vector3.zero;
            }
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
        if (exitingPlayerType == playerType) stationUsed = false;
    }

    public Vector2 GetInput()
    {
        if (playerType != 'A' & playerType != 'B') return new Vector2();
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"),
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
}