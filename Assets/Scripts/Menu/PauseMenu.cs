using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("HUD Elements")] 
    [SerializeField] private GameObject pauseHud;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private Options options;
    
    private EventSystem eventSystem;
    private bool isPaused;
    public static event Action<bool> OnPause;


    private void Awake()
    {
        eventSystem = EventSystem.current;
    }


    /// <summary>
    ///     Toggles if the game is paused or not.
    /// </summary>
    public void TogglePause(bool shouldPauseMenuShow = true)
    {
        if (!isPaused)
        {
            PauseGame(shouldPauseMenuShow);
        }
        else
        {
            if (options.isActiveAndEnabled) return;
            UnPauseGame();
        }
        //Fires an event stating if the game has been paused or not.
    }

    private void PauseGame(bool shouldPauseMenuShow)
    {
        OnPause?.Invoke(true);
        Time.timeScale = 0.0f;
        if (shouldPauseMenuShow) pauseHud.SetActive(true);
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
        //Cursor.visible = true;
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
        //Cursor.visible = false;
        isPaused = false;
    }

    public void ExitToMenu()
    {
        UnPauseGame();
        try
        {
            LevelManager.instance.LoadScene("Menu", shouldTransitionEffect: false);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError($"Level manager null. \n{e}");
        }
    }
}