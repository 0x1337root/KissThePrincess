using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartCollect : MonoBehaviour
{
    [SerializeField]
    private float heartSpeed;

    private float direction;

    private Rigidbody rb;

    private float verticalAxis = -6f;

    private bool victory;
    public bool bum = false;

    private void Start()
    {
        Application.targetFrameRate = 300;

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.transform.Translate(new Vector3(direction * heartSpeed, verticalAxis, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SideWall")
        {
            verticalAxis = 0f;
            direction = -direction;
        }
        
        if (other.tag != "Princess" && other.tag != "Sides" && other.tag != "SideWall")
        {
            verticalAxis = 0f;
            direction = Random.Range(-1, 1);
        }

        if (other.tag == "Plane")
        {
            bum = true;

            victory = FindObjectOfType<ScoreManager>().victory;

            FindObjectOfType<Princess>().gameOver();

            if (!victory)
            {
                FindObjectOfType<ScoreManager>().GameOver();
            }

            transform.position = new Vector3(0, -10f, 0);

            Restart();
            
        }

        if (direction >= 0)
        {
            direction = 1;
        }

        else
        {
            direction = -1;
        }

        if (other.tag == "Player")
        {
            FindObjectOfType<ScoreManager>().AddScore(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag != "Princess" && other.tag != "SideWall")
        {
            direction = 0;
            verticalAxis = -6f;
        }
    }

    public void Restart()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}