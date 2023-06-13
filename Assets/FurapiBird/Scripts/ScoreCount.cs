using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public ObstacleSpawner oS;
    private AudioSource audiosource;
    
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audiosource.Play(); 
        oS.score ++;
    }

    
}
