using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFalling : MonoBehaviour
{

    public float spawnProba;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }

    //React to a collision (collision start)
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
