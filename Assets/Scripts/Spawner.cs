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

    [Header("Corners")]
    public Transform corner1;
    public Transform corner2;

    [Header("Prefabs")]
    public GameObject shop;
    public GameObject[] enemies;

    private void Update()
    {
        SpawnShops();
        SpawnEnemy();
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

    private void SpawnEnemy()
    {

    }
}
