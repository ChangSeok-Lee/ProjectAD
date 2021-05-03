using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class presentation : MonoBehaviour
{
    public float speed;
    public float turnspeed;

    public float rtt;

    public int count;
    public bool rightleft;
    public bool move;
    public bool falling;
    Rigidbody2D rigidbody2D;
    Transform transform;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        speed = 10;
        turnspeed = 400;
        rtt = 0;
        rigidbody2D.gravityScale = 0f;
        count = 0;

    }


    void Update()
    {
        if (falling)
        {
            Falling();
        }
        if (move)
        {
            Move();
        }
    }


    void Falling()
    {
        rtt += Time.deltaTime * turnspeed;
        transform.position += Vector3.down * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, rtt);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        falling = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        rightleft = true;

        InvokeRepeating("RightLeft", 1, 1f);
    }

    void RightLeft()
    {
        if (transform.localScale.x == -1)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        count++;
        if (count == 4)
        {
            CancelInvoke("RightLeft");
            move = true;
        }
    }


    void Move()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
    }


}
