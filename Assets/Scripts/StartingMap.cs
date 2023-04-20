using UnityEngine;

public class StartingMap : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        RadarGameManager.Instance.CollectStartingMap();
        Destroy(gameObject);
    }
}