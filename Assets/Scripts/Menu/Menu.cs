using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private string library;

    /// <summary>
    /// Loads library level.
    /// </summary>
    public void StartGame()
    {
        LevelManager.instance.LoadScene(library.Trim());
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