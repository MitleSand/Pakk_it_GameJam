using System.Collections;
using UnityEngine;

public class RespawnOnlyCanPickUp : MonoBehaviour
{
    public Transform respawnPoint; // The location to respawn objects
    public float respawnDelay = 2.0f; // Delay before respawning

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "canPickUp" tag
        if (other.CompareTag("canPickUp"))
        {
            // Start the respawn process
            StartCoroutine(RespawnObject(other.gameObject));
        }
    }
   
    private IEnumerator RespawnObject(GameObject obj)
    {
        // Optional: Disable the object to simulate removal from the world
        obj.SetActive(false);

        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Move the object to the respawn point
        obj.transform.position = respawnPoint.position;
        obj.transform.rotation = respawnPoint.rotation;

        // Optional: Reset velocity if the object has a Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable the object
        obj.SetActive(true);
    }
}
