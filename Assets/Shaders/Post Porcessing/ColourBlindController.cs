using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColourBlindController : MonoBehaviour
{
    public enum ColourBlindMode
    {
        None,
        Protanopia,
        Deuteranopia,
        Tritanopia
    }

    private static ColourBlindController instance;

    [SerializeField] private UniversalRendererData rendererData;
    [SerializeField] private string featureName;
    [SerializeField] private Vector4 colourMask;
    public ColourBlindMode currentColourblindSetting;

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
        colourMask = new Vector4(1, 1, 1, 1);
        currentColourblindSetting = ColourBlindMode.None;
    }

    // Update is called once per frame
    private void Update()
    {
        if (TryGetFeature(out var feature))
        {
            var blitFeature = feature as Blit;
            var material = blitFeature.blitPass.blitMaterial;

            switch (currentColourblindSetting)
            {
                case ColourBlindMode.None:
                    material.SetVector("_ColourBlindVector", new Vector4(1, 0, 0, 0));
                    break;
                case ColourBlindMode.Tritanopia:
                    material.SetVector("_ColourBlindVector", new Vector4(0, 1, 0, 0));
                    break;
                case ColourBlindMode.Deuteranopia:
                    material.SetVector("_ColourBlindVector", new Vector4(0, 0, 1, 0));
                    break;
                case ColourBlindMode.Protanopia:
                    material.SetVector("_ColourBlindVector", new Vector4(0, 0, 0, 1));
                    break;
            }


            material.SetVector("_colourMask", colourMask);
            //material.SetFloat("_Saturation", saturation);
        }
    }

    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where(f => f.name == featureName).FirstOrDefault();

        return feature != null;
    }
}