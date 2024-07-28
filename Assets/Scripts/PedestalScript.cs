using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public GameObject activatedGate;
    public GameObject arrow;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0.5f, 2f);
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
