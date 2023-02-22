using UnityEngine;

public class MiniGameTransition : MonoBehaviour, IInteractable
{
     [SerializeField] private string sceneString;
    public void Interact()
    {
        LevelManager.instance.LoadScene(sceneString);
    }
}
