using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class ColourBlindController : MonoBehaviour
{

    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    [SerializeField, Range(0, 3)] private int colourBlindType = 0;
    [SerializeField] Vector4 colourMask;
    // Start is called before the first frame update
    void Start()
    {
        colourMask = new Vector4(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
           
        if (TryGetFeature(out var feature))
        {

            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;
            material.SetVector("_colourMask", colourMask);
            //material.SetFloat("_Saturation", saturation);
        }
    }
    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        
        return feature != null;
    }

}
