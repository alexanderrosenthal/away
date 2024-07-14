using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = true;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private AudioSource mainTheme;
    
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
        menuTheme.Stop();
        waterSound.Play();
        mainTheme.Play();
        Debug.Log("Game Started");
    }

    // Stop the game
    public void StopGame()
    {
        Time.timeScale = 0; // Pause the game
        isGamePaused = true;
        timer.GetComponent<Timer>().StopUpdateTime();
        mainTheme.Stop();
        menuTheme.Play();
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
