using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string library;

    public void StartGame()
    {
        SceneManager.LoadScene(library.Trim());
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