using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float time;
    public Vector2 connectionLine;
    public bool collision;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Gekapslete If, damit nicht immer ein Fehler bei der Suche nach einem SpriteRenderer entsteht.
            if (other.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder == transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder)
            {
                collision = true;
                StartCoroutine(MovePlayers());

                connectionLine = transform.position - other.transform.position;

                transform.Translate(connectionLine.normalized * (speed * Time.deltaTime));
            }
        }
    }

    IEnumerator MovePlayers()
    {
        yield return new WaitForSeconds(time);
        collision = false;
    }
}
