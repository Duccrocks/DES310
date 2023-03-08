using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CameraController cameraController; 
    private EventSystem eventSystem;

    [Header("HUD Elements")] 
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pausePanel;


    private bool isPaused;
    public static event Action<bool> HasPaused;


    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        eventSystem = EventSystem.current;
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
        try
        {
            eventSystem.SetSelectedGameObject(resumeButton);
        }
        catch (NullReferenceException e)
        {
            Debug.Log($"Event system null {e}");
        }
        cameraController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
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
        catch (NullReferenceException e)
        {
            Debug.LogError($"Level manager null. \n{e}");
        }
    }
}