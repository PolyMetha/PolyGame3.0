using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameLoad : MonoBehaviour
{
    private string path; // File path for storing the top score

    public TextMeshProUGUI topScoreText; // Text component for displaying the top score

    public GameObject bird; // Reference to the bird game object

    void Start()
    {
        path = Application.dataPath + "/StreamingAssets/TopScoreBird.txt"; // Set the file path for the top score
        string fileContent = File.ReadAllText(path); // Read the content of the file
        int topScore = int.Parse(fileContent); // Parse the top score from the file content
        topScoreText.SetText("TopScore : " + topScore); // Display the top score in the UI text component
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Enable necessary components and start loading the game
            bird.GetComponent<Bird>().enabled = true;
            bird.GetComponent<Rigidbody2D>().gravityScale = 1;
            bird.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(loadGame());
        }
    }

    IEnumerator loadGame()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("FurapiBird"); // Start loading the game scene asynchronously
        while (!asyncload.isDone)
        {
            yield return null; // Wait until the game scene is fully loaded
        }
    }
}
