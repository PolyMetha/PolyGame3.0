using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    bool isSpawning = true;
    public float timer = 2f;
    [SerializeField] TextMeshPro textScore;
    [SerializeField] Canvas gameOverCanvas;

    public int life = 4;
    public int score;
    public int topScore;
    public TextMeshProUGUI textTopScore;

    public SpawnerBrickBreaker spawn;

    public GameObject ballPF;
    public GameObject[] listLife;
    string path;

    private float velocityCST;


    void Start()
    {
        //listLife = new List<GameObject>(life);
        listLife = new GameObject[5];
        path = Application.dataPath + @"/TopScoreBrick.txt";
        string f = File.ReadAllText(path);
        
        topScore = int.Parse(f);

        Vector2 initPos = new Vector2(4.82f, 4.43f);

        for (int i = 0; i < life; i++)
        {
            GameObject newBall = Instantiate(ballPF);
            newBall.transform.position = new Vector3(initPos.x - i * 0.7f, initPos.y, -3);
            //listLife.Add(newBall);
            listLife[i] = newBall;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        if (isSpawning)  //Respawn de la ball
        {
            RestartBall();
        }

        if(transform.position.y <= -5f && life >= 0) //La balle tombe
        {
            RemoveLife();
            ReSpawnBall();
        }
        else if (life < 0) //GameOver
        {
            gameOverCanvas.enabled = true;
            TopScore();        
        }

        //Rectification trajectoire et vitesse
        
        else
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            score += 50;
            textScore.SetText("SCORE : " + score);
        }
    }

    public void RestartBall()
    {
        timer -= 1f*Time.deltaTime;

        if(timer <= 0f)
        {
            isSpawning = false;
                
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0f, -8f - spawn.lineNumber, 0f);
            velocityCST = rb.velocity.magnitude;
        }
    }
    public void ReSpawnBall()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x,-1f, 0f);
        timer = 2f;
        isSpawning = true;
    }
    public void RemoveLife()
    {
        life--;
        Destroy(listLife[life]);
        listLife[life] = null;
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
            File.WriteAllText(path, f);
            
        }
        textTopScore.SetText("TopScore : "+topScore);
    }
}
