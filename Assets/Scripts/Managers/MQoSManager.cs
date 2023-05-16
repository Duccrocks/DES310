using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MQoSManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private GameObject victoryCanvas;

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
            AudioManager.Instance.PlayMusic(musicClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audiomanager Null");
        }

        victoryCanvas.SetActive(false);
    }

    public void Victory()
    {
        StartCoroutine(VictoryPopUp());
    }

    private IEnumerator VictoryPopUp()
    {
        var ui = GameObject.FindGameObjectsWithTag("UI"); //DISABLE ALL UI
        foreach (GameObject obj in ui)
        {
            obj.SetActive(false);
        }

        victoryCanvas.SetActive(true);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        MiniGameProgression.MQoSCompleted = true;

        if (MiniGameProgression.HasWon())
        {
            LevelManager.instance.LoadScene("Credits");
            yield break;
        }

        LevelManager.instance.LoadScene("Library");
    }
}