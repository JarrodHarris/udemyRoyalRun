using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float[] lanes = { -2.65f, 0f, 2.65f };  //positions on the x-axis to determine the left, middle and right lane
    [SerializeField] float coinOffset = 0.5f;   //NOT USED ATM
    [SerializeField] int potentialCoinAmount = 5; 
    
    List<int> availableLanes = new List<int> { 0, 1, 2 };


    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
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
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    private void SpawnCoins()
    {
        if (availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();
        int coinsToSpawn = Random.Range(0, potentialCoinAmount);

        for (int i = 0; i <= coinsToSpawn; i++)
        {
            if (Random.value > coinSpawnChance) break;

            Vector3 spawnPosition2 = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z - i);
            Instantiate(coinPrefab, spawnPosition2, Quaternion.identity, this.transform);
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
