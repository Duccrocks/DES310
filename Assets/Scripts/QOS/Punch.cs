using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayer;
    [SerializeField] float attackRange=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hit");
            RaycastHit hit;
            var forward = transform.TransformDirection(Vector3.forward);
            var mask = (1 << LayerMask.NameToLayer(excludedLayer)) | layerMaskInteract.value;
            
            if (Physics.Raycast(transform.position, forward, out hit, attackRange, mask))
            {
               
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<PunchResponse>().Punched(forward);
                }
            }
        }
    }
}
