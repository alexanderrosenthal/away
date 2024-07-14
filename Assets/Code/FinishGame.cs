using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something is in Trigger!");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the goal!");
            // Play fireworks
            // Play victory music
        }
    } 
}
