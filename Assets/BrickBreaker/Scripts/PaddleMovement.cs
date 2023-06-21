using UnityEngine;
using UnityEngine.UIElements;

public class PaddleMovement : MonoBehaviour
{
    private float SPEED = 8f; // Speed of paddle movement
    private float angleValue = 5f; // Value to adjust ball's velocity on paddle collision

    private Camera cam; // Reference to the main camera

    private Vector2 lastPoint; // Last recorded mouse position

    protected AudioSource audioSourceCoin; // AudioSource component for playing coin sound

    [SerializeField] AudioClip soundOnCoin; // Sound clip for coin collision

    [SerializeField] Ball ball; // Reference to the Ball script

    public MouseMoveEvent MoveEvent; // Mouse move event

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
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 4.5f)
        {
            transform.Translate(SPEED * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -4.5f)
        {
            transform.Translate(-SPEED * Time.deltaTime, 0f, 0f);
        }

        // Move paddle based on mouse position
        else if (lastPoint.x != point.x && point.x > -4.5f && point.x < 4.5f)
        {
            gameObject.transform.position = new Vector3(point.x, pos.y, pos.z);
        }
        lastPoint = point;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // Check if paddle collides with the ball
        {
            // Adjust ball's velocity based on the collision point with the paddle
            float diff = ball.transform.position.x - transform.position.x;
            if (diff < 0)
            {
                ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff * angleValue, 0);
            }
            else
            {
                ball.GetComponent<Rigidbody2D>().velocity += new Vector2(diff * angleValue, 0);
            }
        }
        else if (collision.gameObject.CompareTag("Coin")) // Check if paddle collides with a coin
        {
            audioSourceCoin.Play(); // Play coin collision sound
            Destroy(collision.gameObject); // Destroy the coin game object
            ball.coinHit += 1; // Increment the coin hit count in the Ball script
            ball.score += 200; // Increase the score in the Ball script
            ball.textScore.SetText("SCORE: " + ball.score); // Update the score display in the Ball script
        }
    }
}
