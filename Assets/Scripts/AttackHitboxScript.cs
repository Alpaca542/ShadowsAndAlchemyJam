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
    }
}
