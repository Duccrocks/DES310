using UnityEngine;
using System.Collections.Generic;
public class GuardSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] knightPrefabs;
    Dictionary<string, GameObject> knightDict;
    [SerializeField] private Transform[] guardSpawnPos;

    private void MakeDict()
    {
       knightDict = new Dictionary<string, GameObject>();
        knightDict.Add("Pink", knightPrefabs[0]);
        knightDict.Add("Red", knightPrefabs[1]);
        knightDict.Add("Blue", knightPrefabs[2]);
        knightDict.Add("Green", knightPrefabs[3]);
    }

    public void SpawnGuard()
    {
        MakeDict();
        foreach (var guardPos in guardSpawnPos)
        {
            
            var spawnedGuard = Instantiate(knightDict[guardPos.gameObject.tag],guardPos.position,Quaternion.identity);
            //Look at the player when spawning so won't miss the player.
            spawnedGuard.transform.LookAt(Camera.main.transform);
        }
        
    }
}
