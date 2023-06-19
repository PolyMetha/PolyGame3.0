using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    // Start is called before the first frame update

    protected float alpha = 1;
    protected SpriteRenderer spriteRenderer;
    public float duration =2f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeSceneIn());
    }

    IEnumerator FadeSceneIn()
    {
        while(alpha > 0)
        {
            alpha -= Time.deltaTime / duration;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    IEnumerator FadeSceneOut()
    {
        while (alpha <1)
        {
            alpha += Time.deltaTime / duration;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeSceneOut());
    }
}
