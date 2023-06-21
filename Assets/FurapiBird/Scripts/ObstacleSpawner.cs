using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObstacleSpawner : MonoBehaviour
{
    private float timerObstacles = 1f; // Timer for spawning obstacles
    private float timerDeathSound = 3f; // Timer for playing death sound
    private float timerSpeed = 30f; // Timer for increasing pipe speed

    public float pipeSpeed = 4f; // Initial speed of the pipes

    public int score = 0; // Current score

    public bool isDead = false; // Flag to indicate if the bird is dead
    public bool deadCoroutineStarted = false; // Flag to check if the dead coroutine has started

    public TextMeshProUGUI scoreText; // Text component for displaying the score

    public GameObject commandsText; // UI text for displaying commands

    AudioSource music; // Reference to the audio source

    [SerializeField] GameObject pipe; // Prefab for the obstacle pipes
    [SerializeField] Bird bird; // Reference to the Bird script

    void Start()
    {
        commandsText.SetActive(false); // Disable the commands text at the start
        music = GetComponent<AudioSource>(); // Get the AudioSource component
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>(); // Find the Bird object and get the Bird script component
        bird.oS = this; // Assign the ObstacleSpawner reference to the Bird script
    }

    void FixedUpdate()
    {
        timerObstacles -= Time.deltaTime; // Decrement the obstacles spawn timer
        timerSpeed -= Time.deltaTime; // Decrement the pipe speed timer

        float ypos = Random.Range(-3, 3) + 0.5f; // Generate a random y-position for the pipe

        if (timerObstacles <= 0f)
        {
            GameObject newPipe = Instantiate(pipe); // Spawn a new pipe

            newPipe.GetComponent<PipeObstacle_Script>().scriptSpawner = this; // Assign the ObstacleSpawner reference to the PipeObstacle_Script
            newPipe.transform.position = new Vector3(10f, ypos, 0f); // Set the position of the new pipe
            newPipe.GetComponent<PipeObstacle_Script>().scoreCount.oS = this; // Assign the ObstacleSpawner reference to the ScoreCounter script
            timerObstacles = (Random.value + 1.5f) * 1.1f * 4f / pipeSpeed; // Reset the obstacles spawn timer
        }

        if (timerSpeed < 0f)
        {
            pipeSpeed *= 1.1f; // Increase the pipe speed
            timerSpeed = 30f; // Reset the pipe speed timer
        }

        scoreText.SetText("Score : " + score); // Update the score text

        if (!bird.isAlive)
        {
            timerDeathSound -= Time.deltaTime; // Decrement the death sound timer
            music.Stop(); // Stop the music
            if (timerDeathSound < 0)
            {
                commandsText.SetActive(true); // Enable the commands text
            }
        }

        if (isDead && deadCoroutineStarted == false)
        {
            StartCoroutine("WhenDead"); // Start the dead coroutine
            deadCoroutineStarted = true;
        }
    }

    public IEnumerator WhenDead()
    {
        while (!Input.GetKeyDown(KeyCode.M) && !Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
        }

        Destroy(bird.gameObject); // Destroy the bird game object

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(loadMenu()); // Load the menu scene
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(loadGame()); // Restart the game
        }
    }

    IEnumerator loadGame()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("FurapiBirdLoad"); // Load the game scene asynchronously
        while (!asyncload.isDone)
        {
            yield return null; // Wait until the game scene is fully loaded
        }
    }

    IEnumerator loadMenu()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu"); // Load the menu scene asynchronously
        while (!asyncload.isDone)
        {
            yield return null; // Wait until the menu scene is fully loaded
        }
    }
}
