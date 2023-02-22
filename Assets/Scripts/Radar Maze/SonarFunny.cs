using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarFunny : MonoBehaviour
{
    [SerializeField] private int sonarRes = 300;
    [SerializeField] private int sonarRange = 20;
    [SerializeField] private GameObject sonarSpheres;
    [SerializeField] private LayerMask layerMask;
    
    private bool onCooldown = false;
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !onCooldown) CalcRays();
    }

    void CalcRays()
    {
        onCooldown = true;

        for (int i = 0; i < sonarRes; ++i)
        {
            Vector3 direction = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            
            //Debug.DrawRay(transform.position, direction, Color.magenta, 2.0f);

            if (Physics.Raycast(transform.position, direction, out hit, sonarRange, layerMask))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.distance < sonarRange) DrawSphere();
            }
        }
        
        Invoke(nameof(DestroySphere), 2.0f);
        Invoke(nameof(CooldownReset), 2.0f);
    }

    void DrawSphere()
    {
        GameObject sphere = Instantiate(sonarSpheres, hit.point, new Quaternion(hit.normal.x, hit.normal.y, hit.normal.z, 0));
        sphere.transform.localScale = (sphere.transform.localScale / (hit.distance / 2)) * 2;
    }

    void DestroySphere()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("SonarSphere");

        foreach (var sphere in spheres)
        {
            Destroy(sphere);
        }
    }

    void CooldownReset()
    {
        onCooldown = false;
    }
}
