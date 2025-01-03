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
    [SerializeField] private float animationDuration = 0;
    [SerializeField] private PlayerController playerController;
    SpriteRenderer spriteRenderer;
    private int playerSortingOrder;

    private AudioSource splashAudio;

    // Start is called before the first frame update
    void Start()
    {
        splashAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log(other.gameObject.name + "overboard");
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
        playerController = player.GetComponent<PlayerController>();

        // Player stops moving, animation is played and player is set inactive after animationDuration
        playerController.inWater = true;

            //Anpassen des Sprite Render "Order in Layer";
            spriteRenderer = playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
            playerSortingOrder = spriteRenderer.sortingOrder;
            if (playerSortingOrder == spriteRenderer.sortingOrder)
            {
                spriteRenderer.sortingOrder -= 3;
            }

        animationDuration = playerController.playerAnimationManager.GetAnimationDuration("Water");

        yield return new WaitForSeconds(animationDuration);
        player.SetActive(false);

        // Respawn player after respawnTime
        yield return new WaitForSeconds(respawnTime);

        //Reset Values
        spriteRenderer.sortingOrder += 3;
        playerController.inWater = false;
        player.SetActive(true);

        player.transform.position = respawnPoint.transform.position;
    }


    private void PlayAudio()
    {
        splashAudio.Play();
    }
}
