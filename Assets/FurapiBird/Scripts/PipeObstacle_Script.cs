using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    public ObstacleSpawner scriptSpawner;
    public ScoreCount sC;
    const float despawn_posX = -12f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -scriptSpawner.pipeSpeed * Time.deltaTime , 0, 0 );
        if (transform.position.x < despawn_posX)
        {
            Destroy(gameObject);
        }
    }
}
