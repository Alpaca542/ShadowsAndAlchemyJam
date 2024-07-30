using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public GameObject activatedGate;
    public GameObject arrow;

    public float HP;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0.5f, 2f);
    }
    private void Die()
    {
        Destroy(gameObject);
        GameObject.FindWithTag("EpochManager").GetComponent<EpochManager>().CloseGate();
    }
    public void TakeDamage(float dmg)
    {
        
            if (HP <= 0)
            {
                
                Invoke(nameof(Die), 1f);

            gameObject.GetComponent<soundManager>().PlaySound(1, 0.3f, 1f);
        }
            else
            {
                HP -= dmg;
                Mathf.Clamp(HP, 0, 100);
                if (dmg > 0)
                {
                   // healthBar.UpdateBar(HP, UpdateAnim.Damage);
                }
                else
                {
                    //healthBar.UpdateBar(HP, UpdateAnim.Heal);
                }

            }
        
    }
    public void SetBomb()
    {
        CancelInvoke(nameof(SpawnEnemy));
        Instantiate(activatedGate, transform.position, quaternion.identity);
        arrow.SetActive(false);
        Destroy(gameObject);
    }

    void SpawnEnemy()
    {
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().SpawnEnemyAtPoint(transform.position);
    }
}
