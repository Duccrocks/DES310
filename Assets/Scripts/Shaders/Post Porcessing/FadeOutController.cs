using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class FadeOutController : MonoBehaviour
{
    private float fadeTimer;
    public float fadeDuration;
    public float isFading;
    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    int fadeMult;
    // Start is called before the first frame update
    void Start()
    {
        StartFadeIn();
        isFading = 0;
    }
    void StartFade()
    {
        isFading = 1;
        fadeTimer = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (fadeTimer < 0) { fadeTimer = 0; isFading = 0; }
        if (fadeTimer > 2){ fadeTimer = 2; isFading = 0; }
                


        fadeTimer += Time.deltaTime*fadeMult;
        if (TryGetFeature(out var feature))
        {

            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;

            material.SetFloat("_fadeTimer", fadeTimer);
            material.SetFloat("_fadeDuration", fadeDuration);
            material.SetFloat("_shouldFade", isFading);
        }
    }
    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();

        return feature != null;
    }

    void OnApplicationQuit()
    {
        isFading = 0;
        fadeTimer = 0;
        if (TryGetFeature(out var feature))
        {
            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;

            material.SetFloat("_fadeTimer", fadeTimer);
            material.SetFloat("_fadeDuration", fadeDuration);
            material.SetFloat("_shouldFade",isFading);
        }
    }

    public void StartFadeOut()
    {
        isFading = 1;
        fadeTimer= 0;
        fadeMult = 1;
    }

    public void StartFadeIn()
    {
        fadeTimer = fadeDuration;
        isFading = 1;
        fadeMult= -1;
    }
}
