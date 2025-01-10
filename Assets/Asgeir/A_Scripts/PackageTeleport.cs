using System.Collections;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    // Reference to the GameObject that determines the teleport destination
    public GameObject teleportDestination;

    public A_PickupScript A_PickupScript;
    public float teleportDelay;
    public float delayDelay;

    private Rigidbody rb;




    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        


        //StartCoroutine(TeleportAfterDelay());
        
    }

    private void Update()
    {
        if (A_PickupScript.completedDelivery)
        {
            transform.position = teleportDestination.transform.position;
        }
    }



    private IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        transform.position = teleportDestination.transform.position;
        
        
    }
    
    }

