using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NewWindManager : MonoBehaviour
{
    [SerializeField] private Transform windSource;
    [SerializeField] private float minDelaySeconds;
    [SerializeField] private float maxDelaySeconds;
    // [SerializeField] private float minWindAngle;
    // [SerializeField] private float maxWindAngle;
    [SerializeField] private float windChangeAngle;
    // [SerializeField] private float windAngle;
    [SerializeField] private float newWindAngle;
    [SerializeField] private float changeSeconds;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        // windAngle = getAngle();
        StartCoroutine(ChangeWindDirection());
    }
    

    private float GetAngle()
    {
        return windSource.rotation.eulerAngles.z;
    }

    public Vector2 GetDirection()
    {
        // has to be negated, so the vector points where the wind is going
        return -windSource.transform.up;
    }

    private IEnumerator ChangeWindDirection()
    {
        for(;;)
        {
            int isRight = Random.Range(0, 2);
            newWindAngle = GetAngle() + (isRight == 1? windChangeAngle : -windChangeAngle);
            /*
            if (newWindAngle > maxWindAngle)
            {
                newWindAngle -= 2 * WindChangeAngle;
            }else if (newWindAngle < minWindAngle)
            {
                newWindAngle += 2 * WindChangeAngle;
            }
            */
            StartCoroutine(RotateWind());
            yield return new WaitForSeconds(Random.Range(minDelaySeconds, maxDelaySeconds));
        }
    }

    private IEnumerator RotateWind()
    {
        float timer = 0;
        float currentAngle = GetAngle();
        while (timer < changeSeconds)
        {
            windSource.eulerAngles = new Vector3(0, 0, 
                Mathf.LerpAngle(currentAngle, newWindAngle, timer / changeSeconds));
            timer += Time.deltaTime;
            yield return null;
        }
        
    }
}
