using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent colliderTriggered;
    private bool doOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!doOnce)
        {
            colliderTriggered?.Invoke();
            doOnce = true;
        }
    }
}
