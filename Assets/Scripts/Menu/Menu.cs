using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        //Locks the cursor.
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.PlayMusicWithFade(clip);
    }

    /// <summary>
    /// Loads library level.
    /// </summary>
    public void StartGame()
    {
        try
        {
            LevelManager.instance.LoadScene(sceneToLoad);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError($"Level manager null. \n{e}");
        }
    }

    /// <summary>
    ///     Quits to desktop.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}