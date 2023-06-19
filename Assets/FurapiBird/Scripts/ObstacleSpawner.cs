using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject pipe;
    private float timer = 1f;
    private float timerSpeed = 30f;
    public float pipeSpeed = 4f;
    public TextMeshProUGUI scoreText;
    public GameObject commandsText;
    [SerializeField] Bird b;
    public int score = 0;
    public bool isDead = false;
    public bool deadCoroutineStarted = false;
    AudioSource music;

    void Start()
    {
        commandsText.SetActive(false);
        music = GetComponent<AudioSource>();
        b = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        timerSpeed -= Time.deltaTime;   

        float ypos = Random.Range(-3, 3) + 0.5f;

        if(timer <= 0f)
        {
            
            GameObject newPipe = Instantiate(pipe);
            
            newPipe.GetComponent<PipeObstacle_Script>().scriptSpawner = this;
            newPipe.transform.position = new Vector3(10f, ypos, 0f);
            newPipe.GetComponent<PipeObstacle_Script>().sC.oS = this;
            timer = (Random.value + 1.5f) * 1.1f * 4f/pipeSpeed;
        }

        if(timerSpeed < 0f)
        {
            pipeSpeed *= 1.1f;
            timerSpeed = 30f;
        }

        
        scoreText.SetText("Score : " + score);
        if (!b.isAlive)
        {
            commandsText.SetActive(true);
            music.Pause();

        }

        if (isDead && deadCoroutineStarted == false)
        {
            StartCoroutine("WhenDead");
            deadCoroutineStarted = true;
        }
    }

    public IEnumerator WhenDead()
    {

        while (!Input.GetKeyDown(KeyCode.M) && !Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
        }

        Destroy(b.gameObject);

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(loadMenu());
            Debug.Log("Menu");

        }
        else if (Input.GetKeyDown(KeyCode.R))
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
}
