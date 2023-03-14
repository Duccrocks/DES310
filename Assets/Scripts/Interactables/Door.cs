using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent doorOpened;
    public void Interact()
    {
        doorOpened?.Invoke();
    }
}
