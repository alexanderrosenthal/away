using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOverBoard : MonoBehaviour
{
    // private Collider2D boatCollider;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject respawnPositionParent;
    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private float animationDuration = 0;
    //[SerializeField] private PlayerController playerController;
    SpriteRenderer spriteRenderer;

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
        PlayerController playerController = player.GetComponent<PlayerController>();

        // Player stops moving, animation is played and player is set inactive after animationDuration
        playerController.inWater = true;

        //Anpassen des Sprite Render "Order in Layer";
        SpriteRenderer localRenderer = playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        localRenderer.sortingOrder = 1;

        animationDuration = playerController.playerAnimationManager.GetAnimationDuration("Water");

        yield return new WaitForSeconds(animationDuration);
        player.SetActive(false);

        // Respawn player after respawnTime
        yield return new WaitForSeconds(respawnTime);

        //Reset Values
        localRenderer.sortingOrder = 4;
        playerController.inWater = false;
        player.SetActive(true);

        GameObject respawnPosition = SearchPlayerRespawnPosition(playerController);
        player.transform.position = respawnPosition.transform.position;
    }

    GameObject SearchPlayerRespawnPosition(PlayerController playerController)
    {
        foreach (Transform child in respawnPositionParent.transform)
        {
            if (child.name.Contains(playerController.playerType))
            {
                return child.gameObject;
            }
        }

        Debug.LogWarning($"Kein Respawn-Point für Spielertyp '{playerController.playerType}' gefunden.");
        return null;
    }

    private void PlayAudio()
    {
        splashAudio.Play();
    }
}
