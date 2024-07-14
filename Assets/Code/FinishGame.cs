using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by " + other.gameObject.name);
        if (other.CompareTag("Player")) ;
        {
            Debug.Log("Player entered the trigger!");
        }
    }
}
