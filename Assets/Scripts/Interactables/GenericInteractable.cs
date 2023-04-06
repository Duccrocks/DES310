using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool shouldRunOnce = false;
    private bool doOnce = false;

    public UnityEvent interactableTriggered;

    public void Interact()
    {
        if(shouldRunOnce)
        {
            if(doOnce) return;
            interactableTriggered.Invoke();
        }
        else
        {
            interactableTriggered?.Invoke();
        }

    }
}
