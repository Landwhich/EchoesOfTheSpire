using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject EnemyModel;
    public Transform spawn;
    public float interactionRange = 5f;
    public string playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SpawnEnemy();
            }
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
}
