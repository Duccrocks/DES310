using System;
using UnityEngine;

public class MiniGameTransition : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneString;
    public void Interact()
    {
        try
        {
            LevelManager.instance.LoadScene(sceneString);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError($"Level manager null. \n{e}");
        }
    }
}
