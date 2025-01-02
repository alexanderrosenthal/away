using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;
    public char playerType = 'A';
    [SerializeField] private bool isWalking = false;
    public bool onStation = false;
    public bool usingStation = false;
    public bool inWater = false;
    public GameObject currentStation;
    [SerializeField] private Vector2 inputVec;
    [SerializeField] public GameObject playerSprite;
    [SerializeField] public PlayerAnimationManager playerAnimationManager;
    [SerializeField] public Animator myAnimator;

    public float lookingAngle;


    // Update is called once per frame
    void Update()
    {
        isWalking = inputVec.x != 0f || inputVec.y != 0f;

        if (onStation || inWater)
        {
            inputVec.x = 0; // TODO ANIMATION
            inputVec.y = 0;
        }
        else
        {
            inputVec = GetInput();

            RotatePlayer();
            MovePlayer();
        }

        AnimatePlayer();
    }


    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"),
            Input.GetAxisRaw($"{playerType} Vertical"));
    }

    private void RotatePlayer()
    {
        if (inputVec != Vector2.zero)
        {
            lookingAngle = Mathf.Atan2(-inputVec.x, inputVec.y);
        }

        Quaternion boatRotation = transform.parent.rotation;

        // Set the rotation of the object
        playerSprite.transform.rotation = Quaternion.Euler(0, 0, lookingAngle * Mathf.Rad2Deg) * boatRotation;
    }

    private void MovePlayer()
    {
        transform.Translate(inputVec.normalized * (playerSpeed * Time.deltaTime));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
    }

    private void AnimatePlayer()
    {
        //MOVE
        if (isWalking)
        {
            playerAnimationManager.ChangeAnimation("Move");
        }
        //OVERBOARD
        else if (inWater)
        {
            playerAnimationManager.ChangeAnimation("Water");
        }
        else if (onStation)
        {
            //OAR
            if (currentStation.name == "OarLeft" || currentStation.name == "OarRight" || currentStation.name == "Rudder")
            {
                if (usingStation == true)
                {
                    Debug.Log("usingStation = true");
                    return;
                }
                else
                {
                    Debug.Log("IdleOnStation");
                    IdleOnStation();
                }
            }
            else
            {
                IdleOnStation();
            }
        }
        //IDLE
        else
        {
            playerAnimationManager.ChangeAnimation("Idle1");
        }
    }

    private void IdleOnStation()
    {
        string neededIdleAnimation = currentStation.name + "Idle";
        playerAnimationManager.ChangeAnimation(neededIdleAnimation);
    }
}