using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
