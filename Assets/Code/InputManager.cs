using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameMenu gameMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenu.HandleEscKeyPress();
        }
    }
}