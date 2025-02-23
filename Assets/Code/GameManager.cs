using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Game Handling")]
    [SerializeField] public bool testmode = false;
    public static bool isGamePaused = true;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject windEffect;
    [SerializeField] private GameObject gullSpawner;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private AudioSource mainTheme;

    private void Awake()
    {
        //Über Bool Steuerung, ob Menu und Camerafahrt kommen.
        if (!testmode)
        {
            StopGame();
            uiCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            uiCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Start the game
    [ContextMenu("Start Game")]
    public void FirstStartGame()
    {
        Time.timeScale = 1;
        uiCanvas.transform.GetChild(0).gameObject.SetActive(false);

        Animator animator = GameObject.Find("MainCamera").GetComponent<Animator>();
        animator.Play("StartCam");
        StartCoroutine(WaitForAnimationEnd(animator));
    }

    private IEnumerator WaitForAnimationEnd(Animator animator)
    {
        //Einen Frame warten, damit die explosion auch wirklich gestartet ist.
        yield return null;

        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        //Startet Game bei 
        yield return new WaitForSeconds(clip.length - 1);

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
        uiCanvas.transform.GetChild(1).gameObject.SetActive(status);
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
