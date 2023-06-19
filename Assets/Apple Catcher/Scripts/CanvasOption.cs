using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasOption : MonoBehaviour
{
    
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
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("AppleCatcher");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
    IEnumerator Menu()
    {
        
        //Mettre le bon nom du menu
        
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
