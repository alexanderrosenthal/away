using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [Header("Player Stuff")] [SerializeField]
    private float playerSpeed = 1;
    public char playerType = 'A';
    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private Rigidbody2D boatRb;

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

        MovePlayer();
        // TODO ANIMATION AnimatePlayer(inputVec);

    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"),
            Input.GetAxisRaw($"{playerType} Vertical"));
    }

    private void MovePlayer()
    {
        // myRb.position = Vector3.zero;
        myRb.velocity = boatRb.velocity + inputVec.normalized * (playerSpeed * Time.deltaTime);
        // transform.Translate(inputVec.normalized * (playerSpeed * Time.deltaTime));
    }

    private void AnimatePlayer(Vector2 direction)
    {
        // throw new NotImplementedException();
    }


    void EnterStation()
    {

    }

    void ExitStation()
    {

    }

}
