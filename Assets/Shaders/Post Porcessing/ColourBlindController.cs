using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class ColourBlindController : MonoBehaviour
{
    public enum ColouBlindMode{none, Protanopiaro, Deuteranopia, Tritanopia }

    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    [SerializeField] Vector4 colourMask;
    [SerializeField] ColouBlindMode currentColourblindSetting;
    // Start is called before the first frame update
    void Start()
    {
        colourMask = new Vector4(1, 1, 1, 1);
        currentColourblindSetting = ColouBlindMode.none;
    }

    // Update is called once per frame
    void Update()
    {
           
        if (TryGetFeature(out var feature))
        {

            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;

            switch (currentColourblindSetting) {
                case (ColouBlindMode.none):
                    material.SetVector("_ColourBlindVector", new Vector4(1,0,0,0));
                    break;
                case (ColouBlindMode.Tritanopia):
                    material.SetVector("_ColourBlindVector", new Vector4(0, 1, 0, 0));
                    break;
                case (ColouBlindMode.Deuteranopia):
                    material.SetVector("_ColourBlindVector", new Vector4(0, 0, 1, 0));
                    break;
                case (ColouBlindMode.Protanopiaro):
                    material.SetVector("_ColourBlindVector", new Vector4(0, 0, 0, 1));
                    break;
                default: break;
            }


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
