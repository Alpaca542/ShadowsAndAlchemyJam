using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public GameObject activatedGate;
    public GameObject arrow;
    public GameObject dieParticles;
    public float HP;
    bool KeepParent = false;
    public GameObject parent;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0.5f, 3f);
        gameObject.GetComponent<soundManager>().PlaySound(1, 1, 1);
    }
    private void Die()
    {
        Instantiate(dieParticles, transform.position, Quaternion.identity);
        gameObject.GetComponent<soundManager>().PlaySound(0, 1, 1);
        Destroy(gameObject);
        GameObject.FindWithTag("EpochManager").GetComponent<EpochManager>().CloseGate();

    }
    public void TakeDamage(float dmg)
    {
        //nothing XD

    }
    public void SetBomb()
    {
        CancelInvoke(nameof(SpawnEnemy));
        Instantiate(activatedGate, transform.position, quaternion.identity);
        arrow.SetActive(false);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gatebreaker"))
        {
            KeepParent = true;
            parent = collision.transform.gameObject;
            gameObject.GetComponent<soundManager>().PlaySound(1, 1, 1);
            Invoke("Die", 2f);

            foreach (GameObject enem in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enem.GetComponent<Enemy>().Die();
            }
        }
    }
    void SpawnEnemy()
    {
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().SpawnEnemyAtPoint(transform.position);
    }
    private void Update()
    {
        if (KeepParent)
        {
            transform.position = parent.transform.position;
        }
    }
}
