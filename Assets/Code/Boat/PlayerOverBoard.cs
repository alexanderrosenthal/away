using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverBoard : MonoBehaviour
{
    private Collider2D boatCollider;
    [SerializeField] private string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        boatCollider = GetComponent<Collider2D>();
        if (boatCollider == null)
        {
            Debug.LogError("Boat collider not found");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player overboard");
        }
    }

    // Update is called once per frame

}
