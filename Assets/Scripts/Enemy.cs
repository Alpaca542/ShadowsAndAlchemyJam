using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    [Range(1, 3)]
    public int MyType;
    public float radiusOfPatrolling;
    public float radiusOfSeeing;
    public float attackingCD;

    [Header("Fields")]
    public LayerMask playerLayer;
    private NavMeshAgent agent;
    private Rigidbody2D rb;

    [Header("Debug")]
    private Vector2 patrollingPoint;
    private bool isAttacking;

    void LookAt(Vector3 target)
    {
        if (transform.position != target)
        {
            Vector3 diff = target - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    void Update()
    {
        LookAt(agent.destination);
        if (CanAttack())
        {
            if (!isAttacking)
            {
                isAttacking = true;
                InvokeRepeating(nameof(Attack), 0, attackingCD);
            }
        }
        else
        {
            isAttacking = false;
            if (WeSeePlayer())
            {
                agent.SetDestination(WeSeePlayer().transform.position);
            }
            else
            {
                if (WeAreCloseToPatrollingPoint())
                {
                    Patroll();
                }
            }
        }

    }

    public void Attack()
    {
        Debug.Log("fuck you (psychological attack epta)");
    }

    bool WeAreCloseToPatrollingPoint()
    {
        return Vector2.Distance(transform.position, patrollingPoint) <= agent.stoppingDistance + 1;
    }

    bool CanAttack()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    Collider2D WeSeePlayer()
    {
        return Physics2D.OverlapCircle(transform.position, radiusOfSeeing, playerLayer);
    }

    void Patroll()
    {
        Vector2 newPos = new Vector2(Random.Range(transform.position.x - radiusOfPatrolling, transform.position.x + radiusOfPatrolling), Random.Range(transform.position.y - radiusOfPatrolling, transform.position.y + radiusOfPatrolling));
        patrollingPoint = newPos;
        agent.SetDestination(newPos);
    }
}
