using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MQoSManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(clip);
    }
}
