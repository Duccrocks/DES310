using UnityEngine;

public class MQoSManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(clip);
    }
}
