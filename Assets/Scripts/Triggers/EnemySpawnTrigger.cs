using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnTrigger : MonoBehaviour
{
    public UnityEvent colliderTriggered;
    private bool doOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!doOnce)
        {
            colliderTriggered?.Invoke();
            doOnce = true;
        }
    }
}