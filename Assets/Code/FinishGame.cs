using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] public bool levelfinished = false;
    [SerializeField] private GameObject finishedFirework;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    private GameObject uiManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Boat")) return;

        HandleGameState();

        //Handle UI
        uiManager = GameObject.Find("UIManager");
        uiManager.GetComponent<UIManager>().SpawnUIPrefab(0);

        //Handle TargetReached Stuff
        finishedFirework.SetActive(true);
        scoreManager.SetScoreUI();
    }

    public void OnShipCollission()
    {
        Debug.Log("OnShipCollission");
        HandleGameState();

        //Handle UI
        uiManager = GameObject.Find("UIManager");
        uiManager.GetComponent<UIManager>().SpawnUIPrefab(1);
    }

    private void HandleGameState()
    {
        levelfinished = true;
        gameManager.StopGame();
    }
}