using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChooseGame : MonoBehaviour
{
    public void LoadChoose()
    {
        StartCoroutine(LoadChooseCoroutine());
    }

    public IEnumerator LoadChooseCoroutine()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
