using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    private AudioClip clip;
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
