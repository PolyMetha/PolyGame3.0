using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
