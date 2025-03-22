using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    private GameObject boat;
    private GameObject finish;
    [SerializeField] private RectTransform compassObject;

    private void Start()
    {
        boat = GameObject.Find("Boat");
        finish = GameObject.Find("finishCompassPosition");
    }

    void Update()
    {
        // Berechne die Richtung vom Boot zum Ziel
        Vector3 dir = finish.transform.position - boat.transform.position;

        // Berechne den Winkel zur Ausrichtung des CompassObjects
        float angle = Vector2.SignedAngle(Vector2.up, dir);

        // Berücksichtige die aktuelle Rotation des Boots
        float boatRotation = boat.transform.eulerAngles.z;

        // Kombiniere den Winkel der Richtung mit der Rotation des Boots
        float finalAngle = angle - boatRotation;

        // Setze die Rotation des CompassObjects
        compassObject.rotation = Quaternion.Euler(0, 0, finalAngle);
    }
}