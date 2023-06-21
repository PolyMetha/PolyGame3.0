using UnityEngine;

public class HighLightImage : MonoBehaviour
{
    SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Set the initial color with reduced alpha
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1.5f); // Change the color to highlight with increased alpha
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Restore the initial color with reduced alpha
    }
}
