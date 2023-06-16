using UnityEngine;

public class SpawnerBrickBreaker : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickG;

    public Ball ball;
    public int lineNumber = 0;

    private float brick_width = 1.76f;
    private float brick_height = 0.92f;
    private float largeur = 11f;
    private int colonne = 7;
    private int createdBricks = 0;
    private float proba_bricks = 0.6f;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        spawnGrid();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the score reached a certain threshold to add life, increase line number, spawn a new grid, and respawn the ball
        if (ball.score >= createdBricks * 50 + ball.coinHit*100)
        {
            ball.AddLife();
            lineNumber++;
            spawnGrid();
            ball.ReSpawnBall();
        }
    }

    public void spawnGrid()
    {
        // Calculate the scale and position for grid spawning
        float L_B = largeur / (colonne * 2);
        float R = 1.8f;
        float scale = L_B / R;
        float y = 3.33f - scale / 2;

        for (int j = 0; j <= lineNumber; j++)
        {
            for (int i = 0; i < colonne; i++)
            {
                // Randomly determine if a brick should be spawned based on probability
                if (Random.value <= proba_bricks)
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

                    float x1 = -(brick_width * scale) / 2 - i * scale * brick_width;
                    float y1 = y - j * scale * brick_height;

                    brick1.transform.position = new Vector3(x1, y1);
                    brick1.transform.localScale = new Vector3(scale, scale, -3);

                    brick2.transform.position = new Vector3(-x1, y1);
                    brick2.transform.localScale = new Vector3(scale, scale, -3);
                }
            }
        }
    }
}
