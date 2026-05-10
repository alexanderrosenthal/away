using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;
    public char playerType = 'A';
    [SerializeField] public bool blockGeneralAnimation = false;
    [SerializeField] private bool isWalking = false;
    [SerializeField] public bool onStation = false;
    [SerializeField] public bool usingStation = false;
    [SerializeField] public bool inWater = false;
    public GameObject currentStation;
    [SerializeField] private Vector2 inputVec;
    [SerializeField] public GameObject playerSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public PlayerAnimationManager playerAnimationManager;

    public float lookingAngle;

    private bool interactPressed;
    private bool interactConsumed;

    public void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    public void OnInteract(InputValue value)
    {
        interactPressed = value.isPressed;
        interactConsumed = false;
    }

    public bool ConsumeInteract()
    {
        if (interactPressed && !interactConsumed)
        {
            interactConsumed = true;
            return true;
        }

        return false;
    }

    public Vector2 GetInputVector()
    {
        return inputVec;
    }

    public bool InteractPressed()
    {
        return interactPressed;
    }
    void Update()
    {
        if (GameManager.isGamePaused) return;

        isWalking = inputVec.x != 0f || inputVec.y != 0f;

        isWalking = inputVec.sqrMagnitude > 0.01f;

        if (onStation || inWater)
        {
            inputVec = Vector2.zero;
            isWalking = false;
        }
        else
        {
            RotatePlayer();
            MovePlayer();
        }

        AnimatePlayer();
    }

    private void RotatePlayer()
    {
        if (inputVec != Vector2.zero)
        {
            lookingAngle = Mathf.Atan2(-inputVec.x, inputVec.y);
        }

        Quaternion boatRotation = transform.parent.rotation;
        playerSprite.transform.rotation = Quaternion.Euler(0, 0, lookingAngle * Mathf.Rad2Deg) * boatRotation;
    }

    private void MovePlayer()
    {
        transform.Translate(inputVec.normalized * (playerSpeed * Time.deltaTime));
    }

    private void AnimatePlayer()
    {
        if (!blockGeneralAnimation)
        {
            // MOVE
            if (isWalking)
            {
                playerAnimationManager.ChangeAnimation("Move");
            }
            // OVERBOARD
            else if (inWater)
            {
                playerAnimationManager.ChangeAnimation("Water");
            }
            else if (onStation)
            {
                // OAR
                if (currentStation.name == "OarLeft" || currentStation.name == "OarRight" ||
                    currentStation.name == "Rudder" || currentStation.name == "Sail")
                {
                    if (usingStation == true)
                    {
                        return;
                    }
                    else
                    {
                        IdleOnStation();
                    }
                }
                else
                {
                    IdleOnStation();
                }
            }
            // IDLE
            else
            {
                playerAnimationManager.ChangeAnimation("Idle1");
            }
        }
    }

    private void IdleOnStation()
    {
        string neededIdleAnimation = currentStation.name + "Idle";
        playerAnimationManager.ChangeAnimation(neededIdleAnimation);
    }
}