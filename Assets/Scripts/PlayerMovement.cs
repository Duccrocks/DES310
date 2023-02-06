using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravity = 9.81f;
    private CharacterController controller;
    private float yVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        Gravity();
        Jump();
    }

    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        var vertical = Input.GetAxis("Vertical") * speed;

        var playerTransform = transform;
        var moveDirection = playerTransform.right * horizontal + playerTransform.forward * vertical;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Gravity()
    {
        // Gravity
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = 0;
        }
        else
        {
            yVelocity -= gravity * Time.deltaTime;
            controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) yVelocity = jumpHeight;
    }
}