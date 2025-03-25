using System.Collections;
using UnityEngine;

public class OarManager : StationManager
{
    [Header("OarManager:")]
    [SerializeField] private float strength = 1f;
    [SerializeField] private string sideOfRow = "";
    [SerializeField] private float animationDuration;
    [SerializeField] private Rigidbody2D boatRb;
    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private GameObject forcePoint;
    [SerializeField] private AudioSource splashAudio;
    [SerializeField] private string AnimationStateName;

    // TODO rowing doesn't stop if person falls off

    public override void Start()
    {
        base.Start();

        if (stationPosition == 1)
        {
            sideOfRow = "Left";
        }
        else if (stationPosition == 2)
        {
            sideOfRow = "Right";
        }
        else
        {
            Debug.Log("No stationPosition set");
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (!onStation) return;
        if (input.y == 0) return;
        if (playerController.usingStation) return;
        playerController.usingStation = true;

        if (boatMovement.boatState != BoatState.AtTarget)
        {
            StartCoroutine(RudderStroke());
        }
    }

    private IEnumerator RudderStroke()
    {
        splashAudio.Play();

        if (input.y > 0)
        {
            AnimationStateName = "Row" + sideOfRow + "Row";
            HandleAnimation();

            yield return new WaitForSeconds(animationDuration / 2);

            boatRb.AddForceAtPosition(boatRb.transform.up * strength, forcePoint.transform.position);

        }
        else
        {
            AnimationStateName = "Row" + sideOfRow + "Back";
            HandleAnimation();

            yield return new WaitForSeconds(animationDuration / 2);
            boatRb.AddForceAtPosition(-boatRb.transform.up * strength, forcePoint.transform.position);
        }


        playerController.usingStation = false;
    }

    private void HandleAnimation()
    {
        animationDuration = playerController.playerAnimationManager.GetAnimationDuration(AnimationStateName);
        playerController.playerAnimationManager.ChangeAnimation(AnimationStateName);
    }
}