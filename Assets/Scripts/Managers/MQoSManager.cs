using System;
using UnityEngine;
using System.Collections;
public class MQoSManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] GameObject victoryfield;
    private void Start()
    {
        try
        {
            LevelManager.instance.SceneLoaded();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("LevelManager Null when fading in");
        }

        try
        {
            AudioManager.Instance.PlayMusic(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audiomanager Null");
        }

        victoryfield.SetActive(false);
    }

    public void Victory()
    {
        StartCoroutine(vict());
    }

    IEnumerator vict()
    {
        var UI = GameObject.FindGameObjectsWithTag("UI");//DISABLE ALL UI
        foreach (GameObject obj in UI)
        {
            obj.SetActive(false);
        }
        victoryfield.SetActive(true);

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(5);
        MiniGameProgression.MQoSCompleted = true;

        if (MiniGameProgression.HasWon())
        {
            LevelManager.instance.LoadScene("Credits");
            yield break;
        }

        LevelManager.instance.LoadScene("Library");
    }
}