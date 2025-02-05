using UnityEngine;

public class PackagePickup : MonoBehaviour
{
    public PlatformMover platformMover; // Reference to the PlatformMover script
    public Transform targetPosition; // Target position where the package will be moved (e.g., player's position)

    private bool packagePickedUp = false;


    /*void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "canPickUp" tag
        if (collision.gameObject.CompareTag("canPickUp"))
        {
            // Move the package to the target position (e.g., where the player is)
            collision.transform.position = targetPosition.position;

            // Inform PlatformMover that a package has been delivered
            platformMover.CheckPackagesDelivered();

           
        }
    }*/

    //public void OnTriggerStay(Collider other)
    /*{
        if (other.gameObject.CompareTag("canPickUp"))
        {
            other.transform.position = targetPosition.position;

        
        }

    }*/
    /*public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUp"))
        {
            platformMover.CheckPackagesDelivered();
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a Rigidbody component
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("canPickUp") && !packagePickedUp)
        {

            other.transform.position = targetPosition.position;
            other.transform.rotation = new Quaternion(90,other.transform.rotation.y,0,0);

            packagePickedUp = true;
            if (rb != null)
            {
                // Remove the Rigidbody component
                Destroy(rb);

            }
        }
    }

}