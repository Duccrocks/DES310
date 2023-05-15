using System;
using UnityEngine;

public class SonarPulses : MonoBehaviour
{
    [NonSerialized] public Vector3 pulseOrigin;
    [Header("Sonar Settings")] 
    public float length;
    [SerializeField] private float pulseTimer = 10;
    [SerializeField] private float pulseSpeed = 50;

    [Header("Audio")] 
    [SerializeField] private AudioClip sonarClip;
    
    private bool pulsing;

    // Start is called before the first frame update
    private void Start()
    {
        pulsing = false;
        pulseOrigin = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pulsing)
            pulseTimer += Time.deltaTime;

        length = pulseSpeed * pulseTimer;

        if (pulseTimer > 10)
        {
            pulseOrigin = transform.position;
            pulseTimer = 0;
            pulsing = false;
        }

        if (!pulsing) length = 0;
    }

    public void Pulse()
    {
        if (pulseTimer > 2 || !pulsing)
        {
            try
            {
                AudioManager.Instance.PlaySoundOnce(sonarClip);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("Audio Manager null when playing sonar sound.");
            }

            pulseOrigin = transform.position;
            pulseTimer = 0;
            length = 0;
            pulsing = true;
        }
    }
}