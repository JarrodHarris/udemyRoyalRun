using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;
    [SerializeField] float[] lanes = { -2.65f, 0f, 2.65f };  //positions on the x-axis to determine the left, middle and right lane 

    LevelGenerator levelGenerator;
    ScoreboardManager scoreboardManager;
    List<int> availableLanes = new List<int> { 0, 1, 2 };


    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreboardManager scoreboardManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreboardManager = scoreboardManager;
    }

    //Line 23, RemoveAt(int index) function removes the element at that position but also reorganizes the remaning elements position/index
    private void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length); //Range in this format means; 0,1,2

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;   //just a failsafe measure to make sure there are availableLanes to spawn fences

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);    //randomising which lane to spawn the fence
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChance) return;
        if (availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);    //randomising which lane to spawn the fence
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();;
        newApple.Init(levelGenerator);
    }

    private void SpawnCoins()
    {
        //MY IMPLEMENTATION
        // if (availableLanes.Count <= 0) return;

        // int selectedLane = SelectLane();
        // int potentialCoinAmount = 6;
        // int coinsToSpawn = Random.Range(1, potentialCoinAmount);

        // for (int i = 0; i <= coinsToSpawn; i++)
        // {
        //     if (Random.value > coinSpawnChance) break;

        //     Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z - i);
        //     Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        // }


        //UDEMY IMPLEMENTATION
        if (Random.value > coinSpawnChance) return;
        if (availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);
        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreboardManager);
        }
    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);    //getting a random index from availablesLanes
        int selectedLane = availableLanes[randomLaneIndex]; //using above index to select one of the lanes in availableLanes
        availableLanes.RemoveAt(randomLaneIndex);   //removing 'element' via index so if/when the for loop runs again that position of a possible fence cannot be used

        return selectedLane;
    }
}
