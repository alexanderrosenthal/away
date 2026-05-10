using UnityEngine;

public class PlayerJoinScript : MonoBehaviour
{
    public Transform SpawnPoint1, SpawnPoint2;
    public GameObject PlayerA, PlayerB;
    public Transform PlayerParent;

    private void Awake()
    {
        GameObject playerAInstance = Instantiate(
            PlayerA,
            SpawnPoint1.position,
            SpawnPoint1.rotation,
            PlayerParent
        );

        playerAInstance.name = "PlayerA";
        playerAInstance.GetComponent<PlayerController>().playerType = 'A';


        GameObject playerBInstance = Instantiate(
            PlayerB,
            SpawnPoint2.position,
            SpawnPoint2.rotation,
            PlayerParent
        );

        playerBInstance.name = "PlayerB";
        playerBInstance.GetComponent<PlayerController>().playerType = 'B';
    }
}