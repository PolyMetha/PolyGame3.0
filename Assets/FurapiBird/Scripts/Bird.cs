using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Bird : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isAlive = true;
    private Animator animator;
    [SerializeField] ObstacleSpawner oS;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audiosource;
    public int topScore;

    private string path;
    
    void Start()
    {
        path = Application.dataPath + @"\TopScoreBird.txt";

        string f = File.ReadAllText(path);

        topScore = int.Parse(f);

        animator = this.GetComponent<Animator>();
        audiosource = this.GetComponent<AudioSource>();
        audiosource.clip = audioClips[0];
        this.isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isAlive)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);
            audiosource.Play();
        }

        if (IsRising())
        {
            animator.SetTrigger("IsRising");
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 30f));
        }
        else if (!isAlive)
        {
            animator.SetTrigger("IsDead");

            
        }
        else if (!IsRising())
        {
            animator.SetTrigger("IsFalling");
            if (transform.rotation.z < 30f)
            {
                transform.Rotate(new Vector3(0, 0, -90f * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            StartCoroutine("WhenDead"); 
            audiosource.clip = audioClips[1];
            audiosource.Play(); 
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isAlive = false;
            oS.pipeSpeed = 0;

            TopScore();
        }

    }

    private bool IsRising()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator WhenDead()
    {
        
        while(!Input.GetKeyDown(KeyCode.M) && !Input.GetKeyDown(KeyCode.R))
        {
                Debug.Log("Waiting");
                yield return null;
        }
        
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(loadMenu());
            Debug.Log("Menu");

        }else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("loadGame");
            Debug.Log("Restart");

        }
        
    }

    IEnumerator loadGame()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("FurapiBirdLoad");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    IEnumerator loadMenu()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void TopScore()
    {
        string f;

        if (oS.score > topScore)
        {
            topScore = oS.score;
            f = topScore.ToString();
            File.WriteAllText(path, f);

        }
    }

}
