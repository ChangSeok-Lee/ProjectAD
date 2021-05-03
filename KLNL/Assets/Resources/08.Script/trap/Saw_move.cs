using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_move : MonoBehaviour
{

    public float x_speed = 1;
    public float y_speed = 1;


     float x_sped = 1;
     float y_sped = 1;




    public float x_time;
    public float y_time;

    public Transform Saw;
    public Transform end;
    public Transform start;
    public float x_amp;
    public float y_amp;

    float theta;

    public float x_delta;
    public float y_delta;

    public float x_init;
    public float y_init;

    public float x_first;
    public float y_first;

    public bool On;
    public bool set;

    void Start()
    {
        Saw = transform.Find("Saw_image").GetComponent<Transform>();
        end = transform.Find("saw_endpoint").GetComponent<Transform>();
        start = transform.Find("saw_startpoint").GetComponent<Transform>();


        x_init = Saw.localPosition.x;
        y_init = Saw.localPosition.y;


        x_delta = Mathf.Abs(start.localPosition.x - end.localPosition.x);
        y_delta = Mathf.Abs(start.localPosition.y - end.localPosition.y);

        x_first = 0;
        float x_mid = (start.localPosition.x + end.localPosition.x) / 2;
        float y_mid = (start.localPosition.y + end.localPosition.y) / 2;




        set = true;

        if (x_delta != 0)
        {
            x_amp = x_delta * 0.5f;
            x_first = Mathf.Acos(((x_init > x_mid ? x_init - x_mid : -(x_mid - x_init)) / x_amp));
        }
        else
        {
            x_amp = 0;
        }

        if (y_delta != 0)
        {
            y_amp = y_delta * 0.5f;
            y_first = Mathf.Acos(((y_init > y_mid ? y_init - y_mid : -(y_mid - y_init)) / y_amp));
        }
        else
        {
            y_amp = 0;
        }

      
        x_time = x_first;
        y_time = y_first;


    }

    // Update is called once per frame
    void Update()
    {
        //if (On)
        //{
        //    time += Time.deltaTime;
        //    move();
        //}
        //Debug.Log(Mathf.Cos(0* x_speed) * x_amp+" " + x_init +" "+ (x_init - x_amp > 0 ? +(x_init - x_amp) : -(x_amp - x_init)));


        if (On)
        {
            //Saw.localPosition = new Vector3(Mathf.Cos(x_time) * x_amp + (start.localPosition.x + end.localPosition.x) / 2,
            //                                                      Saw.localPosition.y, Saw.localPosition.z);


            x_sped = Mathf.Lerp(x_sped, x_speed, Time.deltaTime);
            y_sped = Mathf.Lerp(y_sped, y_speed, Time.deltaTime);


            move();
            x_time += Time.deltaTime;
            y_time += Time.deltaTime;
        }

        Debug.Log((start.localPosition.x + end.localPosition.x) / 2 +" "+ x_amp);
    }

    void FixedUpdate()
    {
        

    }


    void move()
    {
        Saw.localPosition = new Vector3(Mathf.Cos(x_time* x_sped) * x_amp + (start.localPosition.x + end.localPosition.x) / 2,
                                                    (Mathf.Cos(y_time* y_sped) * y_amp + (start.localPosition.y + end.localPosition.y) / 2), Saw.localPosition.z);
    }

    //float Pos(float AMP, float INIT, float speed)
    //{

    //    if (AMP != 0)
    //    {
    //        return Mathf.Cos(time * speed) * AMP  + INIT+ (INIT - AMP > 0 ? -(INIT - AMP) : +(AMP - INIT));
    //    }
    //    else
    //        return INIT;



    //}
}


