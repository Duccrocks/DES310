using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispDespawn : MonoBehaviour
{
    // Destroys the wisp when player gets within trigger collider
    void OnTriggerEnter(Collider other)
    {
        bool isActor = other.gameObject.CompareTag("Player");
        if (isActor) Destroy(this.gameObject);
    }
}
