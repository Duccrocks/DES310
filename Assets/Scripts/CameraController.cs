using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    private float horizontalRotation;
    private float verticalRotation;

    [field: Header("Camera Settings")]
    [field: SerializeField]
    public float Sensitivity { get; set; } = 10f;
    [SerializeField] private float clampAngle = 85f;

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
            Sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    private void Update()
    {
        Look();
        //Shows which way the player is looking.
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    /// <summary>
    ///     Moves the camera by mouse.
    /// </summary>
    private void Look()
    {
        //Maps the vertical and horizontal mouse movements to vectors. 
        var mouseVertical = -Input.GetAxis("Mouse Y");
        var mouseHorizontal = Input.GetAxis("Mouse X");
        
        verticalRotation += mouseVertical * Sensitivity * Time.deltaTime;
        horizontalRotation += mouseHorizontal * Sensitivity * Time.deltaTime;

        //Clamps the camera from looping around vertically.
        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        //Rotates the camera.
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }
}