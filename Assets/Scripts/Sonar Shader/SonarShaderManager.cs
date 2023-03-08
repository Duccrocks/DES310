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
        float isTerrain=0;
        if (pillar.name=="Terrain")
        {
            isTerrain = 1;
        }
        for (int i =0; i< pillar.GetComponent<Renderer>().materials.Length;++i)
        {
            Vector3 scale = pillar.transform.localScale;
            scale.x /= 5;
            scale.y /= 5;
            scale.z /= 5;
            pillar.GetComponent<Renderer>().materials[i].SetVector("_PlayerPos", pulseManager.pulseOrigin);
            pillar.GetComponent<Renderer>().materials[i].SetVector("_Scale", scale);
            pillar.GetComponent<Renderer>().materials[i].SetFloat("_PulseLen", pulseManager.length);
            //pillar.GetComponent<Renderer>().materials[i].SetFloat("_isTerrain", isTerrain);
        }


    }
}
