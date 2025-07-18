using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedupParticleSystem;

    [Header("Cinemachine Camera Settings")]
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 0.8f;
    [SerializeField] float zoomSpeedMofifier = 5f;
    CinemachineCamera cinemachineCamera;

    //references to the game object that this script is attached to is preferred to be put in Awake()
    //references to game objects that aren't attached to this script in Start()
    // HELPS WITH NULL REFERENCE ERRORS
    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();    //So multiple coroutines dont occur
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedupParticleSystem.Play();    
        }
    }

    //common implementation of linear interpolation between two points with variables that can be controlled
    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedMofifier, minFOV, maxFOV);


        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV; //quality assurance to make sure the code above goes to the intended target; targetFOV

    }
}
