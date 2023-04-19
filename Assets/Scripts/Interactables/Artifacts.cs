using UnityEngine;

public class Artifacts : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform[] spawnPoints;
    private GameObject kelpie;

    void Awake()
    {
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
    }

    public void Interact()
    {
        try
        {
            RadarGameManager.Instance.ArtifactObtained();
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError(e);
        }
        Destroy(gameObject);
    }
}
