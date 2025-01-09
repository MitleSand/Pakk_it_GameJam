using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{


    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 120f;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isCountdown && countdownTimer > 0)
        {
        countdownTimer -= Time.deltaTime;
        minutes = Mathf.FloorToInt(countdownTimer / 60f);
        seconds = Mathf.FloorToInt(countdownTimer - minutes * 60);
        

        } else if (!isCountdown)
        {
            timeCounter += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeCounter / 60f);
            seconds = Mathf.FloorToInt(timeCounter - minutes * 60);
            
        }
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if (countdownTimer <= 0)
        {
            ReloadCurrentScene();
        }

    }

    // Method to reload the current scene
    public void ReloadCurrentScene()
    {
        // Get the active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the active scene by its name
        SceneManager.LoadScene(currentScene.name);

        Debug.Log("Scene reloaded: " + currentScene.name);
    }
}
