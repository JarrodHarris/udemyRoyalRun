using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [Tooltip("rotation speed of pickups on the y-axis")]
    [SerializeField] float rotationSpeed = 100f;
    const string playerString = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            OnPickup();
            Destroy(this.gameObject);
        }
    }

    protected abstract void OnPickup();
}
