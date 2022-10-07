using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        if (!Player_controller.direction)
        {
            sp.flipX = false;
            rb.velocity = new Vector2(speed, 0f);
        }

        if (Player_controller.direction)
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
