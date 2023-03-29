using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTextureSwitcher : MonoBehaviour
{
    [SerializeField] private Texture newTexture;
    private void Start()
    {
        var allRenderers = FindObjectsOfType<Renderer>();
        foreach (var mesh in allRenderers)
        {
            mesh.material.SetTexture("MainTex", newTexture);
        }
    }
}
