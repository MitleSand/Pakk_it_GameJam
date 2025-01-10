using UnityEngine;
using System.Collections.Generic;

public class MainMenuPackageSpawner: MonoBehaviour
{
    // List of package GameObjects to choose from
    public List<GameObject> packageOptions;

    // Spawn point for the packages
    public Transform spawnPoint;

    // Spawn interval (time in seconds)
    public float spawnInterval = 5f;

    // Timer to control spawning
    private float spawnTimer;

    void Start()
    {
        // Validate references
        if (packageOptions == null || packageOptions.Count == 0)
        {
            Debug.LogError("No package options assigned! Add GameObjects to the packageOptions list.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
        }

        // Initialize the timer
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        // Spawn a package when the timer reaches 0
        if (spawnTimer <= 0f)
        {
            SpawnRandomPackage();
            spawnTimer = spawnInterval; // Reset the timer
        }
    }

    private void SpawnRandomPackage()
    {
        if (packageOptions != null && packageOptions.Count > 0 && spawnPoint != null)
        {
            // Select a random package from the list
            GameObject randomPackage = packageOptions[Random.Range(0, packageOptions.Count)];

            // Spawn the selected package at the spawn point
            Instantiate(randomPackage, spawnPoint.position, spawnPoint.rotation);

            Debug.Log("Random package spawned: " + randomPackage.name);
        }
        else
        {
            Debug.LogError("Cannot spawn package. Missing references or package options.");
        }
    }
}
