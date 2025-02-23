using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] public bool levelfinished = false;
    [SerializeField] private GameObject finishedFirework;
    [SerializeField] private GameObject finishedUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        levelfinished = true;
        Debug.Log("Triggered by " + other.gameObject.name);
        if (!other.CompareTag("Boat")) return;
        Debug.Log("Player entered the trigger!");
        gameManager.StopGame();
        finishedFirework.SetActive(true);
        finishedUI.SetActive(true);
        scoreManager.SetScoreUI();
    }
}
