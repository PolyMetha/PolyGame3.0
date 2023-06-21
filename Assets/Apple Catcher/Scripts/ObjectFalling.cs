using UnityEngine;

public class ObjectFalling : MonoBehaviour
{
    public float spawnProba; // Probability of spawning the object

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10.0f) // Check if the object has fallen below a certain y position
        {
            Destroy(gameObject); // Destroy the game object
        }
    }

    // React to a collision (collision start)
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject); // Destroy the game object
    }
}
