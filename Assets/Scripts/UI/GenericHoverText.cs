using System.Collections;
using TMPro;
using UnityEngine;

public class GenericHoverText : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private GameObject toTrack;

    [Header("Text Options")] 
    [SerializeField] private TMP_Text bindingText;
    [SerializeField] private float fadeDuration = 0.75f;
    
    private bool istoTrackNull;

    private void Awake()
    {
        toTrack = GameObject.FindWithTag("Player");
        istoTrackNull = toTrack == null;
    }
    
    private void Update()
    {
        if (istoTrackNull) return;

        //Billboarding affect so the UI always faces the camera.
        transform.rotation = Quaternion.LookRotation(transform.position - toTrack.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeInText());
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(FadeOutText());
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