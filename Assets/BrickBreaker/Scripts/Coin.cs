using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector3 rotation; // Rotation vector for the coin
    [SerializeField] private float speed; // Speed of rotation

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y <= -5f) // Check if the coin falls off the screen
        {
            Destroy(gameObject); // Destroy the coin game object
        }
        transform.Rotate(rotation * speed * Time.deltaTime); // Rotate the coin based on the specified rotation and speed
    }
}
