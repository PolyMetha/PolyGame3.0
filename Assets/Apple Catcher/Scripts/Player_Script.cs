using UnityEngine;
using TMPro;

public class Player_Script : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public TextMeshProUGUI displayed_text; // Reference to the TextMeshPro component for displaying the score
    public GameMaster gameMaster; // Reference to the GameMaster script
    public GameObject gameOverUI; // Reference to the game over UI element
    public int score = 0; // Player's score

    protected AudioSource ref_audioSource; // Reference to the AudioSource component
    protected Animator ref_animator; // Reference to the Animator component

    private float speed = 10; // Player's movement speed
    private float speedBoostTime = 0f; // Time when the player's speed was boosted

    private Vector2 screenBounds;

    private bool paused;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    void Start()
    {
        ref_audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        ref_animator = GetComponent<Animator>(); // Get the Animator component

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate()
    {
        if (!gameMaster.timerIsRunning) { return; } // If the timer is not running, exit the function

        // Manage movement speed and animations
        shifting();

        // We stop time if the space bar is pushed down
        if (Input.GetKeyDown(KeyCode.Space) && !paused)
        {
            Time.timeScale = 0f; // Set the time scale to 0 (pause the game)
            
        }
        else if(Input.GetKeyDown(KeyCode.Space) && paused)
        {
            Time.timeScale = 1.0f; // Set the time scale back to 1 (resume the game)
        }

        // If sped up before, slow down 5s later
        if (speedBoostTime != 0 && (Time.time - speedBoostTime) > 5f)
        {
            speed = 10; // Reset the speed to normal
            speedBoostTime = 0; // Reset the speed boost time
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255); // Reset the player's color
        }

        
    }

    //out of bounds control
    private void LateUpdate()
    {
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, screenBounds.x*-1, screenBounds.x);
        playerPos.y = Mathf.Clamp(playerPos.y, screenBounds.y*-1, screenBounds.y);
        transform.position = playerPos;

    }

    void shifting()
    {
        // Get moving direction
        int inputX = (int)Input.GetAxisRaw("Horizontal");

        // Inform animator: Are we moving?
        ref_animator.SetBool("isMoving", inputX != 0);

        // Move with the speed found
        transform.Translate(speed * inputX * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Identify the object that fell
        if (col.gameObject.CompareTag("Apple"))
        {
            score++; // Increase the score by 1
        }
        else if (col.gameObject.CompareTag("RottenApple"))
        {
            score -= 2; // Decrease the score by 2
        }
        else if (col.gameObject.CompareTag("Banana"))
        {
            // Speed up for 5s
            speed = 15; // Increase the player's speed
            speedBoostTime = Time.time; // Store the time when the speed was boosted
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 239, 0); // Change the player's color to yellow
        }
        else if (col.gameObject.CompareTag("Bomb"))
        {
            gameOverUI.SetActive(true); // Activate the game over UI element
            gameOverUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score : " + score; // Update the score displayed in the UI
            gameMaster.timerIsRunning = false; // Stop the game timer
        }
        else // Golden apple
        {
            score += 5; // Increase the score by 5
        }

        displayed_text.SetText("Score : " + score); // Update the score displayed in the TextMeshPro component

        ref_audioSource.Play(); // Play the audio
    }
}
