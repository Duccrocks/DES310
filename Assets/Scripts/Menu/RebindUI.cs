using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference; //this is on the SO
    [SerializeField] private bool excludeMouse = true;
    [SerializeField] [Range(0, 10)] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")] [SerializeField]
    private InputBinding inputBinding;

    [Header("UI Fields")] [SerializeField] private Text actionText;

    [SerializeField] private Text rebindText;

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

        RebindManager.RebindComplete += UpdateUI;
        RebindManager.RebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        RebindManager.RebindComplete -= UpdateUI;
        RebindManager.RebindCanceled -= UpdateUI;
    }

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