using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddollManager : MonoBehaviour
{
    [SerializeField]
    private Transform ragDollRoot;
    [SerializeField]
    public bool ragDollEnabled=false;
    [SerializeField]
    private Animator animator;

    public Rigidbody[] Rigidbodies;
    private CharacterJoint[] joints;
    private Collider[] colliders;
    [SerializeField] private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbodies = ragDollRoot.GetComponentsInChildren<Rigidbody>();
        joints = ragDollRoot.GetComponentsInChildren<CharacterJoint>();
        colliders = ragDollRoot.GetComponentsInChildren<Collider>();

        if (ragDollEnabled)
        {
            EnableRagdoll();
        }
        else
        {
            DisableRagdoll() ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ragDollEnabled)
        {
            EnableRagdoll();
        }
        else
        {
            DisableRagdoll();
        }
    }
    public void EnableRagdoll()
    {
        collider.enabled = false;
        animator.enabled = false;
        foreach (CharacterJoint joint in joints)
        {

            joint.enableCollision= true;
        }
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        foreach(Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.detectCollisions= true;
            rigidbody.useGravity= true;
        }
    }

    public void DisableRagdoll() 
    {
        collider.enabled = true;
        animator.enabled = true;
        foreach (CharacterJoint joint in joints)
        {

            joint.enableCollision = false;
        }
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
        }
    }
}
