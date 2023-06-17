using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    AudioSource audioSourceSelectedGame;
    [SerializeField] AudioClip soundOnSelectedGame;

    protected AudioSource audioSourceBackground;
    [SerializeField] AudioClip soundOnBackground;

    void Start()
    {
        audioSourceSelectedGame = gameObject.AddComponent<AudioSource>();
        audioSourceSelectedGame.clip = soundOnSelectedGame;
        audioSourceSelectedGame.playOnAwake = false;

        audioSourceBackground = gameObject.AddComponent<AudioSource>();
        audioSourceBackground.loop = true;
        audioSourceBackground.volume = 0.5f;
        audioSourceBackground.clip = soundOnBackground;
        audioSourceBackground.Play();
    }

    public IEnumerator LoadBirdCoroutine()
    {
        audioSourceBackground.Stop();
        audioSourceSelectedGame.Play();
        yield return new WaitForSeconds(1.5f);
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
        audioSourceBackground.Stop();
        audioSourceSelectedGame.Play();
        yield return new WaitForSeconds(1.5f);
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
        audioSourceBackground.Stop();
        audioSourceSelectedGame.Play();
        yield return new WaitForSeconds(1.5f);
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
