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
    private AudioSource audiosource;
    public int topScore;
    
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audiosource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (!isAlive)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isAlive)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);
            audiosource.Play();
        }

        if (!IsRising())
        {
            animator.SetTrigger("IsRising");
            if (transform.rotation.z >= -15f)
            {
                transform.Rotate(new Vector3(0, 0, -2.4f * 6 * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -transform.rotation.z));
            }
        }
        else if (IsRising())
        {
            animator.SetTrigger("IsFalling");
            if (transform.rotation.z <= 15f)
            {
                transform.Rotate(new Vector3(0, 0, 2.0f * 6 * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -transform.rotation.z));
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetTrigger("IsDead");
            StartCoroutine("WhenDead"); 
            audiosource.Play(); 
            isAlive = false;
            oS.pipeSpeed = 0;
            if (!oS.isDead)
            {
                oS.isDead = true;
            }
            this.GetComponent<Collider2D>().enabled = false;

            TopScore();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            audiosource.Play();
            isAlive = false;
            oS.pipeSpeed = 0;
            if (!oS.isDead)
            {
                oS.isDead = true;
            }

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
            StartCoroutine(loadGame());
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
