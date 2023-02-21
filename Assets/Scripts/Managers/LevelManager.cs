using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     Loads scene through the scenes name.
    /// </summary>
    /// <param name="sceneName">Name of scene to load.</param>
    /// <param name="loadSceneMode">Additive or single loading (defaults to single)</param>
    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    ///     Loads async scenes through scenes name with a coroutine.
    /// </summary>
    /// <param name="sceneName">Name of scene to load.</param>
    /// <param name="loadSceneMode">Additive or single loading (defaults to single)</param>
    public IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        var progress = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        while (!progress.isDone)
            //Can do a transition or loading screen here.
            yield return null;
        Debug.Log($"Scene: {sceneName} has loaded");
    }

    /// <summary>
    ///     Unloads scene asynchronously with scene name with a coroutine.
    /// </summary>
    /// <param name="sceneName">Name of scene to load.</param>
    public IEnumerator UnloadSceneAsync(string sceneName)
    {
        var progress = SceneManager.UnloadSceneAsync(sceneName);

        while (!progress.isDone)
            yield return null;
        Debug.Log($"Scene: {sceneName} has unloaded");
    }
}