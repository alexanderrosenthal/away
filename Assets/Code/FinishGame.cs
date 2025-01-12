using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] public bool levelfinished = false;
    [SerializeField] private GameObject finishedFirework;
    [SerializeField] private GameObject finishedUI;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        levelfinished = true;
        Debug.Log("Triggered by " + other.gameObject.name);
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered the trigger!");
        finishedFirework.SetActive(true);
        playerUI.SetActive(false);
        finishedUI.SetActive(true);
        boatMovement.StopBoat();
        gameManager.StopGame();
        scoreManager.SetScoreUI();
    }
}
