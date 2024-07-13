using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;

    [SerializeField] private Rigidbody2D myRb; // TODO settle for RB or no RB solution
    [SerializeField] private Rigidbody2D boatRb;
    public char playerType = 'A';
    // public PlayerState playerState;
    public bool onStation = false;
    public GameObject currentStation;
    [SerializeField] private Vector2 inputVec;
    // Update is called once per frame
    void Update()
    {
        if (onStation)
        {
            inputVec.x = 0; // TODO ANIMATION
            inputVec.y = 0;
        }
        else
        {
            inputVec = GetInput();
        }

        RotatePlayer();
        MovePlayer();
        // TODO ANIMATION AnimatePlayer(inputVec);
        
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }

    private void RotatePlayer()
    {
        
        // transform.rotation = Quaternion.LookRotation();
    }
    private void MovePlayer()
    {
        myRb.velocity = boatRb.velocity + inputVec.normalized * (100f * playerSpeed * Time.deltaTime);
        // transform.Translate(inputVec.normalized * (playerSpeed * Time.deltaTime));
    }
    private void AnimatePlayer(Vector2 direction)
    {
        throw new NotImplementedException();
    }
  

    void EnterStation()
    {
        
    }

    void ExitStation()
    {
        
    }
}
