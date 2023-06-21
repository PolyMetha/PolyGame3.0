using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    public ObstacleSpawner oS; // Reference to the ObstacleSpawner script

    void Update()
    {
        if (this.transform.position.x <= -18)
        {
            // Reset cloud position to the right side of the screen
            this.transform.position = new Vector3(18, 0f, 5f);
        }
        else
        {
            // Move the cloud to the left based on the obstacle pipe speed
            this.transform.Translate(-oS.pipeSpeed * Time.deltaTime / 3, 0f, 0f);
        }
    }
}
