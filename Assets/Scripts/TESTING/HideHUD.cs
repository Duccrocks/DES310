using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHUD : MonoBehaviour
{
    [SerializeField] private GameObject hudObject;
    
    //Used purely for gameplay videos, do NOT put in final build.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hudObject.activeInHierarchy)
            {
                hudObject.SetActive(false);
            }
            else
            {
                hudObject.SetActive(true);
            }
        }
    }
}
