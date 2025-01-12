using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool testmode = false;
    public static bool isGamePaused = true;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject windEffect;
    [SerializeField] private GameObject gullSpawner;
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private AudioSource mainTheme;

    private void Start()
    {
        //Über Bool Steuerung, ob Menu und Camerafahrt kommen.
        if (testmode == false)
        {
            uiCanvas.transform.GetChild(0).gameObject.SetActive(true);
            StopGame();

            GameObject mainCamera = GameObject.Find("MainCamera");
            mainCamera.GetComponent<Animator>().CrossFade("StartCam", 0.2f);
        }
        else
        {
            uiCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
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
        RestartGame();
        menuTheme.Stop();

        Debug.Log("Game Started");
    }
    
    // Stop the game
    public void StopGame()
    {
        //Time.timeScale = 0; // Pause the game
        isGamePaused = true;
        timer.GetComponent<Timer>().StopUpdateTime();

        uiCanvas.transform.GetChild(1).gameObject.SetActive(false);
        windEffect.SetActive(false);
        gullSpawner.SetActive(false);

        waterSound.Stop();
        mainTheme.Stop();

        menuTheme.Play();
        // Debug.Log("Game Stopped");
    }

    // Stop the game
    public void RestartGame()
    {
        //Time.timeScale = 0; // Pause the game
        isGamePaused = false;
        timer.GetComponent<Timer>().StartUpdateTime();

        uiCanvas.transform.GetChild(1).gameObject.SetActive(true);
        windEffect.SetActive(true);
        gullSpawner.SetActive(true);

        waterSound.Play();
        mainTheme.Play();

        menuTheme.Play();
        // Debug.Log("Game Stopped");
    }
}
