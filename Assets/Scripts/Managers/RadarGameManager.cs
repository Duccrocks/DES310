using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RadarGameManager : MonoBehaviour
{
    public static RadarGameManager instance;

    [Header("Game Objects")] 
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] public MapManager mapManager;
    [SerializeField] private GameObject[] keyObjects;


    [Header("Wisps")]
    [SerializeField] private GameObject wispPrefab;
    [SerializeField] private LayerMask layerMask;


    [Header("Audio")]
    [SerializeField] private AudioClip ambience;

    private int artifactsCount;
    private KelpieAI kelpie;
    [SerializeField] GameObject victoryCanvas;

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
            AudioManager.Instance.PlayMusic(ambience);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null");
        }

        try
        {
            LevelManager.instance.SceneLoaded();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Level Manager Null when attempting to fade in.");
        }

        kelpie = FindObjectOfType<KelpieAI>();
        artifactsCount = FindObjectsOfType<Artifacts>().Length;

        victoryCanvas.SetActive(false);
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

    IEnumerator VictoryPopUp()
    {
        var UI = GameObject.FindGameObjectsWithTag("UI");//DISABLE ALL UI
        foreach (GameObject obj in UI)
        {
            obj.SetActive(false);
        }
        victoryCanvas.SetActive(true);

        kelpie.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        MiniGameProgression.KelpieGameCompleted = true;

        if (MiniGameProgression.HasWon())
        {
            LevelManager.instance.LoadScene("Credits");
            yield break;
        }

        LevelManager.instance.LoadScene("Library");
    }

    public void Death()
    {
        //Play death animation here 
        try
        {
            LevelManager.instance.LoadScene("Library");
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Level manager null");
        }
    }

    public void ArtifactObtained(int id)
    {
        kelpie.IncreaseDiff();
        artifactsCount--;
        mapManager.PieceCollected(id);
        if (artifactsCount <= 0) StartCoroutine(VictoryPopUp());
    }

    public void CollectStartingMap()
    {
        mapManager.bottomLeftCollected = true;
    }
}