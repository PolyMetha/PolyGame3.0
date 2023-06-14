using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Bricks : MonoBehaviour
{
    public int hitCount;

    public GameObject brickPF;

    //public spawner spawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount--;
        if(hitCount == 0)
        {
            Destroy(this.gameObject);
        }

        if (hitCount == 1)
        {
            GameObject newBrick = Instantiate(brickPF);

            newBrick.transform.position = gameObject.transform.position;
            newBrick.transform.localScale = gameObject.transform.localScale;
            
            Destroy(gameObject);
        }
    }
}
