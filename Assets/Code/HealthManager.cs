using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static int health = 5;

    public static List<GameObject> heartList = new List<GameObject>();

    private void Start() 
    {

        for(int i = 0; i < GameObject.Find("Health").transform.childCount; i++)
            {
                GameObject Go = GameObject.Find("Health").transform.GetChild(i).gameObject;
                heartList.Add(Go);
            }
    }

    public static void ModifyHealth(int amount)
    {
        // Modify the static health variable
        health += amount;

        // Ensure health does not go below 0
        if (health < 0) 
        {
            health = 0;
        }
        UpdateHealth();
    }

    private static void UpdateHealth()
    {
        int healthcount = health;

        print("1 healthcount" + healthcount);

        foreach(GameObject heart in heartList)
        {
            print(healthcount);

            if(healthcount!=0) 
            {   
            print("2 healthcount" + healthcount); 

                if(heart.activeSelf == true) 
                {
                    heart.SetActive(false);
                }
            }
            healthcount = healthcount-1;
        }
    }
}
