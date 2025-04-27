using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;

    public void HandleEscKeyPress()
    {
        //Prüft ob das Menu existiert und killt oder spawned entsprechend
        if (!uiManager.FindUI("GameMenu").gameObject.activeSelf)
        {
            uiManager.ActivateUI("GameMenu");
            uiManager.BringUIOnTop("GameMenu");
            gameManager.StopGame();
        }
        else
        {
            BackToGame();
        }
    }
    public void HandleBackButton()
    {
        BackToGame();
    }

    public void HandleLeaveButton()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().EndGame();
    }


    private void BackToGame()
    {
        uiManager.DeActivateUI("GameMenu");
        gameManager.StartGame();
    }

}
