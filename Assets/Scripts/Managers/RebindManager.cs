using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//Source: https://www.youtube.com/watch?v=TD0R5x0yL0Y
public class RebindManager : MonoBehaviour
{
    public static PlayerControls inputActions;

    private void Awake()
    {
        if (inputActions == null)
            inputActions = new PlayerControls();
        //DontDestroyOnLoad(gameObject);
    }

    //Events on different binding states different scripts can listen to and react on a rebind.
    public static event Action RebindComplete;
    public static event Action RebindCanceled;
    public static event Action<InputAction, int> RebindStarted;

    public static void StartRebind(string actionName, int bindingIndex, TMP_Text statusText, bool isMouseExcluded)
    {
        var action = inputActions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }

        //If composite (for example WASD)
        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                DoRebind(action, bindingIndex, statusText, true, isMouseExcluded);
        }
        //If just a normal singular input like space.
        else
        {
            DoRebind(action, bindingIndex, statusText, false, isMouseExcluded);
        }
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText,
        bool isCompositeParts,
        bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0)
            return;

        statusText.text = $"Press a {actionToRebind.expectedControlType}";
        
        //Disables that binding while we're currently changing it.
        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        //When rebind completes.
        rebind.OnComplete(operation =>
        {
            //Reset the binding and then dispose of the rebinding operation.
            actionToRebind.Enable();
            operation.Dispose();

            if (isCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < actionToRebind.bindings.Count &&
                    actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, statusText, isCompositeParts, excludeMouse);
            }

            //Saves the new input and tells listeners the rebinding was successful.
            SaveBindingOverride(actionToRebind);
            RebindComplete?.Invoke();
        });

        //When the user cancels the rebind.
        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            RebindCanceled?.Invoke();
        });
        //A timeout useful so a player doesn't accidentally put in the wrong thing before getting a chance to react.
        rebind.OnMatchWaitForAnother(0.1f);
        //How to exit out of rebinding
        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.WithCancelingThrough("<Gamepad>/select");


        //If mouse should be ignored or not 
        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        //Ignore mouse movement since you'd never want that as a rebind.
        rebind.WithControlsExcluding("<Mouse>/position");
        rebind.WithControlsExcluding("<Mouse>/delta");

        RebindStarted?.Invoke(actionToRebind, bindingIndex);
        //Actually starts the rebinding process
        rebind.Start();
    }

    //Gets the name of the binding for UI.
    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if (inputActions == null)
            inputActions = new PlayerControls();

        var action = inputActions.asset.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    //Saves the new bindings to player prefs.
    private static void SaveBindingOverride(InputAction action)
    {
        for (var i = 0; i < action.bindings.Count; i++)
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
    }

    //Loads previously saved bindings from player prefs.
    public static void LoadBindingOverride(string actionName)
    {
        if (inputActions == null)
            inputActions = new PlayerControls();

        var action = inputActions.asset.FindAction(actionName);

        for (var i = 0; i < action.bindings.Count; i++)
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
    }

    //Reset the current binding back to the default.
    public static void ResetBinding(string actionName, int bindingIndex)
    {
        var action = inputActions.asset.FindAction(actionName);

        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
            for (var i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                action.RemoveBindingOverride(i);
        else
            action.RemoveBindingOverride(bindingIndex);

        //Overwrites saved controls with the default ones.
        SaveBindingOverride(action);
    }
}