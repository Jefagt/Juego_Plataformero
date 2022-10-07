using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_1 : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask PlayerLayer;

    private bool SightRadius, RadiusOfProximity, runAwayPlayer, Ground, DetectPlayer = false;

    public float vision, visionLimit, soldierArea;
    public float speed, time;
    public GameObject Bullet;
    public Transform InstaBullet;

    //Patrolling 
    public Transform pointStart;
    public Transform pointEnd;

    public bool patrolling = true;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player Attack
        #region
        SightRadius = Physics2D.OverlapCircle(transform.position, vision, PlayerLayer);
        RadiusOfProximity = Physics2D.OverlapCircle(transform.position, visionLimit, PlayerLayer);
        runAwayPlayer = Physics2D.OverlapCircle(transform.position, soldierArea, PlayerLayer);

        if (RadiusOfProximity)
        {
            SightRadius = false;
            time += Time.deltaTime;
            anim.SetBool("Walk", false);
        }

        if (SightRadius)
        {
            DetectPlayer = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }

        if (runAwayPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x, transform.position.y, transform.position.z), -speed * Time.deltaTime);
            if (Ground)
            {
                rb.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
            }
        }

        if (Player.position.x > this.transform.position.x && DetectPlayer)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        if (Player.position.x < this.transform.position.x && DetectPlayer)
        {
            this.transform.localScale = new Vector2(-1, 1);
        }

        if (time > 2)
        {
            Instantiate(Bullet, InstaBullet.position, Quaternion.identity);
            anim.SetTrigger("Attack");
            time = 0;
        }
        #endregion

        //Patrolling an area
        #region
        if (patrolling && !DetectPlayer)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, pointEnd.position, speed * Time.deltaTime);
            this.transform.localScale = new Vector2(1, 1);
            anim.SetBool("Walk", true);
            if (transform.position == pointEnd.position)
            {
                patrolling = false;
            }
        }
        if (!patrolling && !DetectPlayer)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, pointStart.position, speed * Time.deltaTime);
            this.transform.localScale = new Vector2(-1, 1);
            anim.SetBool("Walk", true);
            if (transform.position == pointStart.position)
            {
                patrolling = true;
            }
        }
        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionLimit);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soldierArea);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Ground = true;
            anim.SetBool("Jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Ground = false;
            anim.SetBool("Jump", true);
        }
    }
}
