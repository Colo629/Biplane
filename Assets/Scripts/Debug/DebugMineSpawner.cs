using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMineSpawner : MonoBehaviour
{
    public GameObject monitorMine;
    public static int minesToSpawn = 100;
    public int totalMines = 100;
    public float spawnDelay = 0.5f;
    private float spawnTime;

    private void Start()
    {
        minesToSpawn = totalMines;
    }
    private void Update()
    {
        while (Time.time > spawnTime & minesToSpawn > 0)
        {
            spawnTime += spawnDelay;
            if (minesToSpawn > 0)
            {
                Debug.Log(name);
                minesToSpawn -= 1;
                Vector3 randPos = new Vector3(Random.Range(-1500, 1500), Random.Range(130, 400), Random.Range(-1500, 1500));
                Instantiate(monitorMine, randPos, Quaternion.identity, null);
            }
        }
    }
}
