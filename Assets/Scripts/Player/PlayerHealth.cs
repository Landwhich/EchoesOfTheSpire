using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int curHealth;

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
    }

    public void Heal(int amountHeal)
    {
        curHealth += amountHeal;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
    }
}
