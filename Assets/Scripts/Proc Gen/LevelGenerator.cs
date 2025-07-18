using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Referencees")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;

    [Tooltip("Empty game object to group chunks into this parent object")]
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreboardManager scoreboardManager;

    [Header("Level Settings")]
    [Tooltip("The amount of chunks we start with")]
    [SerializeField] int startingChunksAmount = 12;
    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;

    [Header("Physics Gravity Settings")]
    [Tooltip("Minimum gravity setting on the z-axis")]
    [SerializeField] float minGravityZ = -22f;

    [Tooltip("Maximum gravity setting on the z-axis")]
    [SerializeField] float maxGravityZ = -2f;

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
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);    //adjusting Project Settings -> Physics -> Settings, particularly the z value because when the player picks up a lot of apples, the obstacles falling react in a weird manner due to the gravity physics. This is to regulate it

            cameraController.ChangeCameraFOV(speedAmount);
        }
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
        GameObject newChunkGO = Instantiate(chunkPrefab, new Vector3(0f, 0f, (chunks.Count - 1) * chunkLength), Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreboardManager);
    }
}
