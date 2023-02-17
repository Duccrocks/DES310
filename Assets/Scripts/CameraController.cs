using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float horizontalRotation;
    private float verticalRotation;
    
    [SerializeField] private PlayerMovement player;
    [field: Header("Camera Settings")] 
    [SerializeField] [Range(0.1f, 10)] private float sensitivity = 10f;
    [SerializeField] [Range(0, 85)] private float clampAngle = 85f;

    private void Awake()
    {
        //Locks the cursor.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
        //If the player has been in the game before sets the sensitivity to the players prefer.
        if (PlayerPrefs.HasKey("sensitivity"))
            sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    /// <summary>
    ///     Moves the camera by mouse.
    /// </summary>
    /// <param name="inputRotation">Vector2 from input.</param>
    public void Look(Vector2 inputRotation)
    {
        //Shows which way the player is looking.
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);

        horizontalRotation += inputRotation.x * sensitivity * Time.deltaTime;
        verticalRotation -= inputRotation.y * sensitivity * Time.deltaTime;

        //Clamps the camera from looping around vertically.
        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        //Rotates the camera.
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}