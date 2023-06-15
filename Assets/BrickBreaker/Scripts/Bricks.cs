using UnityEngine;

public class Bricks : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public int hitCount;
    public GameObject brickPF;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount--;

        if (hitCount == 0)
        {
            // Destroy the current brick game object
            Destroy(this.gameObject);
        }

        if (hitCount == 1)
        {
            // Instantiate a new brick game object
            GameObject newBrick = Instantiate(brickPF);

            // Set the position and scale of the new brick to match the current brick
            newBrick.transform.position = gameObject.transform.position;
            newBrick.transform.localScale = gameObject.transform.localScale;

            // Destroy the current brick game object
            Destroy(gameObject);
        }
    }
}
