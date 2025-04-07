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
    public Vector3 healthRotation = new Vector3(0, 0, 0); // Customize this if needed

    private Transform player;
    private bool hasInteracted = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
    }

    private void Update()
    {
        if (player == null || hasInteracted) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnEnemy();
                hasInteracted = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnHealth();
                hasInteracted = true;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (EnemyModel != null)
        {
            GameObject enemy = Instantiate(EnemyModel, transform.position, transform.rotation);
            Debug.Log("Enemy spawned");
            ReplaceButtonWith(enemy);
        }
    }

    private void SpawnHealth()
    {
        if (HealthModel != null)
        {
            Quaternion correctedRotation = Quaternion.Euler(healthRotation);
            GameObject health = Instantiate(HealthModel, transform.position, correctedRotation);
            Debug.Log("Health spawned");
            ReplaceButtonWith(health);
        }
    }

    private void ReplaceButtonWith(GameObject spawnedObject)
    {
        spawnedObject.transform.position = transform.position;
        spawnedObject.transform.rotation = transform.rotation;

        Destroy(gameObject); // removes the cube button
    }


}
