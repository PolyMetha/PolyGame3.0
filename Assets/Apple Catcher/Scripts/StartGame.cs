using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class StartGame : MonoBehaviour
{
    public TextMeshProUGUI topScore;

    private void Start()
    {
        // Path to the file storing the top score
        string path = Application.dataPath + "/StreamingAssets/TopScoreApple.txt";

        // Read the contents of the file
        string f = File.ReadAllText(path);

        if (f != null)
        {
            topScore.text = "Top Score : " + f;
        }
        else
        {
            topScore.text = "Top Score : 0";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            StartCoroutine(loadGame());
        }
    }

    IEnumerator loadGame()
    {
        // Asynchronously load the scene named "AppleCatcher"
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("AppleCatcher");

        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
