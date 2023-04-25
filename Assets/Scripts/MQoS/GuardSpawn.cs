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
            //Look at the player when spawning so won't miss the player.
            spawnedGuard.transform.LookAt(Camera.main.transform);
        }
        
    }
}
