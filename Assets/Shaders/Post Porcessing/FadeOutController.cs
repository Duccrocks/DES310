using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class FadeOutController : MonoBehaviour
{
    private float fadeTimer;
    [SerializeField] float fadeDuration;
    public float isFading;
    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    // Start is called before the first frame update
    void Start()
    {
        startFade();
    }
    void startFade()
    {
        isFading = 1;
        fadeTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        fadeTimer += Time.deltaTime;
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
}
