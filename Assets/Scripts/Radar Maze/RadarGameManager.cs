using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGameManager : MonoBehaviour
{
    [SerializeField] GameObject wispPrefab;
    [SerializeField] GameObject[] keyObjects;

    // PROTOTYPE SPAWNING METHOD PLEASE CHANGE OMG PLE$ASE PLEASE PLAESEPLSE APLEASPLEAPSLE

    // Spawning wisps nearby key objects in order to give player's hints to where to go
    void FixedUpdate()
    {
        if (Random.Range(1, 50) == 1)
        {
            // Key objects random spawn
            Vector3 objPos = keyObjects[Random.Range(0, keyObjects.Length)].transform.position;
            Vector3 spawnPos = new Vector3(objPos.x + Random.Range(-15, 15), 3, objPos.z + Random.Range(-15, 15));

            // Random spawn
            // Vector3 spawnPos = new Vector3(Random.Range(-50, 50), Random.Range(1.5f, 4.5f), Random.Range(-50, 50));

            Instantiate(wispPrefab, spawnPos, Quaternion.identity);
        }
    }
}
