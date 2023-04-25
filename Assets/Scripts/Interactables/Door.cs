using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent doorOpened;
    private bool doOnce = false;
    public void Interact()
    {
        if(!doOnce)
        {
            doorOpened?.Invoke();
            doOnce = true;
        }
    }
}
