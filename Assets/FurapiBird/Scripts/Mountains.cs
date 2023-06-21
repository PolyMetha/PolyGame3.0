using UnityEngine;

public class Mountains : MonoBehaviour
{
    [SerializeField] ObstacleSpawner oS;

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x <= -32)
        {
            this.transform.position = new Vector3(25,0f,5f);
        }
        else
        {
            this.transform.Translate(-oS.pipeSpeed* Time.deltaTime / 4, 0f, 0f);
        }
    }
}
