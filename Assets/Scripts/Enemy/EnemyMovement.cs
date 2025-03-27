using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 targetPos;
    private EnemyAttack enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = pointB.position;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        if(enemyAttack != null)
            enemyAttack.TryAttackPlayer();
    }

    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (transform.position == targetPos)
        {
            if (targetPos == pointA.position)
                targetPos = pointB.position;
            else
                targetPos = pointA.position;
        }
    }
}
