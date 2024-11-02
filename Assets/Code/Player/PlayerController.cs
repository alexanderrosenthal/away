using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;
    public char playerType = 'A';
    [SerializeField] private bool isWalking = false;
    public bool onStation = false;
    public bool inWater = false;
    public GameObject currentStation;
    [SerializeField] private Vector2 inputVec;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator myAnimator;

    private float lookingAngle;
    // Update is called once per frame
    void Update()
    {
        isWalking = inputVec.x != 0f || inputVec.y != 0f;

        if (onStation || inWater)
        {
            inputVec.x = 0; // TODO ANIMATION
            inputVec.y = 0;
            // transform.position = currentStation.transform.position; // TODO quick workaround for the rigidbody problem
        }
        else
        {
            inputVec = GetInput();

            RotatePlayer();
            MovePlayer();
            AnimatePlayer();
        }
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

        // Set the rotation of the object
        playerSprite.transform.rotation = Quaternion.Euler(0, 0, lookingAngle * Mathf.Rad2Deg);
        // transform.rotation = Quaternion.LookRotation(inputVec, Vector3.back);
    }
    private void MovePlayer()
    {
        // myRb.velocity = boatRb.velocity + inputVec.normalized * (100f * playerSpeed * Time.deltaTime);
        transform.Translate(inputVec.normalized * (playerSpeed * Time.deltaTime));
    }
    private void AnimatePlayer()
    {
        myAnimator.SetBool("isMoving", isWalking);
    }

    //Use cor-Values to adjust position if needed for special Station
    public void PlacePlayerInStation(bool changeAlsoSprite)
    {
        bool placementFound = false;
        foreach (Transform child in currentStation.transform.parent)
        {
            if (child.name == "PlayerPlacement")
            {
                playerSprite.transform.position = child.position;                

                playerSprite.transform.rotation = child.rotation;

                //Falls für die Animation eine größere/andere Sprite genutzt wird, kann hier die Position korrigiert werden.
                if (changeAlsoSprite)
                {                 
                    if (onStation)
                    {                
                        playerSprite.transform.position = playerSprite.transform.position;  
                    }
                    else
                    {
                        playerSprite.transform.localPosition = Vector3.zero;
                    }
                }

                placementFound = true;
            }
        }
        if (placementFound == false)
        {
            Debug.Log("No PlayerPlacement on " + currentStation);
        }
    }
}