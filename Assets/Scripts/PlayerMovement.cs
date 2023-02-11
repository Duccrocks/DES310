using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravity = -9.81f;
    
    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] [Range(0, 1)] private float groundDistance;
    private CharacterController controller;
    private Vector2 inputMovement;
    private float yVelocity;

    public Vector2 MovementValue { get => inputMovement; set => inputMovement = value;}

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        Gravity();
        //Jump();
    }

    private void Movement()
    {
        var move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        controller.Move(move * (speed * Time.deltaTime));
        //Debug.Log($"Player grounded state {IsGrounded()}");
    }


    private void Gravity()
    {
        // Debug.Log($"Player y velocity {yVelocity}");
        // Gravity
        if (IsGrounded() && yVelocity < 0)
        {
            yVelocity = 0;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
            controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
        }
    }


    public void Jump()
    {
        if (IsGrounded())
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jumping");
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
