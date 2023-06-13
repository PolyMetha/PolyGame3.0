using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOptions : MonoBehaviour
{
    public Game game;
    private void Update()
    {
        if (game.endGame && !game.runGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                GoMenu();
            }
        }
    }

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
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Stack");
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
