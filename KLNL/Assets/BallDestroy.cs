using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroy : MonoBehaviour
{
    public Snowball Snowball;

    private void Start()
    {
        Snowball = transform.GetComponentInParent<Snowball>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name+"눈덩이");
        if(collision.gameObject.name== "SnowBall_image")
        {
            collision.gameObject.transform.position = Snowball.StartPoint.position;
        }
    }
}
