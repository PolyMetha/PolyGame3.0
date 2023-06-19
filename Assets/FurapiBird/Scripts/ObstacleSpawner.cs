using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controls;
    [SerializeField] GameObject pipe;
    private float timer = 1f;
    private float timerSpeed = 30f;
    public float pipeSpeed = 4f;
    public TextMeshPro text;
    public TextMeshPro UI;
    [SerializeField] Bird b;
    [HideInInspector] public int score = 0;
    AudioSource music;

    private float timeRemaining = 20;
    private bool isDestroyed = false;

    void Start()
    {
        UI.fontSize = 0;
        music = GetComponent<AudioSource>();
        b = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>();
        b.oS = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 15 && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(controls);
        }

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

        
        text.SetText("Score : " + score);
        if (!b.isAlive)
        {
            UI.fontSize = 4;
            music.Pause();

        }
    }

    public IEnumerator WhenDead()
    {

        while (!Input.GetKeyDown(KeyCode.M) && !Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
        }


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
