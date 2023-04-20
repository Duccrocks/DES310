using UnityEngine;

public class KelpiePopUp : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject hud;
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