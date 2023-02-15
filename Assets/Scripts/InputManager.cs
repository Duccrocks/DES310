using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private CameraController cameraController;
    private PlayerControls playerControls;

    //Reference to all player controls.
    private PlayerMovement playerMovement;
    private SelectionManager selectionManager;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponentInChildren<CameraController>();
        selectionManager = GetComponentInChildren<SelectionManager>();
    }

    private void Update()
    {
        var movementValue = playerControls.Player.Move.ReadValue<Vector2>();
        playerMovement.Movement(movementValue);

        var cameraRotationValue = playerControls.Player.Look.ReadValue<Vector2>();
        cameraController.Look(cameraRotationValue);
    }

    private void OnEnable()
    {
        //Subscribes all player control events
        playerControls.Enable();
        playerControls.Player.Jump.performed += Jump;
        
        playerControls.Player.Interact.performed += Interact;

        //playerControls.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        //Personally I don't like memory leaks so unsubscribe from all.
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
    }

    #region Player Controls events

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) playerMovement.Jump();
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) selectionManager.Interact();
    }

    #endregion
}