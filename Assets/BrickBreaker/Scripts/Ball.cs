using System.IO;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public bool isSpawning = true; // Flag indicating if the ball is spawning
    public int score; // Current score
    public int coinHit = 0; // Counter for coins hit
    private const int ballSpeedLimit = 13;

    private int life = 3; // Current number of lives
    private int topScore; // Top score

    private float timer = 2f; // Timer for ball respawn
    private float ballVelocity; // Velocity of the ball
    private string path; // File path for storing top score
    private bool gameOverSound = false; // Flag indicating if game over sound has played
    private Vector2 initPos; // Initial position of the ball

    private Rigidbody2D rb; // Rigidbody2D component of the ball
    public TextMeshProUGUI textTopScore; // Reference to the TextMeshProUGUI component for top score display
    public SpawnerBrickBreaker spawn; // Reference to the SpawnerBrickBreaker component for spawning bricks
    public GameObject ballPF; // Prefab of the ball game object
    public GameObject[] listLife; // Array of game objects representing player lives
    public GameObject coinPrefab; // Prefab of the coin game object

    public TextMeshPro textScore; // Reference to the TextMeshPro component for score display
    [SerializeField] Canvas gameOverCanvas; // Reference to the Canvas component for game over display

    AudioSource audioSourceWall; // AudioSource for wall collision sound
    [SerializeField] AudioClip soundOnWall; // Sound clip for wall collision
    AudioSource audioSourcePaddle; // AudioSource for paddle collision sound
    [SerializeField] AudioClip soundOnPaddle; // Sound clip for paddle collision
    AudioSource audioSourceRestart; // AudioSource for ball respawn sound
    [SerializeField] AudioClip soundOnRestart; // Sound clip for ball respawn
    AudioSource audioSourceFall; // AudioSource for ball falling sound
    [SerializeField] AudioClip soundOnFall; // Sound clip for ball falling
    public AudioSource audioSourceEnd; // AudioSource for game over sound
    [SerializeField] AudioClip soundOnEnd; // Sound clip for game over sound


    void Start()
    {
        audioSourceWall = gameObject.AddComponent<AudioSource>();
        audioSourceWall.clip = soundOnWall;
        audioSourceWall.playOnAwake = false;

        audioSourcePaddle = gameObject.AddComponent<AudioSource>();
        audioSourcePaddle.clip = soundOnPaddle;
        audioSourcePaddle.playOnAwake = false;

        audioSourceRestart = gameObject.AddComponent<AudioSource>();
        audioSourceRestart.clip = soundOnRestart;
        audioSourceRestart.playOnAwake = false;

        audioSourceFall = gameObject.AddComponent<AudioSource>();
        audioSourceFall.clip = soundOnFall;
        audioSourceFall.playOnAwake = false;

        audioSourceEnd = gameObject.AddComponent<AudioSource>();
        audioSourceEnd.clip = soundOnEnd;
        audioSourceEnd.playOnAwake = false;

        audioSourceRestart.Play();

        listLife = new GameObject[5]; // Initialize the list of life game objects

        // Read the top score from the file
        path = Application.dataPath + "/StreamingAssets/TopScoreBrick.txt";
        string f = File.ReadAllText(path);
        topScore = int.Parse(f);

        initPos = new Vector2(4.82f, 4.43f); // Set the initial position of the ball

        for (int i = 0; i < life; i++)
        {
            // Instantiate ball game objects and set their positions
            GameObject newBall = Instantiate(ballPF);
            newBall.transform.position = new Vector3(initPos.x - i * 0.7f, initPos.y, -3);
            listLife[i] = newBall;
        }

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        // Set the initial position of the ball
        transform.position = new Vector3(0f, 0f, 0f);
        timer = 2f;
        isSpawning = true;
    }

    void FixedUpdate()
    {
        if (isSpawning) // Respawn the ball
        {
            RestartBall();
        }

        if (transform.position.y <= -5f && life != 0) // Check if the ball falls off the screen
        {
            RemoveLife();
            audioSourceFall.Play();
            if (life > 0)
            {
                ReSpawnBall();
            }
        }
        else if (life == 0) // Check if there are no lives left
        {
            isSpawning = false;
            gameOverCanvas.enabled = true;
            TopScore(); // Update the top score        
        }
        else // Adjust trajectory and velocity
        {
            if (rb.velocity.y <= 2 && rb.velocity.y >= -2)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 5, 0f);
            }

            if (rb.velocity.magnitude != ballVelocity)
            {
                rb.velocity = rb.velocity.normalized * ballVelocity;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick")) // Check if ball collides with a brick
        {
            if (Random.value <= 0.1f)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity); // Instantiate a coin game object
            }
            score += 50; // Increase the score
            textScore.SetText("SCORE: " + score); // Update the score display
            audioSourcePaddle.Play(); // Play paddle collision sound
        }
        else if (collision.gameObject.CompareTag("Wall")) // Check if ball collides with a wall
        {
            audioSourceWall.Play(); // Play wall collision sound
        }
        else if (collision.gameObject.CompareTag("Paddle")) // Check if ball collides with the paddle
        {
            audioSourcePaddle.Play(); // Play paddle collision sound
        }
    }

    public void RestartBall()
    {
        timer -= 0.75f * Time.deltaTime;

        if (timer <= 0f)
        {
            isSpawning = false;

            // Set the velocity of the ball after respawning
            rb.velocity = new Vector3(0f, -8f - spawn.lineNumber, 0f);
            // Update ball speed if it is below the limit of speed
            if(ballVelocity < ballSpeedLimit)
            {
                ballVelocity = rb.velocity.magnitude;
            }            
        }
    }

    public void ReSpawnBall()
    {
        audioSourceRestart.Play();
        rb.velocity = Vector3.zero;

        // Reset the position of the ball after respawning
        transform.position = new Vector3(0f, 0f, 0f);
        timer = 2f;
        isSpawning = true;
    }

    public void RemoveLife()
    {
        if (life > 0)
        {
            Destroy(listLife[life - 1]); // Destroy the game object representing a life
            listLife[life - 1] = null;
        }
        life--;
    }

    public void AddLife()
    {
        if (life < 5)
        {
            GameObject newBall = Instantiate(ballPF);

            newBall.transform.position = listLife[life - 1].transform.position + new Vector3(-0.7f, 0, 0);
            listLife[life] = newBall;
            life++;
        }
    }

    public void TopScore()
    {
        if (!gameOverSound)
        {
            audioSourceEnd.Play();
            gameOverSound = true;
        }

        string f;

        if (score > topScore)
        {
            topScore = score;
            f = topScore.ToString();
            File.WriteAllText(path, f); // Write the updated top score to the file
        }
        textTopScore.SetText("TopScore : " + topScore);
    }
}
