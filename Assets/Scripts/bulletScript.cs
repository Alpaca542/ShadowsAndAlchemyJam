using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool fromEnemy;
    public bool rocket;
    public float Push;
    public GameObject myDeathParticles;


    public int MyType;
    /* 
    0-RED - simple
    1-GREEN - simple
    2-BLUE - simple
    3-PURERED - machine gun
    4-redAndBlue - shotgun
    5-GreenAndBlue - rocket
    6-graphed - goes through
    7-Analyzed - deals knockback
    8-white - destroys pedestals, rocket
    */

    private void Start()
    {
        Invoke(nameof(DieInTime), 3f);
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
    void DieInTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "bg" && other.tag != "bullet")
        {
            if (MyType == 5)
            {
                foreach (Collider2D gmb in Physics2D.OverlapCircleAll(transform.position, 3))
                {
                    if (gmb.tag == "Defender" && fromEnemy)
                    {
                        gmb.GetComponent<DefenderScript1>().TakeDamage(damage);
                    }
                    if (gmb.tag == "carTrigger" && fromEnemy)
                    {
                        gmb.transform.parent.GetComponent<carScript>().TakeDamage(damage);
                    }
                    if (gmb.tag == "Enemy" && !fromEnemy)
                    {
                        gmb.GetComponent<Enemy>().TakeDamage(damage);
                    }
                    else if (gmb.tag == "BombPoint" && !fromEnemy)
                    {
                        gmb.GetComponent<PedestalScript>().TakeDamage(damage);
                    }
                }
            }
            else if (MyType == 7)//
            {
                if (other.tag == "Defender" && fromEnemy)
                {
                    other.GetComponent<DefenderScript1>().TakeDamage(damage);
                }
                else if (other.tag == "carTrigger" && fromEnemy)
                {
                    other.transform.parent.GetComponent<carScript>().TakeDamage(damage);
                }
                else if (other.tag == "Enemy" && !fromEnemy)
                {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                    other.GetComponent<Rigidbody2D>().AddForce(transform.up * Push);
                }
                else if (other.tag == "BombPoint" && !fromEnemy)
                {
                    other.GetComponent<PedestalScript>().TakeDamage(damage);
                }
            }
            else if (MyType == 8)
            {
                if (other.tag == "Defender" && fromEnemy)
                {
                    other.GetComponent<DefenderScript1>().TakeDamage(damage);
                }
                else if (other.tag == "Enemy" && !fromEnemy)
                {
                    foreach (Collider2D gmb in Physics2D.OverlapCircleAll(transform.position, 5))
                    {
                        if (gmb.tag == "Defender" && fromEnemy)
                        {
                            gmb.GetComponent<DefenderScript1>().TakeDamage(damage);
                        }
                        if (gmb.tag == "carTrigger" && fromEnemy)
                        {
                            gmb.transform.parent.GetComponent<carScript>().TakeDamage(damage);
                        }
                        if (gmb.tag == "Enemy" && !fromEnemy)
                        {
                            gmb.GetComponent<Enemy>().TakeDamage(damage);
                        }
                        else if (gmb.tag == "BombPoint" && !fromEnemy)
                        {
                            gmb.GetComponent<PedestalScript>().TakeDamage(damage);
                        }
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
                if (other.tag == "carTrigger" && fromEnemy)
                {
                    other.transform.parent.GetComponent<carScript>().TakeDamage(damage);
                }
                else if (other.tag == "BombPoint" && !fromEnemy)
                {
                    other.GetComponent<PedestalScript>().TakeDamage(damage);
                }
            }


            if (!(other.tag == "Defender" && !fromEnemy) && !(other.tag == "Enemy" && fromEnemy))
            {
                if (MyType != 6)
                {
                    Instantiate(myDeathParticles, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    if ((other.tag != "Defender") || (other.tag != "Enemy"))
                    {
                        Instantiate(myDeathParticles, transform.position, Quaternion.identity);
                        DieInTime();
                    }
                }
            }
        }
    }
}
