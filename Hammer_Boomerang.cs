using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_Boomerang : MonoBehaviour
{
    bool go;

    GameObject player;

    Transform itemToRotate;

    Vector2 locationInFrontOfPlayer;

    Animator anim;

    public static bool HammerB;


    // Start is called before the first frame update
    void Start()
    {
        go = false;
        HammerB = true;

        player = GameObject.Find("bob_1");

        itemToRotate = gameObject.transform.GetChild(0);

        anim = player.GetComponent<Animator>();

        if(!Player_controller.direction)
        {
            locationInFrontOfPlayer = new Vector2(transform.position.x + 15, transform.position.y);
            anim.SetFloat("Blend_Universal", 0);
        }

        if(Player_controller.direction)
        {
            locationInFrontOfPlayer = new Vector2(transform.position.x - 15, transform.position.y);
            anim.SetFloat("Blend_Universal", 0);
        }

        StartCoroutine(Boomerang());
    }

    IEnumerator Boomerang()
    {
        go = true;
        yield return new WaitForSeconds(1.5f);
        go = false;
    }
    // Update is called once per frame
    void Update()
    {
        itemToRotate.transform.Rotate(0, 0, Time.deltaTime * -1000);

        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 10);
        }

        if (!go)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 10);
        }

        if(!go && Vector3.Distance(player.transform.position, transform.position) < 1)
        {
            if (Player_controller.Gun == false) 
            {
                anim.SetTrigger("Attack_Hammer");
                anim.SetFloat("Blend_Universal", 0.33f);
            } 
            HammerB = false;
            Destroy(this.gameObject);
        }
    }
}
