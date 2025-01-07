using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZeroGMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Rotate the character based on mouse input
        RotateCharacter();
    }

    void FixedUpdate()
    {
        // Apply movement based on player input
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // Get input from WASD/Arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down
        float ascend = Input.GetKey(KeyCode.Space) ? 1f : (Input.GetKey(KeyCode.LeftShift) ? -1f : 0f); // Space/Shift for up/down

        // Combine the inputs into a single direction vector
        Vector3 direction = transform.right * horizontal + transform.forward * vertical + transform.up * ascend;

        // Apply movement
        rb.AddForce(direction * movementSpeed, ForceMode.Acceleration);
    }

    void RotateCharacter()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the character around the Y axis (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down (clamp to avoid flipping)
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float cameraRotation = Mathf.Clamp(mainCamera.transform.localEulerAngles.x - mouseY, -90f, 90f);
            mainCamera.transform.localEulerAngles = new Vector3(cameraRotation, mainCamera.transform.localEulerAngles.y, 0f);
        }
    }
}
