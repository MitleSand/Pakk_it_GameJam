using System.Collections;
using UnityEngine;

public class P_PackBoyScript : MonoBehaviour
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
        PackBoyAnimation();
    }

    private void PackBoyAnimation()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!revealedPackboy)
            {
                animator.SetBool("packboyReveal", true);
                revealedPackboy = true;
                Debug.Log("Reaveal Packboy");
                PackBoyDelay();
                
            }
            else
            {
                animator.SetBool("packboyReveal", false);
                revealedPackboy = false;
                Debug.Log("Unreveal Packboy");
                PackBoyDelay();
            }
        }
    }
        

    private IEnumerator PackBoyDelay()
    {
        yield return new WaitForSeconds(1);
    }
}
