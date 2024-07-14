using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StationManager : MonoBehaviour
{
    [HideInInspector] public Vector2 input;
    [HideInInspector] public char playerType;
    [HideInInspector] public GameObject playerThatEntered;
    public PlayerController playerAController;
    public PlayerController playerBController;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public bool playerAInRange;
    [HideInInspector] public bool playerBInRange;
    [HideInInspector] public bool stationUsed;

    public virtual void Update()
    {
        // check if player A is in the trigger and A Action is pressed -> put A on station
        if (Input.GetButtonDown("A Action"))
        {
            if (playerAInRange & !stationUsed)
            {
                stationUsed = true;
                playerType = 'A';
                playerAController.onStation = stationUsed;
                playerAController.currentStation = this.gameObject;
            } else if (playerAInRange & playerAController.onStation)
            {
                stationUsed = false;
                playerType = 'X';
                playerAController.onStation = false;
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
            } else if (playerBInRange & playerBController.onStation)
            {
                stationUsed = false;
                playerType = 'X';
                playerBController.onStation = false;
            }
        }
        
        // only GetInput if station is in use
        if (!stationUsed) return;
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
        char exitingPlayerType = other.GetComponent<PlayerController>().playerType;
        playerAInRange = playerAInRange && exitingPlayerType != 'A';
        playerBInRange = playerBInRange && exitingPlayerType != 'B';
    }

    public Vector2 GetInput()
    {   if (playerType != 'A' & playerType != 'B') return new Vector2();
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
}
