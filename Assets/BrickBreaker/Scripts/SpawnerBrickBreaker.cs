using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBrickBreaker : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickG;
    
    //[SerializeField] PaddleMovement pM;

    public Ball ball;

    private float brick_width = 1.76f;
    private float brick_height = 0.92f;
    
    private float largeur = 11f;

    public int colonne = 7;   //Nombre de colonnes/2
    public int lineNumber = 0;
    int createdBricks = 0;
    
    
    
    public float proba_bricks = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
       spawnGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.score >= createdBricks * 50)
        {
            ball.AddLife();
            lineNumber++;
            spawnGrid();
            ball.ReSpawnBall();
        }
    }

    public void spawnGrid()
    {
        float L_B = largeur / (colonne*2);
        
        float R = 1.8f; //rapport entre scale et vrai largeur

        float scale = L_B / R;

        float y = 3.33f - scale / 2; //hauteur initiale

        for (int j = 0; j <= lineNumber; j++)
        {

            for (int i = 0; i < colonne; i++)
            {
                
                if (Random.value <= proba_bricks)
                {
                    GameObject brick1;
                    GameObject brick2;
                    
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

                    brick1.transform.parent = gameObject.transform;
                    brick2.transform.parent = gameObject.transform;

                    float x1 = -(brick_width * scale) / 2 - i * scale * brick_width;
                    float y1 = y - j * scale * brick_height;

                    brick1.transform.position = new Vector3(x1, y1);
                    brick1.transform.localScale = new Vector3(scale, scale, -3);

                    brick2.transform.position = new Vector3(-x1, y1);
                    brick2.transform.localScale = new Vector3( scale, scale, -3);
                }
            }
        }
    }
}
