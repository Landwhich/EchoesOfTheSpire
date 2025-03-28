using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to PlayerHealth script
    public Image healthBarFill; // Reference to the HealthBar fill image

    void Start()
    {
        // Get the PlayerHealth component from the player object
        if (playerHealth == null)
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        if (healthBarFill == null)
            healthBarFill = transform.Find("BarFill").GetComponent<Image>(); // Adjust if BarFill is named differently
    }

    void Update()
    {
        // Update the health bar based on player's current health
        if (playerHealth != null && healthBarFill != null)
        {
            float healthPercentage = (float)playerHealth.curHealth / (float)playerHealth.maxHealth;
            healthBarFill.fillAmount = healthPercentage; // Set the fill amount based on health percentage
        }
    }
}
