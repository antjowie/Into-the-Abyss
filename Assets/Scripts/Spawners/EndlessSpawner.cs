using UnityEngine;

using System.Collections;

public class EndlessSpawner : Spawner
{
    [SerializeField] GameObject[] spawnPrefabs;
    [SerializeField] int spawnCount = 1;
    [SerializeField] float spawnRate = 1f;
    
    protected override void OnPlayerEnter()
    {
        StartCoroutine(SpawnEnemy());
    }

    protected override void OnUpdate()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = GetPointInArea();
            Instantiate(spawnPrefabs[Random.Range(0,spawnPrefabs.Length)], spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f / spawnRate);
    }
}