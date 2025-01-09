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

        [Header("Grounded Movement")]
        public float GroundMoveSpeed = 5.0f; // Movement speed on the ground
        public float JumpForce = 5.0f; // Jump force when grounded
        public float Gravity = -9.81f; // Gravity value

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget; // Reference to camera target
        public float TopClamp = 90.0f; // Max vertical camera rotation
        public float BottomClamp = -90.0f; // Min vertical camera rotation

        private float _cinemachineTargetPitch;
        private Vector3 currentDirection = Vector3.zero; // The current direction of movement
        private Vector3 glideVelocity = Vector3.zero; // Velocity for gliding when stopping
        private Vector3 groundedVelocity = Vector3.zero; // Velocity for grounded movement

        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private bool isGliding = false; // Flag to determine if gliding

        private bool isGrounded = false;
        private P_PackBoyScript_TEST _packBoyScript;

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
            _packBoyScript = Object.FindFirstObjectByType<P_PackBoyScript_TEST>();
        }

        private void Update()
        {
            isGrounded = _controller.isGrounded;

            if (isGrounded)
            {
                HandleGroundedMovement();
            }
            else if (!isGliding)
            {
                Move(); // Move the player in the current direction (Zero-G)
            }
            else
            {
                Glide(); // Handle gliding to simulate inertia (Zero-G)
            }
        }

        private void LateUpdate()
        {
            if (_packBoyScript != null && !_packBoyScript.revealedPackboy)
            {
                CameraRotation(); // Handle camera rotation separately when packboy is hidden
            }
        }

        // Player movement tied to the currentDirection vector (Zero-G)
        private void Move()
        {
            glideVelocity = currentDirection * MoveSpeed; // Set glide velocity based on current direction
            _controller.Move(glideVelocity * Time.deltaTime); // Apply movement
        }

        // Simulate gliding by damping velocity (Zero-G)
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

        // Methods to set the movement direction from the clickable buttons (Zero-G)
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

        // Handle movement when grounded
        private void HandleGroundedMovement()
        {
            Vector3 move = new Vector3(_input.move.x, 0.0f, _input.move.y);
            Vector3 moveDirection = transform.TransformDirection(move) * GroundMoveSpeed;

            groundedVelocity.x = moveDirection.x;
            groundedVelocity.z = moveDirection.z;

            if (_controller.isGrounded)
            {
                if (_input.jump)
                {
                    groundedVelocity.y = JumpForce;
                }
                else
                {
                    groundedVelocity.y = 0.0f; // Reset vertical velocity when on the ground
                }
            }
            else
            {
                groundedVelocity.y += Gravity * Time.deltaTime; // Apply gravity when airborne
            }

            _controller.Move(groundedVelocity * Time.deltaTime);
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
