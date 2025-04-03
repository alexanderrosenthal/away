using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static int health = 5;

    public static List<GameObject> activeHeartsList = new List<GameObject>();
    public static List<GameObject> inctiveHeartsList = new List<GameObject>();

    private void Start()
    {
        //Stellt sicher, dass static Liste bei Level-Neustart geleert wird
        activeHeartsList.Clear();

        //Add all UI-Hearts to list
        for (int i = 0; i < GameObject.Find("Health").transform.childCount; i++)
        {
            GameObject Go = GameObject.Find("Health").transform.GetChild(i).gameObject;
            activeHeartsList.Add(Go);
        }

        //Be secure that this matches - Health is working only with fitting UI
        if (activeHeartsList.Count != health)
        {
            Debug.Log("Health and UI_Hearts are not matching!");
        }
    }

    public static void ModifyHealth(int amount)
    {
        // Modify the static health variable
        health += amount;
        Debug.Log("Health = " + health);

        // Ensure health does not go below 0
        if (health < 0)
        {
            health = 0;
            Debug.Log("Health = 0!");
        }

        UpdateHealthUI(amount);

        //END GAME BECAUSE OF NO HEALTH
        if (health == 0)
        {
            GameObject.Find("GameTarget").GetComponent<FinishGame>().OnShipCollission();
        }
    }

    private static void UpdateHealthUI(int amount)
    {
        //Deactivate UI-Hearts 
        for (int i = 0; i < (amount * -1); i++)
        {
            //Set UI
            activeHeartsList[i].SetActive(false);

            //Handle Lists
            inctiveHeartsList.Add(activeHeartsList[i]);
            activeHeartsList.Remove(activeHeartsList[i]);
        }
    }
}
