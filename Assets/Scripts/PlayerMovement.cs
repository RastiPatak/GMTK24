using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity;

public class CharacterController3D : MonoBehaviour
{
    private Camera camera;

    public float playerSpeed = 5f;
    public float lookSpeed = 2f;
    public float maxLookAngle = 75f;

    [SerializeField] private float fallingMultiplier = 2f;
    [SerializeField] private float jumpHeight = 1.0f;

    private bool isGrounded;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    private CharacterController controller;

    void Start()
    {
        camera = Camera.main;
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        Move();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = 0f;
            Jump();
        }

        playerVelocity.y += gravityValue * fallingMultiplier * Time.deltaTime;
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