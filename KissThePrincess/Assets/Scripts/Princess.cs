using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour
{
    [SerializeField]
    private GameObject heart;
    
    [SerializeField]
    private float time = 2f;

    [SerializeField]
    private float madTime = 1f;

    private float firsTime = 1f;

    private int madCount = 0;

    private int normalCount = 0;

    private bool mad = false;
    
    private float timer;

    private Animator anim;

    private bool defeat;

    private void Start()
    {
        timer = firsTime;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isKissed", false);

        timer -= Time.deltaTime;

        if(timer <= 0 && !mad)
        {
            if(madCount >= 5)
            {
                normalCount = 0;
                mad = true;
            }

            if(!FindObjectOfType<HeartCollect>().bum) 
            {
                Instantiate(heart, transform.position + (Vector3.up / 3), Quaternion.identity);
                anim.SetBool("isKissed", true);
            }

            timer = time;
            madCount++;
        }

        if (timer <= 0 && mad)
        {
            if(!FindObjectOfType<HeartCollect>().bum) 
            {
                Instantiate(heart, transform.position + (Vector3.up / 3), Quaternion.identity);
                anim.SetBool("isKissed", true);
            }

            normalCount++;
            
            if(normalCount >= 5)
            {
                madCount = 0;
                mad = false;
            }
            timer = madTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            defeat = FindObjectOfType<ScoreManager>().gameOver;
            if (!defeat)
            {
                FindObjectOfType<ScoreManager>().Victory();
            }
            anim.SetBool("isVictory", true);

            StartCoroutine(ChangeScene());
        }
    }

    public void gameOver()
    {
        anim.SetBool("isGameOver", true);
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
