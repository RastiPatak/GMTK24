using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    private Camera camera;

    public float moveSpeed = 5f;
    public float rotateSpeed = 720f; // Degrees per second
    public float jumpForce = 5f;
    public float lookSpeed = 2f;
    public float maxLookAngle = 75f;

    private Rigidbody rb;
    private bool isGrounded;

    private Transform cameraTransform;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        camera = Camera.main;
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        // Calculate direction
        Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;

        // Apply movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        // Get mouse input
        float yRotationInput = Input.GetAxis("Mouse X");
        float xRotationInput = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate character around the Y-axis (left and right)
        transform.Rotate(Vector3.up * yRotationInput * rotateSpeed * Time.deltaTime);

        // Rotate camera up and down
        xRotation -= xRotationInput;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        yRotation += yRotationInput * lookSpeed;

        cameraTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if we are grounded
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        // We are not grounded if we're not touching anything
        isGrounded = false;
    }

    private void LateUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}