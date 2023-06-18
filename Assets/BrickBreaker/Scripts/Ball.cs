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

    private Rigidbody2D rb;

    [SerializeField] TextMeshPro textScore;
    [SerializeField] Canvas gameOverCanvas;

    private int life = 3;
    private float timer = 2f;
    private int topScore;
    private float velocityCST;
    private string path;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    void Start()
    {
        listLife = new GameObject[4];
        path = Application.dataPath + @"/TopScoreBrick.txt";
        string f = File.ReadAllText(path);

        topScore = int.Parse(f);

        Vector2 initPos = new Vector2(4.82f, 4.43f);

        /*
        for (int i = 0; i < life; i++)
        {
            // Instantiate ball game objects and set their positions
            GameObject newBall = Instantiate(ballPF);
            newBall.transform.position = new Vector3(initPos.x - i * 0.7f, initPos.y, -3);
            listLife[i] = newBall;
        }
        */
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

        if (transform.position.y <= -5f && life > 0) // Check if the ball falls off the screen
        {
            RemoveLife();
            ReSpawnBall();
        }
        else if (life <= 0) // Check if there are no lives left
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
        if (collision.gameObject.tag == "Brick")
        {
            score += 50;
            textScore.SetText("SCORE : " + score);
        }
    }

    public void RestartBall()
    {
        timer -= 1f * Time.deltaTime;

        if (timer <= 0f)
        {
            isSpawning = false;

            // Set the velocity of the ball after respawning
            rb.velocity = new Vector3(0f, -8f, 0f);
            velocityCST = rb.velocity.magnitude;
        }
    }

    public void ReSpawnBall()
    {
        rb.velocity = Vector3.zero;

        // Reset the position of the ball after respawning
        transform.position = new Vector3(0f, 0f, 0f);
        timer = 2f;
        isSpawning = true;
    }

    public void RemoveLife()
    {
        life--;
        if (life >= 0)
        {
            Destroy(listLife[life]); // Destroy the game object representing a life
            listLife[life] = null;
        }
    }

    public void AddLife()
    {
        GameObject newBall = Instantiate(ballPF);

        newBall.transform.position = listLife[life - 1].transform.position;
        newBall.transform.Translate(-0.7f, 0, 0);
        listLife[life] = newBall;
        life++;
    }

    public void TopScore()
    {
        string f;

        if (score > topScore)
        {
            topScore = score;
            f = topScore.ToString();
            File.WriteAllText(path, f); // Write the updated top score to the file
        }
        textTopScore.SetText("TopScore : " + topScore);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -1;
    }
}
