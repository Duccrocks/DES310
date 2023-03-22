using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispDespawn : MonoBehaviour
{
    
    private Transform kelpie;
    private void Start()
    {
        kelpie = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    private void Update()
    {
        if (kelpie != null)
        {
            if (Vector3.Distance(kelpie.position, transform.position) < 8)
                Destroy(this.gameObject);
        }
    }
    // Destroys the wisp when player gets within trigger collider
    void OnTriggerEnter(Collider other)
    {
        bool isActor = other.gameObject.CompareTag("Player");
        if (isActor) Destroy(this.gameObject);
    }

}
