using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectabletag = "SelectableObject";
    [SerializeField] private RawImage crosshair;
    [SerializeField] private string excludedLayer; 
    public int rayLength;

    [SerializeField] private LayerMask layerMaskInteract;
    private bool doOnce;
    private bool isCrosshairActive;
    private bool hasInteracted = false;

    // Update is called once per frame
    private void Update()
    {
        //Creates a vector 3 that goes forward. This is to make sure the ray goes forward
        var forward = transform.TransformDirection(Vector3.forward);
        //Purely for debugging reasons, This shows where the raycast went
        Debug.DrawRay(transform.position, forward, Color.cyan, rayLength);

        var mask = (1 << LayerMask.NameToLayer(excludedLayer)) | layerMaskInteract.value;
        if (Physics.Raycast(transform.position, forward, out var hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(selectabletag))
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
                    if (interactable == null) return;
                    //Runs the Interact() method on which ever object it hit.
                    interactable.Interact();
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
    }

    /// <summary>
    ///     Changes the colour of the crosshair.
    /// </summary>
    /// <param name="on">If the crosshair colour is changed or not.</param>
    private void CrosshairChange(bool on)
    {
        //If its on and just changes once.
        if (on && !doOnce)
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