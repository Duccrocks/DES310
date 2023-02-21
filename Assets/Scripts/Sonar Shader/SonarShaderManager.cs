using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarShaderManager : MonoBehaviour
{
   public GameObject pillar;
   public Transform playerPos;
   public SonarPulses pulseManager;

    // Start is called before the first frame update
    void Start()
    {
       pillar = this.gameObject;
       //pulseManager = pillar.GetComponent<SonarPulses>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i< pillar.GetComponent<Renderer>().materials.Length;++i)
        {

            pillar.GetComponent<Renderer>().materials[i].SetVector("_playerPos", pulseManager.pulseOrigin);
            pillar.GetComponent<Renderer>().materials[i].SetVector("_objectPosition", pillar.transform.position);
            pillar.GetComponent<Renderer>().materials[i].SetVector("_objectScale", pillar.transform.localScale);
            pillar.GetComponent<Renderer>().materials[i].SetFloat("_pulseLength", pulseManager.length);
        }


    }
}
