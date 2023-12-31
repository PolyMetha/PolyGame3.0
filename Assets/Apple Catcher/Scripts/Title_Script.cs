using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Script : MonoBehaviour
{
    public SpriteRenderer faderRenderer; // Reference to the sprite renderer for fading effect

    public AudioClip leaveSound; // Sound clip played when leaving the title screen

    protected AudioSource refAudioSource; // Reference to the AudioSource component

    protected bool hasLeft = false; // Flag indicating if the player has left the title screen

    protected float current_alpha = 0; // Current alpha value for fading effect

    // Start is called before the first frame update
    void Start()
    {
        refAudioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // Quit the application if the escape key is pressed
        }
        else if (Input.anyKeyDown && !hasLeft)
        {
            hasLeft = true;
            StartCoroutine(LoadScene_Game()); // Load the game scene when any key is pressed
        }
    }

    IEnumerator LoadScene_Game()
    {
        // Stop the music and play the exit sound
        refAudioSource.Stop();
        refAudioSource.clip = leaveSound;
        refAudioSource.loop = false;
        refAudioSource.Play();

        // Wait for the sound to end with a margin
        yield return new WaitForSeconds(0.8f);

        // Fade the white fader into "existence"
        while (current_alpha < 1)
        {
            current_alpha += Time.deltaTime / 2; // Increase the alpha value over time
            faderRenderer.color = new Color(1, 1, 1, current_alpha); // Set the color with updated alpha value
            yield return null;
        }

        // Wait a tiny bit
        yield return new WaitForSeconds(0.5f);

        // Load the game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AppleCatcher");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
