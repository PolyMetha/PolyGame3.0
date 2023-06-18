using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public AudioClip ref_audioClip; // Reference to the audio clip
    public SpriteRenderer fader_renderer; // Reference to the sprite renderer for fading out effect
    public GameObject apple_pf; // Prefab for spawning an apple
    public GameObject banana_pf; // Prefab for spawning a banana
    public GameObject rottenApple_pf; // Prefab for spawning a rotten apple
    public GameObject goldenApple_pf; // Prefab for spawning a golden apple
    public GameObject bomb_pf; // Prefab for spawning a bomb
    public GameMaster gameMaster; // Reference to the GameMaster script

    protected float spawnTime = 3f; // Time between object spawns
    protected AudioSource ref_audioSource; // Reference to the AudioSource component
    protected float current_alpha = 1; // Current alpha value for fading out effect

    private Vector2 screenBounds;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        ref_audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        ref_audioSource.loop = true; // Set audio clip to loop
        ref_audioSource.volume = 0.5f; // Set audio volume
        ref_audioSource.clip = ref_audioClip; // Set the audio clip

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
            if (randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn an apple
                GameObject newApple = Instantiate(apple_pf);
                newApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba - rottenApple_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a rotten apple
                GameObject newRottenApple = Instantiate(rottenApple_pf);
                newRottenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba - rottenApple_pf.GetComponent<ObjectFalling>().spawnProba - banana_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a banana
                GameObject newBanana = Instantiate(banana_pf);
                newBanana.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba - rottenApple_pf.GetComponent<ObjectFalling>().spawnProba - banana_pf.GetComponent<ObjectFalling>().spawnProba - bomb_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                // Spawn a bomb
                GameObject newBomb = Instantiate(bomb_pf);
                bomb_pf.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else
            {
                // Spawn a golden apple
                GameObject newGoldenApple = Instantiate(goldenApple_pf);
                newGoldenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }

            spawnTime = 0.5f + Random.value * 1f; // Set the next spawn time randomly
        }

    }

    // Coroutine to fade out from white and launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        ref_audioSource.Play(); // Play the audio clip

        while (current_alpha > 0)
        {
            current_alpha -= Time.deltaTime / 2; // Decrease the alpha value over time
            fader_renderer.color = new Color(1, 1, 1, current_alpha); // Set the color with updated alpha value
            yield return null;
        }

        Destroy(fader_renderer.gameObject); // Destroy the fader object

    }
}
