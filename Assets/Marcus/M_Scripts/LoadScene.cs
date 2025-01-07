using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the platform moves
    public float moveDuration = 3f; // Time in seconds before the scene changes
    public string nextSceneName; // Name of the next scene to load
    public GameObject[] packages;


    private bool isMoving = false;
    private bool allPackagesDelivered = false;

    void Update()
    {
        if (isMoving)
        {
            // Move the platform upwards
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        if (!isMoving && allPackagesDelivered)
        {
            isMoving = true; // Start moving the platform
            StartCoroutine(LoadNextScene()); // Use IEnumerator for scene change delay
        }
    }

    public void CheckPackagesDelivered()
    {
        allPackagesDelivered = true;
        foreach (GameObject package in packages)
        {
            if (package != null)
            {
                allPackagesDelivered = false; //Found an undelievered package
                break;
            }
        }
    }  


    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(moveDuration); // Wait for the specified duration

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Load the next scene
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
}