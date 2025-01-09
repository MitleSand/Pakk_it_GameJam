using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_PickupScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos; // Default position for holding the object
    public Transform inspectPos; // Position for inspecting the object
    public float throwForce = 500f; // Force at which the object is thrown
    public float pickUpRange = 10f; // How far the player can pick up the object
    public float moveSpeed = 5f; // Speed for moving the object during inspection

    private GameObject heldObj; // Object currently being held
    private Rigidbody heldObjRb; // Rigidbody of the held object
    private bool canDrop = true; // Prevents dropping/throwing while inspecting
    private bool isInspecting = false; // Tracks if the player is inspecting the object
    private int LayerNumber; // Layer index for holding objects



    public GameObject delivery;

    // List to hold delivery GameObjects
    public List<GameObject> deliveryPoints;

    // Index to track current delivery point
    private int currentDeliveryIndex = 0;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); // Assign layer for held objects

        // Validate the list
        if (deliveryPoints == null || deliveryPoints.Count == 0)
        {
            Debug.LogError("A_PickupScript:.. Delivery points are not assigned!");
        }
        

    }

    void Update()
    {
        // Pick up or drop the object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                TryPickUpObject();

            }
            else if (canDrop)
            {
                StopClipping();
                DropObject();

            }
            else
            {

            }
        }

        // Inspect the held object
        if (heldObj != null && Input.GetKeyDown(KeyCode.Q))
        {
            isInspecting = !isInspecting; // Toggle inspection mode
        }

        // Handle held object movement and interactions
        if (heldObj != null)
        {
            DeactivateCurrentDeliveryPoint();
            //delivery.gameObject.SetActive(false);
            if (isInspecting)
            {
                MoveObjectToPosition(inspectPos.position, inspectPos.rotation); // Move to inspect position
                canDrop = false; // Prevent dropping while inspecting

            }
            else
            {
                MoveObjectToPosition(holdPos.position, holdPos.rotation); // Move to hold position
                canDrop = true;

            }

            // Throw the object
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop)
            {
                StopClipping();
                ThrowObject();
                //delivery.gameObject.SetActive(true);
                // Getting the deliverypoint when the object is thrown
                ActivateDeliveryPoint(currentDeliveryIndex);
            }
        }
        // Example of switching to the next delivery point when pressing 'N'
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            GoToNextDeliveryPoint();
        }*/
    }
    /*public void GoToNextDeliveryPoint()
    {
        if (deliveryPoints != null && deliveryPoints.Count > 0)
        {
            // Deactivate the current delivery point
            deliveryPoints[currentDeliveryIndex].SetActive(false);

            // Move to the next delivery point
            currentDeliveryIndex = (currentDeliveryIndex + 1) % deliveryPoints.Count;
            // Activate the new delivery point
            ActivateDeliveryPoint(currentDeliveryIndex);

            // Log the current delivery point
            Debug.Log("Current Delivery Point: " + deliveryPoints[currentDeliveryIndex].name);
        }
        else
        {
            Debug.LogWarning("No delivery points available!");
        }
    }*/
    // Call this method to mark the current delivery point as completed
    public void CompleteCurrentDelivery()
    {
        if (deliveryPoints != null && deliveryPoints.Count > 0)
        {
            // Deactivate the current delivery point
            DeactivateCurrentDeliveryPoint();

            // Move to the next delivery point if there is one
            currentDeliveryIndex++;
            if (currentDeliveryIndex < deliveryPoints.Count)
            {
                ActivateDeliveryPoint(currentDeliveryIndex);
                Debug.Log("Moved to the next delivery point: " + deliveryPoints[currentDeliveryIndex].name);
            }
            else
            {
                Debug.Log("All delivery points are completed!");
            }
        }
    }

    // Function to activate a specific delivery point
    private void ActivateDeliveryPoint(int index)
    {
        for (int i = 0; i < deliveryPoints.Count; i++)
        {
            // Ensure only the specified delivery point is active
            deliveryPoints[i].SetActive(i == index);
        }

        Debug.Log("Active Delivery Point: " + deliveryPoints[index].name);
    }

    // Function to deactivate the current delivery point
    private void DeactivateCurrentDeliveryPoint()
    {
        if (currentDeliveryIndex < deliveryPoints.Count)
        {
            deliveryPoints[currentDeliveryIndex].SetActive(false);
            Debug.Log("Deactivated Delivery Point: " + deliveryPoints[currentDeliveryIndex].name);
        }
    }

    //______________________________________________________________________________________________________

    void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
        {
            if (hit.transform.gameObject.tag == "canPickUp")
            {
                PickUpObject(hit.transform.gameObject);
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();

            // Disable physics interactions temporarily
            heldObjRb.isKinematic = true;

            // Move and parent the object to the hold position
            heldObj.transform.position = holdPos.position;
            heldObj.transform.rotation = holdPos.rotation;
            heldObj.transform.parent = holdPos;

            // Change the object's layer to avoid interactions
            heldObj.layer = LayerNumber;

            // Ignore collision with the player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Reset layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null; // Clear references
    }

    void MoveObjectToPosition(Vector3 targetPosition, Quaternion targetRotation)
    {
        heldObj.transform.position = Vector3.Lerp(heldObj.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        heldObj.transform.rotation = Quaternion.Lerp(heldObj.transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

    void StopClipping()
    {
        float clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

    

}
