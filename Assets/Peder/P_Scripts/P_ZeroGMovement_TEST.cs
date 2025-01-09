using UnityEngine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class P_ZeroGMovement_TEST : MonoBehaviour
    {
        [Header("Player Movement")]
        public float MoveSpeed = 4.0f; // Movement speed for Zero-G
        public float RotationSpeed = 5.0f; // Mouse rotation speed
        public float GlideDamping = 0.95f; // Glide factor; closer to 1 = slower stop

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget; // Reference to camera target
        public float TopClamp = 90.0f; // Max vertical camera rotation
        public float BottomClamp = -90.0f; // Min vertical camera rotation

        private float _cinemachineTargetPitch;
        private Vector3 currentDirection = Vector3.zero; // The current direction of movement
        private Vector3 glideVelocity = Vector3.zero; // Velocity for gliding when stopping

        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private bool isGliding = false; // Flag to determine if gliding

        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (!isGliding)
            {
                Move(); // Move the player in the current direction
            }
            else
            {
                Glide(); // Handle gliding to simulate inertia
            }
        }

        private void LateUpdate()
        {
            CameraRotation(); // Handle camera rotation separately
        }

        // Player movement tied to the currentDirection vector
        private void Move()
        {
            glideVelocity = currentDirection * MoveSpeed; // Set glide velocity based on current direction
            _controller.Move(glideVelocity * Time.deltaTime); // Apply movement
        }

        // Simulate gliding by damping velocity
        private void Glide()
        {
            // Reduce velocity over time
            glideVelocity *= GlideDamping;

            // Stop completely when velocity is very low
            if (glideVelocity.magnitude < 0.01f)
            {
                glideVelocity = Vector3.zero;
                isGliding = false; // Stop gliding
            }

            _controller.Move(glideVelocity * Time.deltaTime);
        }

        // Camera rotation controlled by mouse input
        private void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= _threshold)
            {
                float deltaTimeMultiplier = Time.deltaTime;

                // Adjust rotation speed for better responsiveness
                float mouseSensitivity = 5.0f; // Adjust sensitivity as needed
                _cinemachineTargetPitch += _input.look.y * RotationSpeed * mouseSensitivity * deltaTimeMultiplier;

                // Clamp the vertical camera rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Apply rotation to the camera target
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // Rotate the player left/right
                transform.Rotate(Vector3.up * _input.look.x * RotationSpeed * mouseSensitivity * deltaTimeMultiplier);
            }
        }

        // Methods to set the movement direction from the clickable buttons
        public void SetDirection(Vector3 direction)
        {
            currentDirection = direction; // Update the movement direction
            isGliding = false; // Cancel gliding when a new direction is set
        }

        public void StopMovement()
        {
            currentDirection = Vector3.zero; // Clear current direction
            isGliding = true; // Start gliding
        }

        // Utility to clamp angles for vertical rotation
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
