using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayer;

    [SerializeField] private float attackRange = 5;


    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator > ();
    }

    public void PunchEnemy()
    {
        Debug.Log("Punching");

        anim.SetTrigger("Attack");

        var forward = transform.TransformDirection(Vector3.forward);
        var mask = (1 << LayerMask.NameToLayer(excludedLayer)) | layerMaskInteract.value;
        Debug.DrawRay(transform.position, forward * attackRange, Color.black);
        if (Physics.Raycast(transform.position, forward, out var hit, attackRange, mask))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                hit.transform.gameObject.GetComponentInParent<PunchResponse>().Punched(forward);
                return;
            }
            else if (hit.collider.CompareTag("Physics Object"))
            {
                hit.transform.gameObject.GetComponent<GenericPunchResponse>().Punched(forward);

            }
        }
    }
}