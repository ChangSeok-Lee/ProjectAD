using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snowball : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public GameObject Ball;

    public int speed=20;
    bool start;

    public bool signal;


    private void Start()
    {
        StartPoint = transform.Find("SnowBall_startpoint");
        EndPoint = transform.Find("SnowBall_endpoint");
        Ball = transform.Find("SnowBall_image").gameObject;
        


        
    }

    private void Update()
    {

        
            Move();

    }

    
    void Move()
    {
        if(!Ball.activeSelf)
        {
            Ball.transform.position = StartPoint.position;
            Ball.SetActive(true);
        }
        Ball.transform.Translate((EndPoint.position - StartPoint.position).normalized * Time.deltaTime * speed);
    }

    
}
