using UnityEngine;
using UnityEngine.UIElements;

public class PaddleMovement : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    private Camera cam;
    private float SPEED = 8f;
    private float angleValue = 5f;
    private Vector2 lastPoint;

    protected AudioSource audioSourceCoin;
    [SerializeField] AudioClip soundOnCoin;

    [SerializeField] Ball ball;

    public MouseMoveEvent MoveEvent;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        audioSourceCoin = gameObject.AddComponent<AudioSource>();
        audioSourceCoin.clip = soundOnCoin;
        audioSourceCoin.playOnAwake = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        Vector2 point = cam.ScreenToWorldPoint(Input.mousePosition);

        // Move paddle based on keyboard input
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 4.6f)
        {
            transform.Translate(SPEED * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -4.6f)
        {
            transform.Translate(-SPEED * Time.deltaTime, 0f, 0f);
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
        if (collision.gameObject.CompareTag("Ball"))
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
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            audioSourceCoin.Play();
            Destroy(collision.gameObject);
            ball.coinHit += 1;
            ball.score += 100;
            ball.textScore.SetText("SCORE: " + ball.score);
        }
    }
}
