using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWindManager : MonoBehaviour
{
    [SerializeField] private Transform windSource;
    [SerializeField] private float minDelaySeconds;
    [SerializeField] private float maxDelaySeconds;
    // [SerializeField] private float minWindAngle;
    // [SerializeField] private float maxWindAngle;
    [SerializeField] private float WindChangeAngle;
    // [SerializeField] private float windAngle;
    [SerializeField] private float newWindAngle;
    [SerializeField] private float changeSeconds;
    public bool changing;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // windAngle = getAngle();
        StartCoroutine(ChangeWindDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
        RotateWind();
        }
    }

    private float getAngle()
    {
        return windSource.rotation.eulerAngles.z;
    }

    private IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            int isRight = Random.Range(0, 2);
            newWindAngle = getAngle() + (isRight == 1? WindChangeAngle : -WindChangeAngle);
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
        changing = true;
        float timer = 0;
        float currentAngle = getAngle();
        while (changing)
        {
            windSource.eulerAngles = new Vector3(0, 0, 
                Mathf.LerpAngle(currentAngle, newWindAngle, timer / changeSeconds));
            print(windSource.eulerAngles);
            timer += Time.deltaTime;
            if (timer > changeSeconds)
            {
                changing = false;
            }
            yield break;
            // Debug.Log("Rotating to " + newWindAngle);
        }
        
    }
}
