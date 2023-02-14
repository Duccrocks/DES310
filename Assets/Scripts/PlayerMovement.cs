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
    [SerializeField][Range(0, 1)] private float groundDistance;
    private CharacterController controller;
    private float yVelocity;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
    }


    public void Movement(Vector2 inputMovement)
    {
        var move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        controller.Move(move * (speed * Time.deltaTime));
    }


    private void Gravity()
    {
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
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the grounds position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
