using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private float playerSpeed = 1;

    [SerializeField] private Rigidbody2D myRb;
    [SerializeField] private char playerType = 'A';
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void movePlayer()
    {
        Vector2 inputVec = new Vector2(Input.GetAxis($"{playerType} Horizontal"), 
            Input.GetAxis($"{playerType} Vertical"));
        Debug.Log(inputVec);
    }
}
