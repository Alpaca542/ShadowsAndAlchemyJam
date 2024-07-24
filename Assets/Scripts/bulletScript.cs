using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float damage;
    public bool fromEnemy;
    public bool rocket;

    public GameObject myDeathParticles;

    private void Start()
    {
        Invoke(nameof(DieInTime), 3f);
        GetComponent<Rigidbody2D>().AddForce(transform.up * 1000);
    }

    void DieInTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "bg" && other.tag != "bullet")
        {
            if (rocket)
            {
                foreach (Collider2D gmb in Physics2D.OverlapCircleAll(transform.position, 3))
                {
                    if (gmb.tag == "Defender" && fromEnemy)
                    {
                        gmb.GetComponent<DefenderScript1>().TakeDamage(damage);
                    }
                    if (gmb.tag == "Enemy" && !fromEnemy)
                    {
                        gmb.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                if (other.tag == "Defender" && fromEnemy)
                {
                    other.GetComponent<DefenderScript1>().TakeDamage(damage);
                }
                else if (other.tag == "Enemy" && !fromEnemy)
                {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            if (!(other.tag == "Defender" && !fromEnemy) || !(other.tag == "Enemy" && fromEnemy))
            {
                Instantiate(myDeathParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
