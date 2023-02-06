using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] private Material SonarShader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Teeheeeee");
            SonarShader.SetFloat("LastPulseTime", Time.time);
            SonarShader.SetVector("PulseOrigin", transform.position);
        }    
    }
}
