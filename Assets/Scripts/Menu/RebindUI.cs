using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//Source: https://www.youtube.com/watch?v=TD0R5x0yL0Y
public class RebindUI : MonoBehaviour
{
    [Header("Control to Rebind")]
    [SerializeField] private InputActionReference inputActionReference; //this is on the SO
    
    [Header("Binding Info - DO NOT MANUALLY EDIT")] 
    [SerializeField] private InputBinding inputBinding;
    
    [Header("Rebind settings")]
    [Tooltip("All the different bindings this action can have (keyboard/controller/mouse etc")]
    [SerializeField] [Range(0, 5)] private int selectedBinding;
    [SerializeField] private bool excludeMouse = true;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;



    [Header("UI Fields")] 
    [SerializeField] private TMP_Text actionText;
    [SerializeField] private TMP_Text rebindText;

    private string actionName;
    private int bindingIndex;
    
    //How many times to retry getting past controls (stops stackoverflow)
    private int retryCount;

    private void OnEnable()
    {
        if (inputActionReference != null) SetUp();

        //Subscribe functions to be called when rebind events are invoked.
        RebindManager.RebindComplete += UpdateUI;
        RebindManager.RebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        //Unsubscribe to save memory
        RebindManager.RebindComplete -= UpdateUI;
        RebindManager.RebindCanceled -= UpdateUI;
    }

    //Function that's ran when a value in the inspector changes (to update the inspector accordingly)
    private void OnValidate()
    {
        if (inputActionReference == null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void SetUp()
    {
        GetBindingInfo();
        
        /*
         * Gets previous overrides from playerPrefs
         * (may not load first time so recursively check until either it's gotten, or we just use the default values
         */
        if (actionName != null && retryCount < 3)
        {
            RebindManager.LoadBindingOverride(actionName);
        }
        else
        {
            SetUp();
            retryCount++;
        }

        UpdateUI();
    }
    
    /// <summary>
    /// Gets the binding info from the generated C# file.
    /// </summary>
    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
            actionName = inputActionReference.action.name;

        if (inputActionReference.action != null && inputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    /// <summary>
    /// Update the UI to reflect whatever new binding we have.
    /// </summary>
    private void UpdateUI()
    {
        if (actionText != null)
            actionText.text = actionName;

        if (rebindText != null)
        {
            if (Application.isPlaying)
                rebindText.text = RebindManager.GetBindingName(actionName, bindingIndex);
            else
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        }
    }

    /// <summary>
    /// Start the rebind to change the binding of the action
    /// </summary>
    public void DoRebind()
    {
        RebindManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    /// <summary>
    /// Reset the binding to the default binding.
    /// </summary>
    public void ResetBinding()
    {
        RebindManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }
}