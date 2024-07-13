using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StationManager : MonoBehaviour
{
    public Vector2 input;
    public char playerType;
    public GameObject playerThatEntered;
    public PlayerController playerController;
    public bool playerInRange;
    public bool stationUsed;

    public virtual void Update()
    {
        if(!playerInRange) return;
        if (Input.GetButtonDown($"{playerType} Action"))
        {
            stationUsed = !stationUsed;
            playerController.onStation = stationUsed;
            playerController.currentStation = this.gameObject;
        }

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
        if (playerInRange) return;
        playerInRange = true;
        playerThatEntered = other.gameObject;
        playerController = playerThatEntered.GetComponent<PlayerController>();
        playerType = playerController.playerType;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public Vector2 GetInput()
    {   if (playerType != 'A' & playerType != 'B') return new Vector2();
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
}
