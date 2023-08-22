using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
    [Header("General")]
    [SerializeField] private float movementSpeed = 15;

    [Header("Falling")]
    [SerializeField] private float gravityFactor = 1f;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayers;

    [Header("Jumping")]
    [SerializeField] private bool canAirControl = false;
    [SerializeField] private float jumpSpeed = 7f;

    [Header("Looking")]
    [SerializeField] private Transform camera;
    [SerializeField] private float mouseSensitivity;

    private CharacterController controller;

    private float verticalSpeed = 0f;
    private float verticalRotation = 0f;
    public bool isGrounded = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        // Figure out if we are on the ground
        RaycastHit collision;
        if (Physics.Raycast(groundPosition.position, Vector3.down, out collision, 0.2f, groundLayers)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        // if were not on the ground update verticle speed
        if (isGrounded) {
            verticalSpeed = 0f;
        } else {
            float accerleration = gravityFactor * -9.81f * Time.deltaTime;
            verticalSpeed += accerleration;
        }

        // rotate the camera based on the mouse movment
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        camera.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);

        Vector3 x = Vector3.zero;
        Vector3 y = Vector3.zero;
        Vector3 z = Vector3.zero;

        // handle jump
        if (isGrounded && Input.GetButtonDown("Jump")) {
            verticalSpeed = jumpSpeed;
            isGrounded = false;
            y = transform.up * verticalSpeed;
        } else {
            y = transform.up * verticalSpeed;
        }
        y = transform.up * verticalSpeed;


        // handle movement
        if (isGrounded || canAirControl) {
            x = transform.right * Input.GetAxis("Horizontal") * movementSpeed; // strafing
            z = transform.forward * Input.GetAxis("Vertical") * movementSpeed; // strafing
        }

        Vector3 movement = (x + y + z) * Time.deltaTime;
        controller.Move(movement);
    }
}
