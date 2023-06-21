using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerBrickBreaker : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickG;
    [SerializeField] GameObject controls;

    public Ball ball;

    public int lineNumber = 1;
    public int createdBricks = 0;

    private float brickWidth = 1.76f;
    private float brickHeight = 0.92f;
    private float playableWidth = 11f;
    private float probaBricks = 0.6f;
    private float timeRemaining = 20;

    private int playableColumns = 7;

    private bool isDestroyed = false;

    void Start()
    {
        spawnGrid();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 10 && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(controls);
        }

        // Check if the score reached a certain threshold to add life, increase line number, spawn a new grid, and respawn the ball
        if (ball.score >= createdBricks * 50 + ball.coinHit * 100)
        {
            ball.AddLife();
            lineNumber += 2;
            spawnGrid();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadMenuCoroutine()); // Quit the application when the escape key is pressed
        }
    }

    public IEnumerator LoadMenuCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void spawnGrid()
    {
        Debug.Log("grid");
        // Calculate the scale and position for grid spawning
        float L_B = playableWidth / (playableColumns * 2);
        float R = 1.8f;
        float scale = L_B / R;
        float y = 3.33f - scale / 2;

        for (int j = 0; j <= lineNumber; j++)
        {
            for (int i = 0; i < playableColumns; i++)
            {
                // Randomly determine if a brick should be spawned based on probability
                if (Random.value <= probaBricks)
                {
                    GameObject brick1;
                    GameObject brick2;

                    // Instantiate brick or brickG based on a random value
                    if (Random.value < 0.6)
                    {
                        brick1 = Instantiate(brick);
                        brick2 = Instantiate(brick);
                        createdBricks += 2;
                    }
                    else
                    {
                        brick1 = Instantiate(brickG);
                        brick2 = Instantiate(brickG);
                        createdBricks += 4;
                    }

                    // Set brick parent and position
                    brick1.transform.parent = gameObject.transform;
                    brick2.transform.parent = gameObject.transform;

                    float x1 = -(brickWidth * scale) / 2 - i * scale * brickWidth;
                    float y1 = y - j * scale * brickHeight;

                    brick1.transform.position = new Vector3(x1, y1);
                    brick1.transform.localScale = new Vector3(scale, scale, -3);

                    brick2.transform.position = new Vector3(-x1, y1);
                    brick2.transform.localScale = new Vector3(scale, scale, -3);
                }
            }
        }
    }
}
