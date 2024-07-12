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
    
    
    private void calculateWindDirection()
    {
        
        // Calculate wind direction
        return float windDirection = Random.Range(0 , 180);
    }
}
