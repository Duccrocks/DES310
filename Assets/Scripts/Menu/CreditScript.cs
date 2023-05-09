using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CreditScript : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private RectTransform scroller;

    [SerializeField] private TMP_Text backText;
    [SerializeField] private float creditTime;

    [Header("Input")] 
    [SerializeField]
    private InputSystemUIInputModule inputSystemUIInputModule;

    [SerializeField] private PlayerInput playerInput;


    private void Awake()
    {
        UpdateBackText();
    }

    public void UpdateBackText()
    {
        Debug.Log("Updating UI");
        var actionName = inputSystemUIInputModule.cancel.action.name;
        var action = playerInput.actions.FindAction(actionName);
        var bindingIndex = action.GetBindingIndex(playerInput.currentControlScheme);


        var displayString = action.GetBindingDisplayString(bindingIndex);
        //Gets the input system for UI so the cancel event can actually be subscribed to.
        backText.text = $"Press: {displayString} to Exit.";
    }

    private void Start()
    {
        var pos = scroller.localPosition;
        pos.y = -850;
        scroller.localPosition = pos;
        inputSystemUIInputModule.cancel.action.performed += Trigger;
    }


    // Update is called once per frame
    private void Update()
    {
        var pos = scroller.localPosition;
        pos.y += 3850f / creditTime * Time.deltaTime;
        scroller.localPosition = pos;


        if (pos.y > 2000) LevelManager.instance.LoadScene("Menu");
    }


    private void OnDisable()
    {
        inputSystemUIInputModule.cancel.action.performed -= Trigger;
    }

    private void Trigger(InputAction.CallbackContext context)
    {
        LevelManager.instance.LoadScene("Menu");
    }
}