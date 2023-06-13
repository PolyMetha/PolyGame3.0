using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;



public class PanierMouvement : MonoBehaviour
{
    [SerializeField] float SPEED = 5f;
    [SerializeField] TextMeshPro text;
    public TextMeshPro text_life;
    
    private Camera cam;
    private Vector2 lastPoint;
    
    public int score = 0;
    public int topScore;
    public TextMeshProUGUI textTopScore;
    public int life = 5;
    protected Animator AnimRef;
    //protected AudioSource basketAudio;

    protected AudioSource basketAudio;
    [SerializeField] AudioClip soundToPlay;

    private string path;
    

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath +@"\TopScoreApple.txt";

        string f = File.ReadAllText(path);
        
        topScore = int.Parse(f);
        
        basketAudio = gameObject.AddComponent<AudioSource>();
        basketAudio.playOnAwake = false;
        basketAudio.volume = 0.7f;

        basketAudio.clip = soundToPlay;
        AnimRef = GetComponent<Animator>();
        
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Mouvement();
        
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        score++;
        text.SetText("Score : " + score);

        Debug.Log("Score actuel : "+ score);
        Destroy(collisionInfo.gameObject);

        basketAudio.Play();
        
        if (life < 0)
        {
            TopScore();
        }
    }

    public void Mouvement()
    {
        Vector3 pos = gameObject.transform.position;
        Vector2 point = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetKey(KeyCode.RightArrow) && point.x < 7.9f){

            transform.Translate(SPEED*Time.deltaTime,0f,0f);
            AnimRef.SetTrigger("Walk");

        }

        else if(Input.GetKey(KeyCode.LeftArrow)&& point.x > -7.9f){

            transform.Translate(-SPEED*Time.deltaTime,0f,0f);
            AnimRef.SetTrigger("Rear");
        }
        else if ( lastPoint.x != point.x && point.x > -7.9f && point.x < 7.9f)
        {
            gameObject.transform.position = new Vector3(point.x, pos.y, pos.z);
            if (lastPoint.x - point.x < 0)
            {
                AnimRef.SetTrigger("Rear");
            }
            else
            {
                AnimRef.SetTrigger("Walk");
            }
        }
        else
        {
            AnimRef.SetTrigger("Idle");
        }

        lastPoint = point;
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
