using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class RespawnWithTrigger : MonoBehaviour
{
    
    public float respawnDelay = 2.0f; // Time to wait before respawning

    public AudioSource respawnSound;
    public AudioClip respawnSoundClip;

    public string loadScene;
    

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a "Player" tag
        if (other.CompareTag("Player"))
        {
            respawnSound.PlayOneShot(respawnSoundClip);
            // Start the respawn process
            StartCoroutine(RespawnPlayer());

            
        }
    }

    private IEnumerator RespawnPlayer()
    {
       
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(loadScene);
    }
}

