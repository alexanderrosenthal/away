using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls _controls; // Angenommen, deine generierte Klasse heißt "PlayerControls"

    public InputAction escAction; // Ziehe die Action aus dem Input Action Asset hierher

    [SerializeField] private GameMenu gameMenu;

    void Awake()
    {
        _controls = new PlayerControls();
    }

    void OnEnable()
    {
        _controls.Enable();
        _controls.Main.Esc.performed += OnEsc; // "Esc" ist der Name deiner Action im Input Action Asset
    }

    void OnDisable()
    {
        _controls.Main.Esc.performed -= OnEsc;
        _controls.Disable();
    }

    void OnEsc(InputAction.CallbackContext context)
    {
        gameMenu.HandleEscKeyPress();
    }
}