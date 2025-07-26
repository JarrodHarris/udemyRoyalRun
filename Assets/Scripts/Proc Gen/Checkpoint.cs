using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;
    GameManager gameManager;
    const string playerString = "Player";

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); //using this way instead of dependency injection as this is only happening every 8 chunks
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkpointTimeExtension);
        }
    }
}
