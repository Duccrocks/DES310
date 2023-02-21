using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPulses : MonoBehaviour
{
    public Vector3 pulseOrigin;
    public float length;

    private float pulseTimer;
    private float pulseSpeed;
    private bool pulsing;
    // Start is called before the first frame update
    void Start()
    {
        pulseSpeed = 40;
        pulseTimer = 0;
        pulsing = true;
        pulseOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pulsing)
            pulseTimer += Time.deltaTime;
        
        length = pulseSpeed * pulseTimer;

        if (Input.GetKeyDown(KeyCode.F)&&(pulseTimer>2||!pulsing))
        {
            pulseOrigin = transform.position;
            pulseTimer = 0;
            pulsing = true;
        }
        if (pulseTimer>10)
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
}
