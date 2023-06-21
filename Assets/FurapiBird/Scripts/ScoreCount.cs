using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public ObstacleSpawner oS; // Reference to the ObstacleSpawner script

    private AudioSource audioSource; // Audio source component for playing score sound

    private bool scoreTook = false; // Flag to track if the score has been counted

    [SerializeField] Bird bird; // Reference to the Bird script

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>(); // Find and assign the Bird component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!scoreTook && bird.isAlive) // If the score hasn't been counted and the bird is alive
        {
            audioSource.Play(); // Play the score sound
            oS.score++; // Increase the score in the ObstacleSpawner script
            scoreTook = true; // Set the score as counted
        }
    }
}
