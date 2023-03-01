using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform[] spawnPoints;
    private GameObject kelpie;

    void Awake()
    {
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
        kelpie = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void Interact()
    {
        ArtifactObtained();
        Destroy(gameObject);
    }

    void ArtifactObtained()
    {
        kelpie.GetComponent<KelpieAI>().IncreaseDiff(); 
        GameObject[] artifactsLeft = GameObject.FindGameObjectsWithTag("SelectableObject");

        if (artifactsLeft.Length <= 1) RadarGameManager.Instance.Victory();

        Debug.Log("how did you get this far...?");
    }
}
