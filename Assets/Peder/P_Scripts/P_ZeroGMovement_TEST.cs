using UnityEngine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class P_ZeroGMovement_TEST : MonoBehaviour
    {
        [Header("Player Movement")]
        public float MoveSpeed = 4.0f; // Movement speed for Zero-G
        public float RotationSpeed = 5.0f; // Increased mouse rotation speed

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget; // Reference to camera target
        public float TopClamp = 90.0f; // Max vertical camera rotation
        public float BottomClamp = -90.0f; // Min vertical camera rotation

        private float _cinemachineTargetPitch;
        private Vector3 currentDirection = Vector3.zero; // The current direction of movement

        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

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
            Move(); // Move the player in the current direction
        }

        private void LateUpdate()
        {
            CameraRotation(); // Handle camera rotation separately
        }

        // Player movement tied to the currentDirection vector
        private void Move()
        {
            _controller.Move(currentDirection * MoveSpeed * Time.deltaTime);
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
        }

        public void StopMovement()
        {
            currentDirection = Vector3.zero; // Stop movement
        }

        // New methods for forward/backward movement
        public void MoveForward()
        {
            SetDirection(transform.forward); // Move forward relative to player orientation
        }

        public void MoveBackward()
        {
            SetDirection(-transform.forward); // Move backward relative to player orientation
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
