using System.Collections;
using UnityEngine;

public class PackBoyScript : MonoBehaviour
{

    public Animator animator;

    private bool revealedPackboy = false;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the state of revealedPackboy
            revealedPackboy = !revealedPackboy;

            // Update the animator based on the new state
            animator.SetBool("packboyReveal", revealedPackboy);

            
        }
    }

    private IEnumerator PackBoyDelay()
    {
        
        yield return new WaitForSeconds(1);

    }
}
