using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;

    GameObject[] chunks = new GameObject[12];
    void Start()
    {
        SpawnChunks();
    }

    // Instantiate with 'chunkParent' automatically makes these new objects become children of that object. Vector 3 position is being multiplied by chunk length(space of 10) so it can find the next sequential spot to place the 'chunk'
    private void SpawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            Instantiate(chunkPrefab, new Vector3(0f, 0f, i * chunkLength), Quaternion.identity, chunkParent);
        }
    }
}
