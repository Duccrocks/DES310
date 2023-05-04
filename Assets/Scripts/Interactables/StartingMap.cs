using System;
using UnityEngine;

public class StartingMap : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip audioClip;
    public void Interact()
    {
        try
        {
            AudioManager.Instance.PlaySoundOnce(audioClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("AudioManager Null");
        }
        RadarGameManager.Instance.CollectStartingMap();
        Destroy(gameObject);
    }
}