using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// This script contains functions that are called when pressing the corresponding buttons in the Main Menu
public class MainMenu : MonoBehaviour
{
    private GameObject selectedObj;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        QuitGame();
    }

    // The code underneath makes sure that mouse is disabled
    void Start()
    {
        Cursor.visible = false;

        selectedObj = EventSystem.current.currentSelectedGameObject;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(selectedObj);

        selectedObj = EventSystem.current.currentSelectedGameObject;
    }
}
