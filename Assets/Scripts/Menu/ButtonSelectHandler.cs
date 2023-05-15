using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject cursors;
    [SerializeField] private AudioClip clip;

    private void OnDisable()
    {
        cursors.SetActive(false);
    }

    //When you move off the button.
    public void OnDeselect(BaseEventData eventData)
    {
        cursors.SetActive(false);
        try
        {
            AudioManager.Instance.PlaySound(clip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null when playing select sound");
        }
    }


    //When you're selecting the button.
    public void OnSelect(BaseEventData eventData)
    {
        cursors.SetActive(true);
    }
}