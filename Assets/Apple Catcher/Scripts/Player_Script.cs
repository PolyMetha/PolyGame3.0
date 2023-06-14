using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Script : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public TextMeshPro displayed_text;
    public GameMaster gameMaster;
    public GameObject gameOverUI;

    public int score = 0;
    protected AudioSource ref_audioSource;
    protected Animator ref_animator;

    private float speed = 10;
    [SerializeField]
    private float speedBoostTime = 0f;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    void Start()
    {
        ref_audioSource = GetComponent<AudioSource>();
        ref_animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!gameMaster.timerIsRunning) { return; }

        //Manage movement speed and animations
        shifting();
        
        //We stop time if the spaceBar is pushed down
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Time.timeScale = 0f;
        }
        else if ( Input.GetKeyUp(KeyCode.Space) )
        {
            Time.timeScale = 1.0f;
        }

        //Quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //if sped up before, slow down 5s later
        if (speedBoostTime != 0 && (Time.time - speedBoostTime) > 5f ) {
            speed = 10;
            speedBoostTime = 0;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }

    void shifting()
    {

        //get moving direction
        int inputX = (int)Input.GetAxisRaw("Horizontal");
        
        //Inform animator : Are we moving?
        ref_animator.SetBool("isMoving", inputX != 0);

        //Move with the speed found
        transform.Translate(speed * inputX * Time.deltaTime, 0, 0);

    }

    void OnCollisionEnter2D( Collision2D col )
    {
        //identify the object that fell
        if (col.gameObject.CompareTag("Apple"))
        {
            score++;
        }
        else if (col.gameObject.CompareTag("RottenApple"))
        {
            score-=2;
        }
        else if (col.gameObject.CompareTag("Banana"))
        {
            //speed up during 5s
            speed = 15;
            speedBoostTime = Time.time;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 239, 0);
        }
        else if (col.gameObject.CompareTag("Bomb"))
        {
            gameOverUI.SetActive(true);
            gameOverUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score : " + score;
            gameMaster.timerIsRunning = false;
        }
        else //golden apple
        {
            score += 5;
        }

        displayed_text.SetText("Score : " + score);

        ref_audioSource.Play();
    }

}
