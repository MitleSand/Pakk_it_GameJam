using UnityEngine;

public class PackagePikk : MonoBehaviour
{
    public PlatformMover platformMover;
    public Transform targetPosition;

    public A_PickupScript pickUpScript;

    void Start()
    {
        // Find the HoldPoint GameObject in the hierarchy
        GameObject holdPoint = GameObject.Find("PlayerCapsule/PlayerCameraRoot");

        if (holdPoint != null)
        {
            // Get the A_PickUpScript from the HoldPoint GameObject
            pickUpScript = holdPoint.GetComponent<A_PickupScript>();

            if (pickUpScript != null)
            {
                Debug.Log("A_PickUpScript found and assigned!");
            }
            else
            {
                Debug.LogError("A_PickUpScript not found on HoldPoint!");
            }
        }
        else
        {
            Debug.LogError("HoldPoint not found in hierarchy!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUp"))
        {
            // Find the index of the package in the PlatformMover's array
            int packageIndex = System.Array.IndexOf(platformMover.packages, other.gameObject);

            // Remove the package from the array
            if (packageIndex >= 0)
            {
                platformMover.packages[packageIndex] = null;
            }

            // Optionally deactivate the package
            //other.gameObject.SetActive(false);
            if (pickUpScript != null)
            {
                // Call a method or access a variable in A_PickUpScript
                // Example: pickUpScript.SomeMethod();
                pickUpScript.CompleteCurrentDelivery();
            }

            platformMover.CheckPackagesDelivered();
        }
    }
}
