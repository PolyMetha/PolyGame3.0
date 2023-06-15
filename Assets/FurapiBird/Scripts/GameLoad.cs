using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameLoad : MonoBehaviour
{

    private string path;
    public TextMeshPro topScoreText;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + @"TopScoreBird.txt";

        string f = File.ReadAllText(path);

        int topScore = int.Parse(f);

        topScoreText.SetText("TopScore : " + topScore);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(loadGame());
        }
    }

    IEnumerator loadGame()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("FurapiBird");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
