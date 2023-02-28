using UnityEngine;

public class Artifacts : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform[] spawnPoints;

    void Awake()
    {
        transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }

    public void Interact()
    {
        RadarGameManager.Instance.Victory();
        Destroy(gameObject);
    }
}
