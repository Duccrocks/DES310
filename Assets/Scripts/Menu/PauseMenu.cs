using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    private bool isPaused = false;

    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            PausePanel.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            PausePanel.SetActive(false);
            isPaused = false;

        }
    }
}
