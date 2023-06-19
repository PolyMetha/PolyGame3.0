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
    public ObstacleSpawner oS;
    private AudioSource audiosource;
    public int topScore;

    private string path;
    
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
    }

    //obstacles pipes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            audiosource.Play(); 
            isAlive = false;
            oS.pipeSpeed = 0;

            TopScore();            
            animator.SetTrigger("IsDead");
        }

    }

    //obstacle screen limits
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            oS.StartCoroutine("WhenDead");
            audiosource.Play();
            isAlive = false;
            oS.pipeSpeed = 0;

            TopScore();            
            Destroy(gameObject);
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

    public void TopScore()
    {
        if (oS.score > topScore)
        {
            string path = Application.dataPath + "/StreamingAssets/TopScoreBird.txt";
            string f = oS.score.ToString();
            File.WriteAllText(path, f);
        }
    }

}
