using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject playerA;
    [SerializeField] private GameObject playerB;
    [SerializeField] private float collisionDistance;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    public Vector3 connectionLine;
    public float distance;
    public bool collision;


    // Update is called once per frame
    void Update()
    {
        connectionLine = playerA.transform.position - playerB.transform.position;
        distance = connectionLine.magnitude;
        if (distance < collisionDistance)
        {
            collision = true;
            StartCoroutine(MovePlayers());
        }

        if (collision)
        {
            playerA.transform.Translate(connectionLine.normalized * (speed * Time.deltaTime));
            playerB.transform.Translate(-connectionLine.normalized * (speed * Time.deltaTime));
        }
        // StartCoroutine(MovePlayers());

    }

    IEnumerator MovePlayers()
    {
        
        yield return new WaitForSeconds(time);
        collision = false;
    }
}
