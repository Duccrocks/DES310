using System;
using UnityEngine;

public class MQoSManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    
    void Start()
    {
        LevelManager.instance.SceneLoaded();
        try
        {
            AudioManager.Instance.PlayMusicWithFade(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audiomanager null");
        }
    }

    public void Victory()
    {
        Debug.Log("Player won MQoS");
        LevelManager.instance.LoadScene("Library");
        MiniGameProgression.MQoSCompleted = true;
    }
}
