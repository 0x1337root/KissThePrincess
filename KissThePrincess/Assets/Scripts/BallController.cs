using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    private bool isGrounded;

    private float vertical;

    private Vector3 vec3R;

    private Vector3 vec3L;

    private Vector3 vec3Default;

    private Vector3 vec3Bounce;

    private Rigidbody rigidBody;

    private bool jumped;

    private bool bounced;

    Animator anim;

    void Start()
    {
        vec3Default = new Vector3(0, 180, 0);

        vec3L = new Vector3(0, -90, 0);

        vec3R = new Vector3(0, 90, 0);

        rigidBody = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        bounced = anim.GetBool("isBounced");

        jumped = anim.GetBool("isJumped");

        if (!isGrounded && !jumped && !bounced)
        {
            anim.SetBool("isJumped", true);
        }

        anim.SetBool("isMoved", false);

        if (joystick.Horizontal >= 0.5f)
        {
            vec3Bounce = vec3R;
            if (!bounced)
            {
                transform.eulerAngles = vec3R;
            }
            Move();
        }
        else if (joystick.Horizontal <= -0.5f)
        {
            vec3Bounce = vec3L;
            if(!bounced)
            {
                transform.eulerAngles = vec3L;
            }
            Move();
        }
        else
        {
            if(!bounced)
            {
                transform.eulerAngles = vec3Default;
            }
        }

        if (joystick.Vertical >= 0.5f && isGrounded)
        {
            vertical = 1f;
            Jump();
        }
        else
        {
            vertical = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isJumped", false);

        if(collision.transform.tag == "Platform" || collision.transform.tag == "Plane")
        {
            anim.SetBool("isBounced", false);
        }

        if (collision.transform.tag != "SideWall" && collision.transform.tag != "Sides")
        {
            isGrounded = true;
        }

        if(collision.transform.tag == "SideWall" || collision.transform.tag == "Sides")
        {
            transform.eulerAngles = vec3Bounce;
            anim.SetBool("isJumped", false);
            anim.SetBool("isBounced", true);
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.transform.tag == "Platform" || other.transform.tag == "Plane")
        {
            anim.SetBool("isJumped", false);
            anim.SetBool("isBounced", false);
        }    
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.tag != "SideWall" && collision.transform.tag != "Sides")
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (!bounced)
        {
            anim.SetBool("isJumped", true);
        }
        rigidBody.AddForce(new Vector3(0, vertical, 0) * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
        isGrounded = false;
    }

    private void Move()
    {
        if (isGrounded && !bounced)
        {
            anim.SetBool("isMoved", true);
        }
            rigidBody.AddForce(new Vector3(joystick.Horizontal,0,0) * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
}