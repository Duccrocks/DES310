using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BookColour : MonoBehaviour
{


     [SerializeField] public Texture UsingTexture;
 
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetTexture("_BaseMap", UsingTexture);
    }
}
