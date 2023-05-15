using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitterRoom : MonoBehaviour
{
    [SerializeField] bool jitter;
    [SerializeField] Punch Punch;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Punch.jitterRoom = jitter;

    }
}
