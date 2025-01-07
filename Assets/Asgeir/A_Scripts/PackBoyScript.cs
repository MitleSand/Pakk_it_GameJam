using System.Collections;
using UnityEngine;

public class PackBoyScript : MonoBehaviour
{

    public Animator animator;

    private bool revealedPackboy = false;

    private bool canToggle = true; // Prevents spamming

    public float toggleCooldown = 1f; // Delay in seconds before the button can be pressed again


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the state of revealedPackboy
            revealedPackboy = !revealedPackboy;

            // Update the animator based on the new state
            animator.SetBool("packboyReveal", revealedPackboy);

            StartCoroutine(ToggleCooldown());
        }
    }

    private IEnumerator ToggleCooldown()
    {
        canToggle = false; // Disable toggling
        yield return new WaitForSeconds(toggleCooldown); // Wait for the cooldown duration
        canToggle = true; // Re-enable toggling
    }
}
