using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    // We create the variables for the movements of the character
    Rigidbody2D rig;
    Animator anim;
    Vector2 input;
    SpriteRenderer sr;
    bool isGrounded;


    public float speed;
    public float impulseJump;
    public GameObject Hammer;
    public GameObject Bullet;
    public static bool direction;

    public static bool Gun;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(input.x * speed, rig.velocity.y);

        if (input.x != 0)
        {
            anim.SetBool("Walk_Hammer", true);
        }
        else
        {
            anim.SetBool("Walk_Hammer", false);
        }

        if (input.x < 0f)
        {
            sr.flipX = true;
            direction = true;
        }
        else if(input.x > 0f)
        {
            sr.flipX = false;
            direction = false;
        }

        if (input.x != 0f && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            anim.SetBool("Run_Hammer", true);
            speed = 8f;
        }
        else
        {
            anim.SetBool("Run_Hammer", false);
            speed = 4f;
        }
    }

    //FixedUpdate for equally spaced changes over time
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rig.AddForce(new Vector2(0f, impulseJump), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.K) && sr.flipX)
        {
            if (Hammer_Boomerang.HammerB == false && Gun == false) 
            {
                Instantiate(Hammer, new Vector3(transform.position.x - 0.9f, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                anim.SetTrigger("Attack_Hammer");
            } 
            else if (Hammer_Boomerang.HammerB == false && Gun == true)
            {
                Instantiate(Bullet, new Vector3(transform.position.x - 1.5f, transform.position.y - 0.4f, transform.position.z), transform.rotation);
                anim.SetTrigger("Attack_Hammer");
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && !sr.flipX)
        {
            if (Hammer_Boomerang.HammerB == false && Gun == false)
            {
                Instantiate(Hammer, new Vector3(transform.position.x + 0.9f, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                anim.SetTrigger("Attack_Hammer");
            }
            else if (Hammer_Boomerang.HammerB == false && Gun == true)
            {
                Instantiate(Bullet, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.4f, transform.position.z), transform.rotation);
                anim.SetTrigger("Attack_Hammer");
            }
        }
    }

    //OnTriggerEnter2D allows us to detect when two GameObjects overlap their Colliders
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Ground")
        {
            anim.SetBool("Jump_Hammer", false);
            isGrounded = true;
            transform.parent = obj.transform;
        }
    }

    //OnTriggerExit2D allows us to detect when a GameObject stops being in contact with a Collider in Trigger mode and we can execute the desired actions when that happens
    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "Ground")
        {
            anim.SetBool("Jump_Hammer", true);
            isGrounded = false;
            transform.parent = null;
        }
    }
}
