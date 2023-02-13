using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private CameraController cameraController;

    //Reference to all player controls.
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponentInChildren<CameraController>();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
   
    }

    private void OnEnable()
    {
        //Subscribes all player control events
        playerControls.Enable();
        playerControls.Player.Move.performed += GetPlayerMovement;
        playerControls.Player.Look.performed += GetAxis;
        playerControls.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        //Personally I don't like memory leaks so unsubscribe from all.
        playerControls.Disable();
        playerControls.Player.Move.performed -= GetPlayerMovement;
        playerControls.Player.Look.performed -= GetAxis;
        playerControls.Player.Jump.performed -= Jump;
    }


    #region Player Controls events

    public void GetPlayerMovement(InputAction.CallbackContext ctx)
    {
        playerMovement.InputMovement = ctx.ReadValue<Vector2>();
    }

    public void GetAxis(InputAction.CallbackContext ctx)
    {
        cameraController.InputRotation = ctx.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started) playerMovement.Jump();
    }

    #endregion
}
