using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public float windDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    float calculateWindDirection()
    {
        // Calculate wind direction
        windDirection = Random.Range(0 , 180);
        return windDirection;
    }
}
