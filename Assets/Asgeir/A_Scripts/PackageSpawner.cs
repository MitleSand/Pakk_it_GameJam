using UnityEngine;
using System.Collections.Generic;

public class PackageSpawner : MonoBehaviour
{
    // List of package objects to choose from
    public List<GameObject> packageOptions;

    // Reference to the spawn point
    public Transform spawnPoint;

    // Reference to the delivery system
    public A_PickupScript deliverySystem;

    // List to keep track of all spawned packages
    private List<GameObject> spawnedPackages = new List<GameObject>();

    void Start()
    {
        // Validate references
        if (packageOptions == null || packageOptions.Count == 0)
        {
            Debug.LogError("No package options assigned! Add some GameObjects to the packageOptions list.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
        }

        if (deliverySystem == null)
        {
            Debug.LogError("Delivery system is not assigned!");
        }
    }

    public void OnPackageDelivered()
    {
        // Complete the current delivery in the delivery system
        if (deliverySystem != null)
        {
            deliverySystem.CompleteCurrentDelivery();
        }

        // Spawn a new package
        SpawnPackage();
    }

    private void SpawnPackage()
    {
        if (packageOptions != null && packageOptions.Count > 0 && spawnPoint != null)
        {
            // Randomly select a package from the list
            GameObject packageToSpawn = packageOptions[Random.Range(0, packageOptions.Count)];

            // Instantiate the selected package
            GameObject newPackage = Instantiate(packageToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Add it to the list of spawned packages
            spawnedPackages.Add(newPackage);

            Debug.Log("New package spawned! Total packages: " + spawnedPackages.Count);
        }
        else
        {
            Debug.LogError("Cannot spawn package. Missing references or empty package options!");
        }
    }

    public void ClearAllPackages()
    {
        // Destroy all packages in the list and clear the list
        foreach (GameObject package in spawnedPackages)
        {
            if (package != null)
            {
                Destroy(package);
            }
        }

        spawnedPackages.Clear();
        Debug.Log("All packages cleared!");
    }
}
