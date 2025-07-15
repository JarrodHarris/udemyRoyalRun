using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    List<GameObject> chunks = new List<GameObject>();
    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        moveSpeed += speedAmount;

        if (moveSpeed < minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
        }
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z -speedAmount);    //adjusting Project Settings -> Physics -> Settings, particularly the z value because when the player picks up a lot of apples, the obstacles falling react in a weird manner due to the gravity physics. This is to regulate it
    }

    // Instantiate with 'chunkParent' automatically makes these new objects become children of that object.
    // Vector 3 position (new Vector3(0f, 0f, i * chunkLength)) is being multiplied by chunk length(space of 10) so it can find the next sequential spot to place the 'chunk'
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));   //forcing brackets around the two floats is more performant as multiplication between vectors and floats is demanding, having both the floats multiplied first before involving the vector is better


            //Checking to destroy chunk once its reached past the camera
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)   //- chunkLength is an offset variable
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }

    private void SpawnChunk()
    {
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(0f, 0f, (chunks.Count - 1) * chunkLength), Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }
}
