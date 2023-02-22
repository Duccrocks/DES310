using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    //Reference to all player controls.
    private PlayerMovement playerMovement;
    private CameraController cameraController;
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
        /*
         * Constantly listen for player input for movement and camera rotation
         * as this is something that needs to be ran every frame.
         */

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
        playerControls.Player.Sprint.started += StartSprinting;
        playerControls.Player.Sprint.canceled += StopSprinting;

    }

    private void OnDisable()
    {
        //Personally I don't like memory leaks so unsubscribe from all.
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Player.Interact.performed -= Interact;
        playerControls.Player.Sprint.started -= StartSprinting;
        playerControls.Player.Sprint.canceled -= StopSprinting;
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

    public void StartSprinting(InputAction.CallbackContext ctx) => playerMovement.StartSprinting();

    public void StopSprinting(InputAction.CallbackContext ctx) => playerMovement.StopSprinting();

    #endregion
}