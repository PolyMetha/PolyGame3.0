using System.Collections.Generic;
using UnityEngine;

public class PipesColorChooser : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
