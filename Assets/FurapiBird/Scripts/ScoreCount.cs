using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    // Start is called before the first frame update

    public ObstacleSpawner oS;
    private AudioSource audioSource;
    private bool scoreTook = false;
    [SerializeField] Bird bird;


    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!scoreTook && bird.isAlive)
        {
            audioSource.Play(); 
            oS.score ++;
            scoreTook = true;
        }
    }
}
