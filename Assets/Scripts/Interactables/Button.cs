using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("You have pressed this button :)");
        Destroy(gameObject);
    }
}