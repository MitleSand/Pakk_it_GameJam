using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves
    public Vector3 moveDirection = Vector3.right; // Direction to move the object (default: right)

    private void OnTriggerStay(Collider other)
    {
        // Check if the object has a Rigidbody to ensure it can move
        if (other.attachedRigidbody != null)
        {
            // Move the object slowly in the specified direction
            other.attachedRigidbody.MovePosition(other.transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
