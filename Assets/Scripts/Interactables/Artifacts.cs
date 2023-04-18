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
        Debug.Log("Hit radar maze artifact");
        Destroy(gameObject);
        try
        {
            RadarGameManager.Instance.ArtifactObtained();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("RadarGame Manager null");
        }
    }
}
