using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    [Range(1, 4)]
    public int MyType;
    public float radiusOfPatrolling;
    public float radiusOfSeeing;
    public float attackingCD;
    public float health;
    public int myValue;
    public AudioClip myCipDmg;

    [Header("Fields")]
    public LayerMask playerLayer;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    public GameObject attack;
    private Animator anim;
    public float meleeDamage;
    public GameObject bullet;
    public GameObject rocket;
    public GameObject myMoney;
    public GameObject myDeathParticles;
    public GameObject audioHandler;

    [Header("Debug")]
    private Vector2 patrollingPoint;
    private bool isAttacking;
    private bool attackedSecondAgo;
    private bool attackedSecondAgoDef;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        patrollingPoint = transform.position;
        gameObject.GetComponent<soundManager>().PlaySoundSecond(2, 1f, 1f, 0.5f);
    }

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
    void attackBuffer()
    {
        attackedSecondAgo = false;
        attackedSecondAgoDef = false;
    }
    void Update()
    {
        if (attackedSecondAgo)
        {
            if (attackedSecondAgoDef)
            {
                LookAt(GameObject.FindGameObjectWithTag("Defender").transform.position);
            }
            else
            {
                LookAt(GameObject.FindGameObjectWithTag("Car").transform.position);
            }
        }
        else
        {
            LookAt(agent.destination);
        }
        if (boolCanAttack())
        {
            agent.SetDestination(transform.position);
            attackedSecondAgo = true;
            attackedSecondAgoDef = colliderCanAttack().tag == "Defender";
            CancelInvoke(nameof(attackBuffer));
            Invoke(nameof(attackBuffer), 0.5f);

            GetComponent<dieInTIme>().Start();
            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log(1);
                InvokeRepeating(nameof(Attack), 0, attackingCD);
            }
        }
        else
        {
            LookAt(agent.destination);
            CancelInvoke(nameof(Attack));
            isAttacking = false;
            if (WeSeePlayer())
            {
                GetComponent<dieInTIme>().Start();
                agent.SetDestination(WeSeePlayer().transform.position);
            }
            else
            {
                agent.SetDestination(patrollingPoint);
                if (WeAreCloseToPatrollingPoint())
                {
                    Patroll();
                }
            }

            if (rb.velocity.magnitude > 0.2f)
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Hitting", false);
                anim.SetBool("Shooting", false);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Hitting", false);
                anim.SetBool("Shooting", false);
            }
        }
    }

    public void Attack()
    {
        if (MyType == 1 || MyType == 2 || MyType == 3)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Hitting", false);
            anim.SetBool("Shooting", true);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Hitting", true);
            anim.SetBool("Shooting", false);
        }

        switch (MyType)
        {
            case 1:
                SummonBulletWithSpread();
                break;

            case 2:
                SummonBullet();
                break;

            case 3:
                SummonRocket();
                break;
            case 4:
                Hit();
                break;
        }
    }

    private void Hit()
    {
        //animation does it by itself, but we can add more functionality here
    }

    private void SummonBulletWithSpread()
    {
        GameObject newBullet = Instantiate(bullet, attack.transform.position, new Quaternion(attack.transform.rotation.x, attack.transform.rotation.y, attack.transform.rotation.z + Random.Range(-0.3f, 0.3f), attack.transform.rotation.w));
        newBullet.GetComponent<bulletScript>().damage = meleeDamage;
        newBullet.GetComponent<bulletScript>().fromEnemy = true;
    }

    private void SummonBullet()
    {
        GameObject newBullet = Instantiate(bullet, attack.transform.position, new Quaternion(attack.transform.rotation.x, attack.transform.rotation.y, attack.transform.rotation.z, attack.transform.rotation.w));
        newBullet.GetComponent<bulletScript>().damage = meleeDamage;
        newBullet.GetComponent<bulletScript>().fromEnemy = true;
    }

    private void SummonRocket()
    {
        GameObject newBullet = Instantiate(rocket, attack.transform.position, new Quaternion(attack.transform.rotation.x, attack.transform.rotation.y, attack.transform.rotation.z, attack.transform.rotation.w));
        newBullet.GetComponent<bulletScript>().damage = meleeDamage;
        newBullet.GetComponent<bulletScript>().fromEnemy = true;
    }

    bool WeAreCloseToPatrollingPoint()
    {
        return Vector2.Distance(transform.position, patrollingPoint) <= agent.stoppingDistance + 1;
    }

    public void TakeDamage(float dmg)
    {
        GetComponent<soundManager>().PlaySound(0, 0.9f, 1.3f);

        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SpawnMoney();
        //GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().enemyKilled();
        Destroy(gameObject);
        gameObject.GetComponent<soundManager>().PlaySound(1, 0.3f, 1f);
    }

    public void SpawnMoney()
    {
        GameObject newMoney = Instantiate(myMoney, transform.position, Quaternion.identity);
        newMoney.GetComponent<flyingMoney>().myValue = myValue;
    }

    public void SpawnParticles()
    {
        Instantiate(myDeathParticles, transform.position, Quaternion.identity);
    }
    private void OnDestroy()
    {
        if (health <= 0)
            GameObject.FindWithTag("EpochManager").GetComponent<EpochManager>().killedShadows += 1;
    }
    bool boolCanAttack()
    {
        switch (MyType)
        {
            case 1:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 2:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 3:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 4:
                return Physics2D.Raycast(transform.position, transform.up, 1f, playerLayer).collider;
            default:
                return false;
        }
    }

    Collider2D colliderCanAttack()
    {
        switch (MyType)
        {
            case 1:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 2:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 3:
                return Physics2D.Raycast(transform.position, transform.up, 5, playerLayer).collider;
            case 4:
                return Physics2D.Raycast(transform.position, transform.up, 1f, playerLayer).collider;
            default:
                return Physics2D.Raycast(transform.position, transform.up, 1f, playerLayer).collider;
        }
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
