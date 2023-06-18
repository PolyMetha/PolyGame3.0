using System.IO;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public bool isSpawning = true;
    public int score;
    public TextMeshProUGUI textTopScore;
    public SpawnerBrickBreaker spawn;
    public GameObject ballPF;
    public GameObject[] listLife;
    public int coinHit = 0;

    [SerializeField] public TextMeshPro textScore;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] public GameObject coinPrefab;

    AudioSource audioSourceWall;
    [SerializeField] AudioClip soundOnWall;
    AudioSource audioSourcePaddle;
    [SerializeField] AudioClip soundOnPaddle;
    AudioSource audioSourceRestart;
    [SerializeField] AudioClip soundOnRestart;
    AudioSource audioSourceFall;
    [SerializeField] AudioClip soundOnFall;
    public AudioSource audioSourceEnd;
    [SerializeField] AudioClip soundOnEnd;

    private int life = 3;
    private float timer = 2f;
    private int topScore;
    private float velocityCST;
    private string path;
    private bool gameOverSound = false;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
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

        listLife = new GameObject[5];

        path = Application.dataPath + @"/TopScoreBrick.txt";
        string f = File.ReadAllText(path);

        topScore = int.Parse(f);

        Vector2 initPos = new Vector2(4.82f, 4.43f);

        for (int i = 0; i < life-1; i++)
        {
            // Instantiate ball game objects and set their positions
            GameObject newBall = Instantiate(ballPF);
            newBall.transform.position = new Vector3(initPos.x - i * 0.7f, initPos.y, -3);
            listLife[i] = newBall;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        // Set the initial position of the ball
        transform.position = new Vector3(0f, 0f, 0f);
        timer = 2f;
        isSpawning = true;
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

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
                rb.velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x,
                    GetComponent<Rigidbody2D>().velocity.y * 5, 0f);
            }

            if (rb.velocity.magnitude != velocityCST)
            {
                rb.velocity = rb.velocity.normalized * velocityCST;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            if (Random.value <= 0.1f)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            score += 50;
            textScore.SetText("SCORE: " + score);
            audioSourcePaddle.Play();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            audioSourceWall.Play();
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            audioSourcePaddle.Play();
        }
    }

    public void RestartBall()
    {
        timer -= 0.75f * Time.deltaTime;

        if (timer <= 0f)
        {
            isSpawning = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // Set the velocity of the ball after respawning
            rb.velocity = new Vector3(0f, -8f - spawn.lineNumber, 0f);
            velocityCST = rb.velocity.magnitude;
        }
    }

    public void ReSpawnBall()
    {
        audioSourceRestart.Play();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        // Reset the position of the ball after respawning
        transform.position = new Vector3(0f, 0f, 0f);
        timer = 2f;
        isSpawning = true;
    }

    public void RemoveLife()
    {
        life--;
        if (life > 0)
        {
            Destroy(listLife[life-1]); // Destroy the game object representing a life
            listLife[life-1] = null;
        }
    }

    public void AddLife()
    {
        if (life < 5)
        {
            Debug.Log(life);
            GameObject newBall = Instantiate(ballPF);
            newBall.transform.position = listLife[life - 1].transform.position;
            newBall.transform.Translate(-0.7f, 0, 0);
            listLife[life] = newBall;
            life++;
        }
    }

    public void TopScore()
    {
        if (!gameOverSound) //On vérifie que le Son n'a pas déjà été joué.
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
