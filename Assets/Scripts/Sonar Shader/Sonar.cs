using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] private Material SonarShader;
    private Camera cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Teeheeeee");
            SonarShader.SetFloat("_LastPulseTime", Time.time);
            SonarShader.SetVector("_PulseOrigin", transform.position);
        }    
    }
}
