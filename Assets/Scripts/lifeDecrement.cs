using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class lifeDecrement : MonoBehaviour
{
    public PanierMouvement PM;
    public Canvas gameOverCanvas;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PM.life--; 
        if (PM.life >= 0) 
        {
            PM.text_life.SetText("Life : " + PM.life);
        }

        if (PM.life < 0)
        {
            PM.text_life.enabled = false;
            PM.TopScore();
            gameOverCanvas.enabled = true;
        }
    }
    
    
}