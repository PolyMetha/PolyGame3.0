using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject pomme_prefab;
    public float timer = 1f;

    public PanierMouvement PM;

    // Update is called once per frame
    void Update()
    {
        if (PM.life >= 0)
        {
            Timer();
        }
    }

    public void Timer()
    {
        //timer -= Time.deltaTime;
        timer -= Time.deltaTime + ((float)PM.score / 20000);

        if(timer <= 0f)
        {
            float Xposition = Random.value * 17.5f - 8.75f;

            GameObject pomme = Instantiate(pomme_prefab);

            pomme.transform.position = new Vector3(Xposition, 3f, -0.5f);

            timer = Random.value + 0.3f;
           
        }
    }
}
