using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraHolder; // <- NICHT direkt die Kamera, sondern ein leerer Holder

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody rb;
    private float xRotation = 0f;

    private InputSystem_Actions controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // verhindert physikalisches Kippen

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update() => HandleLook();

    private void FixedUpdate() => HandleMovement();

    void HandleMovement()
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y; // behalte Gravitation bei
        rb.linearVelocity = velocity;
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
