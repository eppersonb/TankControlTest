using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurning : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;
    private Vector3 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get movement input from player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        movementInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        // Rotate character towards movement direction
        if (movementInput.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        // Move character
        Vector3 movement = movementInput * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
