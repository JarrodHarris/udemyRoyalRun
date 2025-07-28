using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;
    [SerializeField] float obstacleDecreaseTimeAmount = .2f;
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;
    const string playerString = "Player";

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); //using this way instead of dependency injection as this is only happening every 8 chunks
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkpointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }
}
