using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEventHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject cursors;

    private void OnDisable()
    {
        cursors.SetActive(false);
    }

    //When you move off the button.
    public void OnDeselect(BaseEventData eventData)
    {
        cursors.SetActive(false);
    }


    //When you're selecting the button.
    public void OnSelect(BaseEventData eventData)
    {
        cursors.SetActive(true);
    }
}