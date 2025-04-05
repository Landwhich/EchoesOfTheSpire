using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRadius = 8f;
    public int attackDamage = 8;
    public float attackCooldown = 2f;

    private Transform player;
    private float lastAttack = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        lastAttack += Time.deltaTime;
    }

    bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= attackRadius;
    }

    void Attack()
    {
        Debug.Log("Attacking Player. Damage: " + attackDamage);
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);
    }

    public void TryAttackPlayer()
    {
        if (player != null && IsPlayerInAttackRange() && lastAttack >= attackCooldown)
        {
            Debug.Log("Enemy is attacking the player"); Attack();
            lastAttack = 0f;
        }
    }
}