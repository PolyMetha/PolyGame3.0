using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{

    public IEnumerator LoadBirdCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("FurapiBirdLoad");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void LoadBird()
    {
        StartCoroutine(LoadBirdCoroutine());    
    }

    public IEnumerator LoadAppleCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("AppleCatcherLoad");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void LoadApple()
    {
        StartCoroutine(LoadAppleCoroutine());
    }

    public IEnumerator LoadBrickCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("BrickBreaker");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void LoadBrick()
    {
        StartCoroutine(LoadBrickCoroutine());
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuCoroutine());
    }

    public IEnumerator LoadMenuCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void AppQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
