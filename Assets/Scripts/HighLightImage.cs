using UnityEngine;

public class HighLightImage : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer spriteRenderer;  
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1.5f);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
