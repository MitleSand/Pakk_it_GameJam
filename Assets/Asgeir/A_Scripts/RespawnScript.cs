using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class RespawnWithTrigger : MonoBehaviour
{
    private PlayerInput playerInput;

    

    public float respawnDelay = 2.0f; // Time to wait before respawning
    private AudioSource respawnSound;
    public AudioClip respawnSoundClip;

    public string loadScene;
    
    private void OnTriggerEnter(Collider other)
    {
        playerInput = other.GetComponent<PlayerInput>();
        respawnSound = GetComponent<AudioSource>();
        // Check if the object entering the trigger has a "Player" tag
        if (other.CompareTag("Player"))
        {

            playerInput.enabled = false;
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

