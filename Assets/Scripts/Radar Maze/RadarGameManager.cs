using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RadarGameManager : MonoBehaviour
{
    public static RadarGameManager instance;
    
    [Header("Game Objects")]
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject wispPrefab;
    [SerializeField] private GameObject[] keyObjects;
    [SerializeField] private MapManager mapManager;
    
    [Header("Audio")]
    [SerializeField] private AudioClip ambience;

    private int artifactsCount;
    private KelpieAI kelpie;
    
    public static RadarGameManager Instance

    {
        get
        {
            // If doesnt have an instance of singleton get one
            if (instance == null) instance = FindObjectOfType<RadarGameManager>();

            //Returns the instance of type singleton
            return instance;
        }

        //Private setter as the singleton doesn't need to changed anywhere else other than this singleton.
        private set => instance = value;
    }

    private void Start()
    {
        try
        {
            AudioManager.Instance.PlayMusicWithFade(ambience);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null");
        }

        kelpie = FindObjectOfType<KelpieAI>();
        artifactsCount = FindObjectsOfType<Artifacts>().Length;
    }

    private void FixedUpdate()
    {
        if (Random.Range(1, 500) == 1)
        {
            // Key objects random spawn
            var objPos = keyObjects[Random.Range(0, keyObjects.Length)].transform.position;
            var spawnPos = new Vector3(objPos.x + Random.Range(-30, 30), 3, objPos.z + Random.Range(-30, 30));

            // Random spawn
            //Vector3 spawnPos = new Vector3(Random.Range(-250, 250), Random.Range(1.5f, 4.5f), Random.Range(100, -50));

            Instantiate(wispPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void Victory()
    {
        MiniGameProgression.RadarMazeCompleted = true;
        LevelManager.instance.LoadScene("Library");
    }

    public void Death()
    {
        LevelManager.instance.LoadScene("Library");
    }

    public void ArtifactObtained()
    {
        kelpie.IncreaseDiff();
        artifactsCount--;
        mapManager.PieceCollected(artifactsCount);
        if (artifactsCount <= 0) Victory();
    }
}