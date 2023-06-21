using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public ObstacleSpawner oS;
    private AudioSource audiosource;
    private bool scoreTook = false;
    
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!scoreTook)
        {
            audiosource.Play(); 
            oS.score ++;
            scoreTook = true;
            Debug.Log(oS.score);
        }
    }
}
