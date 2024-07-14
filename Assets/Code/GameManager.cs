using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = true;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private float delayTime = 3f;
    
    private void Start()
    {
        playerUI.SetActive(false);
        StopGame();
    }
    
    // Start the game
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        Time.timeScale = 1;
        Invoke("StartGameAfterDelay", delayTime);
    }

    void StartGameAfterDelay()
    {
        playerUI.SetActive(true);
        isGamePaused = false;
        timer.GetComponent<Timer>().StartUpdateTime();
        Debug.Log("Game Started");
    }

    // Stop the game
    public void StopGame()
    {
        Time.timeScale = 0; // Pause the game
        isGamePaused = true;
        timer.GetComponent<Timer>().StopUpdateTime();
        Debug.Log("Game Stopped");
    }

    // Toggle game state
    public void ToggleGame()
    {
        if (isGamePaused)
        {
            StartGame();
        }
        else
        {
            StopGame();
        }
    }
}
