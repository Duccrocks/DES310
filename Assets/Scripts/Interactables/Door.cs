using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent doorOpened;
    public UnityEvent doorClosed;
    private bool doorOpen = false;
    public void Interact()
    {
        if (!doorOpen)
        {
            doorOpened?.Invoke();
            doorOpen = true;
        }
        else 
        {
            doorClosed?.Invoke();
            doorOpen = false;
        }
    }
}
