using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayer;

    [SerializeField] private float attackRange = 5;
    [Header("Audio")]
    [SerializeField] private AudioClip punchClip;
    [SerializeField] private AudioClip punchImpactClip;

    public bool jitterRoom;
    bool canPunch = true;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void PunchEnemy()
    {
        if (!jitterRoom)
        {
            if (canPunch == false) return;
            canPunch = false;
            Invoke(nameof(CooldownReset), 0.5f);
        }

        Debug.Log("Punching");

        anim.SetTrigger("Attack");
        try
        {
            AudioManager.Instance.PlaySoundOnce(punchClip);
        }
        catch (System.NullReferenceException)
        {

            Debug.LogError("AudioManager Null");
        }


        var forward = transform.TransformDirection(Vector3.forward);
        var mask = (1 << LayerMask.NameToLayer(excludedLayer)) | layerMaskInteract.value;
        Debug.DrawRay(transform.position, forward * attackRange, Color.black);
        if (Physics.Raycast(transform.position, forward, out var hit, attackRange, mask))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                hit.transform.gameObject.GetComponentInParent<PunchResponse>().Punched(forward);
                try
                {
                    AudioManager.Instance.PlaySoundOnce(punchImpactClip);
                }
                catch (System.NullReferenceException)
                {

                    Debug.LogError("AudioManager Null");
                }
                return;
            }
            else if (hit.collider.CompareTag("Physics Object"))
            {
                hit.transform.gameObject.GetComponent<GenericPunchResponse>().Punched(forward);
                try
                {
                    AudioManager.Instance.PlaySoundOnce(punchImpactClip);
                }
                catch (System.NullReferenceException)
                {

                    Debug.LogError("AudioManager Null");
                }
            }
        }
    }

    void CooldownReset()
    {
        canPunch = true;
    }
}