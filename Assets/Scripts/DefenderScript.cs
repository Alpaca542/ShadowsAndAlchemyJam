using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    private float health;
    private MicroBar healthBar;
    private Gradient healthGradient;

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
    }
}
