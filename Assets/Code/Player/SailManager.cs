using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailManager : MonoBehaviour
{
    [SerializeField] private char playerType = 'A';

    [SerializeField] private GameObject sail;
    [SerializeField] private float rotationSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        Vector2 input = GetInput();
        MoveSail(input);

    }
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw($"{playerType} Horizontal"), 
            Input.GetAxisRaw($"{playerType} Vertical"));
    }
    private void MoveSail(Vector2 direction)
    {
        sail.transform.Rotate(Vector3.forward * direction.x * rotationSpeed * Time.deltaTime);
    }
}
