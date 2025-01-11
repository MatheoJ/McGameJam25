using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // ---------------- New Dash Variables ----------------
    [Header("Dash Settings")]
    public KeyCode dashKey = KeyCode.LeftShift;  // Which key will trigger the dash
    public float dashForce = 25f;                // How powerful the dash is
    public float dashDuration = 0.2f;            // How long the dash should last
    public float dashCooldown = 1f;              // How long before you can dash again

    private bool canDash = true;                 // Whether the player can currently dash
    private bool isDashing = false;              // Whether the player is mid-dash
    // ----------------------------------------------------

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Check for dash input
        if (Input.GetKeyDown(dashKey) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void MovePlayer()
    {
        // if dashing, skip normal movement forces
        if (isDashing) return;

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // in air
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // if dashing, skip limiting velocity so we don't kill the dash
        if (isDashing) return;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    
    public IEnumerator ScaleSpeedForDuration(float duration, float scale)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= scale;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }

    // ------------------- Dash Routine --------------------
    public IEnumerator Dash()
    {
        canDash = false;     // You cannot dash again until the cooldown ends
        isDashing = true;    // We are now dashing

        // Capture current velocity so we can restore it or modify it if desired
        Vector3 originalVelocity = rb.linearVelocity;

        // Determine dash direction based on movement inputs:
        // If there's no input, we could default to forward.
        Vector3 dashDirection = moveDirection.normalized;
        if (dashDirection.magnitude == 0)
        {
            dashDirection = orientation.forward;
        }

        // Apply a force instantly by setting the rigidbody's velocity
        rb.linearVelocity = dashDirection * dashForce;

        // Wait for the dash to conclude
        yield return new WaitForSeconds(dashDuration);

        // Optionally restore velocity or set it to zero
        rb.linearVelocity = originalVelocity;

        // Dash is over
        isDashing = false;

        // Wait for cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    // -----------------------------------------------------
}
