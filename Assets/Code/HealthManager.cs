using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    private static TextMeshProUGUI healthText;
    public static int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        healthText = this.gameObject.GetComponent<TextMeshProUGUI>();
        healthText.text = "Health: " + health;
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
        UpdateHealthText();
    }
    private static void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }
}
