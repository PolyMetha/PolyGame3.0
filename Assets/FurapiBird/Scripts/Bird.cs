using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Bird : MonoBehaviour
{
    public bool isAlive = true; // Flag to track if the bird is alive or dead

    public ObstacleSpawner oS; // Reference to the ObstacleSpawner script

    private Animator animator; // Animator component for bird animation

    private AudioSource audioSourceMovement; // AudioSource component for bird movement sound
    [SerializeField] AudioClip soundOnMovement; // Sound clip for bird movement
    private AudioSource audioSourceImpact; // AudioSource component for impact sound
    [SerializeField] AudioClip soundOnImpact; // Sound clip for impact
    private AudioSource audioSourceDeath; // AudioSource component for death sound
    [SerializeField] AudioClip soundOnDeath; // Sound clip for death

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSourceMovement = gameObject.AddComponent<AudioSource>();
        audioSourceMovement.clip = soundOnMovement;
        audioSourceMovement.playOnAwake = false;

        audioSourceImpact = gameObject.AddComponent<AudioSource>();
        audioSourceImpact.clip = soundOnImpact;
        audioSourceImpact.playOnAwake = false;

        audioSourceDeath = gameObject.AddComponent<AudioSource>();
        audioSourceDeath.clip = soundOnDeath;
        audioSourceDeath.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            audioSourceMovement.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);
        }

        if (!IsRising())
        {
            animator.SetTrigger("IsRising");
            if (transform.rotation.z >= -15f)
            {
                transform.Rotate(new Vector3(0, 0, -2.4f * 6 * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -transform.rotation.z));
            }
        }
        else if (IsRising())
        {
            animator.SetTrigger("IsFalling");
            if (transform.rotation.z <= 15f)
            {
                transform.Rotate(new Vector3(0, 0, 2.0f * 6 * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -transform.rotation.z));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
            StartCoroutine(LoadMenuCoroutine()); // Quit the application when the escape key is pressed
        }
    }

    // Coroutine to load the menu scene
    public IEnumerator LoadMenuCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Handle collision with obstacle pipes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSourceImpact.Play();

            oS.pipeSpeed = 0;
            if (!oS.isDead)
            {
                oS.isDead = true;
            }
            GetComponent<Collider2D>().enabled = false;

            TopScore();
            isAlive = false;
            animator.SetTrigger("IsDead");
        }
    }

    // Handle collision with screen limits (obstacles)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            oS.pipeSpeed = 0;
            if (!oS.isDead)
            {
                oS.isDead = true;
                audioSourceImpact.Play();
            }

            TopScore();
            isAlive = false;
            animator.SetTrigger("IsDead");
        }
    }

    private bool IsRising()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update the top score if the current score is higher
    public void TopScore()
    {
        if (!isAlive) return;
        audioSourceDeath.Play();

        string path = Application.dataPath + "/StreamingAssets/TopScoreBird.txt";
        string fileContent = File.ReadAllText(path);
        int topScore = int.Parse(fileContent);

        Debug.Log("Top score: " + topScore);
        Debug.Log("Current score: " + oS.score);
        if (oS.score > topScore)
        {
            string f = oS.score.ToString();
            File.WriteAllText(path, f);
        }
    }
}
