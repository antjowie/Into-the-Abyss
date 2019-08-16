using UnityEngine;
using UnityEngine.SceneManagement;

// This script contains functions that are called when pressing the corresponding buttons in the Main Menu
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        QuitGame();
    }

}
