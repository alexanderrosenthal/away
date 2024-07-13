using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = true;
    
    private void Start()
    {
        StopGame();
    }
    
    // Start the game
    public void StartGame()
    {
        Time.timeScale = 1; // Resume the game
        isGamePaused = false;
        Debug.Log("Game Started");
    }

    // Stop the game
    public void StopGame()
    {
        Time.timeScale = 0; // Pause the game
        isGamePaused = true;
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
