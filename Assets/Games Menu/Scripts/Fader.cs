using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    protected float alpha = 1; // The current alpha value

    protected SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public float duration = 2f; // The duration of the fade

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        StartCoroutine(FadeSceneIn()); // Start fading in the scene
    }

    IEnumerator FadeSceneIn()
    {
        while (alpha > 0) // While the alpha is greater than 0
        {
            alpha -= Time.deltaTime / duration; // Decrease the alpha over time
            spriteRenderer.color = new Color(1, 1, 1, alpha); // Set the new color with the updated alpha
            yield return null;
        }
    }

    IEnumerator FadeSceneOut()
    {
        while (alpha < 1) // While the alpha is less than 1
        {
            alpha += Time.deltaTime / duration; // Increase the alpha over time
            spriteRenderer.color = new Color(1, 1, 1, alpha); // Set the new color with the updated alpha
            yield return null;
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeSceneOut()); // Start fading out the scene
    }
}
