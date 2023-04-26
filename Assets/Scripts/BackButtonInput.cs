using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class BackButtonInput : MonoBehaviour
{ 
    
    [SerializeField] private UnityEvent onCancel;
    private InputSystemUIInputModule inputSystemUIInputModule;
    private void Awake()
    {
        //Gets the input system for UI so the cancel event can actually be subscribed to.
        inputSystemUIInputModule = EventSystem.current.currentInputModule.GetComponent<InputSystemUIInputModule>();
    }

    private void OnEnable()
    {
        inputSystemUIInputModule.cancel.action.performed += Trigger;
    }

    private void OnDisable()
    {
        inputSystemUIInputModule.cancel.action.performed -= Trigger;
    }

    private void Trigger(InputAction.CallbackContext context)
    {
        onCancel?.Invoke();
    }
}