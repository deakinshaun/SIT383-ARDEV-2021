using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private CharacterController controller;

    [SerializeField] private float cameraSpeed;
    [SerializeField] private float speed = 6.0F;
    [SerializeField] private float jumpSpeed = 8.0F;
    [SerializeField] private float gravity = 20.0F;
    private float rotateX;
    private float rotateY;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        RotateCamera();
        RotatePlayer();
        PlayerMove();
    }

    private void RotateCamera()
    {
        rotateX += Input.GetAxis("Mouse Y")*Time.deltaTime*cameraSpeed;
        rotateX = Mathf.Clamp(rotateX, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(-rotateX, 0, 0);
    }

    private void RotatePlayer()
    {
        rotateY += Input.GetAxis("Mouse X")*Time.deltaTime*cameraSpeed;
        transform.rotation = Quaternion.Euler(0,rotateY,0);
    }

    private void PlayerMove()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDirection = moveDirection.normalized;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
