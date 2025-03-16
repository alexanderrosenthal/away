using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    UIManager uiManager;
    GameManager gameManager;

    private void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscKeyPress();
        }
    }

    public void HandleEscKeyPress()
    {
        //Prüft ob das Menu existiert und killt oder spawned entsprechend
        if (!uiManager.FindUI("GameMenu").gameObject.activeSelf)
        {
            uiManager.ActivateUI("GameMenu");
            gameManager.StopGame();
        }
        else
        {
            uiManager.DeActivateUI("GameMenu");
            gameManager.StartGame();
        }
    }
}