using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] public bool isHit;
    [SerializeField] private float collissionEnergy;
    [SerializeField] private float durationOfHit;
    private float calculatedForce;
    private float calculatedDuration;
    private GameObject hitObj;
    private Vector2 direction;
    [SerializeField] private PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (isHit) MovePlayer();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Triggered by " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            HandleHit(other, collissionEnergy, durationOfHit);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            HandleHit(other, 2, durationOfHit);
        }
    }


    public void HandleHit(Collider2D other, float inputForce, float inputDuration)
    {
        LeaveStation();

        calculatedForce = inputForce;
        calculatedDuration = inputDuration;

        transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        isHit = true;
        StartCoroutine(HitPlayerTimer());

        hitObj = other.gameObject;
        direction = transform.position - hitObj.transform.position;
        direction.Normalize();
    }

    private void LeaveStation()
    {
        //Spieler aus der Station werfen
        if (playerController.onStation)
        {
            GameObject currentStation = playerController.currentStation;
            for (int i = 0; i < currentStation.transform.childCount; i++)
            {
                Transform child = currentStation.transform.GetChild(i);

                if (child.name.Contains("Trigger"))
                {
                    child.GetComponent<StationManager>().LeaveStation(playerController);
                }
            }
        }
    }

    private void MovePlayer()
    {
        transform.Translate(direction * (calculatedForce * Time.deltaTime));
    }


    ///NICHT VERBAUT!!!!!!!!!!!!!!!!!!
    private IEnumerator HandleAnimation()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        playerController.blockGeneralAnimation = true;

        PlayerAnimationManager playerAnimationManager = transform.GetChild(0).GetComponent<PlayerAnimationManager>();
        float animationDuration = playerAnimationManager.GetAnimationDuration("Falling");
        playerAnimationManager.ChangeAnimation("Falling");

        yield return new WaitForSeconds(animationDuration);
        playerController.blockGeneralAnimation = false;
    }



    IEnumerator HitPlayerTimer()
    {
        yield return new WaitForSeconds(calculatedDuration);
        isHit = false;
    }
}