using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class StartGame : MonoBehaviour
{
    public TextMeshProUGUI topScore;
    private string path;
    private void Start()
    {
        //topscore
        path = Application.dataPath + "/StreamingAssets/TopScoreApple.txt";
        string f = File.ReadAllText(path);

        if (f != null)
        {
            topScore.text =  "Top Score : "+f;
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
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("AppleCatcher");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
