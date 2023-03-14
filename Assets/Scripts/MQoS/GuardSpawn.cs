using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject guard;

    public void SpawnGuard()
    {
        var spawnedGuard = Instantiate(guard,transform.position,Quaternion.identity);
    }
}
