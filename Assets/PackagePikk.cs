using UnityEngine;

public class PackagePikk : MonoBehaviour
{
    public PlatformMover platformMover;
    public Transform targetPosition;

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

            platformMover.CheckPackagesDelivered();
        }
    }
}
