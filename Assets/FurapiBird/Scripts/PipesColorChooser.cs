using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesColorChooser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
