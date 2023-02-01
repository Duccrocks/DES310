using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravity = 9.81f;
    private bool wasGrounded = true;
    private CharacterController controller;
    private float yVelocity = 0;

    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
  
        Gravity();
        Jump();
    }
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        Debug.Log($"Move diection: {moveDirection}");
        controller.Move(moveDirection * Time.deltaTime);

        //Gravity
       // Gravity(moveDirection);

    }
    private void Gravity()
    {
        // Gravity
        if (controller.isGrounded&&yVelocity<0)
        {
                yVelocity = 0;
        }
        else
        {
            yVelocity -= gravity * Time.deltaTime;
            controller.Move(new Vector3(0, yVelocity, 0)*Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (!controller.isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space)) yVelocity = jumpHeight;
    }

  
}
