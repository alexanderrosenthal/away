using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StationManager : MonoBehaviour
{    [Header("StationManager:")]
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
                // now the station is in use.
                stationUsed = true;
                playerType = 'A';
                playerAController.onStation = stationUsed;
                playerAController.currentStation = this.gameObject;
                playerAController.PlacePlayerInStation(changeAlsoSprite);
            } 
            else if (playerAInRange & playerAController.onStation)
            {
                stationUsed = false;
                playerType = 'X';
                playerAController.onStation = false; 
                playerAController.PlacePlayerInStation(changeAlsoSprite);              
            }
        }
        
        if (Input.GetButtonDown("B Action"))
        {
            if (playerBInRange & !stationUsed)
            {
                stationUsed = true;
                playerType = 'B';
                playerBController.onStation = stationUsed;
                playerBController.currentStation = this.gameObject;                
                playerBController.PlacePlayerInStation(changeAlsoSprite);
            } 
            else if (playerBInRange & playerBController.onStation)
            {
                stationUsed = false;
                playerType = 'X';
                playerBController.onStation = false;
                playerBController.PlacePlayerInStation(changeAlsoSprite);
            }
        }
        
        // only GetInput if station is in use
        if (!stationUsed) return;
        particleEffect.SetActive(false);
        input = GetInput();
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
    {   if (playerType != 'A' & playerType != 'B') return new Vector2();
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
}