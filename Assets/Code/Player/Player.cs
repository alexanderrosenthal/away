using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;

    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private char playerType = 'A';
    
    
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVec = GetInput();
        MovePlayer(inputVec);
        // AnimatePlayer(inputVec);
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }

    private void MovePlayer(Vector2 direction)
    {
        myRb.velocity = direction.normalized * (playerSpeed * Time.deltaTime);
    }
    private void AnimatePlayer(Vector2 direction)
    {
        throw new NotImplementedException();
    }
}
