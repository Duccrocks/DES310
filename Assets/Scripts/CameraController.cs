using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    [Header("Camera Settings")] 
    [SerializeField] [Range(0.001f, 0.1f)] private float sensitivity = 0.0500f;
    [SerializeField] [Range(0, 90)] private float clampAngle = 90f;
    private float horizontalRotation;
    private float verticalRotation;

    private void Awake()
    {
        //Locks the cursor.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        StaticVariables.cameraSensitivity = sensitivity;
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
        //If the player has been in the game before sets the sensitivity to the players prefer.
        if (PlayerPrefs.HasKey("sensitivity"))
            StaticVariables.cameraSensitivity = PlayerPrefs.GetFloat("sensitivity",sensitivity);
        
            

    }
    /// <summary>
    ///     Moves the camera by mouse.
    /// </summary>
    /// <param name="inputRotation">Vector2 from input.</param>
    public void Look(Vector2 inputRotation)
    {
        //Shows which way the player is looking.
        var cameraTransform = transform;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 2, Color.red);

        horizontalRotation += inputRotation.x * StaticVariables.cameraSensitivity;
        verticalRotation -= inputRotation.y * StaticVariables.cameraSensitivity;

        //Clamps the camera from looping around vertically.
        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        //Rotates the camera.
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

}