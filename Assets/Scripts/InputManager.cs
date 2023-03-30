using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private enum ControlType
    {
        General,
        RadarMaze,
        MaryQueenOfScots,
        DuckMiniGame
    }

    [Header("Controls Enum")]
    [Tooltip("Which scenes controls to use.")] 
    [SerializeField] private ControlType controlType;

    private CameraController cameraController;
    private bool isPaused;
    private PauseMenu pauseMenu;
    private PlayerControls playerControls;

    //Reference to all player controls.
    private PlayerMovement playerMovement;
    private SelectionManager selectionManager;
    private SonarPulses sonarPulses;
    private Punch punch;

    private void Awake()
    {
        playerControls = RebindManager.inputActions ?? new PlayerControls();

        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponentInChildren<CameraController>();
        selectionManager = GetComponentInChildren<SelectionManager>();

        try
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Pause menu null");
        }

        //Depending on what scene we're in set up certain input events or not.
        switch (controlType)
        {
            case ControlType.General:
                return;
            case ControlType.RadarMaze:
                sonarPulses = GetComponent<SonarPulses>();
                break;
            case ControlType.MaryQueenOfScots:
                punch = GetComponent<Punch>();
                break;
            case ControlType.DuckMiniGame:
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

        if (isPaused) return;
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
        playerControls.Player.Sprint.performed += StartSprinting;
        playerControls.Player.Sprint.canceled += StopSprinting;
        if (pauseMenu) playerControls.Player.Pause.performed += TogglePause;
        if (sonarPulses) playerControls.Player.SonarPulse.performed += SonarPulse;
        if (punch) playerControls.Player.Punch.performed += PunchEnemy;

        PauseMenu.OnPause += OnPause;
    }


    private void OnDisable()
    {
        //Personally I don't like memory leaks so unsubscribe from all.
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Player.Interact.performed -= Interact;
        playerControls.Player.Sprint.performed -= StartSprinting;
        playerControls.Player.Sprint.canceled -= StopSprinting;
        playerControls.Player.Pause.performed -= TogglePause;
        playerControls.Player.SonarPulse.performed -= SonarPulse;
        playerControls.Player.Punch.performed -= PunchEnemy;

        PauseMenu.OnPause -= OnPause;
    }

    private void OnPause(bool hasPaused)
    {
        isPaused = hasPaused;
        if (hasPaused)
        {
            playerControls.Player.Jump.performed -= Jump;
            playerControls.Player.Interact.performed -= Interact;
            playerControls.Player.Sprint.performed -= StartSprinting;
            playerControls.Player.Sprint.canceled -= StopSprinting;
            playerControls.Player.SonarPulse.performed -= SonarPulse;
            playerControls.Player.Punch.performed -= PunchEnemy;
        }
        else
        {
            playerControls.Player.Jump.performed += Jump;
            playerControls.Player.Interact.performed += Interact;
            playerControls.Player.Sprint.performed += StartSprinting;
            playerControls.Player.Sprint.canceled += StopSprinting;
            if (sonarPulses) playerControls.Player.SonarPulse.performed += SonarPulse;
            if (punch) playerControls.Player.Punch.performed += PunchEnemy;

        }
    }




    #region Player Controls events

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) playerMovement.Jump();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) selectionManager.Interact();
    }

    private void StartSprinting(InputAction.CallbackContext ctx)

    {
        if (ctx.performed) playerMovement.StartSprinting();
    }

    private void StopSprinting(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled) playerMovement.StopSprinting();
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) pauseMenu.TogglePause();
    }

    private void SonarPulse(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) sonarPulses.Pulse();
    }

    private void PunchEnemy(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) punch.PunchEnemy();
    }

    #endregion
}