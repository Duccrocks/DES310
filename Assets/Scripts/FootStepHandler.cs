using System;
using System.Linq;
using UnityEngine;

public class FootStepHandler : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float soundDelayThreshold = 0.5f;
    [Header("Player References")]
    [SerializeField] private PlayerMovement playerMovement;
    //Checking here as well as regular ground check only applies for ground layer, thus wont work in library.
    [SerializeField] private Transform groundPositionTransform;
    private int footstepIndex;
    private float soundDelay;

    private void Start()
    {
        if (!footstepClips.Any()) Debug.LogError("Footstep audio clips empty");
        if (playerMovement == null) Debug.Log("Footsteps player movement null in footstep handler");
        footstepIndex = 0;
    }

    private void Update()
    {
        if (soundDelay >= soundDelayThreshold) PlaySound();
        if (!Physics.Raycast(groundPositionTransform.position, Vector3.down, 0.5f) || !playerMovement.IsMoving()) return;

        soundDelay += Time.deltaTime;
        if (playerMovement.isSprinting) soundDelay += Time.deltaTime;
    }

    private void PlaySound()
    {
        if (footstepIndex >= footstepClips.Length)
            footstepIndex = 0;
        try
        {
            AudioManager.Instance.PlaySoundOnce(footstepClips[footstepIndex]);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("AudioManager null for footstep sounds");
        }
        footstepIndex++;
        soundDelay = 0;
    }
}