using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEventHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject cursor;


    //When you move off the button.
    public void OnDeselect(BaseEventData eventData)
    {
        cursor.SetActive(false);
    }


    //When you're selecting the button.
    public void OnSelect(BaseEventData eventData)
    {
        cursor.SetActive(true);
    }
}