using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasOptionBrick : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    public void RestartGame()
    {
        StartCoroutine(Restart());
    }

    public void GoMenu()
    {
        StartCoroutine(Menu());
    }

    IEnumerator Restart()
    {
        // Load the "BrickBreaker" scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BrickBreaker");

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator Menu()
    {
        // Load the "MainMenu" scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
