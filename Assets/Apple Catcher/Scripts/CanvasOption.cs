using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasOption : MonoBehaviour
{
    // This class represents a canvas options script attached to a GameObject in Unity.
    public void RestartGame()
    {
        // This method is called when the "RestartGame" button is clicked.
        StartCoroutine(Restart());
        // Starts the coroutine for restarting the game.
    }

    public void GoMenu()
    {
        // This method is called when the "GoMenu" button is clicked.
        StartCoroutine(Menu());
        // Starts the coroutine for going back to the main menu.
    }

    IEnumerator Restart()
    {
        // This coroutine is responsible for restarting the game.
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("AppleCatcher");
        // Loads the scene named "AppleCatcher" asynchronously and assigns the reference to an AsyncOperation object.

        while (!asyncload.isDone)
        {
            // Executes the following block of code until the scene loading is complete.
            yield return null;
            // Pauses the execution of the coroutine and waits for the next frame update.
        }
    }

    IEnumerator Menu()
    {
        // This coroutine is responsible for transitioning to the main menu.
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        // Loads the scene named "MainMenu" asynchronously and assigns the reference to an AsyncOperation object.

        while (!asyncload.isDone)
        {
            // Executes the following block of code until the scene loading is complete.
            yield return null;
            // Pauses the execution of the coroutine and waits for the next frame update.
        }
    }
}
