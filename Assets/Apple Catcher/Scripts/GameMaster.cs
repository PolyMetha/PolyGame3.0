using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private float timeRemaining = 90;
    private bool timerIsRunning = false;

    public int life = 4;
    public int score;
    public int topScore;
    [SerializeField] TextMeshPro textTimeLeft;

    private float minutes = 0;
    private float seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                minutes = Mathf.FloorToInt(timeRemaining / 60);
                seconds = Mathf.FloorToInt(timeRemaining % 60);
                textTimeLeft.SetText(string.Format("{0:00}:{1:00}", minutes, seconds) + " Left");
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                textTimeLeft.SetText("00:00 Left");
            }
        }
    }
}