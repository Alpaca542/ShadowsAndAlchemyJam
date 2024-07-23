using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;
using UnityEngine.Video;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    private float health;
    public MicroBar healthBar;
    public Gradient healthGradient;

    public Sprite[] weaponSprites;
    public Sprite[] weaponNames;
    private int activeWeapon;
    private Animator anim;
    private Rigidbody2D rb;

    public Image WeaponShowcase;

    private void Update()
    {
        if (GetComponent<PlayerScript>().selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (activeWeapon == 1)
                {
                    Shoot();
                }
            }

            AnimateMe();
        }
    }

    public void Shoot()
    {

    }

    private void AnimateMe()
    {
        if (rb.velocity.magnitude >= 0.2f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!Died)
        {
            if (health <= 0)
            {
                Died = true;
                Invoke(nameof(Die), 1f);
            }
            else
            {
                health -= dmg;
                if (health - dmg > health)
                {
                    healthBar.UpdateBar(health, UpdateAnim.Heal);
                }
                else
                {
                    healthBar.UpdateBar(health, UpdateAnim.Damage);
                }
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        healthBar.Initialize(health);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
}
