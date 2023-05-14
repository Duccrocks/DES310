using System;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        try
        {
            LevelManager.instance.SceneLoaded();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Level Manager Null while trying to fade in");
        }
        try
        {
            AudioManager.Instance.PlayMusic(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null");
        }
    }
}