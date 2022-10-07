using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public GameObject Platform;

    public Transform StartPoint;
    public Transform EndPoint;

    public float speed;

    Vector3 movetowards;

    // Start is called before the first frame update
    void Start()
    {
        movetowards = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, movetowards, speed * Time.deltaTime);

        if (Platform.transform.position == EndPoint.position)
        {
            movetowards = StartPoint.position;
        }

        if (Platform.transform.position == StartPoint.position)
        {
            movetowards = EndPoint.position;

        }
    }
}