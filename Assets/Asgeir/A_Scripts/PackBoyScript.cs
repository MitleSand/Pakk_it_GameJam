using System.Collections;
using UnityEngine;

public class PackBoyScript : MonoBehaviour
{

    public Animator animator;

    public bool revealedPackboy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!revealedPackboy)
            {
                animator.SetBool("PackboyReveal", true);
                
            }

            if (revealedPackboy)
            {
                animator.SetBool("PackboyReveal", false);
            }
        }


    }

    private IEnumerator PackBoyDelay()
    {
        yield return new WaitForSeconds(2);
    }
}
