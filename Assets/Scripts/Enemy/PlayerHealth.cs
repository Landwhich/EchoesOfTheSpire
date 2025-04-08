using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Needed for UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Max Health
    public int curHealth;        // Current Health
    public GameObject gameOverPanel; // Game over UI panel

    // UI Elements for the hearts
    public Transform heartContainer;  // The container that will hold the heart images
    public GameObject fullHeartPrefab;  // Full heart prefab
    public GameObject halfHeartPrefab;  // Half heart prefab
    public GameObject emptyHeartPrefab; // Empty heart prefab

    void Start()
    {
        curHealth = maxHealth;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHealthBar();  // Set the initial health UI
    }

    void UpdateHealthBar()
    {
        // Clear the previous hearts
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // Calculate how many full, half, and empty hearts we need
        int fullHearts = Mathf.FloorToInt(curHealth / 20);  // Full hearts (each representing 20 health)
        int halfHearts = Mathf.FloorToInt((curHealth % 20) / 10);  // Half hearts (each representing 10 health)
        int emptyHearts = 5 - (fullHearts + halfHearts);  // Empty hearts

        // Instantiate full hearts
        for (int i = 0; i < fullHearts; i++)
        {
            Instantiate(fullHeartPrefab, heartContainer);
        }

        // Instantiate half hearts
        for (int i = 0; i < halfHearts; i++)
        {
            Instantiate(halfHeartPrefab, heartContainer);
        }

        // Instantiate empty hearts
        for (int i = 0; i < emptyHearts; i++)
        {
            Instantiate(emptyHeartPrefab, heartContainer);
        }
    }

    void Die()
    {
        Debug.Log("Dead");

        var movement = GetComponent<PlayerControls>();
        if (movement != null)
            movement.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        if (curHealth < 0)
        {
            curHealth = 0;
            Die();
        }
        Debug.Log("Player Health: " + curHealth);
        UpdateHealthBar();  // Update health bar (hearts) when damage is taken
    }

    public void Heal(int amountHeal)
    {
        if (curHealth == maxHealth)
            return;

        curHealth += amountHeal;
        if (curHealth > maxHealth)
            curHealth = maxHealth;

        UpdateHealthBar();  // Update health bar (hearts) when healing
    }
}
