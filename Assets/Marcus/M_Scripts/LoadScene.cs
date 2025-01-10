using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using StarterAssets;


    public class PlatformMover : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float moveDuration = 3f;
        public string nextSceneName;
        public GameObject[] packages;

        private bool isMoving = false;

        
        


        void Update()
        {
            
            if (isMoving)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
        }

        public void CheckPackagesDelivered()
        {
            bool allDelivered = true;

            foreach (GameObject package in packages)
            {
                if (package != null)
                {
                    allDelivered = false;
                    break;
                }
            }

            if (allDelivered)
            {


                
                    StartMoving();
                    Debug.Log("Ismoving");
                    
                

            }
        }

        private void StartMoving()
        {
            if (!isMoving)
            {
                isMoving = true;
                StartCoroutine(LoadNextScene());
            }
        }

        private IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(moveDuration);

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not set!");
            }
        }
    }
