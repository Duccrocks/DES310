using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private EventSystem eventSystem;

    [Header("HUD Elements")] 
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pausePanel;


    private bool isPaused;
    public static Action<bool> OnPause;


    private void Awake()
    {
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
    }

    private void PauseGame()
    {
        OnPause?.Invoke(true);
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
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    private void UnPauseGame()
    {
         OnPause?.Invoke(false);
        Time.timeScale = 1.0f;
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        pauseHud.SetActive(false);
        hudPanel.SetActive(true);
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