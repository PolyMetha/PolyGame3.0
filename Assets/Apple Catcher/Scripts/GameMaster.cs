using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public bool timerIsRunning = false; // Flag indicating whether the timer is running
    public GameObject noMoreTimeUI; // Reference to the UI element displayed when time runs out
    public Player_Script player; // Reference to the player script

    [SerializeField] TextMeshPro textTimeLeft; // Reference to the TextMeshPro component for displaying time

    private float timeRemaining = 90; // The initial time remaining in seconds
    private float minutes = 0; // Stores the calculated minutes from timeRemaining
    private float seconds = 0; // Stores the calculated seconds from timeRemaining

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true; // Start the timer
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerIsRunning) // Check if the timer is running
        {
            if (timeRemaining > 0) // Check if there is time remaining
            {
                timeRemaining -= Time.deltaTime; // Decrease the time remaining by the time passed since the last frame
                minutes = Mathf.FloorToInt(timeRemaining / 60); // Calculate the minutes from the remaining time
                seconds = Mathf.FloorToInt(timeRemaining % 60); // Calculate the seconds from the remaining time
                if (seconds < 10 && minutes < 1)
                {
                    textTimeLeft.color = Color.red;
                }
                textTimeLeft.SetText(string.Format("{0:00}:{1:00}", minutes, seconds) + " Left"); // Update the TextMeshPro component to display the time left
            }
            else
            {
                timeRemaining = 0; // Set the time remaining to 0
                timerIsRunning = false; // Stop the timer
                textTimeLeft.SetText("00:00 Left"); // Update the TextMeshPro component to display 00:00
                noMoreTimeUI.SetActive(true); // Activate the UI element for displaying time ran out message
                noMoreTimeUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score : " + player.score; // Update the score displayed in the UI element

            }
        }
    }
}
