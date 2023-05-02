using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [Header("Controls Enum")] [Tooltip("Which scenes controls to use.")] 
    [SerializeField] private ControlType controlType;


    private bool isPaused;
    private PlayerControls playerControls;

    //Reference to all player controls.
    private PlayerMovement playerMovement;
    private CameraController cameraController;
    private SelectionManager selectionManager;
    private SonarPulses sonarPulses;
    private MapManager mapManager;
    private Punch punch;
    private PauseMenu pauseMenu;

    private enum ControlType
    {
        General,
        RadarMaze,
        MaryQueenOfScots,
        FinalMiniGame
    }
    
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
                SetUpRadarMaze();
                break;
            case ControlType.MaryQueenOfScots:
                SetUpMQOS();
                break;
            case ControlType.FinalMiniGame:
                Debug.LogError("No final minigame has been made");
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
        if (sonarPulses) playerControls.Player.Sonar.performed += SonarPulse;
        if (punch) playerControls.Player.Punch.performed += PunchEnemy;
        if (mapManager) playerControls.Player.Map.performed += ToggleMap;

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
        playerControls.Player.Sonar.performed -= SonarPulse;
        playerControls.Player.Punch.performed -= PunchEnemy;

        PauseMenu.OnPause -= OnPause;
    }

    public static event Action PlayerDeviceChanged;

    private void SetUpRadarMaze()
    {
        sonarPulses = GetComponent<SonarPulses>();
        if (TryGetComponent(out SonarPulses sonarPulsesComponent))
            sonarPulses = sonarPulsesComponent;
        else
            Debug.LogError("SonarPulses Null\n " +
                           "If you're not in Kelpie then switch controlType, else add a Punch script");

        mapManager = FindObjectOfType<MapManager>();

        if (!mapManager)
            Debug.LogError("Map Manager Null\n " +
                           "If you're not in Radar Maze then switch controlType, else add a Map Manager script");
    }

    private void SetUpMQOS()
    {
        punch = GetComponentInChildren<Punch>();
        if (!punch)
            Debug.LogError("Punch Null\n " +
                           "If you're not in MQoS then switch controlType, else add a Punch script");
    }

    public void HandleDeviceChange()
    {
        PlayerDeviceChanged?.Invoke();
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
            playerControls.Player.Sonar.performed -= SonarPulse;
            playerControls.Player.Punch.performed -= PunchEnemy;
        }
        else
        {
            playerControls.Player.Jump.performed += Jump;
            playerControls.Player.Interact.performed += Interact;
            playerControls.Player.Sprint.performed += StartSprinting;
            playerControls.Player.Sprint.canceled += StopSprinting;
            if (sonarPulses) playerControls.Player.Sonar.performed += SonarPulse;
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
    
    private void ToggleMap(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) mapManager.ToggleMap();
    }

    #endregion
}