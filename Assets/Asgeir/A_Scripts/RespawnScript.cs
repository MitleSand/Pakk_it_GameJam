using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RespawnWithTrigger : MonoBehaviour
{
    public Transform respawnPoint; // Assign this to your respawn point in the Inspector
    public float respawnDelay = 2.0f; // Time to wait before respawning

    public AudioSource respawnSound;


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a "Player" tag
        if (other.CompareTag("Player"))
        {
            respawnSound.Play();
            // Start the respawn process
            StartCoroutine(RespawnPlayer(other.gameObject));
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        // Optional: Disable the player object to simulate "death"
        player.SetActive(false);

        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Move the player to the respawn point
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        // Optional: Reset velocity if it has a Rigidbody
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable the player object
        player.SetActive(true);
    }
}

