using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    public float damage;
    public bool fromPlayer;
    private void Start()
    {
        if (transform.parent.tag == "Enemy")
        {
            damage = transform.parent.GetComponent<Enemy>().meleeDamage;
        }
        else if (transform.parent.tag == "Defender")
        {
            damage = transform.parent.GetComponent<DefenderScript1>().meleeDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fromPlayer && other.tag == "Defender")
        {
            other.GetComponent<DefenderScript1>().TakeDamage(damage);
        }
        else if (fromPlayer && other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
