using System.Collections;
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

    private bool isScrolling;


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

    private IEnumerator Start()
    {
        var pos = scroller.localPosition;
        pos.y = -850;
        scroller.localPosition = pos;
        inputSystemUIInputModule.cancel.action.performed += Trigger;
        //Wait for a second before starting to scroll.
        yield return new WaitForSeconds(1);
        isScrolling = true;
    }


    private void Update()
    {
        if (isScrolling)
        {
            var pos = scroller.localPosition;
            pos.y += 3850f / creditTime * Time.deltaTime;
            scroller.localPosition = pos;


            if (pos.y > 3000) LevelManager.instance.LoadScene("Menu", shouldTransitionEffect: false);
        }


    }


    private void OnDisable()
    {
        inputSystemUIInputModule.cancel.action.performed -= Trigger;
    }

    private void Trigger(InputAction.CallbackContext context)
    {
        LevelManager.instance.LoadScene("Menu", shouldTransitionEffect: false);
    }
}