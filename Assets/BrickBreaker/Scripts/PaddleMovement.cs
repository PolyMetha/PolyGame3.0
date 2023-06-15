using UnityEngine;
using UnityEngine.UIElements;

public class PaddleMovement : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    private Camera cam;
    private float SPEED = 8f;
    private float angleValue = 3f;
    private Vector2 lastPoint;

    [SerializeField] Ball ball;
    AudioSource audioSource;
    [SerializeField] AudioClip soundToPlay;

    public MouseMoveEvent MoveEvent;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
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

        // Move paddle based on keyboard input
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 4.6f)
        {
            transform.Translate(SPEED * Time.deltaTime*7, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -4.6f)
        {
            transform.Translate(-SPEED * Time.deltaTime*7, 0f, 0f);
        }

        // Move paddle based on mouse position
        else if (lastPoint.x != point.x && point.x > -4.6f && point.x < 4.6f)
        {
            gameObject.transform.position = new Vector3(point.x, pos.y, pos.z);
        }
        lastPoint = point;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Adjust ball's velocity based on the collision point with the paddle
        float diff = ball.transform.position.x - transform.position.x;
        if (ball.transform.position.x - transform.position.x < 0)
        {
            ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff * angleValue, 0);
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff * angleValue, 0);
        }

        audioSource.Play(); // Play collision sound
    }
}
