using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPulses : MonoBehaviour
{
    [NonSerialized] public Vector3 pulseOrigin;
    [SerializeField] public float length;

    [SerializeField] private float pulseTimer = 10;
    [SerializeField] private float pulseSpeed = 50;
    private bool pulsing;
    // Start is called before the first frame update
    void Start()
    {
        pulsing = false;
        pulseOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
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
        if (!pulsing)
        {
            length = 0;
        }
    }

    public void Pulse()
    {
        if (pulseTimer > 2 || !pulsing)
        {
            pulseOrigin = transform.position;
            pulseTimer = 0;
            pulsing = true;
        }
    }
}
