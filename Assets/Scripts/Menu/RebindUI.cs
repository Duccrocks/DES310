using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//Source: https://www.youtube.com/watch?v=TD0R5x0yL0Y
public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference; //this is on the SO
    [SerializeField] private bool excludeMouse = true;
    [SerializeField] [Range(0, 10)] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")] 
    [SerializeField] private InputBinding inputBinding;

    [Header("UI Fields")] 
    [SerializeField] private TMP_Text actionText;
    [SerializeField] private TMP_Text rebindText;

    private string actionName;
    private int bindingIndex;

    private void OnEnable()
    {
        if (inputActionReference != null)
        {
            RebindManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }
        
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

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
            actionName = inputActionReference.action.name;

        if (inputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

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

    public void DoRebind()
    {
        RebindManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    public void ResetBinding()
    {
        RebindManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }
}