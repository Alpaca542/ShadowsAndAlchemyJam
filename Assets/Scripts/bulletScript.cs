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
   0-RED
   1-GREEN
   2-BLUE
   3-PURERED
   4-redAndBlue
   5-GreenAndBlue
   6-graphed
   7-Analyzed
   8-white



    */

    private void Start()
    {
        Invoke(nameof(DieInTime), 3f);
        
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up*speed;
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
                    if (gmb.tag == "Enemy" && !fromEnemy)
                    {
                        gmb.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            else if(MyType == 0)
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
            else if (MyType == 1)
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
            else if (MyType == 2)
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
            else if (MyType == 3)//
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
            else if (MyType == 4)//
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
            else if (MyType == 6)//
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
            else if (MyType == 7)//
            {
                if (other.tag == "Defender" && fromEnemy)
                {
                    other.GetComponent<DefenderScript1>().TakeDamage(damage);
                }
                else if (other.tag == "Enemy" && !fromEnemy)
                {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                    other.GetComponent<Rigidbody2D>().AddForce(transform.up * Push);
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
                        if (gmb.tag == "Enemy" && !fromEnemy)
                        {
                            gmb.GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                }
            }
            if (!(other.tag == "Defender" && !fromEnemy) && !(other.tag == "Enemy" && fromEnemy))
            {
                if(MyType!=6)
                {
                    Instantiate(myDeathParticles, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    if((other.tag != "Defender")|| (other.tag != "Enemy"))
                    {
                        Instantiate(myDeathParticles, transform.position, Quaternion.identity);
                        DieInTime();
                    }
                }
            }
        }
    }
}
