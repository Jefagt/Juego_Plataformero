using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Weapon : MonoBehaviour
{
    GameObject player;

    Animator anim;

    private void Start()
    {
        player = GameObject.Find("bob_1");

        anim = player.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetFloat("Blend_Universal", 1f);
            Player_controller.Gun = true;
            Destroy(this.gameObject);
        }
    }
}
