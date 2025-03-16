using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Game Handling")]
    [SerializeField] public bool testmode = false;
    [SerializeField] public bool GameManagerInLevel = false;
    public static bool isGamePaused = true;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject windEffect;
    [SerializeField] private GameObject gullSpawner;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private AudioSource mainTheme;

    public void Start()
    {
        Time.timeScale = 1;

        //Deaktiviert den Loadingscreen, der am Anfang eines Levels immer im Übergang kommt.
        GameObject.Find("UIManager").GetComponent<UIManager>().KillUI("Loadscreen(Clone)");

        uiCanvas.transform.GetChild(0).gameObject.SetActive(false);

        StartGame();
    }

    public void StartGame()
    {
        isGamePaused = false;
        timer.GetComponent<Timer>().StartUpdateTime();

        HandleEnviroment(true);
        HandleUI(true);
        HandleSound(true);
    }

    public void StopGame()
    {
        isGamePaused = true;
        timer.GetComponent<Timer>().StopUpdateTime();

        HandleEnviroment(false);
        HandleUI(false);
        HandleSound(false);
    }

    private void HandleEnviroment(bool status)
    {
        windEffect.SetActive(status);
        gullSpawner.SetActive(status);
    }

    private void HandleUI(bool status)
    {
        //PlayerUI
        uiCanvas.transform.GetChild(0).gameObject.SetActive(status);
    }

    public void HandleSound(bool status)
    {
        audioManager.crossfadeDuration = 1.0f;

        if (status)
        {
            //waterSound.Play();
            audioManager.CrossfadeTo(menuTheme, mainTheme);
        }
        else
        {
            //waterSound.Stop();
            audioManager.CrossfadeTo(mainTheme, menuTheme);
        }
    }
}
