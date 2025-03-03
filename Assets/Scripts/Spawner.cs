using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float amountOfShops;
    public float amountOfStorages;
    public float amountOfEnemies;
    public float minSpawnRadius;
    public float maxSpawnRadius;
    public float enemiesSpawningCD;

    [Header("Corners")]
    public Transform corner1;
    public Transform corner2;

    [Header("Fields")]
    public GameObject shop;
    public CookScript cook;
    public GameObject[] enemies;
    public GameObject[] shadowGates;
    public GameObject car;
    public GameObject defender;
    public GameObject arrow;
    public LayerMask shopLayer;

    [Header("Debug")]
    public int HowManyEnemiesKilled;
    private int gateCnt = 0;
    private bool SpawningEnemies;

    private void Update()
    {
        //SpawnShops();
        // SpawnEnemies();
    }


    private void Win()
    {
        CameraShaker.Instance.ShakeOnce(100f, 100f, 100f, 100f);
    }
    public void SpawnShops()
    {
        if (GameObject.FindGameObjectsWithTag("Shop").Length < amountOfShops)
        {
            Vector3 newPos = new Vector3(Random.Range(corner1.position.x, corner2.position.x), Random.Range(corner1.position.y, corner2.position.y), 0);
            if (Vector3.Distance(newPos, car.transform.position) > 5 && !Physics2D.OverlapCircle(newPos, 2, shopLayer))
            {
                GameObject newShop = Instantiate(shop, newPos, Quaternion.identity);
                foreach (Canvas cnv in newShop.GetComponentsInChildren<Canvas>())
                {
                    cnv.worldCamera = Camera.main;
                }
            }
        }
    }

    public void SpawnEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < amountOfEnemies)
        {
            if (!SpawningEnemies)
            {
                SpawningEnemies = true;
                InvokeRepeating(nameof(SpawnEnemy), 0, enemiesSpawningCD);
            }
        }
        else
        {
            CancelInvoke(nameof(SpawnEnemy));
            SpawningEnemies = false;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(cook.transform.position.x - maxSpawnRadius, cook.transform.position.x + maxSpawnRadius), Random.Range(cook.transform.position.y - maxSpawnRadius, cook.transform.position.y + maxSpawnRadius), 0);
        if (Vector3.Distance(spawnPoint, car.transform.position) > 3 && Vector3.Distance(spawnPoint, defender.transform.position) > minSpawnRadius)
        {
            GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint, Quaternion.identity);
        }
    }

    public void SpawnEnemyAtPoint(Vector2 point)
    {
        GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], point, Quaternion.identity);
        newEnemy.GetComponent<dieInTIme>().time_ = 5f;
    }
}
