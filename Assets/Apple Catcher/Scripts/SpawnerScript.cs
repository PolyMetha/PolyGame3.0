using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public AudioClip refAudioClip; // Reference to the audio clip

    public SpriteRenderer faderRenderer; // Reference to the sprite renderer for fading out effect

    public GameObject applePF; // Prefab for spawning an apple
    public GameObject bananaPF; // Prefab for spawning a banana
    public GameObject rottenApplePF; // Prefab for spawning a rotten apple
    public GameObject goldenApplePF; // Prefab for spawning a golden apple
    public GameObject bombPF; // Prefab for spawning a bomb
    public GameMaster gameMaster; // Reference to the GameMaster script

    protected float spawnTime = 3f; // Time between object spawns
    protected float currentAlpha = 1; // Current alpha value for fading out effect

    protected AudioSource refAudioSource; // Reference to the AudioSource component

    private Vector2 screenBounds;

    void Start()
    {
        refAudioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        refAudioSource.loop = true; // Set audio clip to loop
        refAudioSource.volume = 0.5f; // Set audio volume
        refAudioSource.clip = refAudioClip; // Set the audio clip

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(FadeOutFromWhite()); // Start the coroutine for fading out from white
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameMaster.timerIsRunning) { return; } // If the timer is not running, exit the function

        spawnTime -= Time.deltaTime; // Decrease the spawn time

        if (spawnTime <= 0)
        {
            float randomX = Random.Range(screenBounds.x*-1, screenBounds.x); // Generate a random x position within the bounds of the screen

            // Randomly choose which object is going to be spawned
            float randomObject = Random.Range(0f, 1f);
            if (randomObject - applePF.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn an apple
                GameObject newApple = Instantiate(applePF);
                newApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - applePF.GetComponent<ObjectFalling>().spawnProba - rottenApplePF.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a rotten apple
                GameObject newRottenApple = Instantiate(rottenApplePF);
                newRottenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - applePF.GetComponent<ObjectFalling>().spawnProba - rottenApplePF.GetComponent<ObjectFalling>().spawnProba - bananaPF.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a banana
                GameObject newBanana = Instantiate(bananaPF);
                newBanana.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - applePF.GetComponent<ObjectFalling>().spawnProba - rottenApplePF.GetComponent<ObjectFalling>().spawnProba - bananaPF.GetComponent<ObjectFalling>().spawnProba - bombPF.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a bomb
                GameObject newBomb = Instantiate(bombPF);
                bombPF.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else
            {
                // Spawn a golden apple
                GameObject newGoldenApple = Instantiate(goldenApplePF);
                newGoldenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }

            spawnTime = 0.5f + Random.value * 1f; // Set the next spawn time randomly
        }

    }

    // Coroutine to fade out from white and launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        refAudioSource.Play(); // Play the audio clip

        while (currentAlpha > 0)
        {
            currentAlpha -= Time.deltaTime / 2; // Decrease the alpha value over time
            faderRenderer.color = new Color(1, 1, 1, currentAlpha); // Set the color with updated alpha value
            yield return null;
        }

        Destroy(faderRenderer.gameObject); // Destroy the fader object
    }
}
