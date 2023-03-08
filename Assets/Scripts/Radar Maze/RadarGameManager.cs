using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class RadarGameManager : MonoBehaviour
{
    public static RadarGameManager instance;

    [SerializeField] GameObject[] treePrefabs;
    [SerializeField] GameObject wispPrefab;
    [SerializeField] GameObject[] keyObjects;


    public static RadarGameManager Instance

    {
        get
        {
            // If doesnt have an instance of singleton get one
            if (instance == null)
            {
                instance = FindObjectOfType<RadarGameManager>();
            }

            //Returns the instance of type singleton
            return instance;
        }

        //Private setter as the singleton doesn't need to changed anywhere else other than this singleton.
        private set
        {
            instance = value;
        }
    }

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        if (Random.Range(1, 500) == 1)
        {
            // Key objects random spawn
            Vector3 objPos = keyObjects[Random.Range(0, keyObjects.Length)].transform.position;
            Vector3 spawnPos = new Vector3(objPos.x + Random.Range(-30, 30), 3, objPos.z + Random.Range(-30, 30));
            
            // Random spawn
            //Vector3 spawnPos = new Vector3(Random.Range(-250, 250), Random.Range(1.5f, 4.5f), Random.Range(100, -50));

            Instantiate(wispPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void Victory()
    {
        //LevelManager.instance.LoadScene("Menu");
        SceneManager.LoadScene(0);
    }

    public void Death()
    {
        LevelManager.instance.LoadScene("Library");
    }
}
