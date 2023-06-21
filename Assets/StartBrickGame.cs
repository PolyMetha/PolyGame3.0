using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBrickGame : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            StartCoroutine(loadGame());
        }
    }

    IEnumerator loadGame()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("BrickBreaker");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
