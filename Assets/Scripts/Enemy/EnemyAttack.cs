using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRadius = 5f;
    public int attackDamage = 10;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= attackRadius;
    }

    void Attack()
    {
        Debug.Log("Attacking Player. Damage: " + attackDamage);
        //player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }

    public void TryAttackPlayer()
    {
        if (player != null && IsPlayerInAttackRange())
            Attack();
    }
}
