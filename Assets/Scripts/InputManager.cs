using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private enum ControlType
    {
        General,
        RadarMaze,
        MaryQueenOfScots,
        DuckMinigame
    }

    [Header("Controls Enum")]
    [Tooltip("What scenes controls to use")]
    [SerializeField] private ControlType controlType;
    private PlayerControls playerControls;

    //Reference to all player controls.
    private PlayerMovement playerMovement;
    private CameraController cameraController;
    private SelectionManager selectionManager;
    private PauseMenu pauseMenu;
    private SonarPulses sonarPulses;

    private void Awake()
    {
        if (RebindManager.inputActions != null)
        {

            playerControls = RebindManager.inputActions;
        }
        else
        {
            playerControls = new PlayerControls();
        }

        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponentInChildren<CameraController>();
        selectionManager = GetComponentInChildren<SelectionManager>();

        try
        {
            pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Pause menu null");
        }

        switch (controlType)
        {
            case ControlType.General:
                return;
            case ControlType.RadarMaze:
            sonarPulses = GetComponent<SonarPulses>();
                break;
            case ControlType.MaryQueenOfScots:
            case ControlType.DuckMinigame:
                Debug.LogError("We haven't made Duck game yet.");
                break;

        }
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
        if (pauseMenu) playerControls.Player.Pause.performed += TogglePause;
        if (sonarPulses) playerControls.Player.SonarPulse.performed += SonarPulse;
    }


    private void OnDisable()
    {
        //Personally I don't like memory leaks so unsubscribe from all.
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Player.Interact.performed -= Interact;
        playerControls.Player.Sprint.started -= StartSprinting;
        playerControls.Player.Sprint.canceled -= StopSprinting;
        playerControls.Player.Pause.performed -= TogglePause;
        playerControls.Player.SonarPulse.performed -= SonarPulse;
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

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pauseMenu.TogglePause();
        }
    }

    private void SonarPulse(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            sonarPulses.Pulse();
        }
    }


    #endregion
}