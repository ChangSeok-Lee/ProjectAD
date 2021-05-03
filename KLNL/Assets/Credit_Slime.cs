using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit_Slime : MonoBehaviour
{
    public Vector3 pos;
    public bool free;
    public Vector3 King;
    public float speed;
    public float x;
    public float y;

    public bool king;

    void Start()
    {
         
        speed = 3;
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!king)
        {
            this.transform.position += new Vector3(Time.deltaTime * x * speed, Time.deltaTime * y * speed, 0);

            //Debug.Log(pos + " " + x + " " + y);

            pos = Camera.main.WorldToViewportPoint(transform.position);



            if (pos.x < 0.05f)
            {
                pos.x = 0.05f;
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);
                transform.position = Camera.main.ViewportToWorldPoint(pos);
            };

            if (pos.x > 0.95f)
            {
                pos.x = 0.95f;
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);
                transform.position = Camera.main.ViewportToWorldPoint(pos);
            }

            if (pos.y < 0.05f)
            {
                pos.y = 0.05f;
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);
                transform.position = Camera.main.ViewportToWorldPoint(pos);
            }

            if (pos.y > 0.95f)
            {
                pos.y = 0.95f;
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);
                transform.position = Camera.main.ViewportToWorldPoint(pos);
            }


            if (x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {

            transform.position = Vector3.Lerp(this.transform.position, King,Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);

        }
   



    }
}
