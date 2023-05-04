using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private float attackRange = 5;
    
    [Header("Layer Masks")]
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayer;
    
    [Header("Audio")]
    [SerializeField] private AudioClip clip;
    
    private readonly int attack = Animator.StringToHash("Attack");
    
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void PunchEnemy()
    {
        Debug.Log("Punching");

        anim.SetTrigger(attack);
        AudioManager.Instance.PlaySoundOnce(clip);
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

            if (hit.collider.CompareTag("Physics Object"))
                hit.transform.gameObject.GetComponent<GenericPunchResponse>().Punched(forward);
        }
    }
}