using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Prefabs")]
    public GameObject shop;
    public CookScript cook;
    public GameObject[] enemies;

    private bool SpawningEnemies;

    private void Update()
    {
        SpawnShops();
        SpawnEnemies();
    }

    private void SpawnShops()
    {
        if (GameObject.FindGameObjectsWithTag("Shop").Length < amountOfShops)
        {
            GameObject newShop = Instantiate(shop, new Vector3(Random.Range(corner1.position.x, corner2.position.x), Random.Range(corner1.position.y, corner2.position.y), 0), Quaternion.identity);
            foreach (Canvas cnv in newShop.GetComponentsInChildren<Canvas>())
            {
                cnv.worldCamera = Camera.main;
            }
        }
    }

    private void SpawnEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < amountOfEnemies)
        {
            if (!SpawningEnemies)
            {
                SpawningEnemies = true;
                InvokeRepeating(nameof(SpawnEnemy), 0, enemiesSpawningCD);
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(cook.transform.position.x - maxSpawnRadius, cook.transform.position.x + maxSpawnRadius), Random.Range(cook.transform.position.y - maxSpawnRadius, cook.transform.position.y + maxSpawnRadius), 0), Quaternion.identity);
    }
}
