using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float speed = 5.0f;
    [SerializeField] [Range(1, 5)] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] [Range(0, 1)] private float groundDistance;

    private CharacterController controller;
    private SprintController sprintController;
    
    [NonSerialized]  public bool isSprinting;
    private bool isGrounded;
    private float sprintSpeed;
    private float walkSpeed;
    private float yVelocity;


    private void Start()
    {
        //Set differing speed values.
        walkSpeed = speed;
        sprintSpeed = speed * sprintMultiplier;

        controller = GetComponent<CharacterController>();
        sprintController = GetComponent<SprintController>();
    }

    private void Update()
    {
        Gravity();
        if (isSprinting) StaminaCheck();
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the grounds position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }


    public void Movement(Vector2 inputMovement)
    {
        var playerTransform = transform;
        var move = playerTransform.right * inputMovement.x + playerTransform.forward * inputMovement.y;
        controller.Move(move * (speed * Time.deltaTime));
    }


    private void Gravity()
    {
        IsGroundedCheck();
        // Gravity
        if (isGrounded && yVelocity < 0)
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
        if (isGrounded) yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void StaminaCheck()
    {
        if (sprintController.isOutOfStamina)
        {
            StopSprinting();
            return;
        }

        if (isSprinting) sprintController.UseStamina();
    }

    public void StartSprinting()
    {
        if (IsMoving() && !sprintController.isOutOfStamina)
        {
            speed = sprintSpeed;
            isSprinting = true;
        }

    }

    public void StopSprinting()
    {
        speed = walkSpeed;
        isSprinting = false;
    }

    public bool IsMoving()
    {
        return controller.velocity.magnitude > 0.05f;
    }

    private void IsGroundedCheck()
    {
        isGrounded =  Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}