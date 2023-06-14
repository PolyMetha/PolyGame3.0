using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public AudioClip ref_audioClip;
    public SpriteRenderer fader_renderer;

    public GameObject apple_pf;

    //changes
    public GameObject banana_pf;
    public GameObject rottenApple_pf;
    public GameObject goldenApple_pf;

    protected float timer = 3f;
    protected AudioSource ref_audioSource;
    protected float current_alpha = 1;

    // Start is called before the first frame update
    void Start()
    {

        //apple_prefab = Resources.Load<GameObject>("Apple_prefab");

        ref_audioSource = gameObject.AddComponent<AudioSource>();
        ref_audioSource.loop = true;
        ref_audioSource.volume = 0.5f;
        ref_audioSource.clip = ref_audioClip;

        StartCoroutine( FadeOutFromWhite() );

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if ( timer <= 0)
        {
            float randomX = Random.value * 17f - 8.5f;

            //random to choose which object is going to be spawned
            float randomObject = Random.Range(0f, 1f);
            Debug.Log(randomObject);
            if(randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                //spawn apple
                GameObject newApple = Instantiate(apple_pf);
                newApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if(randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba - rottenApple_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                //spawn rotten apple
                GameObject newRottenApple = Instantiate(rottenApple_pf);
                newRottenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else if (randomObject - apple_pf.GetComponent<ObjectFalling>().spawnProba - rottenApple_pf.GetComponent<ObjectFalling>().spawnProba - banana_pf.GetComponent<ObjectFalling>().spawnProba < 0)
            {
                //spawn banana
                GameObject newBanana = Instantiate(banana_pf);
                newBanana.transform.position = new Vector3(randomX, 6.0f, 0);
            }
            else
            {
                //spawn golden apple
                GameObject newGoldenApple = Instantiate(goldenApple_pf);
                newGoldenApple.transform.position = new Vector3(randomX, 6.0f, 0);
            }

            timer = 0.5f + Random.value*1f ;
        }
        
    }

    //Coroutine to fade out from white/launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f);

        ref_audioSource.Play();

        while (current_alpha > 0)
        {
            current_alpha -= Time.deltaTime / 2;
            fader_renderer.color = new Color(1, 1, 1, current_alpha);
            yield return null;
        }

        Destroy(fader_renderer.gameObject);

    }
}
