using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method is called when the Play button is clicked
    public void PlayGame()
    {
        // Replace "GameScene" with the name of your scene
        SceneManager.LoadScene("MainScene");
    }

    // This method is called when the Quit button is clicked
    public void QuitGame()
    {
        // Quits the application
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
