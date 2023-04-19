using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoverText : MonoBehaviour
{
    [Header("Player")] [SerializeField] private GameObject toTrack;

    [Header("Control")] [SerializeField] private InputActionReference inputActionReference;

    [Header("Text Options")] [SerializeField]
    private TMP_Text bindingText;

    [SerializeField] private float fadeDuration = 0.75f;
    private bool istoTrackNull;

    private PlayerInput playerInput;

    private void Awake()
    {
        toTrack = GameObject.FindWithTag("Player");
        istoTrackNull = toTrack == null;
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Start()
    {
        UpdateBinding();
    }


    private void Update()
    {
        if (istoTrackNull) return;

        //Billboarding affect so the UI always faces the camera.
        transform.rotation = Quaternion.LookRotation(transform.position - toTrack.transform.position);
    }

    private void OnEnable()
    {
        InputManager.PlayerDeviceChanged += UpdateBinding;
        RebindManager.RebindComplete += UpdateBinding;
    }

    private void OnDisable()
    {
        InputManager.PlayerDeviceChanged -= UpdateBinding;
        RebindManager.RebindComplete -= UpdateBinding;
    }


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeInText());
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(FadeOutText());
    }

    private void UpdateBinding()
    {
        var action = playerInput.actions.FindAction(inputActionReference.name);

        var bindingIndex = action.GetBindingIndex(playerInput.currentControlScheme);

        // var displayString = action.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);

        var displayString = RebindManager.GetBindingName(inputActionReference.name, bindingIndex);
        bindingText.text = $"Press {displayString} to {action.name}";
    }


    private IEnumerator FadeInText()
    {
        bindingText.gameObject.SetActive(true);
        var currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            var alpha = Mathf.Lerp(0, 1f, currentTime / fadeDuration);
            bindingText.color = new Color(bindingText.color.r, bindingText.color.g, bindingText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutText()
    {
        var currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            var alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            bindingText.color = new Color(bindingText.color.r, bindingText.color.g, bindingText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        bindingText.gameObject.SetActive(false);
    }
}