using System;
using UnityEngine;

public class MQoSManager : MonoBehaviour
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
            Debug.LogError("LevelManager Null when fading in");
        }

        try
        {
            AudioManager.Instance.PlayMusic(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audiomanager Null");
        }
    }

    public void Victory()
    {
        Debug.Log("Player won MQoS");
        MiniGameProgression.MQoSCompleted = true;
        if (MiniGameProgression.HasWon())
        {
            LevelManager.instance.LoadScene("Credits");
            return;
        }

        LevelManager.instance.LoadScene("Library");
    }
}