using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightImage : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer spriteRenderer;  
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }
}
