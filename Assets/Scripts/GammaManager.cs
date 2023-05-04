
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GammaManager : MonoBehaviour
{

    private static GammaManager instance;

    [SerializeField] private UniversalRendererData rendererData;
    [SerializeField] private string featureName;
    public float gamma;

    private void Awake()
    {

        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    private void Start()
    {
        gamma = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (TryGetFeature(out var feature))
        {
            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;

            material.SetFloat("_Gamma", gamma);
            //material.SetFloat("_Saturation", saturation);
        }
    }

    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where(f => f.name == featureName).FirstOrDefault();

        return feature != null;
    }
}