using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(Random.Range(-300, 300), Random.Range(-300, 300), Random.Range(-300, 300)), Quaternion.identity);
        transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
    }
}
