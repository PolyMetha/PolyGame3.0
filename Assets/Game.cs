using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class Game : MonoBehaviour
{
    public GameObject cube;
    public GameObject newCube;
    public GameObject lastCube;

    public Material colour;

    public GameObject camera;

    public int vitesse = 10;

    public bool runGame = false;
    public bool endGame = false;
    private bool nextTour;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI restartText;

    private int tour = 0;
    private int topScore;
    private string path;
    

    private System.Random ran = new System.Random();

    void Start()
    {

        path = Application.dataPath + @"\TopScoreStack.txt";
        string f = File.ReadAllText(path);
        topScore = int.Parse(f);
        topScoreText.SetText("TopScore : "+topScore);
        
        colour.color = Random.ColorHSV();
    }
    void Update()
    {
        if (!runGame && !endGame)
        {
            StartGame();
        }

        if (runGame && !endGame)
        {
            if (nextTour)
            {
                NewCube();
            }
            else if (!nextTour)
            {
                MouvCube();
                StopCube();
                TopScore();
            }
        }
        else if (runGame && endGame)
        {
            //TopScore();
            if (endGame)
            {
                float diff = tour * Time.deltaTime * tour / 5;
                camera.transform.Translate(-diff/30,-diff/100,-diff/30);

                if (camera.transform.position.y <= tour - tour / 10)
                {
                    restartText.enabled = true;
                    runGame = false;
                }
            }
        }
    }

    void StartGame()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            runGame = true;
            nextTour = true;
            print("lesgo");
        }
    }
    void NewCube()
    {
        
        //score.GetComponent<TextMeshPro>().SetText("Score : " + tour);
        scoreText.SetText("Score : " + tour);
        tour++;
        
        newCube = Instantiate(cube);

        newCube.transform.localScale = lastCube.transform.localScale;
        newCube.transform.position = lastCube.transform.position;
        
        NewColor();
        
        if (tour % 2 == 0)
        {
            //newCube.transform.position = new Vector3(20, tour, 0);
            newCube.transform.Translate(new Vector3(20, 1, 0));
        }
        else if (tour % 2 == 1)
        {
            //newCube.transform.position = new Vector3(0, tour, 20);
            newCube.transform.Translate(new Vector3(0, 1, 20));
        }

        nextTour = false;
    }
    void MouvCube()
    {
        if (tour % 2 == 0)
        {
            Vector3 left = new Vector3(-1, 0, 0);
            newCube.transform.Translate(left * Time.deltaTime * vitesse);
        }
        else if (tour % 2 == 1)
        {
            Vector3 right = new Vector3(0, 0, -1);
            newCube.transform.Translate(right * Time.deltaTime * vitesse);
        }
    }
    void StopCube()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newCube.transform.Translate(Vector3.zero);
            
            camera.transform.Translate(Vector3.up);
            
            CheckPos();

            nextTour = true;
        }
    }
    void CheckPos()
    {
        float diff;
        Vector3 scale = newCube.transform.localScale;
        
        if (tour % 2 == 0)
        {
            diff = lastCube.transform.position.x - newCube.transform.position.x;
            
            print(scale.x - Mathf.Abs(diff));

            if (scale.x - Mathf.Abs(diff) <= 0) //Si les cubes ne se touchent pas
            {
                //runGame = false;
                endGame = true;
                //newCube.GetComponent<Rigidbody>().useGravity = true;
                newCube.AddComponent<Rigidbody>();
            }
            else
            {
                newCube.transform.localScale = new Vector3(scale.x - Mathf.Abs(diff), scale.y, scale.z);
                newCube.transform.Translate(new Vector3(diff / 2,0,0));

                GameObject fallingCube = Instantiate(cube);
                
                fallingCube.transform.localScale = new Vector3(Mathf.Abs(diff), scale.y, scale.z);
                fallingCube.transform.position = lastCube.transform.position;

                
                fallingCube.GetComponent<MeshRenderer>().material.color = newCube.GetComponent<MeshRenderer>().material.color;
                fallingCube.AddComponent<Rigidbody>();
                
                if (diff < 0)
                {
                    fallingCube.transform.Translate(new Vector3(scale.x/2 + Mathf.Abs(diff)/2,1,0));
                }
                else
                {
                    fallingCube.transform.Translate(new Vector3(-scale.x/2 - Mathf.Abs(diff)/2,1,0));
                }
            }

            
        }
        else if (tour % 2 == 1)
        {
            diff = lastCube.transform.position.z - newCube.transform.position.z;

            if (scale.z - Mathf.Abs(diff) <= 0)
            {
                endGame = true;
                newCube.AddComponent<Rigidbody>();
            }
            else
            {
                newCube.transform.localScale = new Vector3(scale.x, scale.y, scale.z  - Mathf.Abs(diff));
                newCube.transform.Translate(new Vector3(0,0,diff / 2));
                
                GameObject fallingCube = Instantiate(cube);
                
                fallingCube.transform.localScale = new Vector3(scale.x, scale.y, Mathf.Abs(diff));
                fallingCube.transform.position = lastCube.transform.position;

                
                fallingCube.GetComponent<MeshRenderer>().material.color = newCube.GetComponent<MeshRenderer>().material.color;
                fallingCube.AddComponent<Rigidbody>();
                
                if (diff < 0)
                {
                    fallingCube.transform.Translate(new Vector3(0,1,scale.z/2 + Mathf.Abs(diff)/2));
                }
                else
                {
                    fallingCube.transform.Translate(new Vector3(0,1,-scale.z/2 - Mathf.Abs(diff)/2));
                }
            }
        }

        lastCube = newCube;
    }
    void NewColor()
    {
        Vector4 newColor = new Vector4((float)ran.NextDouble()/5, (float)ran.NextDouble()/5, 
                    (float)ran.NextDouble()/5, (float)ran.NextDouble()/10);

        newCube.GetComponent<MeshRenderer>().material.color = lastCube.GetComponent<MeshRenderer>().material.color;

        if ((tour / 10) % 2 == 0)
        {
            newCube.GetComponent<MeshRenderer>().material.color +=
                new Color(newColor.x, newColor.y, newColor.z, newColor.w);
        }
        else
        {
            newCube.GetComponent<MeshRenderer>().material.color -=
                new Color(newColor.x, newColor.y, newColor.z, newColor.w);
        }
    }
    public void TopScore()
    {
        string f;
        int score = tour - 1;

        if (score > topScore)
        {
            topScore = score;
            f = topScore.ToString();
            File.WriteAllText(path, f);
            
        } 
        topScoreText.SetText("TopScore : "+topScore);
    }
    
    
}
