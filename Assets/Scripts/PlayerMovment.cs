using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    private CharacterController controller;
    private float yVelocity;

    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        Gravity(moveDirection);

    }
    private void Gravity(Vector3 moveDirection)
    {
        //Reset the player to spawn if they fall off.
        if (gameObject.transform.position.y < -20)
        {
            gameObject.transform.position = new Vector3(0, 10, 0);
        }
        //Gravity
        yVelocity += gravity;
        moveDirection.y = yVelocity;

        //Moves the player
        controller.Move(moveDirection);
    }

    private void Jump()
    {
       
    }

  
}
