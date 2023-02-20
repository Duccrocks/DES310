using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "SelectableObject";
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayer;
    [SerializeField][Range(0,10)] private float rayLength;
    [SerializeField] private RawImage crosshair;

    private bool doOnce;
    private bool hasInteracted;
    private bool isCrosshairActive;

    // Update is called once per frame
    private void Update()
    {
        //Creates a vector 3 that goes forward. This is to make sure the ray goes forward
        var forward = transform.TransformDirection(Vector3.forward);
        //Purely for debugging reasons, This shows where the raycast went
        Debug.DrawRay(transform.position, forward * rayLength, Color.cyan);

        var mask = (1 << LayerMask.NameToLayer(excludedLayer)) | layerMaskInteract.value;
        if (Physics.Raycast(transform.position, forward, out var hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(selectableTag))
            {
                if (!doOnce) CrosshairChange(true);
                doOnce = true;
                isCrosshairActive = true;
                //If the player presses E.
                if (hasInteracted)
                {
                    //Caches the hit object.
                    var hitObject = hit.collider.gameObject;
                    Debug.Log($"Hit interactable {hitObject.name}");
                    //Gets the IInteractable interface.
                    var interactable = hitObject.GetComponent<IInteractable>();
                    //Runs the Interact() method on which ever object it hit.
                    interactable?.Interact();
                    hasInteracted = false;
                }
            }
            else
            {
                //No change to crosshair.
                if (isCrosshairActive)
                {
                    CrosshairChange(false);
                    doOnce = false;
                }
            }
        }
        //In the case nothing was hit.
        else
        {
            //No change to crosshair.
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }

        hasInteracted = false;
    }

    /// <summary>
    ///     Changes the colour of the crosshair.
    /// </summary>
    /// <param name="isOn">If the crosshair colour is changed or not.</param>
    private void CrosshairChange(bool isOn)
    {
        //If its on and just changes once.
        if (isOn && !doOnce)
        {
            //Turns the crosshair red.
            crosshair.color = Color.red;
        }
        //In the case that it isn't then.
        else
        {
            //Turns the crosshair white.
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }

    public void Interact()
    {
        hasInteracted = true;
    }
}