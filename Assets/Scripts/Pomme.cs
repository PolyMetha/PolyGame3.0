using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomme : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < -10f){
            Destroy(gameObject);
            
        }
    }
}