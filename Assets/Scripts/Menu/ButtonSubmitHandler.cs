using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSubmitHandler : MonoBehaviour, ISubmitHandler
{
    [SerializeField] private AudioClip submitClip;

    public void OnSubmit(BaseEventData eventData)
    {
        try
        {
            AudioManager.Instance.PlaySound(submitClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Audio Manager Null when playing submit sound");
        }
    }
}