using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [Tooltip("variable to increase intensity of camera shake when boulder has a collision")]
    [SerializeField] float shakeModifier = 10f;
    [Tooltip("Timer for how long in between replaying the SFX & VFX")]
    [SerializeField] float collisionCooldown = 1f;
    CinemachineImpulseSource cinemachineImpulseSource;
    float collisionTimer = 1f;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCooldown) return;

        FireImpulse();
        CollisionFX(collision);

        collisionTimer = 0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position); //returning the distance between the rock and the camera
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f); //shakeIntensity could be more than one, this forces shakeIntensity to onlny ever reach 1f and not more. NOTE: 1f is considered to be the default amount of camera shake
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    private void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];   //collision.contacts returns multiple contact points but only need first contact point, hence the [0]
        collisionParticleSystem.transform.position = contactPoint.point;    //moves the particle system to the point where the collision first makes contact

        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();

        
    }
}
