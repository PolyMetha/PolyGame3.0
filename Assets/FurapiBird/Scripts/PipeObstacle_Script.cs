using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    public ObstacleSpawner scriptSpawner;
    public ScoreCount scoreCount;
    const float despawnPosX = -12f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -scriptSpawner.pipeSpeed * Time.deltaTime , 0, 0 );
        if (transform.position.x < despawnPosX)
        {
            Destroy(gameObject);
        }
    }
}
