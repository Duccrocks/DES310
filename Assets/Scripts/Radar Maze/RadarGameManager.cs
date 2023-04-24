using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RadarGameManager : MonoBehaviour
{
    public static RadarGameManager instance;
    
    [Header("Game Objects")]
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] public MapManager mapManager;

    [Header("Wisps")]
    [SerializeField] private GameObject[] keyObjects;
    [SerializeField] private GameObject wispPrefab;
    [SerializeField] LayerMask layerMask;
    
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
        // if (Random.Range(1, 500) == 1)
        // {
        //     // Key objects random spawn

        //     Vector3 objPos = keyObjects[Random.Range(0, keyObjects.Length - 1)].transform.position;
        //     Vector3 spawnPos = new Vector3(objPos.x + Random.Range(-30, 30), 0, objPos.z + Random.Range(-30, 30));

        //     Physics.Raycast(new Vector3(spawnPos.x, 100.0f, spawnPos.z), Vector3.down, out var hit, 200.0f);

        //     Debug.Log(hit.transform.position);

        //     Instantiate(wispPrefab, new Vector3(spawnPos.x, hit.transform.position.y, spawnPos.z), Quaternion.identity);
        // }
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

    public void ArtifactObtained(int id)
    {
        kelpie.IncreaseDiff();
        artifactsCount--;
        mapManager.PieceCollected(id);
        if (artifactsCount <= 0) Victory();
    }

    public void CollectStartingMap()
    {
        mapManager.bottomLeftCollected = true;
    }
}