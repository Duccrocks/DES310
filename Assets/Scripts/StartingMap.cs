using UnityEngine;

public class StartingMap : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        RadarGameManager.Instance.collectStartingMap();
        Destroy(gameObject);
    }
}