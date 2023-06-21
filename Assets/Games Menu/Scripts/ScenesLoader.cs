using System.Collections;
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
        // Initialize audio sources and clips
        audioSourceSelectedGame = gameObject.AddComponent<AudioSource>();
        audioSourceSelectedGame.clip = soundOnSelectedGame;
        audioSourceSelectedGame.playOnAwake = false;

        audioSourceBackground = gameObject.AddComponent<AudioSource>();
        audioSourceBackground.loop = true;
        audioSourceBackground.volume = 0.5f;
        audioSourceBackground.clip = soundOnBackground;
        audioSourceBackground.Play();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppQuit(); // Quit the application when the escape key is pressed
        }
    }

    public IEnumerator LoadBirdCoroutine()
    {
        audioSourceBackground.Stop();
        audioSourceSelectedGame.Play();
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FurapiBirdLoad"); // Load "FurapiBirdLoad" scene asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AppleCatcherLoad"); // Load "AppleCatcherLoad" scene asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BrickBreakerLoad"); // Load "BrickBreakerLoad" scene asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu"); // Load "MainMenu" scene asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }
    }

    public void AppQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit the application in the Unity Editor
#else
                Application.Quit(); // Quit the application in a built executable
#endif
    }
}
