using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    private Camera camera;

    public float playerSpeed = 5f;
    public float lookSpeed = 2f;
    public float maxLookAngle = 75f;

    private bool isGrounded;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    [SerializeField] private float jumpHeight = 1.0f;

    private CharacterController controller;

    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Move();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = camera.transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    private void Rotate()
    {
        float yRotationInput = Input.GetAxis("Mouse X");
        float xRotationInput = Input.GetAxis("Mouse Y") * lookSpeed;

        xRotation -= xRotationInput;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        yRotation += yRotationInput * lookSpeed;

        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    private void LateUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}