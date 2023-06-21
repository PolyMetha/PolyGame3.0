using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    public ObstacleSpawner scriptSpawner; // Reference to the ObstacleSpawner script

    public ScoreCount scoreCount; // Reference to the ScoreCount script

    const float despawnPosX = -12f; // X-position at which the pipe is destroyed

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-scriptSpawner.pipeSpeed * Time.deltaTime, 0, 0); // Move the pipe to the left based on the pipe speed
        if (transform.position.x < despawnPosX)
        {
            Destroy(gameObject); // Destroy the pipe when it reaches the despawn position
        }
    }
}
