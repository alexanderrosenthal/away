using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public enum PlayerState {
        walking,
        rudder,
        sail
    }
    
    
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;

    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private char playerType = 'A';
    public PlayerState playerState;
    public bool onStation = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVec = GetInput();
        if (playerState == PlayerState.walking)
        {
            MovePlayer(inputVec);
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Station")) return;
        Debug.Log(other.gameObject.name);
    }
}
