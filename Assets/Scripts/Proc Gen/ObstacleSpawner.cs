using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [Tooltip("Time before an obstacle is spawned")]
    [Header("Spawn Settings")]
    [SerializeField] float obstacleSpawnTime = 3f;
    [Tooltip("Minimum time for an obstacle to spawn so the player isn't overwhelmed")]
    [SerializeField] float minObstacleSpawnTime = 0.4f;
    [SerializeField] float spawnWidth = 4f;
    [SerializeField] Transform obstacleParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        if (obstacleSpawnTime <= minObstacleSpawnTime) return;

        obstacleSpawnTime -= amount;
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];   //Randomising which obstacle to spawn
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z); //Randomising the x-axis where to spawn obstacle
            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
