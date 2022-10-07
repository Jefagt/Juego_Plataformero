using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_Soldier : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    Transform transformS;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        transformS = GameObject.FindGameObjectWithTag("Soldier_#0").GetComponent<Transform>();

        if (transformS.localScale.x == 1)
        {
            sp.flipX = false;
            rb.velocity = new Vector2(speed, 0f);
        }

        if (transformS.localScale.x == -1)
        {
            sp.flipX = true;
            rb.velocity = new Vector2(-speed, 0f);
        }
    }
    void Update()
    {
        Destroy(this.gameObject, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
