using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChooseGame : MonoBehaviour
{
    public void LoadChoose()
    {
        StartCoroutine(LoadChooseCoroutine());
    }

    public IEnumerator LoadChooseCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu"); // Load the "MainMenu" scene asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }
    }
}
