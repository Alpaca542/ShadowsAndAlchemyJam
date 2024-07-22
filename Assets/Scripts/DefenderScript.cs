using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderScript1 : MonoBehaviour
{
    private bool Died;
    private bool healing;
    private float AllTheDamage;
    private float health;
    private Slider healthBar;
    private Gradient healthGradient;
    private Image fill;
    public void TakeDamage(float dmg)
    {
        if (!Died)
        {
            AllTheDamage += dmg;
            health -= dmg;
            health = Mathf.Clamp(health, 0, 100);
            if (health <= 0)
            {
                Died = true;
                Invoke(nameof(Die), 1f);
            }
            if (!healing)
            {
                StartCoroutine(CrtnTakeDamage());
            }
        }

    }
    private void Die()
    {

    }
    public IEnumerator CrtnTakeDamage()
    {
        healing = true;
        while (AllTheDamage >= 0.4f)
        {
            healthBar.value -= 0.2f;
            AllTheDamage -= 0.2f;
            fill.color = healthGradient.Evaluate(healthBar.normalizedValue);
            yield return new WaitForSeconds(0.01f);
        }
        healing = false;
    }
}
