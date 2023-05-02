using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{

    Animator animcamera;

    // Start is called before the first frame update
    void Start()
    {
        animcamera = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animcamera.SetTrigger("fart");
        }


    }
}
