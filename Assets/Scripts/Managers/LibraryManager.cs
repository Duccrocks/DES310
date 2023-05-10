using System;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        try
        {
            AudioManager.Instance.PlayMusicWithFade(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null");
        }
    }
}