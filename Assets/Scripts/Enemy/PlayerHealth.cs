using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("Dead");
        gameObject.SetActive(false);
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

        if (damage > 0)
        {
            Debug.Log("Player took " + damage + " damage. Current Health: " + curHealth);
        }
    }

    public void Heal(int amountHeal)
    {
        if (curHealth == maxHealth)
            return;
        else
        {
            curHealth += amountHeal;
            if (curHealth > maxHealth)
                curHealth = maxHealth;
        }
    }
}