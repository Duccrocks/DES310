using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private Transform[] guardSpawnPos;

    public void SpawnGuard()
    {
        foreach (var guardPos in guardSpawnPos)
        {
            var spawnedGuard = Instantiate(guardPrefab,guardPos.position,Quaternion.identity);
        }
        
    }
}
