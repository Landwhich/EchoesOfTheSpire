using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRadius = 8f; // Distance at which the enemy starts chasing
    private bool isChasing = false;

    private EnemyAttack enemyAttack;

    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Start chasing when the player is close
        if (distanceToPlayer <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }

        if (enemyAttack != null)
        {
            enemyAttack.TryAttackPlayer();
        }
    }

    void MoveTowardsPlayer()
{
    if (player == null) return;

    // Corrected target position (keeping Y unchanged)
    Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

    // Move towards the player
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    // Make the enemy face the player
    transform.LookAt(targetPosition);
}
}
