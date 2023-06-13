using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PaddleMovement : MonoBehaviour

{
    private Camera cam;
    [SerializeField] Ball ball;

    public float SPEED = 1f;
    public float angleValue = 1f;

    private Vector2 lastPoint;
    //public int score = 0;

    public MouseMoveEvent MoveEvent;

    AudioSource audioSource;
    [SerializeField] AudioClip soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundToPlay;
        audioSource.playOnAwake = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = gameObject.transform.position;
        Vector2 point = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 4.6f){

            transform.Translate(SPEED * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -4.6f)
        {

            transform.Translate(-SPEED * Time.deltaTime, 0f, 0f);
        }
        else if ( lastPoint.x != point.x && point.x > -4.6f && point.x < 4.6f)
        {
            gameObject.transform.position = new Vector3(point.x, pos.y, pos.z);
            
        }
        lastPoint = point;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float diff = ball.transform.position.x - transform.position.x;
        if (ball.transform.position.x - transform.position.x < 0)
        {
            ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff * angleValue, 0);
            
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff*angleValue, 0);
        }

        audioSource.Play();
    }
}
