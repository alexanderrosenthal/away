using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverBoard : MonoBehaviour
{
    private Collider2D boatCollider;
    [SerializeField] private string playerTag = "Player";

    [SerializeField] private GameObject boat;
    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private float yOffset = -1f;
    private AudioSource splashAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        boatCollider = GetComponent<Collider2D>();
        splashAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            //  Debug.Log("Player overboard");
            RespawnPlayer(other.gameObject);
            PlayAudio();
        }
    }
    
    private void RespawnPlayer(GameObject player)
    {
        player.SetActive(false); // Hide the player
        StartCoroutine(RespawnCoroutine(player));
    }

    private IEnumerator RespawnCoroutine(GameObject player)
    {
        yield return new WaitForSeconds(respawnTime);
        // After waiting, move the player to the boat's position and active the player
        Vector3 newPosition = boat.transform.position + Vector3.up * yOffset;

        player.transform.position = newPosition;
        player.SetActive(true);
    }

    private void PlayAudio()
    {
        splashAudio.Play();
    }

    // Update is called once per frame

}
