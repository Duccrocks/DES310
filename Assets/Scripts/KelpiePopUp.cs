using System;
using UnityEngine;

public class KelpiePopUp : MonoBehaviour, IInteractable
{
    [Header("UI")]
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject hud;

    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }


    private void OnDisable()
    {
        PauseMenu.OnPause -= HandlePause;
    }

    public void Interact()
    {
        try
        {
            AudioManager.Instance.PlaySoundOnce(audioClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("AudioManager Null");
        }
        
        Show();
        PauseMenu.OnPause += HandlePause;
    }

    private void HandlePause(bool isPaused)
    {
        if (!isPaused)
        {
            PauseMenu.OnPause -= HandlePause;
            Hide();
        }
    }

    private void Show()
    {
        popUp.SetActive(true);
        hud.SetActive(false);
        pauseMenu.TogglePause(false);
    }

    private void Hide()
    {
        popUp.SetActive(false);
        hud.SetActive(true);
    }
}