using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    
    [Header("HUD Elements")] 
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject hudPanel;

    private bool isPaused;
    public static event Action<bool> HasPaused;


    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    /// <summary>
    ///     Toggles if the game is paused or not.
    /// </summary>
    public void TogglePause()
    {
        if (!isPaused)
            PauseGame();
        else
            UnPauseGame();
        //Fires an event stating if the game has been paused or not.
        HasPaused?.Invoke(isPaused);
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseHud.SetActive(true);
        hudPanel.SetActive(false);
        cameraController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        pauseHud.SetActive(false);
        hudPanel.SetActive(true);
        cameraController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void ExitToMenu()
    {
        try
        {
            UnPauseGame();
            LevelManager.instance.LoadScene("Menu");
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Level manager null.");
        }
    }
}