using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveManager : MonoBehaviour
{
    Renderer renderer;
    MeshFilter meshFilter;
    float clip;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        meshFilter = GetComponent<MeshFilter>();
        clip = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float height = meshFilter.mesh.bounds.extents.y;
        clip -= Time.deltaTime/5.0f;
        clip = Mathf.Max(clip, 0);
        renderer.material.SetFloat("_alphaClip", clip);
        renderer.material.SetFloat("_height", height);
    }

    private void OnApplicationQuit()
    {
        clip = 1;
    }
}
