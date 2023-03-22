using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveManager : MonoBehaviour
{
    Renderer renderer;
    float clip;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        clip = 1;
    }

    // Update is called once per frame
    void Update()
    {
        clip -= Time.deltaTime/5.0f;
        clip = Mathf.Max(clip, 0);
        renderer.material.SetFloat("_alphaClip", clip);
    }

    private void OnApplicationQuit()
    {
        clip = 1;
    }
}
