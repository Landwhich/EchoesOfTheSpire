using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int heal = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if(playerHealth.curHealth < playerHealth.maxHealth)
                {
                    if(playerHealth.curHealth >= 90)
                        heal = 10;
                    playerHealth.Heal(heal);
                    Destroy(gameObject);
                }
            }
        }
    }
}
