using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    public ObstacleSpawner oS;
    void Update()
    {
        if (this.transform.position.x <= -18)
        {
            this.transform.position = new Vector3(18, 0f, 5f);
        }
        else
        {
            this.transform.Translate(-oS.pipeSpeed * Time.deltaTime / 3, 0f, 0f);
        }
    }
}
