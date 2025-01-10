using System.Collections;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    // Reference to the GameObject that determines the teleport destination
    public GameObject teleportDestination;

    public A_PickupScript A_PickupScript;
    public float teleportDelay;
    public float delayDelay;

    void Update()
    {
        
            Teleport();
        
    }

    private void Teleport()
    {
        StartCoroutine(TeleportAfterDelay());
        StartCoroutine(TeleportDelay());
    }

    
    private IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        transform.position = teleportDestination.transform.position;
        Debug.Log($"{gameObject.name} teleported after {teleportDelay} seconds to {teleportDestination}");
        
    }
    private IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(delayDelay);
    }
    }

