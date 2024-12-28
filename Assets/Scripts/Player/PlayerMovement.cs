using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public Transform cameraTransform; // Reference to the camera's transform
    private Rigidbody rb;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.1f; // Distance for ground check
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground detection

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the Player!");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Camera transform is not assigned!");
        }
    }

    private void Update()
    {
        GroundCheck();

        MoveWithCamera();

        Jump();
    }

    private void GroundCheck()
    {
        // Perform a raycast to check if the player is grounded
        isGrounded = Physics.Raycast(transform.position + new Vector3(0f,0.04f,0f), Vector3.down, groundCheckDistance, groundLayer);

        // Debug visualization of the ground check
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private void MoveWithCamera(){
        // Read the "Move" action value
        Vector2 moveValue = moveAction.action.ReadValue<Vector2>();

        // Get camera-relative directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Flatten camera directions to ignore vertical tilt
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction relative to the camera
        Vector3 moveDirection = (cameraForward * moveValue.y + cameraRight * moveValue.x) * moveSpeed;

        // Apply movement
        Vector3 velocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(moveDirection.x, velocity.y, moveDirection.z);

        // Rotate player to face the movement direction
        if (moveDirection.sqrMagnitude > 0.01f) // Avoid tiny movements
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void Jump(){
        // Check if the jump button is pressed and player is grounded
        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
