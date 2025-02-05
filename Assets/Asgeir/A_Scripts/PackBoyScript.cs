using System.Collections;
using StarterAssets;
using UnityEngine;

public class PackBoyScript : MonoBehaviour
{
    public Animator animator;
    private bool revealedPackboy = false;
    public float toggleCooldown = 1f; // Delay in seconds before the button can be pressed again

    // Tracks whether the cursor is currently visible
    private bool isCursorVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            // Toggle the state of revealedPackboy
            revealedPackboy = !revealedPackboy;

            // Toggle cursor visibility
            isCursorVisible = !isCursorVisible;

            // Update cursor state
            Cursor.visible = isCursorVisible;

            // Optionally, lock or unlock the cursor
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;

            // Update the animator based on the new state
            animator.SetBool("packboyReveal", revealedPackboy);

            StartCoroutine(ToggleCooldown());
        }

        

    }

    private IEnumerator ToggleCooldown()
    {
        yield return new WaitForSeconds(toggleCooldown); // Wait for the cooldown duration
    }
}
