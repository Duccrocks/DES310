using System;
using UnityEngine;

public class Artifacts : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int artifactID;

    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;

    private GameObject kelpie;

    void Awake() 
    {
        kelpie = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void Interact()
    {
        try
        {
            RadarGameManager.Instance.ArtifactObtained(artifactID);
            AudioManager.Instance.PlaySoundOnce(audioClip);
            if (kelpie) kelpie.GetComponent<KelpieAI>().IncreaseDiff();
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e);
        }

        Destroy(gameObject);
    }
}