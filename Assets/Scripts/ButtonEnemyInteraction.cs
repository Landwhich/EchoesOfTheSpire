using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject EnemyModel;
    public GameObject HealthModel;
    public Transform spawn;
    public float interactionRange = 16f;
    public string playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SpawnEnemy();
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SpawnHealth();
        }
    }

    private void SpawnEnemy()
    {
        if(EnemyModel != null && spawn != null)
        {
            Instantiate(EnemyModel, spawn.position, spawn.rotation);
            Debug.Log("Enemy spawn");
        }
        Destroy(gameObject);
    }

    private void SpawnHealth()
    {
        if (HealthModel != null && spawn != null)
        {
            Instantiate(HealthModel, spawn.position, spawn.rotation);
            Debug.Log("Health spawn");
        }
        Destroy(gameObject);
    }
}
