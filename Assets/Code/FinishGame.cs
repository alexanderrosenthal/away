using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private GameObject finishedFirework;
    [SerializeField] private GameObject finishedUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by " + other.gameObject.name);
        if (other.CompareTag("Player")) ;
        {
            Debug.Log("Player entered the trigger!");
            finishedFirework.SetActive(true);
        }
    }
}
