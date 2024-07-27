using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOverBoard : MonoBehaviour
{
    // private Collider2D boatCollider;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private float animationDuration = 1.55f;
    private AudioSource splashAudio;

    // Start is called before the first frame update
    void Start()
    {
        // boatCollider = GetComponent<Collider2D>();
        splashAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag(playerTag))
        {
            //  Debug.Log("Player overboard");
            RespawnPlayer(other.gameObject);
            PlayAudio();
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(RespawnCoroutine(player));
        }
    }

    private IEnumerator RespawnCoroutine(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        Animator playerAnim = player.GetComponentInChildren<Animator>();

        // Player stops moving, animation is played and player is set inactive after animationDuration
        playerController.inWater = true;
        playerAnim.SetBool("IsWater", true);
        yield return new WaitForSeconds(animationDuration);
        player.SetActive(false);

        // Respawn player after respawnTime
        yield return new WaitForSeconds(respawnTime);
        playerAnim.SetBool("IsWater", false);
        playerController.inWater = false;
        player.transform.position = respawnPoint.transform.position;
        player.SetActive(true);
    }

    private void PlayAudio()
    {
        splashAudio.Play();
    }
}
